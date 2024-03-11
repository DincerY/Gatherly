using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities;

public class Gathering
{
    public Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string name, string? location)
    {
        Id = id;
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }
    public Guid Id { get; private set; }
    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public string Name { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string? Location { get; private set; }
    public int? MaximumNumberOfAttendees { get; set; }
    public DateTime? InvitationsExpireAtUtc { get; set; }
    public int NumberOfAttendees { get; set; }
    public List<Attendee> Attendees { get; set; }
    public List<Invitation> Invitations { get; set; }


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
                    gathering.ScheduleAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
        return gathering;
    }
}