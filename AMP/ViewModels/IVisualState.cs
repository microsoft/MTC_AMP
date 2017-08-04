using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP.ViewModels
{
    public class VisualStateEventArgs : EventArgs
    {
        public string NewState { get; set; }

        public VisualStateEventArgs()
        {

        }

        public VisualStateEventArgs(string newState)
        {
            NewState = newState;
        }
    }

    public interface IVisualState
    {
        event EventHandler<VisualStateEventArgs> VisualStateChanged;
    }
}
