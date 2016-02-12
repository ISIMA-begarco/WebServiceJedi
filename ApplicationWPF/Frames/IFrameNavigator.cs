using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationWPF.Frames
{
    public class FrameChangedEventArgs : EventArgs
    {
        public string nextFramePath;

        public FrameChangedEventArgs(string nfp)
        {
            nextFramePath = nfp;
        }
    }

    public interface IFrameNavigator
    {
        EventHandler<FrameChangedEventArgs> OnFrameChanged
        {
            get;
            set;
        }
    }
}
