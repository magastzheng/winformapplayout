using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EnumType
{
    public enum FundAccountType
    {
        //封闭式基金
        ClosedEndFund = 1,
        //开放式基金
        OpenEndFund = 2,
        //社保基金
        SocialInsuranceFund = 3,
        //年金产品
        AnnuityProduct = 5,
        //专户产品
        SpecialProduct = 6,
        //年金
        Annuities = 8,
        //专户理财
        SeparateManagedAccount = 9,
        //保险
        Insurance = 10,
        //一对多专户
        OneToManyAccounts = 11,
        //定向理财
        DirectionalFinancing = 12,
        //集合理财
        CollectiveFinance = 13,
        //自营
        SelfOperated = 14,
        //信托
        Trust = 15,
        //私募
        PrivatePlacement = 16,
        //委托资产
        EntrustedAssets = 17,
    }
}
