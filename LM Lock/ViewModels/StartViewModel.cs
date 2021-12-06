using LM_Lock.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LM_Lock.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public StartWindow StartWindows { get; set; }

        public RelayCommand LoadedCommand { get; set; }
        public RelayCommand MouseDownCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand GithubCommand { get; }
        public RelayCommand EmailCommand { get; }
        public RelayCommand FileCommand { get; set; }
        public RelayCommand StartCommand { get; set; }
        public RelayCommand PausePlayCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ObservableCollection<string> Extensions { get; set; }

        private string _Extension;
        public string Extension { get { return _Extension; } set { _Extension = value; OnPropertyChanged(); } }

        public Stopwatch watch = Stopwatch.StartNew();

        string text = string.Empty;

        public static object obj = new object();

        string path = string.Empty;
        bool hasEncrypt = false;
        bool hasDecrypt = false;
        double p = 0.0;
        double pMax = 0.0;
        double pMin = 0.0;
        long length1 = 0;
        long length2 = 0;
        int pauseclick = 0;
        int selectedIndex = 0;
        ComboBox combobox = new ComboBox();
        string extension = string.Empty;
        int count = 100;
        int currentcount = 0;
        string extensiontext = string.Empty;
 
        int count2 = 50;

        private double _pv;
        public double Pv { get { return _pv; } set { _pv = value; OnPropertyChanged(); } }

        int threadcount = 1001;

        bool hasenable = false;

        public StartViewModel()
        {
      

            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += Timer_Tick;
            timer.Interval =new TimeSpan(0, 0, 0, 0,50);

            DispatcherTimer timer2 = new DispatcherTimer();

            timer2.Tick += Timer2_Tick;
            timer2.Interval = new TimeSpan(0, 0, 0, 0, 50);


            Extensions = new ObservableCollection<string>();


            Thread[] threadarray = new Thread[threadcount];

            for (int i = 0; i < threadcount; i++)
            {
                   

                threadarray[i] = new Thread(() =>
                {
                  //  var selected = StartWindows.ExtensionComboBox.SelectedItem.ToString() as string;

                    EncryptandDecrypt(path, extension, watch, selectedIndex);


                });
            }

            timer.Start();

            LoadedCommand = new RelayCommand((sender) =>
            {
               

                StartWindows.EncryptRadioButton.Checked += EncryptRadioButton_Checked;
                StartWindows.DecryptRadioButton.Checked += DecryptRadioButton_Checked;


                Extension = ".mp4";
                Extensions.Add(Extension.ToLower());
                Extension = ".WEBM";
                Extensions.Add(Extension.ToLower());
                Extension = ".mp2";
                Extensions.Add(Extension.ToLower());
                Extension = ".mkv";
                Extensions.Add(Extension.ToLower());
                Extension = ".MOV";
                Extensions.Add(Extension.ToLower());
                Extension = ".AVI";
                Extensions.Add(Extension.ToLower());
                Extension = ".WMV";
                Extensions.Add(Extension.ToLower());
                Extension = ".FLV";
                Extensions.Add(Extension.ToLower());

                Extension = ".MP3";
                Extensions.Add(Extension.ToLower());
                Extension = ".WAV";
                Extensions.Add(Extension.ToLower());
                Extension = ".WMA";
                Extensions.Add(Extension.ToLower());
                Extension = ".AAC";
                Extensions.Add(Extension.ToLower());
                Extension = ".PCM";
                Extensions.Add(Extension.ToLower());
                Extension = ".AIFF";
                Extensions.Add(Extension.ToLower());
                Extension = ".OGG";
                Extensions.Add(Extension.ToLower());
                Extension = ".ALAC";
                Extensions.Add(Extension.ToLower());

                Extension = ".JPEG";
                Extensions.Add(Extension.ToLower());
                Extension = ".JPG";
                Extensions.Add(Extension.ToLower());
                Extension = ".PNG";
                Extensions.Add(Extension.ToLower());
                Extension = ".GIF";
                Extensions.Add(Extension.ToLower());
                Extension = ".TIFF";
                Extensions.Add(Extension.ToLower());
                Extension = ".RAW";
                Extensions.Add(Extension.ToLower());
                Extension = ".BMP";
                Extensions.Add(Extension.ToLower());
                Extension = ".ICO";
                Extensions.Add(Extension.ToLower());

                Extension = ".rar";
                Extensions.Add(Extension.ToLower());
                Extension = ".iso";
                Extensions.Add(Extension.ToLower());
                Extension = ".tar";
                Extensions.Add(Extension.ToLower());
                Extension = ".gz";
                Extensions.Add(Extension.ToLower());
                Extension = ".7z";
                Extensions.Add(Extension.ToLower());
                Extension = ".zip";
                Extensions.Add(Extension.ToLower());
                Extension = ".Z";
                Extensions.Add(Extension.ToLower());
                Extension = ".7-Zip";
                Extensions.Add(Extension.ToLower());

          


                MessageBox.Show($" This locking program is free. \n https://github.com/MehrajLatifli/LM-Lock-in-MVVM", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            });


            MouseDownCommand = new RelayCommand((sender) =>
            {
                StartWindows.MouseDown += StartWindows_MouseDown;


            });



            CloseCommand = new RelayCommand((sender) =>
            {
                StartWindows.Close();

                Environment.Exit(0);

            });

            GithubCommand = new RelayCommand((sender) =>
            {
                using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(@"Software\Clients\StartMenuInternet"))
                {
                    var first = userChoiceKey?.GetSubKeyNames().FirstOrDefault();
                    if (userChoiceKey == null || first == null) return;
                    var reg = userChoiceKey.OpenSubKey(first + @"\shell\open\command");
                    var prog = (string)reg?.GetValue(null);
                    if (prog == null) return;
                    Process.Start(new ProcessStartInfo(prog, "https://github.com/MehrajLatifli"));
                }

            });

            EmailCommand = new RelayCommand((sender) =>
            {
                MessageBox.Show($"This locking program is free. \n meracletifli@gmail.com", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            });

            FileCommand = new RelayCommand((sender) =>
            {
             

                OpenFileDialog openFile = new OpenFileDialog();

                //openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (StartWindows.EncryptRadioButton.IsChecked == true)
                {


                    openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                   
                    openFile.Filter = "mp4 Files(*.mp4)|*.mp4|webm Files(*.webm)|*.webm|mp2 Files(*.mp2)|*.mp2|mkv Files(*.mkv)|*.mkv|mov Files(*.mov)|*.mov|avi Files(*.avi)|*.avi|wmv Files(*.wmv)|*.wmv|flv Files(*.flv)|*.flv|" +
                    "mp3 Files(*.mp3)|*.mp3|wav Files(*.wav)|*.wav|wma Files(*.wma)|*.wma|aac Files(*.aac)|*.aac|pcm Files(*.pcm)|*.pcm|aiff Files(*.aiff)|*.aiff|ogg Files(*.ogg)|*.ogg|alac Files(*.alac)|*.alac|" +
                    "jpeg Files(*.jpeg)|*.jpeg|jpg Files(*.jpg)|*.jpg|png Files(*.png)|*.png|gif Files(*.gif)|*.gif|tiff Files(*.tiff)|*.tiff|raw Files(*.raw)|*.raw|bmp Files(*.bmp)|*.bmp|ico Files(*.ico)|*.ico|" +
                    "rar Files(*.rar)|*.rar|iso Files(*.iso)|*.iso|gz Files(*.gz)|*.gz|7z Files(*.7z)|*.7z|zip Files(*.zip)|*.zip|tar Files(*.tar)|*.tar|Z Files(*.Z)|*.Z|7-Zip Files(*.7-Zip)|*.7-Zip|"+
                    "All files(*.*) | *.*";


                    if (openFile.ShowDialog() == true)
                    {
                        StartWindows.PathTextBox.Text = openFile.FileName;

                        path = StartWindows.PathTextBox.Text;

                        FileInfo fi = new FileInfo(StartWindows.PathTextBox.Text);

                        if (fi.Length <= 500000000)
                        {
                            extension = fi.Extension;
                        }

                        else
                        {
                            MessageBox.Show($" Max limit = 476.837158203125 MB (in binary) \n Max limit = 500 MB (in decimal)");

                            StartWindows.PathTextBox.Text = "Path";
                        }

                    }
                }

                if (StartWindows.DecryptRadioButton.IsChecked == true)
                {
               
                    openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    openFile.Filter = ".Lock files (*.Lock)|*.Lock";


                    if (openFile.ShowDialog() == true)
                    {
                        StartWindows.PathTextBox.Text = openFile.FileName;

                        path = StartWindows.PathTextBox.Text;

                        FileInfo fi = new FileInfo(StartWindows.PathTextBox.Text);

                        if (fi.Length <= 500000000)
                        {
                            extension = fi.Extension;
                        }

                        else
                        {
                            MessageBox.Show($" Max limit = 476.837158203125 MB (in binary) \n Max limit = 500 MB (in decimal)");

                            StartWindows.PathTextBox.Text = "Path";
                        }

                    }
                }

  

                else if (StartWindows.EncryptRadioButton.IsChecked == false && StartWindows.DecryptRadioButton.IsChecked == false)
                {
                    MessageBox.Show("Select Encrypt or Dencrypt.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            });

            StartCommand = new RelayCommand((sender) =>
            {

                                    timer.Start();
                                    timer2.Start();

                try
                {
                 

                    if (StartWindows.DecryptRadioButton.IsChecked == true)
                    {

                        if (StartWindows.ExtensionComboBox.SelectedIndex != -1)
                        {

                            if (StartWindows.PathTextBox.Text != "Path")
                            {

                                if (hasEncrypt == true || hasDecrypt == true)
                                {
                                    if (StartWindows.PathTextBox.Text.Contains(StartWindows.ExtensionComboBox.SelectedItem.ToString()))
                                    {

                                        threadcount--;

                                        threadarray.ElementAt(threadcount).Start();

                                    }

                                    else
                                    {
                                        MessageBox.Show("The selected format does not support the selected file format.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }

                                }
                            }
                            else
                            {

                                MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }

                        if (StartWindows.ExtensionComboBox.SelectedIndex == -1)
                        {
                            MessageBox.Show("Choose the format of the file (extension) to be decrypted.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }


                    }

                    if (StartWindows.EncryptRadioButton.IsChecked == true)
                    {



                        if (StartWindows.PathTextBox.Text != "Path")
                        {

                            if (hasEncrypt == true || hasDecrypt == true)
                            {

                                    if (!StartWindows.PathTextBox.Text.Contains(".Lock"))
                                    {

                                        threadcount--;


                                        threadarray.ElementAt(threadcount).Start();

                                    }

                                    else
                                    {

                                        MessageBox.Show("This format does not support for encryption.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }


                                }
                        }
                        else
                        {

                            MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        


                    }

                    if (StartWindows.EncryptRadioButton.IsChecked == false && StartWindows.DecryptRadioButton.IsChecked == false)
                    {
                        MessageBox.Show("Choose the operation (Encrypt or Decrypt).", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception)
                {

                }

            },
            (check) =>
            {

                if (hasenable==false)
                {
                    return true;
                }

                return false;


            });

            PausePlayCommand = new RelayCommand((sender) =>
            {
                hasenable = true;
                try
                {

                    if (!string.IsNullOrEmpty(StartWindows.PathTextBox.Text) || StartWindows.PathTextBox.Text != "Path")
                    {
              

                        pauseclick++;

                        timer.Start();

                        if (pauseclick % 2 != 0)
                        {
                            StartWindows.PausePlayButton.Content = "Play";
                            threadarray.ElementAt(threadcount).Suspend();
                        }

                        else
                        {
                            StartWindows.PausePlayButton.Content = "Pause";
                            threadarray.ElementAt(threadcount).Resume();
                        }
                    }

                    else
                    {
                     
                        MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                catch (Exception)
                {


                }

                timer.Stop();
            });

            CancelCommand = new RelayCommand((sender) =>
            {
             

                timer.Start();

                try
                {



                    if (!string.IsNullOrEmpty(StartWindows.PathTextBox.Text) || StartWindows.PathTextBox.Text != "Path")
                    {
                       

                        for (int j = currentcount; j >= 0; j--)
                        {
                            Thread.Sleep(50);
                            Pv = j;
                        }


                        threadarray.ElementAt(threadcount).Abort();

                    
                            FileInfo fi = new FileInfo(StartWindows.PathTextBox.Text);

                        if (StartWindows.EncryptRadioButton.IsChecked == true)
                        {

                            

                            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Encrypt File_{Path.GetFileNameWithoutExtension(StartWindows.PathTextBox.Text)} {fi.Extension}.Lock"))
                            {

                                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Encrypt File_{Path.GetFileNameWithoutExtension(StartWindows.PathTextBox.Text)} {fi.Extension}.Lock");
                            }
                        }


                        if (StartWindows.DecryptRadioButton.IsChecked == true)
                        {

                            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Decrypt File_{Path.GetFileNameWithoutExtension(fi.Name)}{StartWindows.ExtensionComboBox.SelectedItem.ToString()}"))
                            {

                                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Decrypt File_{Path.GetFileNameWithoutExtension(fi.Name)}{StartWindows.ExtensionComboBox.SelectedItem.ToString()}");
                            }

                            MessageBox.Show($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Decrypt File_{Path.GetFileNameWithoutExtension(fi.Name)}{StartWindows.ExtensionComboBox.SelectedItem.ToString()}");
                        }
                    }

                    else
                    {
                       
                        MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                catch (Exception)
                {

                }

                timer.Stop();



            });


        }

        private void DecryptRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (StartWindows.DecryptRadioButton.IsChecked == true)
            {
                StartWindows.ExtensionComboBox.IsEnabled = true;

                StartWindows.PathTextBox.Text = "Path";

                StartWindows.ExtensionComboBox.SelectedIndex = -1;
            }

         
        }

        private void EncryptRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (StartWindows.EncryptRadioButton.IsChecked == true)
            {
                StartWindows.ExtensionComboBox.IsEnabled = false;

                StartWindows.PathTextBox.Text = "Path";


                StartWindows.ExtensionComboBox.SelectedIndex = -1;
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            StartWindows.ExtensionComboBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (StartWindows.ExtensionComboBox.SelectedItem != null)
                {

                    extensiontext = StartWindows.ExtensionComboBox.SelectedItem.ToString();
                    combobox = StartWindows.ExtensionComboBox;
                    selectedIndex = StartWindows.ExtensionComboBox.SelectedIndex;
                }

            }));
        }

        private void StartWindows_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                StartWindows.DragMove();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StartWindows.Dispatcher.BeginInvoke(new Action(() =>
            {

                path = StartWindows.PathTextBox.Text;
                hasEncrypt = (bool)StartWindows.EncryptRadioButton.IsChecked;
                hasDecrypt = (bool)StartWindows.DecryptRadioButton.IsChecked;
                p = StartWindows.ProcessControlProgressBar.Value;
                pMax = StartWindows.ProcessControlProgressBar.Maximum;
                pMin = StartWindows.ProcessControlProgressBar.Minimum;


                if (StartWindows.DecryptRadioButton.IsChecked == true)
                {
                    StartWindows.ExtensionComboBox.IsEnabled = true;
                }

                if (StartWindows.EncryptRadioButton.IsChecked == true)
                {

                    StartWindows.ExtensionComboBox.IsEnabled = false;
                }


            }));
        }



        private async void EncryptandDecrypt(string path, string extension, Stopwatch watch, int selectedIndex)
        {

            lock (obj)
            {
   

                watch = new Stopwatch();

                FileInfo fi = new FileInfo($"{path}");
                FileInfo fi2 = new FileInfo($"{path}");

                        hasenable = true;

                if (hasEncrypt == true)
                {
                    if (path != "Path")
                    {

                        MessageBox.Show($"You have enabled encryption.");

                        MessageBox.Show($"{path}");


                        for (int i = 0; i < count; i++)
                        {
                            Thread.Sleep(count2);
                            if (i == count - 1)
                            {

                                for (int j = 0; j < 101; j++)
                                {
                                    Thread.Sleep(500);


                                    Pv = j;




                                    if (Pv >= 35)
                                    {


                                        EncryptFile(watch, path, $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Encrypt File_{Path.GetFileNameWithoutExtension(fi.Name)} {fi.Extension}.Lock");

                                       
                                    }

                                    if (Pv == 100)
                                    {
                                        MessageBox.Show($"Encryption complete. \n Seconds spent on encryption: {watch.Elapsed.TotalSeconds}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                                        hasenable = false;
                                    }

                                    if(Pv==0)
                                    {

                                        hasenable = false;
                                    }


                                }
                            }
                        }
                       




                    }
                    if (path == "Path")
                    {

                        MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }

                if (hasDecrypt == true)
                {

                    if (selectedIndex != -1)
                    {
                        if (path != "Path")
                        {
                            MessageBox.Show($"You have enabled decryption.");

                            MessageBox.Show($"{path}");

                  


                            for (int i = 0; i < count; i++)
                            {
                                Thread.Sleep(count2);
                                if (i == count - 1)
                                {

                                    for (int j = 0; j < 101; j++)
                                    {
                                        Thread.Sleep(500);


                                        Pv = j;




                                        if (Pv >= 35)
                                        {

                                            DecryptFile(watch, $"{path}", $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Decrypt File_{Path.GetFileNameWithoutExtension(path)}{extensiontext}");
                                        }

                                        if (Pv == 100)
                                        {
                                            MessageBox.Show($"Decryption complete. \n Seconds spent on decryption: {watch.Elapsed.TotalSeconds}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                                            hasenable = false;
                                        }

                                        if (Pv == 0)
                                        {

                                            hasenable = false;
                                        }


                                    }
                                }
                            }

                         
                        }
                        if (path == "Path")
                        {

                            MessageBox.Show("Select Path.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                    }




                }

            }
        }


        private static void EncryptFile(Stopwatch watch, string inputFile, string outputFile)
        {
            try
            {

           
               watch.Start();

                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();

                watch.Stop();

                

            }
            catch (Exception)
            {

 
            }



        }


        private static void DecryptFile(Stopwatch watch, string inputFile, string outputFile)
        {
            try
            {

           
           
                watch.Start();

                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

                watch.Stop();
            }
            catch (Exception)
            {


            }

        }
    }
}
