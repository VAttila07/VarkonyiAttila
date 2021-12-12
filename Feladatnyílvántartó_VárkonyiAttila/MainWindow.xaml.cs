using System;
using System.Collections.Generic;
using System.IO;
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

namespace Feladatnyílvántartó_VárkonyiAttila
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CheckBox> ujChboxok = new List<CheckBox>();
        private List<CheckBox> toroltekListaja = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void elemekFrissitese(ListBox listak, List<CheckBox> lElemei)
        {
            listak.ItemsSource = lElemei;
            listak.Items.Refresh();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fText != null || fText.Text !=" ")
            {
                CheckBox chGeneral = new CheckBox();
                chGeneral.IsChecked = false;
                chGeneral.Content = fText.Text;
                ujChboxok.Add(chGeneral);
                chGeneral.Checked += new RoutedEventHandler(bePipalt);
                chGeneral.Unchecked += new RoutedEventHandler(bePipalt);
            }
        }

        private void bePipalt(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
