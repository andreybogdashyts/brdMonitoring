namespace BRD.Monitoring.Infrastructure.FileHandlers
{
    public interface IFileHandlerResolver
    {
        IFileHandler Get(string ext);
    }
}
