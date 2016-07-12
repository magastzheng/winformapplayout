using log4net;
using Model.EnumType;
using Model.UI;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DBAccess
{
    public class EntrustDAO : BaseDAO
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SP_CreateEntrustCommand = "procEntrustCommandInsert";
        private const string SP_CreateEntrustSecurity = "procEntrustSecurityInsert";
        private const string SP_ModifyEntrustCommandStatus = "procEntrustCommandUpdateEntrustStatus";
        private const string SP_ModifyEntrustSecurityStatusBySubmitId = "procEntrustSecurityUpdateEntrustStatusBySubmitId";
        private const string SP_ModifySecurityEntrustStatus = "procEntrustSecurityUpdateEntrustStatus";
        private const string SP_ModifySecurityEntrustNoByRequestId = "procEntrustSecurityUpdateEntrustNoByRequestId";

        public EntrustDAO()
            : base()
        { 
            
        }

        public EntrustDAO(DbHelper dbHelper)
            : base(dbHelper)
        { 
            
        }

        public int Submit(EntrustCommandItem cmdItem, List<EntrustSecurityItem> entrustItems)
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
                dbCommand.CommandText = SP_CreateEntrustCommand;
                _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, cmdItem.CommandId);
                _dbHelper.AddInParameter(dbCommand, "@Copies", System.Data.DbType.Int32, cmdItem.Copies);
                _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

                _dbHelper.AddReturnParameter(dbCommand, "@return", System.Data.DbType.Int32);

                ret = dbCommand.ExecuteNonQuery();

                if (ret >= 0)
                {
                    int submitId = -1;
                    if (ret > 0)
                    {
                        submitId = (int)dbCommand.Parameters["@return"].Value;
                        cmdItem.SubmitId = submitId;
                    }

                    foreach (var entrustItem in entrustItems)
                    {
                        dbCommand.Parameters.Clear();
                        dbCommand.CommandText = SP_CreateEntrustSecurity;

                        _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
                        _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, entrustItem.CommandId);
                        _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, entrustItem.SecuCode);
                        _dbHelper.AddInParameter(dbCommand, "@SecuType", System.Data.DbType.Int32, entrustItem.SecuType);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustAmount", System.Data.DbType.Int32, entrustItem.EntrustAmount);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustPrice", System.Data.DbType.Decimal, entrustItem.EntrustPrice);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustDirection", System.Data.DbType.Int32, (int)entrustItem.EntrustDirection);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustItem.EntrustStatus);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustPriceType", System.Data.DbType.Int32, (int)entrustItem.EntrustPriceType);
                        _dbHelper.AddInParameter(dbCommand, "@PriceType", System.Data.DbType.Int32, (int)entrustItem.PriceType);
                        _dbHelper.AddInParameter(dbCommand, "@EntrustDate", System.Data.DbType.DateTime, entrustItem.EntrustDate);
                        _dbHelper.AddInParameter(dbCommand, "@CreatedDate", System.Data.DbType.DateTime, DateTime.Now);

                        //string newid = string.Empty;
                        ret = dbCommand.ExecuteNonQuery();
                        if (ret > 0)
                        {
                            int requetId = (int)dbCommand.Parameters["@return"].Value;
                            entrustItem.RequestId = requetId;
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

        public int UpdatetEntrustStatus(List<int> submitIds, EntrustStatus entrustStatus)
        {
            int ret = -1;

            foreach (var submitId in submitIds)
            {
                ret = UpdateOneEntrustStatus(submitId, entrustStatus);
            }

            return ret;
        }

        public int UpdateOneEntrustStatus(int submitId, EntrustStatus entrustStatus)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                DateTime now = DateTime.Now;

                dbCommand.CommandText = SP_ModifyEntrustCommandStatus;
                _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
                _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, entrustStatus);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                ret = dbCommand.ExecuteNonQuery();

                if (ret > 0)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_ModifyEntrustSecurityStatusBySubmitId;

                    _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
                    _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                    ret = dbCommand.ExecuteNonQuery();
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

        public int UpdateSecurityEntrustStatus(List<EntrustSecurityItem> entrustItems, EntrustStatus entrustStatus)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                DateTime now = DateTime.Now;
                foreach (var item in entrustItems)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_ModifySecurityEntrustStatus;

                    _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
                    _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
                    _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                    ret = dbCommand.ExecuteNonQuery();
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

        public int UpdateSecurityEntrustNoByRequestId(List<EntrustSecurityItem> entrustItems)
        {
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                DateTime now = DateTime.Now;
                foreach (var item in entrustItems)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_ModifySecurityEntrustNoByRequestId;

                    _dbHelper.AddInParameter(dbCommand, "@RequestId", System.Data.DbType.Int32, item.RequestId);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustNo", System.Data.DbType.Int32, item.EntrustNo);
                    _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                    ret = dbCommand.ExecuteNonQuery();
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

        public int UpdateCommandSecurityEntrustStatus(int submitId, List<EntrustSecurityItem> entrustItems, EntrustStatus entrustStatus)
        { 
            var dbCommand = _dbHelper.GetCommand();
            _dbHelper.Open(dbCommand);

            //use transaction to execute
            DbTransaction transaction = dbCommand.Connection.BeginTransaction();
            dbCommand.Transaction = transaction;
            dbCommand.CommandType = System.Data.CommandType.StoredProcedure;
            int ret = -1;
            try
            {
                DateTime now = DateTime.Now;
                foreach (var item in entrustItems)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.CommandText = SP_ModifySecurityEntrustStatus;

                    _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, item.SubmitId);
                    _dbHelper.AddInParameter(dbCommand, "@CommandId", System.Data.DbType.Int32, item.CommandId);
                    _dbHelper.AddInParameter(dbCommand, "@SecuCode", System.Data.DbType.String, item.SecuCode);
                    _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, (int)entrustStatus);
                    _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                    ret = dbCommand.ExecuteNonQuery();
                }

                //update the entrust status of EntrustCommand
                dbCommand.Parameters.Clear();
                dbCommand.CommandText = SP_ModifyEntrustCommandStatus;
                _dbHelper.AddInParameter(dbCommand, "@SubmitId", System.Data.DbType.Int32, submitId);
                _dbHelper.AddInParameter(dbCommand, "@EntrustStatus", System.Data.DbType.Int32, entrustStatus);
                _dbHelper.AddInParameter(dbCommand, "@ModifiedDate", System.Data.DbType.DateTime, now);

                ret = dbCommand.ExecuteNonQuery();

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
