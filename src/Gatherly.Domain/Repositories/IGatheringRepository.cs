using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IGatheringRepository
{
    public void Add(Gathering gathering);

    public Task<Gathering> GetByIdWithCreatorAsync(Guid gatheringId, CancellationToken cancellationToken);
}