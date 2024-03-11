using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(Guid InvitationId) : IRequest;