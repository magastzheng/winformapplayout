using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Entity
{
    public class AutoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class AutoItemManager
    {
        private IList<AutoItem> _dataSource;
        public IList<AutoItem> DataSource { set { _dataSource = value; } }
        public IList<AutoItem> GetMatch(string filter)
        {
            IList<AutoItem> results = new List<AutoItem>();
            if (_dataSource == null)
            {
                //TODO: load the data
                //_dataSource = LoadData();
                return results;
            }
            else
            {
                results = _dataSource;

            }

            return results.Where(f => f.Id.Length >= filter.Length && f.Id.Substring(0, filter.Length) == filter).ToList<AutoItem>();
        }
    }
}
