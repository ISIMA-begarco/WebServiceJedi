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
    /// Logique d'interaction pour PlayChoice.xaml
    /// </summary>
    public partial class PlayChoice : UserControl
    {

        /// <summary>
        /// Identified the ImSource dependency property
        /// </summary>
        public static readonly DependencyProperty ImSourceProperty;

        /// <summary>
        /// Gets or sets the ImSource which is displayed next to the field
        /// </summary>
        public BitmapImage PlayChoiceImSource
        {
            get { return (BitmapImage)GetValue(ImSourceProperty); }
            set { SetValue(ImSourceProperty, value); }
        }
        

        /// <summary>
        /// Gets or sets the Label which is displayed next to the field
        /// </summary>
        public String PlayChoiceTitle
        {
            get { return this.Title.Text; }
            set { this.Title.Text = value; }
        }

        public String PlayChoiceDescription
        {
            get { return this.Description.Text; }
            set { this.Description.Text = value; }
        }


        public event EventHandler Event;

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (Event != null)
            {
                Event(this, new EventArgs());
            }
        }
       

        static PlayChoice()
        {
            ImSourceProperty = DependencyProperty.Register("PlayChoiceImSource", 
                        typeof(BitmapImage),typeof(PlayChoice),
                        new PropertyMetadata());

        }


        public PlayChoice()
        {
            InitializeComponent();
        }
    }
}
