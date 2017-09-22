using BLL.UsageTracking;
using Config;
using DBAccess.EntrustCommand;
using Model.EnumType;
using Model.Permission;
using Model.UsageTracking;
using System.Collections.Generic;

namespace BLL.EntrustCommand
{
    public class EntrustCommandBLL
    {
        private EntrustCommandDAO _entrustcmddao = new EntrustCommandDAO();

        private UserActionTrackingBLL _userActionTrackingBLL = new UserActionTrackingBLL();

        public EntrustCommandBLL()
        { 
        }

        #region update

        public int UpdateEntrustCommandStatus(int submitId, EntrustStatus entrustStatus)
        {
            Tracking(ActionType.Edit, ResourceType.EntrustCommand, submitId, entrustStatus.ToString());
            return _entrustcmddao.UpdateEntrustCommandStatus(submitId, entrustStatus);
        }

        public int UpdateEntrustCommandBatchNo(int submitId, int batchNo, EntrustStatus entrustStatus, int entrustFailCode, string entrustFailCause)
        {
            Tracking(ActionType.Edit, ResourceType.EntrustCommand, submitId, entrustStatus.ToString());
            return _entrustcmddao.UpdateEntrustCommandBatchNo(submitId, batchNo, entrustStatus, entrustFailCode, entrustFailCause);
        }

        public int UpdateDealStatus(int submitId, DealStatus dealStatus)
        {
            Tracking(ActionType.Edit, ResourceType.EntrustCommand, submitId, dealStatus.ToString());
            return _entrustcmddao.UpdateDealStatus(submitId, dealStatus);
        }

        #endregion

        #region delete

        public int DeleteByCommandId(int commandId)
        {
            Tracking(ActionType.Delete, ResourceType.EntrustCommand, commandId, string.Empty);
            return _entrustcmddao.DeleteByCommandId(commandId);
        }

        public int DeleteByCommandIdStatus(int commandId, EntrustStatus status)
        {
            Tracking(ActionType.Delete, ResourceType.EntrustCommand, commandId, status.ToString());
            return _entrustcmddao.DeleteByCommandIdStatus(commandId, status);
        }

        #endregion

        #region get/fetch

        public List<Model.Database.EntrustCommand> GetCancel(int commandId)
        {
            Tracking(ActionType.Get, ResourceType.EntrustCommand, commandId, string.Empty);
            return _entrustcmddao.GetCancel(commandId);
        }

        public List<Model.Database.EntrustCommand> GetByCommandId(int commandId)
        {
            return _entrustcmddao.GetByCommandId(commandId);
        }

        public Model.Database.EntrustCommand GetBySubmitId(int submitId)
        {
            return _entrustcmddao.GetBySubmitId(submitId);
        }

        #endregion

        #region user action tracking
        
        private int Tracking(ActionType action, ResourceType resourceType, int resourceId, string details)
        {
            int userId = LoginManager.Instance.GetUserId();

            return _userActionTrackingBLL.Create(userId, action, resourceType, resourceId, 1, ActionStatus.Normal, details);
        }

        #endregion
    }
}
