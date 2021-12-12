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
        public static List<CheckBox> ujChbox = new List<CheckBox>();
        MainWindow mentes1;
        public Window1()
        {
            InitializeComponent();
            Betoltes();
        }

        CheckBox utolsoElem = null;
        private void kijelolesValtozott(object sender, SelectionChangedEventArgs e)
        {
            CheckBox selectedItem = (CheckBox)modositasiLista.SelectedItem;
            if (selectedItem == null) return;
            utolsoElem = selectedItem;
            szNev.Text = selectedItem.Content.ToString();
        }

        private void Betoltes()
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
                ujChbox.Add(ujboliLetrehozas);
            }

            modositasiLista.ItemsSource = chBoxok;
        }

        private void elemekFrissitese(ListBox listak, List<CheckBox> lElemei)
        {
            listak.ItemsSource = lElemei;
            listak.Items.Refresh();
        }
        private void atnevezes(object sender, RoutedEventArgs e)
        {

            if (utolsoElem == null) return;


            utolsoElem.Content = szNev.Text;
            elemekFrissitese(modositasiLista, ujChbox);
        }

        private void mentes(object sender, RoutedEventArgs e)
        {
            string[] Feladatok = new string[modositasiLista.Items.Count];

            for (int i = 0; i < Feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)modositasiLista.Items[i];
                Feladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("Feladatok.txt", Feladatok);
            mentes1 = new MainWindow();
            mentGomb.Click += vege;
        }

        void vege(object sender, EventArgs e)
        {
            mentes1.Betoltes2();
        }
    }
}
