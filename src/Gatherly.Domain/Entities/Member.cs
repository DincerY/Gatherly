using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities;

public class Member : Entity    
{
    public Member(Guid id) : base(id)
    {
        
    }
}