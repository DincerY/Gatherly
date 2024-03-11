using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IAttendeeRepository
{
    public void Add(Attendee attendee);
}