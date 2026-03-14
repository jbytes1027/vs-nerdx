using EnvDTE;
using EnvDTE80;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VsNerdX.Core;
using VsNerdX.Dispatcher;
using System.Threading;
using System;
using DebugLogger = VsNerdX.Util.DebugLogger;

namespace VsNerdX
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid("c8973938-38ba-4db7-9798-11c7f5b4bc1f")]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class VsNerdXPackage : AsyncPackage
    {
        public static VsNerdXPackage Instance;
        public static DTE2 Dte;

        private CommandProcessor _commandProcessor;
        private ConditionalKeyDispatcher _keyDispatcher;
        private DebugLogger _logger;

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.

            BackgroundThreadInitialization();

            _logger.Log("VSNerd Switching to main thread");
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            _logger.Log("VSNerd loading on main thread");
            Dte = await GetServiceAsync(typeof(_DTE)) as DTE2;
            var solutionExplorerControl = new HierarchyControl(this, _logger);

            _commandProcessor = new CommandProcessor(solutionExplorerControl, _logger);

            _keyDispatcher = new ConditionalKeyDispatcher(
                new SolutionExplorerDispatchCondition(solutionExplorerControl, _logger),
                new KeyDispatcher(_commandProcessor),
                _logger);
        }

        private void BackgroundThreadInitialization()
        {
            _logger = new DebugLogger();
            Instance = this;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _keyDispatcher.Dispose();
        }
    }
}