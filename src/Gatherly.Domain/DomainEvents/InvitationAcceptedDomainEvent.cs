using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent;