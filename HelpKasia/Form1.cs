using System;
using System.IO;
using System.Windows.Forms;

namespace HelpKasia
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            CreateButtons();
            label1.Text = "";


        }

        private void CreateButtons()
        {
            string ff = Environment.CurrentDirectory;
            string[] pliki = Directory.GetFiles(Environment.CurrentDirectory, "*.txt");
            // Number of buttons to create (for example, 5 buttons)
            int numberOfButtons = 5;

            // Loop to create buttons
            for (int i = 0; i < pliki.Length; i++)
            {
                // Create a new Button instance
                Button newButton = new Button();
                var filename = Path.GetFileName(pliki[i]);
                // Set button properties
                newButton.Text = filename;
                newButton.Name = "btnDynamic" + i; // Button name
                newButton.Width = 100; // Button width
                newButton.Height = 30; // Button height
                newButton.Location = new System.Drawing.Point(i * 100, 0);
                // Attach a common event handler for button clicks
                newButton.Click += help_Click;

                // Add the button to the panel
                panel1.Controls.Add(newButton);
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;


        }

        private void help_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            label1.Text = "wybrano " + b.Text;
            if (b != null)
            {

                string zaw = File.ReadAllText(b.Text);
                Clipboard.SetText(zaw);
                if (chkZwin.Checked)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                //
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
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
