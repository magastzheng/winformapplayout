using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX.obsolete
{
    public interface IStrategryDataChange
    {
        void SetData(object data);
        object GetDate();
    }
}
