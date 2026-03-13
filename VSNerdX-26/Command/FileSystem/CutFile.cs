using System;
using System.Linq;
using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    public class CutFile : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public CutFile(IHierarchyControl hierarchyControl)
        {
            _hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            try
            {
                Dte.ExecuteCommand("Edit.Cut");
            }
            catch
            {
            }

            executionContext = executionContext.Clear();

            return new ExecutionResult(executionContext, CommandState.Handled);
        }
    }
}
