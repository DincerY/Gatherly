using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IInvitationRepository
{
    public void Add(Invitation invitation);

    //public Task<Invitation> GetByIdAsync(Guid invitationId, CancellationToken cancellationToken);
}
