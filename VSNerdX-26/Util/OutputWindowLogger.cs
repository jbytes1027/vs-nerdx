using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsNerdX.Util
{
    public class OutputWindowLogger : ILogger
    {
        private readonly AsyncPackage _package;
        private readonly IVsOutputWindowPane _pane;

        private OutputWindowLogger(AsyncPackage package, IVsOutputWindowPane pane)
        {
            _package = package;
            _pane = pane;
        }

        public static async Task<OutputWindowLogger> CreateAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var outputWindow = await package.GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;

            var paneGuid = new Guid("582cc5c8-3731-4e74-a5a4-044954dc0c95");
            outputWindow.CreatePane(paneGuid, "VsNerdX", 1, 1);

            outputWindow.GetPane(ref paneGuid, out IVsOutputWindowPane pane);

            pane.OutputString("Logging started\n");

            return new OutputWindowLogger(package, pane);
        }

        public void Log(string message)
        {
            _package.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                _pane?.OutputString($"VsNerdX: {message}\n");
            }).Task.Forget();
        }
    }
}
