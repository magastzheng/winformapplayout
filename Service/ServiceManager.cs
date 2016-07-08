using System.Collections.Generic;

namespace Service
{
    public class ServiceManager
    {
        private static readonly ServiceManager _instance = new ServiceManager();
        public static ServiceManager Instance
        {
            get { return _instance; }
        }

        private List<IService> _services = new List<IService>();

        private ServiceManager()
        {
            var ufxService = new UFXHeartBeatService();
            _services.Add(ufxService);

            //add other service
        }

        public void Start()
        {
            foreach (var service in _services)
            {
                service.Start();
            }
        }

        public void Stop()
        {
            foreach (var service in _services)
            {
                service.Stop();
            }
        }
    }
}
