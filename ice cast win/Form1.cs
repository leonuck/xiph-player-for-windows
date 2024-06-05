using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;
using NAudio.Wave;
using Newtonsoft.Json;
using Microsoft.Win32;
using NAudio.Utils;

namespace ice_cast_win
{
    public partial class Form1 : Form
    {
        private WaveOutEvent? outputDevice;
        private MediaFoundationReader? mediaReader;
        private const string FavoritesFilePath = "favorites.json";
        private List<RadioStation> allStations = new List<RadioStation>();

        public Form1()
        {
            InitializeComponent();
            ApplyTheme(); // Aplica o tema aos controles
            UseImmersiveDarkMode(); // Aplica o tema à barra de título
        }

        private void ApplyTheme()
        {
            if (IsDarkTheme())
            {
                this.BackColor = Color.FromArgb(45, 45, 48);
                this.ForeColor = Color.White;

                foreach (Control control in this.Controls)
                {
                    if (control is CustomScrollableListBox customListBox)
                    {
                        customListBox.BackColor = Color.FromArgb(30, 30, 30);
                        customListBox.ForeColor = Color.White;
                    }
                    else if (control is Button button)
                    {
                        button.BackColor = Color.FromArgb(0, 122, 204); // Azul do Windows
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.FromArgb(63, 63, 70); // Cinza escuro
                        button.FlatAppearance.BorderSize = 1;
                    }
                    else if (control is TextBox textBox)
                    {
                        textBox.BackColor = Color.FromArgb(30, 30, 30);
                        textBox.ForeColor = Color.White;
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
        }

        private bool IsDarkTheme()
        {
            var tKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (tKey != null)
            {
                var theme = tKey.GetValue("AppsUseLightTheme");
                if (theme != null && (int)theme == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadRadios();
            LoadFavorites();
        }

        private async Task LoadRadios()
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("http://dir.xiph.org/yp.xml");
                XDocument xmlDoc = XDocument.Parse(response);

                foreach (var station in xmlDoc.Descendants("entry"))
                {
                    string? listenUrl = station.Element("listen_url")?.Value;
                    string? stationName = station.Element("server_name")?.Value;

                    if (!string.IsNullOrEmpty(listenUrl) && !string.IsNullOrEmpty(stationName))
                    {
                        var radioStation = new RadioStation { Name = stationName, Url = listenUrl };
                        listBoxRadios.Items.Add(radioStation);
                        allStations.Add(radioStation);
                    }
                }
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (listBoxRadios.SelectedItem != null)
            {
                RadioStation selectedStation = (RadioStation)listBoxRadios.SelectedItem;
                PlayRadio(selectedStation.Url);
            }
            else if (listBoxFavorites.SelectedItem != null)
            {
                RadioStation selectedStation = (RadioStation)listBoxFavorites.SelectedItem;
                PlayRadio(selectedStation.Url);
            }
        }

        private void buttonAddToFavorites_Click(object sender, EventArgs e)
        {
            if (listBoxRadios.SelectedItem != null)
            {
                RadioStation selectedStation = (RadioStation)listBoxRadios.SelectedItem;
                if (!listBoxFavorites.Items.Contains(selectedStation))
                {
                    listBoxFavorites.Items.Add(selectedStation);
                    SaveFavorites();
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.ToLower();
            listBoxRadios.Items.Clear();

            foreach (var station in allStations)
            {
                if (station.Name.ToLower().Contains(searchText))
                {
                    listBoxRadios.Items.Add(station);
                }
            }
        }

        private void PlayRadio(string url)
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                mediaReader?.Dispose();
            }

            outputDevice = new WaveOutEvent();
            mediaReader = new MediaFoundationReader(url);
            outputDevice.Init(mediaReader);
            outputDevice.Play();
        }

        private void SaveFavorites()
        {
            var favorites = new List<RadioStation>();
            foreach (RadioStation station in listBoxFavorites.Items)
            {
                favorites.Add(station);
            }

            string json = JsonConvert.SerializeObject(favorites, Formatting.Indented);
            File.WriteAllText(FavoritesFilePath, json);
        }

        private void LoadFavorites()
        {
            if (File.Exists(FavoritesFilePath))
            {
                string json = File.ReadAllText(FavoritesFilePath);
                var favorites = JsonConvert.DeserializeObject<List<RadioStation>>(json);
                if (favorites != null)
                {
                    listBoxFavorites.Items.Clear();
                    foreach (var station in favorites)
                    {
                        listBoxFavorites.Items.Add(station);
                    }
                }
            }
        }

        private class RadioStation
        {
            public string Name { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;

            public override string ToString()
            {
                return Name;
            }
        }

        private void UseImmersiveDarkMode()
        {
            var attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
            if (Environment.OSVersion.Version.Build >= 19041)
            {
                attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE;
            }

            int useImmersiveDarkMode = 1;
            NativeMethods.DwmSetWindowAttribute(this.Handle, attribute, ref useImmersiveDarkMode, sizeof(int));
        }
    }
}
