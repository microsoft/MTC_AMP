using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP.UX.Services
{
    public enum ViewTarget
    { 
        MainFrame,
        SubFrame
    }
    public class ViewInfo
    {
        public Type View { get; set; }
        public ViewTarget Target { get; set; }
    }
}
