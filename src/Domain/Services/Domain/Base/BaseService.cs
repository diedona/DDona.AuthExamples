using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Domain.Base
{
    public abstract class BaseService
    {
        public IEnumerable<string> Errors { get; protected set; }

        protected BaseService()
        {
            Errors = new List<string>();
        }
    }
}
