using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Domain.Base
{
    public abstract class BaseDomainService
    {
        public List<string> Errors { get; protected set; } = new List<string>();
    }
}
