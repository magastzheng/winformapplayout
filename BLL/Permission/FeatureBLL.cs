using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class FeatureBLL
    {
        private FeatureDAO _featuredao = new FeatureDAO();

        public FeatureBLL()
        { 
        }

        public int Create(Feature feature)
        {
            return _featuredao.Create(feature);
        }

        public int Update(Feature feature)
        {
            return _featuredao.Update(feature);
        }

        public int Delete(int featureId)
        {
            return _featuredao.Delete(featureId);
        }

        public Feature GetByCode(string code)
        {
            return _featuredao.GetByCode(code);
        }

        public Feature Get(int featureId)
        {
            return _featuredao.Get(featureId);
        }

        public List<Feature> GetAll()
        {
            return _featuredao.Get();
        }
    }
}
