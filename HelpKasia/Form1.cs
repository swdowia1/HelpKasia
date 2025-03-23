using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace HelpKasia
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            trayMenu = new ContextMenuStrip();
            CreateButtons();

            // Tworzenie menu kontekstowego dla zasobnika

            trayMenu.Items.Add("Otwórz", null, OnShowClicked);
            trayMenu.Items.Add("Wyjście", null, close_click);

            // Tworzenie ikony zasobnika
            trayIcon = new NotifyIcon();
            trayIcon.Text = "Moja aplikacja";
            trayIcon.Icon = SystemIcons.Application;
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.DoubleClick += OnShowClicked;

            // Obsługa zdarzenia zamykania
            this.Resize += OnResize;

        }
        private void OnShowClicked(object sender, EventArgs e)
        {
            this.Show(); // Pokazuje okno
            this.WindowState = FormWindowState.Normal;
            trayIcon.Visible = false; // Ukrywa ikonę w trayu
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); // Ukrywa okno
                trayIcon.Visible = true; // Pokazuje ikonę w trayu
            }
        }
        private void CreateButtons()
        {
            int buttonWidth = 100;
            int buttonHeight = 30;
            int padding = 10; // Odstęp między przyciskami
            int buttonsPerRow = panel1.Width / (buttonWidth + padding);
            string ff = Environment.CurrentDirectory;
            string[] pliki = Directory.GetFiles(Environment.CurrentDirectory, "*.txt");

            int lastI = 0;
            // Loop to create buttons
            for (int i = 0; i < pliki.Length; i++)
            {
                lastI = i;
                Button newButton = new Button();
                var filename = Path.GetFileName(pliki[i]);

                newButton.Text = filename;
                newButton.Name = "btnDynamic" + i;
                newButton.Width = buttonWidth;
                newButton.Height = buttonHeight;

                // Obliczenie pozycji wiersza i kolumny
                int row = i / buttonsPerRow; // Określenie rzędu
                int col = i % buttonsPerRow; // Określenie kolumny

                newButton.Location = new System.Drawing.Point(col * (buttonWidth + padding), row * (buttonHeight + padding));

                newButton.Click += help_Click;
                trayMenu.Items.Add(filename, null, help_Click);
                // Add the button to the panel
                panel1.Controls.Add(newButton);
            }
            lastI++;
            //zamykanie
            Button newButton1 = new Button();


            newButton1.Text = "Zamknij";
            newButton1.Name = "btnDynamic";
            newButton1.Width = buttonWidth;
            newButton1.Height = buttonHeight;

            // Obliczenie pozycji wiersza i kolumny
            int row1 = lastI / buttonsPerRow; // Określenie rzędu
            int col1 = lastI % buttonsPerRow; // Określenie kolumny

            newButton1.Location = new System.Drawing.Point(col1 * (buttonWidth + padding), row1 * (buttonHeight + padding));

            newButton1.Click += close_click;

            // Add the button to the panel
            panel1.Controls.Add(newButton1);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;


        }
        public void PopUp(string tresc, int wielkosc = 100)
        {
            PopupNotifier popup = new PopupNotifier();
            //if (tresc.Length > 30)
            //{
            //    tresc = tresc.Substring(tresc.Length - wielkosc);
            //}
            popup.TitleText = "Informacja";
            popup.ContentText = tresc;
            popup.Popup(); // show
        }
        private void help_Click(object sender, EventArgs e)
        {
            var b = sender as Button;

            string popText = "";
            if (sender is Button)
            {
                Button button = (Button)sender;
                popText = button.Text;
            }
            else if (sender is ToolStripItem)
            {
                ToolStripItem textBox = (ToolStripItem)sender;
                popText = textBox.Text;
            }


            string zaw = File.ReadAllText(popText);

            PopUp(popText);
            Clipboard.SetText(zaw);
            if (chkZwin.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
                PopUp(popText + " jest w tray");
            }
            else
                PopUp(popText);
            //this.WindowState = FormWindowState.Minimized;
            //

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void close_click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Czy na pewno zamknac", "Zamknac", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                return;
            }


        }
    }
}
