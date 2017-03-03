using ServiceInterface;
using Quote;
using System;
using System.Collections.Generic;
using System.Threading;
using Service;

namespace ServiceInterface
{
    public class ServiceManager
    {
        private static readonly ServiceManager _instance = new ServiceManager();
        public static ServiceManager Instance
        {
            get { return _instance; }
        }

        //TODO: update it to register the event handler from the outside.
        private event Action<ServiceType, int, string> ConnectedHandler;
        private event Action<NotifyArgs> NotifyHandler;

        //
        private Dictionary<ServiceType, Action<ServiceType, int, string>> _connectedHandlerMap = new Dictionary<ServiceType, Action<ServiceType, int, string>>();
        private Dictionary<ServiceType, Action<NotifyArgs>> _notifyHandlerMap = new Dictionary<ServiceType, Action<NotifyArgs>>();

        private CountdownEvent _cdEvent = null;
        //private Dictionary<ServiceType, AutoResetEvent> _eventMap;

        private List<IService> _services = new List<IService>();
        private Dictionary<ServiceType, int> _serviceConnectedMap = new Dictionary<ServiceType, int>();

        private ServiceManager()
        {
            
        }

        public void Init()
        {
            Stop();
            
            var ufxService = new UFXHeartBeatService(Connected, Notify);
            _services.Add(ufxService);

            //add other service
            //TODO: Init the QuoteCenter before using.
            var quote = QuoteCenter2.Instance.Quote;
            var quoteService = new QuoteService(quote, Connected, Notify);
            _services.Add(quoteService);
        }

        public void Start()
        {
            _cdEvent = new CountdownEvent(2);
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

            _services.Clear();
            _serviceConnectedMap.Clear();
        }

        public bool Wait()
        {
            _cdEvent.Wait();

            bool allSuccess = true;
            foreach (var kv in _serviceConnectedMap)
            {
                if (kv.Value != 0)
                {
                    allSuccess = false;
                }
            }

            return allSuccess;
        }

        public void RegisterConnectedHandler(ServiceType serviceType, Action<ServiceType, int, string> connectedHandler)
        {
            if (_connectedHandlerMap == null)
            {
                _connectedHandlerMap = new Dictionary<ServiceType, Action<ServiceType, int, string>>();
            }

            if (_connectedHandlerMap.ContainsKey(serviceType))
            {
                _connectedHandlerMap[serviceType] = connectedHandler;
            }
            else
            {
                _connectedHandlerMap.Add(serviceType, connectedHandler);
            }
        }

        public void UnRegisterConnectedHandler(ServiceType serviceType)
        {
            if (_connectedHandlerMap.ContainsKey(serviceType))
            {
                _connectedHandlerMap.Remove(serviceType);
            }
        }

        public void RegisterNotifyHandler(ServiceType serviceType, Action<NotifyArgs> notifyHandler)
        {
            if (_notifyHandlerMap == null)
            {
                _notifyHandlerMap = new Dictionary<ServiceType, Action<NotifyArgs>>();
            }
            if (_notifyHandlerMap.ContainsKey(serviceType))
            {
                _notifyHandlerMap[serviceType] = notifyHandler;
            }
            else
            {
                _notifyHandlerMap.Add(serviceType, notifyHandler);
            }
        }

        public void UnRegisterNotifyHandler(ServiceType serviceType)
        {
            if (_notifyHandlerMap.ContainsKey(serviceType))
            {
                _notifyHandlerMap.Remove(serviceType);
            }
        }

        private void Connected(ServiceType serviceType, int code, string message)
        {
            if (_serviceConnectedMap.ContainsKey(serviceType))
            {
                _serviceConnectedMap[serviceType] = code;
            }
            else
            {
                _serviceConnectedMap.Add(serviceType, code);
            }

            if (code != 0)
            {
                if (ConnectedHandler != null)
                {
                    ConnectedHandler(serviceType, code, message);
                }
            }

            _cdEvent.Signal();
        }

        private void Notify(NotifyArgs args)
        { 
            if (NotifyHandler != null)
            {
                NotifyHandler(args);
            }
        }
    }
}
