using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Directory
{
    public class CloseAllNodes : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public CloseAllNodes(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            var selectedNode = this._hierarchyControl.GetSelectedItem();

            try
            {
                Dte.ExecuteCommand("SolutionExplorer.CollapseAll");
            }
            catch
            {
            }

            return new ExecutionResult(executionContext.Clear(), CommandState.Handled);
        }
    }
}