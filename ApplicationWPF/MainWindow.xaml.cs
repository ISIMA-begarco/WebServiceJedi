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
using System.Windows.Shapes;

using ApplicationWPF.Frames;

namespace ApplicationWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFrameNavigator m_currentFrame;

        /*public IFrameNavigator CurrentFrame
        {
            get { return m_currentFrame; }
            set { m_currentFrame = value; }
        }*/

        public MainWindow()
        {
            InitializeComponent();
        }

        void ChangeFrame (object sender, FrameChangedEventArgs e)
        {
            // Unsubscribe to event handler
            m_currentFrame.OnFrameChanged -= ChangeFrame;

            // Update de frame
            this.MainFrame.NavigationService.Navigate(new System.Uri(e.nextFramePath, UriKind.Relative));
        }

        void FrameLoadCompleted (object sender, EventArgs e)
        {
            /*if (MainFrame.NavigationService.Content != null)
            {*/
                m_currentFrame = MainFrame.NavigationService.Content as IFrameNavigator;
                if (m_currentFrame != null)
                {
                    m_currentFrame.OnFrameChanged += ChangeFrame;
                }
            /*}
            else
            {
                this.MainFrame.NavigationService.Navigate(new System.Uri("Frames/MainMenu.xaml", UriKind.Relative));
            }*/
        }

        private void WindowLoaded(object sender, EventArgs e)
        {
            this.MainFrame.NavigationService.Navigate(new System.Uri("Frames/MainMenu.xaml", UriKind.Relative));
            MainFrame.NavigationService.LoadCompleted += FrameLoadCompleted;
        }
    }
}
