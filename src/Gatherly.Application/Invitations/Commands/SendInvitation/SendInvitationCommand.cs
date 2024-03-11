using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.SendInvitation;

public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : IRequest;