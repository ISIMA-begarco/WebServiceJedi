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

namespace ApplicationWPF.ViewModel.Gestion
{
    /// <summary>
    /// Interaction logic for CtrlResearch.xaml
    /// </summary>
    public partial class CtrlResearch : UserControl
    {
        List<string> nameList;
        public CtrlResearch()
        {
            InitializeComponent();
            nameList = new List<string>
            {
                "A0-Word","A1-Word","A2-Word","A3-Word"
            };

            txtAuto.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);

        }
        private void txtAuto_TextChanged(object sender, TextChangedEventArgs e) 
        {
            string typedString = txtAuto.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in nameList)
            {
                if (!string.IsNullOrEmpty(txtAuto.Text))
                {
                    if (item.StartsWith(typedString))
                    {
                        autoList.Add(item);
                    }
                }
            }
            if (autoList.Count > 0)
            {
                lbSuggestion.ItemsSource = autoList;
                lbSuggestion.Visibility = Visibility.Visible;
            }
            else if (txtAuto.Text.Equals(""))
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
            }
            else
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
            }
        }
        private void lbSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSuggestion.ItemsSource != null)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                txtAuto.TextChanged -= new TextChangedEventHandler(txtAuto_TextChanged);
                if (lbSuggestion.SelectedIndex != -1)
                {
                    txtAuto.Text = lbSuggestion.SelectedItem.ToString();
                }
                txtAuto.TextChanged += new TextChangedEventHandler(txtAuto_TextChanged);
            }
        }
    }
}