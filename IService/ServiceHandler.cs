
namespace ServiceInterface
{
    public delegate void Connected(ServiceType serviceType, int code, string message);

    public delegate void Notify(NotifyArgs args);
}
