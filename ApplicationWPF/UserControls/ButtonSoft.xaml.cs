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

namespace ApplicationWPF.UserControls
{
    /// <summary>
    /// Logique d'interaction pour AppButton1.xaml
    /// </summary>
    public partial class ButtonSoft : UserControl
    {
        /// <summary>
        /// Gets or sets the Label which is displayed next to the field
        /// </summary>
        public String Label
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        public ButtonSoft()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event EventHandler Event;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (Event != null)
            {
                Event(this, new EventArgs());
            }
        }
    }
}
