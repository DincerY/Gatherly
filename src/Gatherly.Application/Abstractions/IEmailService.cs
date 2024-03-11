using Gatherly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gatherly.Application.Abstractions;

public interface IEmailService
{
    public Task SendInvitationSentEmailAsync(Member member, Gathering gathering, CancellationToken cancellationToken);
}