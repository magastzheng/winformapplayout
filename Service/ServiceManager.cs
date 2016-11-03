using Quote;
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
            //TODO: Init the QuoteCenter before using.
            //var quote = QuoteCenter.Instance.Quote;
            var quote = QuoteCenter2.Instance.Quote;
            var quoteService = new QuoteService(quote);
            _services.Add(quoteService);
        }

        //public void Init()
        //{
        //    QuoteCenter.Instance.Quote;
        //}

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
