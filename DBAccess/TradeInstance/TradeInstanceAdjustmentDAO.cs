using log4net;
using Model.EnumType;
using Model.SecurityInfo;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.TradeInstance
{
    public class TradeInstanceAdjustmentDAO : BaseDAO
    {
        private const string SP_Create = "procTradeInstanceAdjustmentInsert";
        private const string SP_Select = "procTradeInstanceAdjustmentSelect";
        private const string SP_Delete = "procTradeInstanceAdjustmentDelete";

        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TradeInstanceAdjustmentDAO()
            : base()
        {

        }

        public TradeInstanceAdjustmentDAO(DbHelper dbHelper)
            : base(dbHelper)
        {

        }

        //need to handle the permission
        public List<int> CreateTran(List<TradeInstanceAdjustmentItem> items)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            List<int> idList = new List<int>();
            int ret = -1;
            try
            {
                dbCommand.CommandText = SP_Create;

                foreach (var item in items)
                {
                    dbCommand.Parameters.Clear();

                    _dbHelper.AddInParameter(dbCommand, "@SourceInstanceId", System.Data.DbType.Int32, item.SourceInstanceId);
                    _dbHelper.AddInParameter(dbCommand, "@SourceFundCode", System.Data.DbType.String, item.SourceFundCode);
                    _dbHelper.AddInParameter(dbCommand, "@SourcePortfolioCode", System.Data.DbType.String, item.SourcePortfolioCode);
                    _dbHelper.AddInParameter(dbCommand, "@DestinationInstanceId", System.Data.DbType.Int32, item.DestinationInstanceId);
                    _dbHelper.AddInParameter(dbCommand, "@DestinationFundCode", System.Data.DbType.String, item.DestinationFundCode);
                    _dbHelper.AddInParameter(dbCommand, "@DestinationPortfolioCode", System.Data.DbType.String, item.DestinationPortfolioCode);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)item.SecuType);
                    _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)item.PositionType);
                    _dbHelper.AddInParameter(dbCommand, "@Price", System.Data.DbType.Double, item.Price);
                    _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, item.Amount);
                    _dbHelper.AddInParameter(dbCommand, "@AdjustType", System.Data.DbType.Int32, (int)item.AdjustType);
                    _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, item.Operator);
                    _dbHelper.AddInParameter(dbCommand, "@StockHolderId", System.Data.DbType.String, item.StockHolderId);
                    _dbHelper.AddInParameter(dbCommand, "@SeatNo", System.Data.DbType.String, item.SeatNo);
                    _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, item.Notes);

                    _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                    ret = dbCommand.ExecuteNonQuery();
                    int id = -1;
                    if (ret > 0)
                    {
                        id = (int)dbCommand.Parameters["@return"].Value;
                        if (id > 0)
                        {
                            idList.Add(id);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                idList.Clear();

                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand);
                transaction.Dispose();
            }

            return idList;
        }

        public int Create(TradeInstanceAdjustmentItem item)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Create);
            _dbHelper.AddInParameter(dbCommand, "@SourceInstanceId", System.Data.DbType.Int32, item.SourceInstanceId);
            _dbHelper.AddInParameter(dbCommand, "@SourceFundCode", System.Data.DbType.String, item.SourceFundCode);
            _dbHelper.AddInParameter(dbCommand, "@SourcePortfolioCode", System.Data.DbType.String, item.SourcePortfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@DestinationInstanceId", System.Data.DbType.Int32, item.DestinationInstanceId);
            _dbHelper.AddInParameter(dbCommand, "@DestinationFundCode", System.Data.DbType.String, item.DestinationFundCode);
            _dbHelper.AddInParameter(dbCommand, "@DestinationPortfolioCode", System.Data.DbType.String, item.DestinationPortfolioCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
            _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, (int)item.SecuType);
            _dbHelper.AddInParameter(dbCommand, "@PositionType", System.Data.DbType.Int32, (int)item.PositionType);
            _dbHelper.AddInParameter(dbCommand, "@Price", System.Data.DbType.Double, item.Price);
            _dbHelper.AddInParameter(dbCommand, "@Amount", System.Data.DbType.Int32, item.Amount);
            _dbHelper.AddInParameter(dbCommand, "@AdjustType", System.Data.DbType.Int32, (int)item.AdjustType);
            _dbHelper.AddInParameter(dbCommand, "@Operator", System.Data.DbType.String, item.Operator);
            _dbHelper.AddInParameter(dbCommand, "@StockHolderId", System.Data.DbType.String, item.StockHolderId);
            _dbHelper.AddInParameter(dbCommand, "@SeatNo", System.Data.DbType.String, item.SeatNo);
            _dbHelper.AddInParameter(dbCommand, "@Notes", System.Data.DbType.String, item.Notes);

            _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            int id = -1;
            if (ret > 0)
            {
                id = (int)dbCommand.Parameters["@return"].Value;
            }

            return id;
        }

        public int Delete(int id)
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Delete);
            _dbHelper.AddInParameter(dbCommand, "@Id", System.Data.DbType.Int32, id);

            int ret = _dbHelper.ExecuteNonQuery(dbCommand);

            return ret;
        }

        public List<TradeInstanceAdjustmentItem> GetAll()
        {
            var dbCommand = _dbHelper.GetStoredProcCommand(SP_Select);

            List<TradeInstanceAdjustmentItem> items = new List<TradeInstanceAdjustmentItem>();
            var reader = _dbHelper.ExecuteReader(dbCommand);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TradeInstanceAdjustmentItem item = new TradeInstanceAdjustmentItem();
                    item.Id = (int)reader["Id"];
                    if (reader["CreatedDate"] != null && reader["CreatedDate"] != DBNull.Value)
                    {
                        item.CreateDate = (DateTime)reader["CreatedDate"];
                    }

                    item.SourceInstanceId = (int)reader["SourceInstanceId"];
                    item.SourceFundCode = (string)reader["SourceFundCode"];
                    item.SourcePortfolioCode = (string)reader["SourcePortfolioCode"];
                    item.DestinationInstanceId = (int)reader["DestinationInstanceId"];
                    item.DestinationFundCode = (string)reader["DestinationFundCode"];
                    item.DestinationPortfolioCode = (string)reader["DestinationPortfolioCode"];
                    item.SecuCode = (string)reader["SecuCode"];
                    item.SecuType = (SecurityType)reader["SecuType"];
                    item.PositionType = (PositionType)reader["PositionType"];
                    item.Price = (double)(decimal)reader["Price"];
                    item.Amount = (int)reader["Amount"];
                    item.AdjustType = (AdjustmentType)reader["AdjustType"];
                    item.Operator = (string)reader["Operator"];
                    item.StockHolderId = (string)reader["StockHolderId"];
                    item.SeatNo = (string)reader["SeatNo"];
                    item.Notes = (string)reader["Notes"];

                    items.Add(item);
                }
            }
            reader.Close();
            _dbHelper.Close(dbCommand);

            return items;
        }
    }
}
