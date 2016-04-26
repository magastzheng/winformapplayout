using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class TSNavBarItems : List<TSNavBarItem>
    {
        private TSNavBarContainer _owner;
        public TSNavBarContainer Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public TSNavBarItems(TSNavBarContainer owner)
            : base()
        {
            _owner = owner;
        }

        public new void Add(TSNavBarItem item)
        {
            this._owner.SetLayout(item);
            base.Add(item);
            item.BarIndex = this.Count - 1;
            //
        }

        public void Add()
        {
            TSNavBarItem item = new TSNavBarItem(_owner);
            this.Add(item);
        }
    }
}
