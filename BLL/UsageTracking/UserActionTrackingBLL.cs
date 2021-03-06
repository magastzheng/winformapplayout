﻿using DBAccess.UsageTracking;
using Model.Permission;
using Model.UsageTracking;
using System;
using System.Collections.Generic;

namespace BLL.UsageTracking
{
    public class UserActionTrackingBLL
    {
        private UserActionTrackingDAO _useractiontrackingdao = new UserActionTrackingDAO();

        public UserActionTrackingBLL()
        { 
        }

        public int Create(int userId, ActionType action, ResourceType resourceType, int resourceId, int num, ActionStatus actionStatus, string details)
        {
            var item = new UserActionTracking 
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                ActionType = action,
                ResourceType = resourceType,
                ResourceId = resourceId,
                Num = num,
                ActionStatus = actionStatus,
                Details = details,
            };

            return Create(item);
        }

        public int Create(UserActionTracking item)
        {
            return _useractiontrackingdao.Create(item);
        }

        public List<UserActionTracking> GetByUser(int userId)
        {
            return _useractiontrackingdao.GetByUser(userId);
        }

        public List<UserActionTracking> GetByUserPeriod(int userId, DateTime startDate, DateTime endDate)
        { 
            return _useractiontrackingdao.GetByUserPeriod(userId, startDate, endDate);
        }

        public List<UserActionTracking> GetByResource(int resourceId, ResourceType resourceType)
        {
            return _useractiontrackingdao.GetByResource(resourceId, resourceType);
        }
    }
}
