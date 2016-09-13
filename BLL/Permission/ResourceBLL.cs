using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class ResourceBLL
    {
        private ResourceDAO _resourcedao = new ResourceDAO();
        public ResourceBLL()
        { 
        }

        public int Create(Resource resource)
        {
            return _resourcedao.Create(resource);
        }

        public int Update(Resource resouce)
        {
            return _resourcedao.Update(resouce);
        }

        public int Delete(int refId, ResourceType type)
        {
            return _resourcedao.Delete(refId, type);
        }

        public Resource Get(int refId, ResourceType type)
        {
            return _resourcedao.Get(refId, type);
        }

        public List<Resource> GetAll()
        {
            return _resourcedao.GetAll();
        }
    }
}
