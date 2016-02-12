using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApplicationWPF.Frames
{
    /// <summary>
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page, IFrameNavigator
    {
        public event EventHandler<FrameChangedEventArgs> m_changeFrame;

        public MainMenu()
        {
            InitializeComponent();
        }

        public EventHandler<FrameChangedEventArgs> OnFrameChanged
        {
            get { return m_changeFrame; }
            set { m_changeFrame = value; }
        }

        private void ButtonPlay_Event(object sender, EventArgs e)
        {
            string nextFrame = "Frames/PlayPage.xaml";
            OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
        }

        private void ButtonManage_Event(object sender, EventArgs e)
        {
            string nextFrame = "Frames/GestionTournament.xaml";
            OnFrameChanged(this, new FrameChangedEventArgs(nextFrame));
        }

        private void ButtonQuit_Event(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
