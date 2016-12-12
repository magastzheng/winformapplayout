using log4net;
using Model.Archive;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccess.EntrustCommand
{
    public class ArchiveEntrustDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //entrustcommand table
        private const string SP_CreateArchiveEntrustCommand = "procArchiveEntrustCommandInsert";
        private const string SP_DeleteArchiveEntrustCommand = "procArchiveEntrustCommandDelete";

        private const string SP_CreateArchiveEntrustSecurity = "procArchiveEntrustSecurityInsert";
        private const string SP_DeleteArchiveEntrustSecurity = "procArchiveEntrustSecurityDelete";

        public ArchiveEntrustDAO()
            : base()
        { 
        }

        public ArchiveEntrustDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
        }

        public int Create(ArchiveEntrustCommand cmdItem, List<ArchiveEntrustSecurity> entrustItems)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(_dbHelper.Connection);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                //delete all old one
                dbCommand.CommandText = SP_CreateArchiveEntrustCommand;
                _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, cmdItem.SubmitId);
                _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, cmdItem.CommandId);
                _dbHelper.AddInParameter(dbCommand, "@Copies", System.Data.DbType.Int32, cmdItem.Copies);
                _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, cmdItem.EntrustNo);
                _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, cmdItem.BatchNo);
                _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, cmdItem.EntrustStatus);
                _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, cmdItem.DealStatus);
                _dbHelper.AddInParameter(dbCommand, "@SubmitPerson", System.Data.DbType.Int32, cmdItem.SubmitPerson);
                _dbHelper.AddInParameter(dbCommand, "@ArchiveDate", System.Data.DbType.DateTime, cmdItem.ArchiveDate);
                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, cmdItem.CreatedDate);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, cmdItem.ModifiedDate);
                _dbHelper.AddInParameter(dbCommand, "@EntrustFailCode", System.Data.DbType.Int32, cmdItem.EntrustFailCode);
                _dbHelper.AddInParameter(dbCommand, "@EntrustFailCause", System.Data.DbType.String, cmdItem.EntrustFailCause);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    int archiveId = -1;
                    if (ret > 0)
                    {
                        archiveId = (int)dbCommand.Parameters["@return"].Value;
                        cmdItem.ArchiveId = archiveId;
                    }

                    foreach (var entrustItem in entrustItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateArchiveEntrustSecurity;

                        _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                        _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, entrustItem.RequestId);
                        _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, entrustItem.SubmitId);
                        _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, entrustItem.CommandId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, entrustItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, entrustItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, entrustItem.EntrustAmount);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustPrice", System.Data.DbType.Decimal, entrustItem.EntrustPrice);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, (int)entrustItem.EntrustDirection);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustItem.EntrustStatus);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustPriceType", System.Data.DbType.Int32, (int)entrustItem.EntrustPriceType);
                        _dbHelper.AddInParameter(dbCommand, "@PriceType", System.Data.DbType.Int32, (int)entrustItem.PriceType);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, entrustItem.EntrustNo);
                        _dbHelper.AddInParameter(dbCommand, "@BatchNo", System.Data.DbType.Int32, entrustItem.BatchNo);
                        _dbHelper.AddInParameter(dbCommand, "@DealStatus", System.Data.DbType.Int32, (int)entrustItem.DealStatus);
                        _dbHelper.AddInParameter(dbCommand, "@TotalDealAmount", System.Data.DbType.Int32, entrustItem.TotalDealAmount);
                        _dbHelper.AddInParameter(dbCommand, "@TotalDealBalance", System.Data.DbType.Decimal, entrustItem.TotalDealBalance);
                        _dbHelper.AddInParameter(dbCommand, "@TotalDealFee", System.Data.DbType.Decimal, entrustItem.TotalDealFee);
                        _dbHelper.AddInParameter(dbCommand, "@DealTimes", System.Data.DbType.Int32, entrustItem.DealTimes);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustDate", System.Data.DbType.DateTime, entrustItem.EntrustDate);
                        _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, entrustItem.CreatedDate);
                        _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, entrustItem.ModifiedDate);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustFailCode", System.Data.DbType.Int32, entrustItem.EntrustFailCode);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustFailCause", System.Data.DbType.String, entrustItem.EntrustFailCause);

                        ret = dbCommand.ExecuteNonQuery();
                        if (ret < 0)
                        {
                            string msg = string.Format("Fail to insert: ArchiveId: {0}, SubmitId: {1}, RequestId: {2}, SecuCode: {3}", 
                                archiveId, entrustItem.SubmitId, entrustItem.RequestId, entrustItem.SecuCode);
                            logger.Error(msg);
                        }
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand.Connection);
                transaction.Dispose();
            }

            return ret;
        }

        public int Delete(int archiveId)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(_dbHelper.Connection);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                //delete all old one
                dbCommand.CommandText = SP_DeleteArchiveEntrustCommand;
                _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);
                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_DeleteArchiveEntrustSecurity;

                    _dbHelper.AddInParameter(dbCommand, "@ArchiveId", System.Data.DbType.Int32, archiveId);

                    ret = dbCommand.ExecuteNonQuery();
                    if (ret < 0)
                    {
                        string msg = string.Format("Fail to delete: ArchiveId: {0}", archiveId);
                        logger.Error(msg);
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //TODO: add log
                logger.Error(ex);
                ret = -1;
                throw;
            }
            finally
            {
                _dbHelper.Close(dbCommand.Connection);
                transaction.Dispose();
            }

            return ret;
        }
    }
}
