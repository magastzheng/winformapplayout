using Model.Binding;
using Model.EnumType;
using Model.EnumType.EnumTypeConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.UI
{
    public class AssetUnit
    {
        [BindingAttribute("fundname")]
        public string FundName { get; set; }

        [BindingAttribute("fundcode")]
        public string FundCode { get; set; }

        [BindingAttribute("accounttype")]
        public string AccountType
        {
            get { return EnumTypeDisplayHelper.GetFundAccountType(EAccountType); }
        }

        [BindingAttribute("capitalaccount")]
        public string CapitalAccount { get; set; }

        [BindingAttribute("assetno")]
        public string AssetNo { get; set; }

        [BindingAttribute("assetname")]
        public string AssetName { get; set; }

        public FundAccountType EAccountType { get; set; }

        //public int AssetUnitId { get; set; }

        //public string AssetUnitCode { get; set; }

        //public string AssetUnitName { get; set; }

        //public int FundId { get; set; }

        ////1 正常，2 过期， 3 冻结
        //public int AssetUnitStatus { get; set; }

        ////1 允许透支; 2 不允许透支
        //public int CanOverdraft { get; set; }

        ////1 收益资产; 2 保本资产
        //public int AssetType { get; set; }

        //public DateTime CreatedDate { get; set; }

        //public DateTime ModifiedDate { get; set; }
    }
}
