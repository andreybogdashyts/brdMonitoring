
namespace BRD.Monitoring.Infrastructure.Helpers
{
    public interface IDirectoryHelper
    {
        void CreateMissedFolders();
        string[] GetFiles();

        void DeleteFile(string path);
    }
}
