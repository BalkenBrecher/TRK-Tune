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
        //  Globale Bool Variablen
        //
        bool b_Neue_Liste = true;

        string aVz = Directory.GetCurrentDirectory();
        string aktLos = "";

        int aILB = -1;  //  Aktueller Index der Baumliste

        List<string> L_Listen       = new List<string>();
        List<string> L_Temp         = new List<string>();
        List<string> L_Baeume_Los1  = new List<string>(); List<string> L_Baeume_Los1_Neu = new List<string>();
        List<string> L_Baeume_Los2  = new List<string>(); List<string> L_Baeume_Los2_Neu = new List<string>();
        List<string> L_Baeume_Los3  = new List<string>();
        List<string> L_Baeume_Los4  = new List<string>();
        List<string> L_Baeume_Los5  = new List<string>();
        List<string> L_Baeume_Los6  = new List<string>();
        List<string> L_Baeume_Los7  = new List<string>();
        List<string> L_Baeume_Los8  = new List<string>();
        List<string> L_Baeume_Los9  = new List<string>();

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
        }

        private void Waehle_Los(object sender, SelectionChangedEventArgs e)
        {
            if(cBox_Los.SelectedIndex != -1)
            { 
                string text = cBox_Los.SelectedItem.ToString(); aktLos = text.Substring(text.LastIndexOf(':') + 1);
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
                GroupBox_Baum.IsEnabled = true; string Baum = cBox_Baeume.SelectedItem.ToString();
                int tmp_Baum_Zaehler = 0; List<string> tmp_Baumliste = new List<string>();
                if (aktLos.Contains("Los 1")) { tmp_Baum_Zaehler = L_Baeume_Los1.Count; tmp_Baumliste = L_Baeume_Los1; }
                else if (aktLos.Contains("Los 2")) { tmp_Baum_Zaehler = L_Baeume_Los2.Count; tmp_Baumliste = L_Baeume_Los2; }
                else if (aktLos.Contains("Los 3")) { tmp_Baum_Zaehler = L_Baeume_Los3.Count; tmp_Baumliste = L_Baeume_Los3; }
                else if (aktLos.Contains("Los 4")) { tmp_Baum_Zaehler = L_Baeume_Los4.Count; tmp_Baumliste = L_Baeume_Los4; }
                else if (aktLos.Contains("Los 5")) { tmp_Baum_Zaehler = L_Baeume_Los5.Count; tmp_Baumliste = L_Baeume_Los5; }
                else if (aktLos.Contains("Los 6")) { tmp_Baum_Zaehler = L_Baeume_Los6.Count; tmp_Baumliste = L_Baeume_Los6; }
                else if (aktLos.Contains("Los 7")) { tmp_Baum_Zaehler = L_Baeume_Los7.Count; tmp_Baumliste = L_Baeume_Los7; }
                else if (aktLos.Contains("Los 8")) { tmp_Baum_Zaehler = L_Baeume_Los8.Count; tmp_Baumliste = L_Baeume_Los8; }
                else { tmp_Baum_Zaehler = L_Baeume_Los9.Count; tmp_Baumliste = L_Baeume_Los9; }

                for (int i = 0; i < tmp_Baum_Zaehler; i++) //(string s in L_Baeume_Los2)
                {
                    string s = tmp_Baumliste[i];
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
                        if(Baumdaten.Count() > 10)
                        {
                            if (Baumdaten[6].Contains("True")) chkB_1_Baum_Nicht_Da.IsChecked = true; else chkB_1_Baum_Nicht_Da.IsChecked = false;
                            if (Baumdaten[7].Contains("True")) chkB_2_Totholzbeseitigung.IsChecked = true; else chkB_2_Totholzbeseitigung.IsChecked = false;
                            if (Baumdaten[8].Contains("True")) chkB_3_Lichte_Hoehe_25.IsChecked = true; else chkB_3_Lichte_Hoehe_25.IsChecked = false;
                            if (Baumdaten[9].Contains("True")) chkB_4_Lichte_Hoehe_45.IsChecked = true; else chkB_4_Lichte_Hoehe_45.IsChecked = false;
                            if (Baumdaten[10].Contains("True")) chkB_5_Lichtraumprofil.IsChecked = true; else chkB_5_Lichtraumprofil.IsChecked = false;
                            if (Baumdaten[11].Contains("True")) chkB_6_Baumfaellung.IsChecked = true; else chkB_6_Baumfaellung.IsChecked = false;
                            if (Baumdaten[12].Contains("True")) chkB_7_Krone_Kuerzen.IsChecked = true; else chkB_7_Krone_Kuerzen.IsChecked = false;
                            if (Baumdaten[13].Contains("True")) chkB_8_Leiter_Benoetigt.IsChecked = true; else chkB_8_Leiter_Benoetigt.IsChecked = false;
                            if (Baumdaten[14].Contains("True")) chkB_9_Hebebuehne_Benoetigt.IsChecked = true; else chkB_9_Hebebuehne_Benoetigt.IsChecked = false;
                        }
                        else
                        {
                            chkB_1_Baum_Nicht_Da.IsChecked = false; chkB_2_Totholzbeseitigung.IsChecked = false; chkB_3_Lichte_Hoehe_25.IsChecked = false; chkB_4_Lichte_Hoehe_45.IsChecked = false;
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
                chkB_3_Lichte_Hoehe_25.IsChecked = false; chkB_3_Lichte_Hoehe_25.IsEnabled = false;
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
                chkB_3_Lichte_Hoehe_25.IsChecked = false; chkB_3_Lichte_Hoehe_25.IsEnabled = true;
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
            if(_Liste.Length <= 0)
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
            L_Baeume_Los2_Neu = L_Baeume_Los2;
            if(L_Baeume_Los2_Neu.Count > 0) {
                string tmp = L_Baeume_Los2_Neu[aILB];
                L_Baeume_Los2_Neu[aILB] = tmp + Erstelle_Neue_Eintrag();
            }

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
                        using (TextWriter tw = new StreamWriter("Baumkontrolle_" + aktLos + "_" + lbl_Aktuelles_Datum.Content + ".txt"))
                        {
                            foreach (String s in L_Baeume_Los2_Neu)
                                tw.WriteLine(s);
                        }
                        MessageBox.Show("Die Liste wurde erfolgreich angelegt.\nSie ist unter folgendem Pfad zu finden:\n\n" + aVz);
                    }
                }
                catch (Exception e_Speichere_Datei)
                {
                    MessageBox.Show("Fehler beim speichern: \n\n" + e_Speichere_Datei);
                }   //  Ende catch
            }   //  Ende ob "Ja" gewählt wurde   
        }   //  Ende Mehtode Liste speichern

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
                            L_Temp = Lese_Baeume(theDialog.FileName);
                            Pruefe_Lokale_Listen(theDialog.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler! Konnte die Datei nicht lesen. Eine genauere Fehlerbeschreibung:\n\n" + ex.Message);
                }
            }
        }   //  Ende Methode 


        private void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                lbl_Aktuelle_Uhrzeit.Content = DateTime.Now.ToString("hh:mm:ss");
            });

            // lbl_Aktuelle_Uhrzeit.Content = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
