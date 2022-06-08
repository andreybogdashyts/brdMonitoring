using BRD.Monitoring.Models;
using BRD.Monitoring.Models.Output;

namespace BRD.Monitoring.Infrastructure.FileHandlers
{
    public interface IFileHandler
    {
        GenerationReport Parse(string path);
        void Save(string filePath, GenerationOutput output);
    }
}
