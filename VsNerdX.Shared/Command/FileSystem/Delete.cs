using System;
using System.Linq;
using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Navigation
{
    public class Delete : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public Delete(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            try
            {
                Dte.ExecuteCommand("Edit.Delete");
            }
            catch
            {
            }

            executionContext = executionContext.Clear();

            return new ExecutionResult(executionContext, CommandState.Handled);
        }
    }
}