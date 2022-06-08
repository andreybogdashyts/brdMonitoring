using BRD.Monitoring.Models;
using BRD.Monitoring.Models.Output;
using System.IO;
using System.Xml.Serialization;

namespace BRD.Monitoring.Infrastructure.FileHandlers
{
    public class XmlFileHandler : IFileHandler
    {
        public GenerationReport Parse(string filePath)
        {
            GenerationReport res = null;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var ser = new XmlSerializer(typeof(GenerationReport));
                res = (GenerationReport)ser.Deserialize(fs);
            }
            return res;
        }

        public void Save(string filePath, GenerationOutput output)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                var ser = new XmlSerializer(typeof(GenerationOutput));
                ser.Serialize(sw, output);
            }
        }
    }
}
