using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities;

public class Attendee
{
    internal Attendee(Invitation invitation)
    {
        MemberId = invitation.MemberId;
        GathernigId = invitation.GatheringId;
        CreatedOnUtc = DateTime.UtcNow;
    }
    public Guid MemberId { get; private set; }
    public Guid GathernigId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public static Attendee Create(Invitation invitation)
    {
        return new Attendee(invitation);
    }
}