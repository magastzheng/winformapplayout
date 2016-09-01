using Config;
using hundsun.t2sdk;
using log4net;
using Model;
using Model.config;
using Model.strategy;

namespace BLL.UFX.impl
{
    public class AccountBLL
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private T2SDKWrap _t2SDKWrap;

        public AccountBLL(T2SDKWrap t2SDKWrap)
        {
            _t2SDKWrap = t2SDKWrap;
        }

        public ConnectionCode QueryAccount()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryAccount);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryAccount);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (parser.ErrorCode == ConnectionCode.Success)
            {
                for (int i = 1, count = parser.DataSets.Count; i < count; i++)
                {
                    var dataSet = parser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        AccountItem acc = new AccountItem();
                        acc.AccountCode = dataRow.Columns["account_code"].GetStr();
                        acc.AccountName = dataRow.Columns["account_name"].GetStr();
                        acc.AccountType = dataRow.Columns["account_type"].GetStr();

                        LoginManager.Instance.AddAccount(acc);
                    }
                    break;
                }

                return ConnectionCode.Success;
            }
            else
            {
                logger.Error("账户查询失败");
                return ConnectionCode.ErrorConn;
            }
        }

        public ConnectionCode QueryAssetUnit()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryAssetUnit);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryAssetUnit);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "capital_account":
                        packer.AddStr("");
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (parser.ErrorCode == ConnectionCode.Success)
            {
                for (int i = 1, count = parser.DataSets.Count; i < count; i++)
                {
                    var dataSet = parser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        AssetItem asset = new AssetItem();
                        asset.CapitalAccount = dataRow.Columns["capital_account"].GetStr();
                        asset.AccountCode = dataRow.Columns["account_code"].GetStr();
                        asset.AssetNo = dataRow.Columns["asset_no"].GetStr();
                        asset.AssetName = dataRow.Columns["asset_name"].GetStr();

                        LoginManager.Instance.AddAsset(asset);
                    }
                    break;
                }

                return ConnectionCode.Success;
            }
            else
            {
                logger.Error("资产单元查询失败");

                return parser.ErrorCode;
            }
        }

        public ConnectionCode QueryPortfolio()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryPortfolio);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryPortfolio);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "capital_account":
                        packer.AddStr("");
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    case "combi_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();
            if (parser.ErrorCode != ConnectionCode.Success)
            {
                logger.Error("组合查询失败失败");

                return parser.ErrorCode;
            }

            for (int i = 1, count = parser.DataSets.Count; i < count; i++)
            {
                var dataSet = parser.DataSets[i];
                foreach (var dataRow in dataSet.Rows)
                {
                    PortfolioItem p = new PortfolioItem();
                    p.AccountCode = dataRow.Columns["account_code"].GetStr();
                    p.AssetNo = dataRow.Columns["asset_no"].GetStr();
                    p.CombiNo = dataRow.Columns["combi_no"].GetStr();
                    p.CombiName = dataRow.Columns["combi_name"].GetStr();
                    p.CapitalAccount = dataRow.Columns["capital_account"].GetStr();
                    p.MarketNoList = dataRow.Columns["market_no_list"].GetStr();
                    p.FutuInvestType = dataRow.Columns["futu_invest_type"].GetStr();
                    p.EntrustDirectionList = dataRow.Columns["entrust_direction_list"].GetStr();

                    LoginManager.Instance.AddPortfolio(p);
                }
                break;
            }

            return ConnectionCode.Success;
        }

        public ConnectionCode QueryHolder()
        {
            FunctionItem functionItem = ConfigManager.Instance.GetFunctionConfig().GetFunctionItem(FunctionCode.QueryHolder);
            if (functionItem == null || functionItem.RequestFields == null || functionItem.RequestFields.Count == 0)
            {
                return ConnectionCode.ErrorLogin;
            }

            CT2BizMessage bizMessage = new CT2BizMessage();
            //初始化
            bizMessage.SetFunction((int)FunctionCode.QueryHolder);
            bizMessage.SetPacketType(CT2tag_def.REQUEST_PACKET);

            //业务包
            CT2Packer packer = new CT2Packer(2);
            packer.BeginPack();
            foreach (FieldItem item in functionItem.RequestFields)
            {
                packer.AddField(item.Name, item.Type, item.Width, item.Scale);
            }

            foreach (FieldItem item in functionItem.RequestFields)
            {
                switch (item.Name)
                {
                    case "user_token":
                        packer.AddStr(LoginManager.Instance.LoginUser.Token);
                        break;
                    case "account_code":
                        packer.AddStr("");
                        break;
                    case "asset_no":
                        packer.AddStr("");
                        break;
                    case "combi_no":
                        packer.AddStr("");
                        break;
                    case "market_no":
                        packer.AddStr("");
                        break;
                    default:
                        break;
                }
            }

            packer.EndPack();

            unsafe
            {
                bizMessage.SetContent(packer.GetPackBuf(), packer.GetPackLen());
            }

            var parser = _t2SDKWrap.SendSync2(bizMessage);
            packer.Dispose();
            bizMessage.Dispose();

            if (parser.ErrorCode != ConnectionCode.Success)
            {
                logger.Error("交易股东查询失败");

                return parser.ErrorCode;
            }

            var response = T2ErrorHandler.Handle(parser);
            if (T2ErrorHandler.Success(response.ErrorCode))
            {
                for (int i = 1, count = parser.DataSets.Count; i < count; i++)
                {
                    var dataSet = parser.DataSets[i];
                    foreach (var dataRow in dataSet.Rows)
                    {
                        HolderItem p = new HolderItem();
                        p.AccountCode = dataRow.Columns["account_code"].GetStr();
                        p.AssetNo = dataRow.Columns["asset_no"].GetStr();
                        p.CombiNo = dataRow.Columns["combi_no"].GetStr();
                        p.StockHolderId = dataRow.Columns["stockholder_id"].GetStr();
                        p.MarketNo = dataRow.Columns["market_no"].GetStr();

                        LoginManager.Instance.AddHolder(p);
                    }
                    break;
                }

                return ConnectionCode.Success;
            }
            else
            {
                logger.Error(response.ErrorMessage);
                return ConnectionCode.ErrorFailContent;
            }
        }
    }
}
