
namespace Quote
{
    public interface ISubscriber
    {
        void Handle(object data);
    }
}
