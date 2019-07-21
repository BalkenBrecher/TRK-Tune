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

namespace RuW_Baumkontrolle
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        //  Globale Bool Variablen
        //
        bool b_Neue_Liste = true;

        string aVz = Directory.GetCurrentDirectory();

        int aILB = -1;  //  Aktueller Index der Baumliste

        List<string> L_Listen = new List<string>();
        List<string> L_Baeume_Los1 = new List<string>();
        List<string> L_Baeume_Los2 = new List<string>(); List<string> L_Baeume_Los2_Neu = new List<string>();
        List<string> L_Baeume_Los3 = new List<string>();
        List<string> L_Baeume_Los4 = new List<string>();
        List<string> L_Baeume_Los5 = new List<string>();
        List<string> L_Baeume_Los6 = new List<string>();
        List<string> L_Baeume_Los7 = new List<string>();
        List<string> L_Baeume_Los8 = new List<string>();
        List<string> L_Baeume_Los9 = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            Startmethoden();
        }

        private void Startmethoden()
        {
            L_Baeume_Los2_Neu = L_Baeume_Los2;

            lbl_Aktuelles_Datum.Content = DateTime.Today.ToString("dd.MM.yyyy");
            Pruefe_Lokale_Listen();
        }

        private void Waehle_Los(object sender, SelectionChangedEventArgs e)
        {
            if(cBox_Los.SelectedIndex != -1)
            { 
                string text = cBox_Los.SelectedItem.ToString(); text = text.Substring(text.LastIndexOf(':') + 1);
                cBox_Baeume.Items.Clear();
                if (text.Contains("Los 1"))
                    foreach (string s in L_Baeume_Los1)
                        if (s.Length > 0)
                            cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 2"))
                    foreach (string s in L_Baeume_Los2)
                        if(s.Length > 0)
                            cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));          
            }
        }

        private void Waehle_Baum(object sender, SelectionChangedEventArgs e)
        {
            if (cBox_Baeume.SelectedIndex != -1)
            {
                //
                //  0. Baumnummer
                //  1. Alter
                //  2. Höhe
                //  3. Durchmesser in 1,3m Höhe
                //  4. Kronendurchmesser
                //  5. ---
                //
                GroupBox_Baum.IsEnabled = true;
                string Baum = cBox_Baeume.SelectedItem.ToString();
                foreach (string s in L_Baeume_Los2)
                {
                    if (s.Contains(Baum))
                    {
                        aILB = cBox_Baeume.SelectedIndex;
                        string[] Baumdaten = s.Split('|');
                        lbl_Baumnummer.Content = Baumdaten[0];
                        lbl_Alter.Content = Baumdaten[1];
                        lbl_Hoehe.Content = Baumdaten[2];
                        lbl_Durchmesser.Content = Baumdaten[3];
                        lbl_Kronendruchmesser.Content = Baumdaten[4];
                        break;
                    }
                }   //  Ende foreach
            }   //  Ende if Ein Baum gewählt wurde
            else
                GroupBox_Baum.IsEnabled = false;
        }   //  Ende Funktion Waehle_Baum

        private void Pruefe_Lokale_Listen()
        {
            try
            {
                DirectoryInfo d     = new DirectoryInfo(aVz);
                FileInfo[] Files    = d.GetFiles("*.txt");
                foreach (FileInfo file in Files)
                {
                    L_Listen.Add(file.Name);
                    if(file.Name.Contains("Los1"))
                        L_Baeume_Los1 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los2"))
                        L_Baeume_Los2 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los3"))
                        L_Baeume_Los3 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los4"))
                        L_Baeume_Los4 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los5"))
                        L_Baeume_Los5 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los6"))
                        L_Baeume_Los6 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los7"))
                        L_Baeume_Los7 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los8"))
                        L_Baeume_Los8 = Lese_Baeume(file.Name);
                    if (file.Name.Contains("Los9"))
                        L_Baeume_Los9 = Lese_Baeume(file.Name);
                }
            }
            catch(Exception e_Pruefe_Lokale_Liste)
            {
                MessageBox.Show("Fehler beim Prüfen der Lokalen Dateien:\n\n" + e_Pruefe_Lokale_Liste);
            }
        }

        private List<string> Lese_Baeume(string _BL)
        {
            List<string> L_Temp = new List<string>();

            try
            {
                var fileStream = new FileStream(_BL, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        L_Temp.Add(line);
                    }
                }
            }
            catch (Exception e_Lese_Baeume)
            {
                MessageBox.Show("Fehler beim Einlesen der Baumdatei \"" + _BL + "\":\n\n" + e_Lese_Baeume);
            }

            return L_Temp;
        }


        private string Erstelle_Neue_Eintrag()
        {
            string s_Temp = "";

            bool BNV = false; if (chkB_1_Baum_Nicht_Da.IsChecked == true) BNV = true; else BNV = false;         //  Baum nicht vorhanden
            bool THB = false; if (chkB_2_Totholzbeseitigung.IsChecked == true) THB = true; else THB = false;    //  Totholbeseitigung
            bool LH2 = false; if (chkB_3_Lichte_Hoehe_25.IsChecked == true) LH2 = true; else LH2 = false;       //  Lichte Höhe 2,5m
            bool LH4 = false; if (chkB_4_Lichte_Hoehe_45.IsChecked == true) LH4 = true; else LH4 = false;       //  Lichte Höhe 4,5m
            bool LRP = false; if (chkB_5_Lichtraumprofil.IsChecked == true) LRP = true; else LRP = false;       //  Lichtraumprofil
            bool BFG = false; if (chkB_6_Baumfaellung.IsChecked == true) BFG = true; else BFG = false;          //  Baumfällung
            bool KEV = false; if (chkB_7_Krone_Kuerzen.IsChecked == true) KEV = true; else KEV = false;         //  Krone einkürzen/verschneiden
            bool LBT = false; if (chkB_8_Leiter_Benoetigt.IsChecked == true) LBT = true; else LBT = false;      //  Leiter benötigt
            bool HBT = false; if (chkB_9_Hebebuehne_Benoetigt.IsChecked == true) HBT = true; else HBT = false;  //  Hebebühne benötigt
            s_Temp = "Baum nicht vorhanden:" + BNV + "|Totholbeseitigung:" + THB + "|Lichte Höhe 2,5m:" + LH2 + "|Lichte Höhe 4,5m:" + LH4 + "|Lichtraumprofil:" + LRP +
                "|Baumfällung:" + BFG + "|Krone einkürzen/verschneiden:" + KEV + "|Leiter benötigt:" + LBT + "|Hebebühne benötigt:" + LBT + "|Bemerkungen:" + textBox.Text;

            return s_Temp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(L_Baeume_Los2.Count.ToString());
        }

        //###########################################################################################################################################
        //++++++++++++
        //
        //  B U T T O N  F U N K T I O N E N
        //
        //++++++++++++
        //###########################################################################################################################################

        private void CBox_A_Neue_Liste_Checked(object sender, RoutedEventArgs e)
        {
            if (cBox_A_Neue_Liste.IsChecked == true)
                b_Neue_Liste = true;
            else
                b_Neue_Liste = false;
        }

        private void Btn_Baum_Speichern_Click(object sender, RoutedEventArgs e)
        {
            L_Baeume_Los2_Neu[aILB] = Erstelle_Neue_Eintrag();
        }
    }
}
