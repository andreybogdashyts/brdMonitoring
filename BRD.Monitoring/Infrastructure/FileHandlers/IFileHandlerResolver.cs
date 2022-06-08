using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRD.Monitoring.Infrastructure.FileHandlers
{
    public interface IFileHandlerResolver
    {
        IFileHandler Get(string ext);
    }
}
