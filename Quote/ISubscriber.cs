using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quote
{
    public interface ISubscriber
    {
        void Handle(object data);
    }
}
