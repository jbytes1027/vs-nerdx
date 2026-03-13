using System.Windows.Forms;
using VsNerdX.Core;
using static VsNerdX.VsNerdXPackage;

namespace VsNerdX.Command.Directory
{
    public class CloseNodeRecursively : ICommand
    {
        private readonly IHierarchyControl _hierarchyControl;

        public CloseNodeRecursively(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            var selectedNode = this._hierarchyControl.GetSelectedItem();

            try
            {
                Dte.ExecuteCommand("SolutionExplorer.CollapseAllDescendants");
            }
            catch
            {
            }

            return new ExecutionResult(executionContext.Clear(), CommandState.Handled);
        }
    }
}
