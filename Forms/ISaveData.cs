using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms
{
    public interface ISaveData
    {
        bool OnSave(object sender, object data);
    }
}
