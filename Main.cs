using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NetrayaDashboard
{
    public partial class Main : MaterialForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cmbRoom.SelectedIndex = -1;
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRoom.Text == "SMT-MAINROOM")
            {
                FormMains formMains = new FormMains();
                this.Hide();
                formMains.Show();
            }
            else if (cmbRoom.Text == "SMT-SA")
            {
                FormMainNine form = new FormMainNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Show();

            }
            else if (cmbRoom.Text == "SMT-DIPPING")
            {
                FormMainNine form = new FormMainNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Show();                
            }
            else if (cmbRoom.Text == "SMT-OUT")
            {
                FormMainNine form = new FormMainNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Show();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
