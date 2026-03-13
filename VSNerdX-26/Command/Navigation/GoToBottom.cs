using System.Windows.Forms;
using VsNerdX.Core;

namespace VsNerdX.Command.Navigation
{
    public class GoToBottom : ICommand
    {
        private IHierarchyControl _hierarchyControl;

        public GoToBottom(IHierarchyControl hierarchyControl)
        {
            this._hierarchyControl = hierarchyControl;
        }

        public ExecutionResult Execute(IExecutionContext executionContext, Keys key)
        {
            try
            {
                if (((HierarchyControl)this._hierarchyControl).GetHierarchyListBox().IsKeyboardFocusWithin)
                {
                    this._hierarchyControl.GoToBottom();
                }
                else
                {
                    ((HierarchyControl)this._hierarchyControl).helpViewControl.GoToBottom();
                }
            }
            catch
            {
            }

            return new ExecutionResult(executionContext.Clear(), CommandState.Handled);
        }
    }
}
