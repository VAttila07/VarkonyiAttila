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
using System.Windows.Shapes;

namespace Feladatnyílvántartó_VárkonyiAttila
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Betoltes();
        }

        private void Betoltes()
        {
            {
                if (!File.Exists("Feladatok.txt."))
                    return;
                string[] Feladatok = File.ReadAllLines("Feladatok.txt");
                List<CheckBox> chBoxok = new List<CheckBox>();

                for (int i = 0; i < Feladatok.Length; i++)
                {
                    string[] nev = Feladatok[i].Split(';');
                    CheckBox ujboliLetrehozas = new CheckBox();

                    ujboliLetrehozas.Content = nev[0];
                    if (nev[1] == "True")
                    {
                        ujboliLetrehozas.IsChecked = true;
                    }

                    chBoxok.Add(ujboliLetrehozas);
                }

                modositasiLista.ItemsSource = chBoxok;
            }
        }
    }
}
