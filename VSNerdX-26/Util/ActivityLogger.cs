using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsNerdX.Util
{
    public class ActivityLogger : ILogger
    {
        private readonly IVsActivityLog _log;

        private ActivityLogger(IVsActivityLog log)
        {
            _log = log;
        }

        public static async Task<ActivityLogger> CreateAsync(AsyncPackage package)
        {
            IVsActivityLog log = await package.GetServiceAsync(typeof(SVsActivityLog)) as IVsActivityLog;
            return new ActivityLogger(log);
        }

        public void Log(string message)
        {
            _log.LogEntry((uint)__ACTIVITYLOG_ENTRYTYPE.ALE_INFORMATION, "VsNerdX", message);
        }
    }
}
