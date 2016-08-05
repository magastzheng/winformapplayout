using DBAccess;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Template
{
    public class MonitorUnitBLL
    {
        private MonitorUnitDAO _monitorunitdao = new MonitorUnitDAO();

        public MonitorUnitBLL()
        { 
        }

        public List<OpenPositionItem> GetActive()
        {
            return _monitorunitdao.GetActive();
        }
    }
}
