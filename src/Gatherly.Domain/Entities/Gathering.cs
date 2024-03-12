using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities;

public class Gathering : AggregateRoot
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();
    
    private Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string name, string? location) : base(id)
    {

        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }
    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public string Name { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string? Location { get; private set; }
    public int? MaximumNumberOfAttendees { get; private set; }
    public DateTime? InvitationsExpireAtUtc { get; private set; }
    public int NumberOfAttendees { get; private set; }
    public IReadOnlyCollection<Attendee> Attendees => _attendees;
    public IReadOnlyCollection<Invitation> Invitations => _invitations;


    public static Gathering Create(
        Guid id, 
        Member creator, 
        GatheringType type, 
        DateTime scheduledAtUtc, 
        string name,    
        string? location, 
        int? maximumNumberOfAttendees, 
        int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(Guid.NewGuid(), creator, type, scheduledAtUtc, name, location);

        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new Exception($"{nameof(maximumNumberOfAttendees)} cant be null");
                }

                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new Exception($"{nameof(invitationsValidBeforeInHours)} cant be null");
                }

                gathering.InvitationsExpireAtUtc =
                    gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            throw new Exception("Can t send invitation to the gathering creator");
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception("Can t send invitation for gathering in the past");
        }

        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        var expired = (Type == GatheringType.WithFixedNumberOfAttendees &&
                       NumberOfAttendees == MaximumNumberOfAttendees) ||
                      (Type == GatheringType.WithExpirationForInvitations &&
                       InvitationsExpireAtUtc < DateTime.UtcNow);
        if (expired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept();

        RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id, Id));

        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}