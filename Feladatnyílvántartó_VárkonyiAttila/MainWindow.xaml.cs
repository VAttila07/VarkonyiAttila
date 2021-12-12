using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        public List<CheckBox> ujChboxok = new List<CheckBox>();
        public List<CheckBox> toroltekListaja = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Exit += new ExitEventHandler(BezarEsElment);
            Betoltes();
        }

        public void elemekFrissitese(ListBox listak, List<CheckBox> lElemei)
        {
            listak.ItemsSource = lElemei;
            listak.Items.Refresh();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fText != null)
            {
                CheckBox chGeneral = new CheckBox();
                chGeneral.IsChecked = false;
                chGeneral.Content = fText.Text;
                ujChboxok.Add(chGeneral);
                chGeneral.Checked += new RoutedEventHandler(bePipalt);
                chGeneral.Unchecked += new RoutedEventHandler(bePipalt);
            }
            else
                return;

            string[] Feladatok = new string[fLista.Items.Count];
            string[] toroltFeladatok = new string[tElemek.Items.Count];

            for (int i = 0; i < Feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)fLista.Items[i];
                Feladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("Feladatok.txt", Feladatok);

            elemekFrissitese(fLista, ujChboxok);
        }


        private void bePipalt(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if (box.IsChecked == true)
            {
                box.FontStyle = FontStyles.Italic;
                box.Foreground = Brushes.Gray;
            }
            else
            {
                box.FontStyle = FontStyles.Normal;
                box.Foreground = Brushes.Black;
            }

            elemekFrissitese(fLista, ujChboxok);
        }

        private void fModositasa(object sender, RoutedEventArgs e)
        {
            var newWindow = new Window1();
            newWindow.Show();
        }

        CheckBox utolsoElem = null;
        private void kijelolesValtozott(object sender, SelectionChangedEventArgs e)
        {
            CheckBox selectedItem = (CheckBox)fLista.SelectedItem;
            if (selectedItem == null) return;
            utolsoElem = selectedItem;
            fText.Text = selectedItem.Content.ToString();
        }

        private void torles(object sender, RoutedEventArgs e)
        {
            if (fLista.SelectedItem == null)
                return;

            CheckBox töröltListaElem = (CheckBox)fLista.SelectedItem;
            toroltekListaja.Add(töröltListaElem);
            ujChboxok.Remove(töröltListaElem);

            string[] Feladatok = new string[fLista.Items.Count];
            string[] toroltFeladatok = new string[tElemek.Items.Count];

            for (int i = 0; i < Feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)fLista.Items[i];
                Feladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("Feladatok.txt", Feladatok);

            elemekFrissitese(fLista, ujChboxok);
            elemekFrissitese(tElemek, toroltekListaja);
        }

        private void vissszaAll(object sender, RoutedEventArgs e)
        {
            if (tElemek.SelectedItem == null)
                return;

            CheckBox visszaAllit = (CheckBox)tElemek.SelectedItem;
            ujChboxok.Add(visszaAllit);
            toroltekListaja.Remove(visszaAllit);

            string[] Feladatok = new string[fLista.Items.Count];
            string[] toroltFeladatok = new string[tElemek.Items.Count];

            for (int i = 0; i < Feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)fLista.Items[i];
                Feladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("Feladatok.txt", Feladatok);

            elemekFrissitese(fLista, ujChboxok);
            elemekFrissitese(tElemek, toroltekListaja);
        }

        private void veglegT(object sender, RoutedEventArgs e)
        {
            if (tElemek.SelectedItem == null)
                return;

            CheckBox veglegTorol = (CheckBox)tElemek.SelectedItem;
            toroltekListaja.Remove(veglegTorol);

            elemekFrissitese(tElemek, toroltekListaja);
        }

        private void BezarEsElment(object sender, ExitEventArgs e)
        {
            string[] Feladatok = new string[fLista.Items.Count];
            string[] toroltFeladatok = new string[tElemek.Items.Count];

            for (int i = 0; i < Feladatok.Length; i++)
            {
                CheckBox box = (CheckBox)fLista.Items[i];
                Feladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("Feladatok.txt", Feladatok);

            for (int i = 0; i < toroltFeladatok.Length; i++)
            {
                CheckBox box = (CheckBox)tElemek.Items[i];
                toroltFeladatok[i] = box.Content.ToString() + ";" + box.IsChecked;
            }

            File.WriteAllLines("töröltFeladatok.txt", toroltFeladatok);

        }

        public void Betoltes()
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
                ujboliLetrehozas.Checked += new RoutedEventHandler(bePipalt);
                ujboliLetrehozas.Unchecked += new RoutedEventHandler(bePipalt);

                if (ujboliLetrehozas.IsChecked == true)
                {
                    ujboliLetrehozas.FontStyle = FontStyles.Italic;
                    ujboliLetrehozas.Foreground = Brushes.Gray;
                }
                else
                {
                    ujboliLetrehozas.FontStyle = FontStyles.Normal;
                    ujboliLetrehozas.Foreground = Brushes.Black;
                }

                chBoxok.Add(ujboliLetrehozas);
                ujChboxok.Add(ujboliLetrehozas);
            }

            fLista.ItemsSource = chBoxok;



            if (!File.Exists("töröltFeladatok.txt."))
                return;
            string[] toroltFeladatok = File.ReadAllLines("töröltFeladatok.txt");
            List<CheckBox> toroltChBoxok = new List<CheckBox>();

            for (int i = 0; i < toroltFeladatok.Length; i++)
            {
                string[] nev = toroltFeladatok[i].Split(';');
                CheckBox toroltUj = new CheckBox();

                toroltUj.Content = nev[0];
                if (nev[1] == "True")
                {
                    toroltUj.IsChecked = true;
                }
                toroltUj.Checked += new RoutedEventHandler(bePipalt);
                toroltUj.Unchecked += new RoutedEventHandler(bePipalt);

                if (toroltUj.IsChecked == true)
                {
                    toroltUj.FontStyle = FontStyles.Italic;
                    toroltUj.Foreground = Brushes.Gray;
                }
                else
                {
                    toroltUj.FontStyle = FontStyles.Normal;
                    toroltUj.Foreground = Brushes.Black;
                }

                toroltChBoxok.Add(toroltUj);
                toroltekListaja.Add(toroltUj);
            }
            tElemek.ItemsSource = toroltChBoxok;


            elemekFrissitese(fLista, ujChboxok);
            elemekFrissitese(tElemek, toroltekListaja);
        }

        public void Betoltes2()
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
                ujboliLetrehozas.Checked += new RoutedEventHandler(bePipalt);
                ujboliLetrehozas.Unchecked += new RoutedEventHandler(bePipalt);

                if (ujboliLetrehozas.IsChecked == true)
                {
                    ujboliLetrehozas.FontStyle = FontStyles.Italic;
                    ujboliLetrehozas.Foreground = Brushes.Gray;
                }
                else
                {
                    ujboliLetrehozas.FontStyle = FontStyles.Normal;
                    ujboliLetrehozas.Foreground = Brushes.Black;
                }

                chBoxok.Add(ujboliLetrehozas);
                ujChboxok.Add(ujboliLetrehozas);
            }

            fLista.ItemsSource = chBoxok;
            elemekFrissitese(fLista, ujChboxok);
        }

    }
}
