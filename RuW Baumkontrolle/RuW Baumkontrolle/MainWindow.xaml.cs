using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace RuW_Baumkontrolle
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        //  Threads
        //
        Thread T_Speichern;
        //
        //  Globale Bool Variablen
        //
        bool b_Neue_Liste = true;
        bool b_Gespeichert = false;
        bool b_Liste_laden = false;

        string aVz = Directory.GetCurrentDirectory();
        string aktLos = "";
        string gew_Liste = "";
        string T_Dateiname = "";

        int aILB = -1;  //  Aktueller Index der Baumliste
        int i_Tmp_List_Count = 0;

        List<string> L_Listen = new List<string>();
        List<string> L_Temp = new List<string>();
        List<string> L_Baeume_Los1 = new List<string>(); List<string> L_Baeume_Los1_Neu = new List<string>();
        List<string> L_Baeume_Los2 = new List<string>(); List<string> L_Baeume_Los2_Neu = new List<string>();
        List<string> L_Baeume_Los3 = new List<string>(); List<string> L_Baeume_Los3_Neu = new List<string>();
        List<string> L_Baeume_Los4 = new List<string>(); List<string> L_Baeume_Los4_Neu = new List<string>();
        List<string> L_Baeume_Los5 = new List<string>(); List<string> L_Baeume_Los5_Neu = new List<string>();
        List<string> L_Baeume_Los6 = new List<string>(); List<string> L_Baeume_Los6_Neu = new List<string>();
        List<string> L_Baeume_Los7 = new List<string>(); List<string> L_Baeume_Los7_Neu = new List<string>();
        List<string> L_Baeume_Los8 = new List<string>(); List<string> L_Baeume_Los8_Neu = new List<string>();
        List<string> L_Baeume_Los9 = new List<string>(); List<string> L_Baeume_Los9_Neu = new List<string>();
        List<string> L_Baeume_Los10 = new List<string>(); List<string> L_Baeume_Los10_Neu = new List<string>();
        List<string> L_Baeume_Los11 = new List<string>(); List<string> L_Baeume_Los11_Neu = new List<string>();
        List<string> L_Baeume_Los12 = new List<string>(); List<string> L_Baeume_Los12_Neu = new List<string>();

        System.Timers.Timer myTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();
            Startmethoden();

            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 1000; // 1000 ms is one second
            myTimer.Start();
        }

        private void Startmethoden()
        {
            lbl_Aktuelles_Datum.Content = DateTime.Today.ToString("dd.MM.yyyy");
            Pruefe_Lokale_Listen("");
            Pruefe_Auf_Andere_Listen();

            //
            //  Thread starten
            //
            T_Speichern = new Thread(Baum_Speichern);
            T_Speichern.Name = "Baum_Speichern";
            T_Speichern.IsBackground = true;
            T_Speichern.Start();
        }

        private void Waehle_Los(object sender, SelectionChangedEventArgs e)
        {
            if (cBox_Los.SelectedIndex != -1)
            {
                string text = cBox_Los.SelectedItem.ToString(); aktLos = text.Substring(text.LastIndexOf(':') + 1); b_Liste_laden = false;
                cBox_Baeume.Items.Clear();
                if (text.Contains("Los 1"))
                    foreach (string s in L_Baeume_Los1)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 2"))
                    foreach (string s in L_Baeume_Los2)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 3"))
                    foreach (string s in L_Baeume_Los3)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 4"))
                    foreach (string s in L_Baeume_Los4)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 5"))
                    foreach (string s in L_Baeume_Los5)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 6"))
                    foreach (string s in L_Baeume_Los6)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 7"))
                    foreach (string s in L_Baeume_Los7)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 8"))
                    foreach (string s in L_Baeume_Los8)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 9"))
                    foreach (string s in L_Baeume_Los9)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 10"))
                    foreach (string s in L_Baeume_Los10)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 11"))
                    foreach (string s in L_Baeume_Los11)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                if (text.Contains("Los 12"))
                    foreach (string s in L_Baeume_Los12)
                        if (s.Length > 0)
                            if (!cBox_Baeume.Items.Contains(s))
                                cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
            }
        }

        private void Waehle_Baum(object sender, SelectionChangedEventArgs e)
        {
            if (cBox_Baeume.SelectedIndex != -1)
            {
                //
                //  0.  Baumnummer
                //  1.  Alter
                //  2.  Höhe
                //  3.  Durchmesser in 1,3m Höhe
                //  4.  Kronendurchmesser
                //  5.  Baumart Latein
                //  6.  Baumart Deutsch
                //  7.  Baum nicht mehr vorhanden
                //  8.  Totholzbeseitigung
                //  9.  Lichte Höhe 2,5m
                //  10. Lichte Hö he 4,5m
                //  11. Baumfällung
                //  12. Krone einkürzen/verschneiden
                //  13. Leiter benötigt
                //  14. Hebebühne benötigt
                //  15. Bemerkungen
                //  16. Preis
                GroupBox_Baum.IsEnabled = true; string Baum = cBox_Baeume.SelectedItem.ToString();
                if (!b_Liste_laden)
                    Gib_Aktuelles_Los_Zurueck(false);
                else
                    i_Tmp_List_Count = L_Temp.Count();

                for (int i = 0; i < i_Tmp_List_Count; i++)
                {
                    string s = L_Temp[i];
                    if (s.Contains(Baum))
                    {
                        aILB = i;
                        string[] Baumdaten = s.Split('|');
                        if (Baumdaten[0].Contains("xxx"))
                            Baum_Nicht_Vorhanden(true);
                        else
                            Baum_Nicht_Vorhanden(false);
                        lbl_Baumnummer.Content = Baumdaten[0];
                        lbl_Alter.Content = Baumdaten[1];
                        lbl_Hoehe.Content = Baumdaten[2];
                        lbl_Durchmesser.Content = Baumdaten[3];
                        lbl_Kronendruchmesser.Content = Baumdaten[4];
                        lbl_Lat_Name.Content = Baumdaten[5];
                        lbl_Deu_Name.Content = Baumdaten[6];
                        cBox_Preis.SelectedIndex = -1;



                        if (Baumdaten.Count() > 10)
                        {
                            if (Baumdaten[7].Contains("True")) chkB_1_Baum_Nicht_Da.IsChecked = true; else chkB_1_Baum_Nicht_Da.IsChecked = false;
                            if (Baumdaten[8].Contains("True")) chkB_2_Totholzbeseitigung.IsChecked = true; else chkB_2_Totholzbeseitigung.IsChecked = false;
                            if (Baumdaten[9].Contains("True")) chkB_3_Stammaustrieb.IsChecked = true; else chkB_3_Stammaustrieb.IsChecked = false;
                            if (Baumdaten[10].Contains("True")) chkB_4_Lichte_Hoehe_45.IsChecked = true; else chkB_4_Lichte_Hoehe_45.IsChecked = false;
                            if (Baumdaten[11].Contains("True")) chkB_5_Lichtraumprofil.IsChecked = true;  else chkB_5_Lichtraumprofil.IsChecked = false;
                            if (Baumdaten[12].Contains("True")) chkB_6_Baumfaellung.IsChecked = true; else chkB_6_Baumfaellung.IsChecked = false;
                            if (Baumdaten[13].Contains("True")) chkB_7_Krone_Kuerzen.IsChecked = true; else chkB_7_Krone_Kuerzen.IsChecked = false;
                            if (Baumdaten[14].Contains("True")) chkB_8_Leiter_Benoetigt.IsChecked = true; else chkB_8_Leiter_Benoetigt.IsChecked = false;
                            if (Baumdaten[15].Contains("True")) chkB_9_Hebebuehne_Benoetigt.IsChecked = true; else chkB_9_Hebebuehne_Benoetigt.IsChecked = false;
                            txtBox_Bemerkungen.Text = Baumdaten[16].ToString().Substring(12);

                            for (int a = 0; a < cBox_Preis.Items.Count; a++)
                            {
                                if (cBox_Preis.Items[a].ToString().Contains(Baumdaten[17].ToString().Substring(6)))
                                {
                                    cBox_Preis.SelectedIndex = a;
                                    break;
                                }
                            }


                        }
                        else
                        {
                            chkB_1_Baum_Nicht_Da.IsChecked = false; chkB_2_Totholzbeseitigung.IsChecked = false; chkB_3_Stammaustrieb.IsChecked = false; chkB_4_Lichte_Hoehe_45.IsChecked = false; txtBox_Bemerkungen.Text = "";
                            chkB_5_Lichtraumprofil.IsChecked = false; chkB_6_Baumfaellung.IsChecked = false; chkB_7_Krone_Kuerzen.IsChecked = false; chkB_8_Leiter_Benoetigt.IsChecked = false; chkB_9_Hebebuehne_Benoetigt.IsChecked = false;
                        }
                        break;
                    }
                }   //  Ende foreach
            }   //  Ende if Ein Baum gewählt wurde
            else
                GroupBox_Baum.IsEnabled = false;
        }   //  Ende Funktion Waehle_Baum

        private void Baum_Nicht_Vorhanden(bool _v)
        {
            if (_v)
            {
                chkB_1_Baum_Nicht_Da.IsChecked = true; chkB_1_Baum_Nicht_Da.IsEnabled = false;
                chkB_2_Totholzbeseitigung.IsChecked = false; chkB_2_Totholzbeseitigung.IsEnabled = false;
                chkB_3_Stammaustrieb.IsChecked = false; chkB_3_Stammaustrieb.IsEnabled = false;
                chkB_4_Lichte_Hoehe_45.IsChecked = false; chkB_4_Lichte_Hoehe_45.IsEnabled = false;
                chkB_5_Lichtraumprofil.IsChecked = false; chkB_5_Lichtraumprofil.IsEnabled = false;
                chkB_6_Baumfaellung.IsChecked = false; chkB_6_Baumfaellung.IsEnabled = false;
                chkB_7_Krone_Kuerzen.IsChecked = false; chkB_7_Krone_Kuerzen.IsEnabled = false;
                chkB_8_Leiter_Benoetigt.IsChecked = false; chkB_8_Leiter_Benoetigt.IsEnabled = false;
                chkB_9_Hebebuehne_Benoetigt.IsChecked = false; chkB_9_Hebebuehne_Benoetigt.IsEnabled = false;
                lbl_Baumnummer.Content = "xxx";
                lbl_Alter.Content = "xxx";
                lbl_Hoehe.Content = "xxx";
                lbl_Durchmesser.Content = "xxx";
                lbl_Kronendruchmesser.Content = "xxx";
                lbl_Deu_Name.Content = "Baum nicht vorhanden!";
            }
            else
            {
                chkB_1_Baum_Nicht_Da.IsChecked = false; chkB_1_Baum_Nicht_Da.IsEnabled = true;
                chkB_2_Totholzbeseitigung.IsChecked = false; chkB_2_Totholzbeseitigung.IsEnabled = true;
                chkB_3_Stammaustrieb.IsChecked = false; chkB_3_Stammaustrieb.IsEnabled = true;
                chkB_4_Lichte_Hoehe_45.IsChecked = false; chkB_4_Lichte_Hoehe_45.IsEnabled = true;
                chkB_5_Lichtraumprofil.IsChecked = false; chkB_5_Lichtraumprofil.IsEnabled = true;
                chkB_6_Baumfaellung.IsChecked = false; chkB_6_Baumfaellung.IsEnabled = true;
                chkB_7_Krone_Kuerzen.IsChecked = false; chkB_7_Krone_Kuerzen.IsEnabled = true;
                chkB_8_Leiter_Benoetigt.IsChecked = false; chkB_8_Leiter_Benoetigt.IsEnabled = true;
                chkB_9_Hebebuehne_Benoetigt.IsChecked = false; chkB_9_Hebebuehne_Benoetigt.IsEnabled = true;
            }
        }

        private void Pruefe_Lokale_Listen(string _Liste)
        {
            if (_Liste.Length <= 0)
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(aVz);
                    FileInfo[] Files = d.GetFiles("*.txt");
                    foreach (FileInfo file in Files)
                    {
                        L_Listen.Add(file.Name);
                        if (file.Name.Contains("Los_1_")) { cBox_Los.Items.Add("Los 1"); L_Baeume_Los1 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_2_")) { cBox_Los.Items.Add("Los 2"); L_Baeume_Los2 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_3_")) { cBox_Los.Items.Add("Los 3"); L_Baeume_Los3 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_4_")) { cBox_Los.Items.Add("Los 4"); L_Baeume_Los4 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_5_")) { cBox_Los.Items.Add("Los 5"); L_Baeume_Los5 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_6_")) { cBox_Los.Items.Add("Los 6"); L_Baeume_Los6 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_7_")) { cBox_Los.Items.Add("Los 7"); L_Baeume_Los7 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_8_")) { cBox_Los.Items.Add("Los 8"); L_Baeume_Los8 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_9_")) { cBox_Los.Items.Add("Los 9"); L_Baeume_Los9 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_10_")) { cBox_Los.Items.Add("Los 10"); L_Baeume_Los10 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_11_")) { cBox_Los.Items.Add("Los 11"); L_Baeume_Los11 = Lese_Baeume(file.Name); }
                        if (file.Name.Contains("Los_12_")) { cBox_Los.Items.Add("Los 12"); L_Baeume_Los12 = Lese_Baeume(file.Name); }
                    }
                }
                catch (Exception e_Pruefe_Lokale_Liste)
                {
                    MessageBox.Show("Fehler beim Prüfen der Lokalen Dateien:\n\n" + e_Pruefe_Lokale_Liste);
                }   //  Ende catch
            }   //  Ende if, ob ein String übergeben wurde
            else
            {
                if (_Liste.Contains("Los1"))
                    L_Baeume_Los1 = L_Temp;
                if (_Liste.Contains("Los2"))
                    L_Baeume_Los2 = L_Temp;
                if (_Liste.Contains("Los3"))
                    L_Baeume_Los3 = L_Temp;
                if (_Liste.Contains("Los4"))
                    L_Baeume_Los4 = L_Temp;
                if (_Liste.Contains("Los5"))
                    L_Baeume_Los5 = L_Temp;
                if (_Liste.Contains("Los6"))
                    L_Baeume_Los6 = L_Temp;
                if (_Liste.Contains("Los7"))
                    L_Baeume_Los7 = L_Temp;
                if (_Liste.Contains("Los8"))
                    L_Baeume_Los8 = L_Temp;
                if (_Liste.Contains("Los9"))
                    L_Baeume_Los9 = L_Temp;
                if (_Liste.Contains("Los9"))
                    L_Baeume_Los10 = L_Temp;
                if (_Liste.Contains("Los9"))
                    L_Baeume_Los11 = L_Temp;
                if (_Liste.Contains("Los9"))
                    L_Baeume_Los12 = L_Temp;
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
            string s_Temp = ""; string s_Preis = cBox_Preis.SelectionBoxItem.ToString();

            bool BNV = false; if (chkB_1_Baum_Nicht_Da.IsChecked == true) BNV = true; else BNV = false;         //  Baum nicht vorhanden
            bool THB = false; if (chkB_2_Totholzbeseitigung.IsChecked == true) THB = true; else THB = false;    //  Totholbeseitigung
            bool LH2 = false; if (chkB_3_Stammaustrieb.IsChecked == true) LH2 = true; else LH2 = false;       //  Lichte Höhe 2,5m
            bool LH4 = false; if (chkB_4_Lichte_Hoehe_45.IsChecked == true) LH4 = true; else LH4 = false;       //  Lichte Höhe 4,5m
            bool LRP = false; if (chkB_5_Lichtraumprofil.IsChecked == true) LRP = true; else LRP = false;       //  Lichtraumprofil
            bool BFG = false; if (chkB_6_Baumfaellung.IsChecked == true) BFG = true; else BFG = false;          //  Baumfällung
            bool KEV = false; if (chkB_7_Krone_Kuerzen.IsChecked == true) KEV = true; else KEV = false;         //  Krone einkürzen/verschneiden
            bool LBT = false; if (chkB_8_Leiter_Benoetigt.IsChecked == true) LBT = true; else LBT = false;      //  Leiter benötigt
            bool HBT = false; if (chkB_9_Hebebuehne_Benoetigt.IsChecked == true) HBT = true; else HBT = false;  //  Hebebühne benötigt
            bool PRE = false; if (cBox_Preis.SelectedIndex == -1) s_Preis = "0 €";                 //  Preis benötigt
            s_Temp = "Baum nicht vorhanden:" + BNV + "|Totholbeseitigung:" + THB + "|Lichte Höhe 2,5m:" + LH2 + "|Lichte Höhe 4,5m:" + LH4 + "|Lichtraumprofil:" + LRP +
                "|Baumfällung:" + BFG + "|Krone einkürzen/verschneiden:" + KEV + "|Leiter benötigt:" + LBT + "|Hebebühne benötigt:" + LBT + "|Bemerkungen:" + txtBox_Bemerkungen.Text + "|Preis:" + s_Preis;

            return s_Temp;
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
            Gib_Aktuelles_Los_Zurueck(true);
            b_Gespeichert = true;
        }

        private void Btn_Liste_Speichern_Click(object sender, RoutedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Soll eine neue Liste mit dem Namen:\n\nBaumkontrolle_" + aktLos + "_" + lbl_Aktuelles_Datum.Content + "\n\nangelegt werden?",
                                                                 "Neue Liste anlegen",
                                                                 MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (b_Neue_Liste)
                    {
                        if (File.Exists("Baumkontrolle_" + aktLos + "_" + lbl_Aktuelles_Datum.Content + ".txt"))
                        {
                            DialogResult result2 = MessageBox.Show("Es gibt bereits eine Liste mit dem Namen:\n\nBaumkontrolle_" + aktLos + "_" + lbl_Aktuelles_Datum.Content + "\n\nSoll diese überschrieben werden?",
                                                                 "Bestehende Liste überschreiben?",
                                                                 MessageBoxButtons.YesNo);
                            if (result2 == System.Windows.Forms.DialogResult.Yes)
                            {
                                using (TextWriter tw = new StreamWriter("Baumkontrolle_" + aktLos + "_N_" + lbl_Aktuelles_Datum.Content + ".txt"))
                                {
                                    Gib_Aktuelles_Los_Zurueck(false);
                                    foreach (String s in L_Temp)
                                        tw.WriteLine(s);
                                }
                            }
                            else
                            {
                                using (TextWriter tw = new StreamWriter("Baumkontrolle_" + aktLos + "_N_" + lbl_Aktuelles_Datum.Content + "_" + DateTime.Now.ToString("HH_mm_ss") + ".txt"))
                                {
                                    Gib_Aktuelles_Los_Zurueck(false);
                                    foreach (String s in L_Temp)
                                        tw.WriteLine(s);
                                }
                            }
                        }
                        else
                        {
                            using (TextWriter tw = new StreamWriter("Baumkontrolle_" + aktLos + "_N_" + lbl_Aktuelles_Datum.Content + ".txt"))
                            {
                                Gib_Aktuelles_Los_Zurueck(false);
                                foreach (String s in L_Temp)
                                    tw.WriteLine(s);
                            }
                        }
                        Pruefe_Auf_Andere_Listen();
                        MessageBox.Show("Die Liste wurde erfolgreich angelegt.\nSie ist unter folgendem Pfad zu finden:\n\n" + aVz);
                    }
                }
                catch (Exception e_Speichere_Datei)
                {
                    MessageBox.Show("Fehler beim speichern: \n\n" + e_Speichere_Datei);
                }   //  Ende catch
            }   //  Ende ob "Ja" gewählt wurde   
        }   //  Ende Mehtode Liste speichern

        private void Gib_Aktuelles_Los_Zurueck(bool _Neu_Anlegen)
        {
            if (aktLos.Contains("Los 1")) { i_Tmp_List_Count = L_Baeume_Los1.Count; L_Temp = L_Baeume_Los1; if (_Neu_Anlegen) { L_Baeume_Los1_Neu = L_Baeume_Los1; string tmp = L_Baeume_Los1_Neu[aILB]; L_Baeume_Los1_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 2")) { i_Tmp_List_Count = L_Baeume_Los2.Count; L_Temp = L_Baeume_Los2; if (_Neu_Anlegen) { L_Baeume_Los2_Neu = L_Baeume_Los2; string tmp = L_Baeume_Los2_Neu[aILB]; L_Baeume_Los2_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 3")) { i_Tmp_List_Count = L_Baeume_Los3.Count; L_Temp = L_Baeume_Los3; if (_Neu_Anlegen) { L_Baeume_Los3_Neu = L_Baeume_Los3; string tmp = L_Baeume_Los3_Neu[aILB]; L_Baeume_Los3_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 4")) { i_Tmp_List_Count = L_Baeume_Los4.Count; L_Temp = L_Baeume_Los4; if (_Neu_Anlegen) { L_Baeume_Los4_Neu = L_Baeume_Los4; string tmp = L_Baeume_Los4_Neu[aILB]; L_Baeume_Los4_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 5")) { i_Tmp_List_Count = L_Baeume_Los5.Count; L_Temp = L_Baeume_Los5; if (_Neu_Anlegen) { L_Baeume_Los5_Neu = L_Baeume_Los5; string tmp = L_Baeume_Los5_Neu[aILB]; L_Baeume_Los5_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 6")) { i_Tmp_List_Count = L_Baeume_Los6.Count; L_Temp = L_Baeume_Los6; if (_Neu_Anlegen) { L_Baeume_Los6_Neu = L_Baeume_Los6; string tmp = L_Baeume_Los6_Neu[aILB]; L_Baeume_Los6_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 7")) { i_Tmp_List_Count = L_Baeume_Los7.Count; L_Temp = L_Baeume_Los7; if (_Neu_Anlegen) { L_Baeume_Los7_Neu = L_Baeume_Los7; string tmp = L_Baeume_Los7_Neu[aILB]; L_Baeume_Los7_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 8")) { i_Tmp_List_Count = L_Baeume_Los8.Count; L_Temp = L_Baeume_Los8; if (_Neu_Anlegen) { L_Baeume_Los8_Neu = L_Baeume_Los8; string tmp = L_Baeume_Los8_Neu[aILB]; L_Baeume_Los8_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 9")) { i_Tmp_List_Count = L_Baeume_Los9.Count; L_Temp = L_Baeume_Los9; if (_Neu_Anlegen) { L_Baeume_Los9_Neu = L_Baeume_Los9; string tmp = L_Baeume_Los9_Neu[aILB]; L_Baeume_Los9_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 10")) { i_Tmp_List_Count = L_Baeume_Los10.Count; L_Temp = L_Baeume_Los10; if (_Neu_Anlegen) { L_Baeume_Los10_Neu = L_Baeume_Los10; string tmp = L_Baeume_Los10_Neu[aILB]; L_Baeume_Los10_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else if (aktLos.Contains("Los 11")) { i_Tmp_List_Count = L_Baeume_Los11.Count; L_Temp = L_Baeume_Los11; if (_Neu_Anlegen) { L_Baeume_Los11_Neu = L_Baeume_Los11; string tmp = L_Baeume_Los11_Neu[aILB]; L_Baeume_Los11_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
            else { i_Tmp_List_Count = L_Baeume_Los12.Count; L_Temp = L_Baeume_Los12; if (_Neu_Anlegen) { L_Baeume_Los12_Neu = L_Baeume_Los12; string tmp = L_Baeume_Los12_Neu[aILB]; L_Baeume_Los12_Neu[aILB] = tmp + Erstelle_Neue_Eintrag(); } }
        }

        //###########################################################################################################################################
        //++++++++++++
        //
        //  Liste laden und verarbeiten
        //
        //++++++++++++
        //###########################################################################################################################################

        private void Pruefe_Auf_Andere_Listen()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(aVz);
                FileInfo[] Files = d.GetFiles("*.txt");
                LB_Bish_Baueme.Items.Clear();
                foreach (FileInfo file in Files)
                {
                    if (file.Name.Contains("_N_"))
                    {
                        LB_Bish_Baueme.Items.Add(file.Name);
                    }
                }
            }
            catch (Exception e_Pruefe_Lokale_Liste)
            {
                MessageBox.Show("Fehler beim Prüfen der Lokalen Dateien:\n\n" + e_Pruefe_Lokale_Liste);
            }   //  Ende catch
        }

        private void ListBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogResult result2 = MessageBox.Show("Soll die Liste mit dem Namen:\n\n" + LB_Bish_Baueme.SelectedItem.ToString() + "\n\ngeladen werden?",
                                                                 "Bestehende Liste laden?",
                                                                 MessageBoxButtons.YesNo);
            if (result2 == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    gew_Liste = LB_Bish_Baueme.SelectedItem.ToString();
                    L_Temp = Lese_Baeume(gew_Liste); b_Liste_laden = true;
                    Console.WriteLine("Größe: " + L_Temp.Count() + " Wert an Stelle 0: " + L_Temp[0]);
                    foreach (string s in L_Temp)
                    {
                        if (s.Length > 0 && s.Contains("|"))
                        {
                            //Console.WriteLine(s);
                            cBox_Baeume.Items.Add(s.Substring(0, s.IndexOf('|')));
                        }
                    }
                    cBox_Los.SelectedIndex = -1; cBox_Baeume.SelectedIndex = 0;
                }
                catch (Exception e_Vorh_Liste_Laden)
                {
                    MessageBox.Show("Fehler beim laden der bestehenden Liste:\n\n" + LB_Bish_Baueme.SelectedItem.ToString() + "\n\n" + e_Vorh_Liste_Laden);
                }
            }
        }

        private void CBox_Vorhandene_Liste_Laden_Checked(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Eine bestehende Baumkontrollliste laden";
            theDialog.Filter = "txt Dateien|*.txt";
            theDialog.InitialDirectory = aVz;
            if (theDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            T_Dateiname = theDialog.FileName;
                            L_Temp = Lese_Baeume(theDialog.FileName);
                            Pruefe_Lokale_Listen(theDialog.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler! Konnte die Datei nicht lesen. Eine genauere Fehlerbeschreibung:\n\n" + ex.Message);
                    cBox_Vorhandene_Liste_Laden.IsChecked = false;
                }
            }
            else
            {
                cBox_Vorhandene_Liste_Laden.IsChecked = false;
            }
        }   //  Ende Methode 


        private void Baum_Speichern()
        {
            while (true)
            {
                if (b_Gespeichert)
                {
                    btn_Baum_Speichern.Dispatcher.Invoke((Action)delegate { btn_Baum_Speichern.Background = Brushes.Green; });
                    Thread.Sleep(1000);
                    btn_Baum_Speichern.Dispatcher.Invoke((Action)delegate { btn_Baum_Speichern.Background = Brushes.White; });
                    b_Gespeichert = false;
                }
                Thread.Sleep(200);
            }
        }

        private void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                lbl_Aktuelle_Uhrzeit.Content = DateTime.Now.ToString("HH:mm:ss");
            });
        }


        private void Erstelle_Excel(List<string> _Los)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel ist nicht installiert");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            // 7|40 Jahre|11 m|35 cm|9 m|Corylus colurna|Baumhasel|Baum nicht vorhanden:False|Totholbeseitigung:False|Lichte Höhe 2,5m:False
            //|Lichte Höhe 4,5m:False|Lichtraumprofil:True|Baumfällung:False|Krone einkürzen/verschneiden:False|Leiter benötigt:False
            //|Hebebühne benötigt:False|Bemerkungen:|Preis:75€
            
            xlWorkSheet.Cells[3, 1] = "Baumnummer"; 
            xlWorkSheet.Cells[3, 2] = "Alter";
            xlWorkSheet.Cells[3, 3] = "Höhe in m";
            xlWorkSheet.Cells[3, 4] = "Stammdurchmesser in 1m Höhe in cm"; 
            xlWorkSheet.Cells[3, 5] = "Kronendurchmesser in m"; 
            xlWorkSheet.Cells[3, 6] = "Lateinischer Name";
            xlWorkSheet.Cells[3, 7] = "Deutscher Name";
            xlWorkSheet.Cells[3, 8] = "Maßnahme";
            xlWorkSheet.Cells[3, 9] = "Preis in €";
            
            int i = 0; int a = 4;

            while (i < _Los.Count)
            {
                string[] l_tmp = _Los[i].Split('|');
                //Console.WriteLine(l_tmp[0] + " | " + l_tmp.Length + " | " + _Los.Count);
                if (l_tmp.Count() > 8)
                {
                    string mas = "";
                    if (l_tmp[7].Contains("True")) mas = "Baum nicht vorhanden! ";
                    if (l_tmp[8].Contains("True")) mas = mas + "Totholzbeseitigung ";
                    if (l_tmp[9].Contains("True")) mas = mas + "Stammaustrieb entfernen ";
                    if (l_tmp[10].Contains("True")) mas = mas + "Lichte Höhe 4,5m ";
                    if (l_tmp[11].Contains("True")) mas = mas + "Lichtraumprofil ";
                    if (l_tmp[12].Contains("True")) mas = mas + "Baumfällung ";
                    if (l_tmp[13].Contains("True")) mas = mas + "Krone einkürzen/verschneiden ";
                    if (l_tmp[16].Contains("True")) mas = mas + "Bemerkungen ";
                    for (int y = 0; y != 7; y++)
                    {
                        xlWorkSheet.Cells[a, 1 + y] = l_tmp[y];
                    }
                    xlWorkSheet.Cells[a, 8] = mas;
                    xlWorkSheet.Cells[a, 9] = l_tmp[17].Substring(l_tmp[17].IndexOf(":") + 1, l_tmp[17].Length - l_tmp[17].IndexOf(":") - 1);
                    a++;
                }
                else
                {
                    xlWorkSheet.Cells[a, 7] = "Alles o.K.";
                    xlWorkSheet.Cells[a, 8] = "0";
                    for (int z = 0; z != 8; z++)
                    {
                        //Console.WriteLine("Wert von z: " + z +" wert von a: " + a + " l_tmp: " + l_tmp.Length + " der Wert: " + l_tmp[0] );
                        xlWorkSheet.Cells[a, 1 + z] = l_tmp[z];
                    }
                    a++;
                }
                i++;
            }
            xlWorkSheet.Columns.AutoFit();
            string name_Los = "";
            if (cBox_Los.SelectedIndex != -1)
                name_Los = cBox_Los.SelectedItem.ToString();
            else
                { name_Los = T_Dateiname.Substring(T_Dateiname.IndexOf("_") + 1); name_Los = name_Los.Substring(0, name_Los.IndexOf("_")); }

            xlWorkSheet.Cells[1, 1] = "Ritter & Wagner GbR - Baumliste " + name_Los;
            xlWorkSheet.Columns.ClearFormats();

            xlWorkBook.SaveAs(aVz + "\\" + name_Los, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Datei erstellt: " + name_Los + ".xls");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cBox_Los.SelectedIndex >= 0)
            {
                if (cBox_Los.SelectedItem.ToString().Contains("Los 1"))
                    Erstelle_Excel(L_Baeume_Los1);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 2"))
                    Erstelle_Excel(L_Baeume_Los2);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 3"))
                    Erstelle_Excel(L_Baeume_Los3);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 4"))
                    Erstelle_Excel(L_Baeume_Los4);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 5"))
                    Erstelle_Excel(L_Baeume_Los5);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 6"))
                    Erstelle_Excel(L_Baeume_Los6);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 7"))
                    Erstelle_Excel(L_Baeume_Los7);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 8"))
                    Erstelle_Excel(L_Baeume_Los8);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 9"))
                    Erstelle_Excel(L_Baeume_Los9);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 10"))
                    Erstelle_Excel(L_Baeume_Los10);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 11"))
                    Erstelle_Excel(L_Baeume_Los11);
                else if (cBox_Los.SelectedItem.ToString().Contains("Los 12"))
                    Erstelle_Excel(L_Baeume_Los12);
            }
            else
            {
                Erstelle_Excel(L_Temp);  
            }
                
        }

        private void chkB_5_Lichtraumprofil_Checked(object sender, RoutedEventArgs e)
        {
            cBox_Preis.SelectedIndex = 2;
        }

        private void chkB_2_Totholzbeseitigung_Checked(object sender, RoutedEventArgs e)
        {
            cBox_Preis.SelectedIndex = 5;
        }

        private void chkB_3_Stammaustrieb_Checked(object sender, RoutedEventArgs e)
        {
            cBox_Preis.SelectedIndex = 0;
        }
    }
}
