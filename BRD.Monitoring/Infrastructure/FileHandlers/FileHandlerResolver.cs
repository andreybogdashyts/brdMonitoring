using System;

namespace BRD.Monitoring.Infrastructure.FileHandlers
{
    public class FileHandlerResolver : IFileHandlerResolver
    {
        public IFileHandler Get(string ext)
        {
            switch (ext.ToLower())
            {
                case ".xml":
                    return new XmlFileHandler();
                default:
                    throw new Exception($"Failed to resolve exception for {ext} extension");
        }
    }
}
}
