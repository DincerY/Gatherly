using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities;

public class Invitation
{
    internal Invitation(Guid id, Member member, Gathering gathering)
    {
        Id = id;
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreateOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid MemberId { get;  private set; }
    public Guid GatheringId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreateOnUtc { get; private set; }
    public DateTime? ModifiedOnUtc { get; private set; }


}