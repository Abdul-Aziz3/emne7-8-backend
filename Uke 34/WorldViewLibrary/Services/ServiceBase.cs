using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldViewLibrary.Services
{

    public abstract class ServiceBase
    {
        // private fields
        protected Action<string>? _logAction;        

        protected ServiceBase(Action<string>? logAction)
        {
            _logAction = logAction;
        }

        protected void LogMessage(string message)
        {
            if (_logAction != null)
                _logAction(message);            
        }
    }
}
