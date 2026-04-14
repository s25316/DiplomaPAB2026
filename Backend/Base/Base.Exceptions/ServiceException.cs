namespace Base.Exceptions;

public abstract class ServiceException(string message) : Exception(message)
{
    public class NotAvailable(string message) : ServiceException(message);
    public class MaintenanceBreak(string message) : ServiceException(message);
    public class Other(string message) : ServiceException(message);
}
