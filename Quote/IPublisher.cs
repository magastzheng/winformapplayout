using System.Collections.Generic;

namespace Quote
{
    public abstract class IPublisher
    {
        private Dictionary<PublishEvent, List<ISubscriber>> observerMap = new Dictionary<PublishEvent, List<ISubscriber>>();

        public void Subscribe(PublishEvent ev, ISubscriber sub)
        {
            if (!observerMap.ContainsKey(ev))
            {
                var newSubList = new List<ISubscriber>();
                newSubList.Add(sub);
                observerMap.Add(ev, newSubList);
            }
            else
            {
                if (!observerMap[ev].Contains(sub))
                {
                    observerMap[ev].Add(sub);
                }
            }
        }

        public void UnSubscribe(PublishEvent ev, ISubscriber sub)
        {
            if (!observerMap.ContainsKey(ev))
                return;
            if (observerMap[ev].Contains(sub))
            {
                observerMap[ev].Remove(sub);
            }
        }

        public void Notify(PublishEvent ev, object data)
        {
            if (observerMap.ContainsKey(ev))
            {
                foreach (var sub in observerMap[ev])
                {
                    sub.Handle(data);
                }
            }
        }
    }
}
