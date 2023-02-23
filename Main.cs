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
                //FormMains formMains = new FormMains();
                //formMains.Text += " ("+cmbRoom.Text+ ")";
                //this.Hide();
                //formMains.Show();

                Dashboard dashboard = new Dashboard();
                dashboard.Text += " (" + cmbRoom.Text + ")";
                this.Hide();
                dashboard.Show();
            }
            else if (cmbRoom.Text == "SMT-SA")
            {
                //FormMainNine form = new FormMainNine();
                //this.Hide();
                //form.roomtb.Text = cmbRoom.Text;
                //form.Text += " (" + cmbRoom.Text + ")";
                //form.Show();

                DashboardNine form = new DashboardNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Text += " (" + cmbRoom.Text + ")";
                form.Show();
            }
            else if (cmbRoom.Text == "SMT-DIPPING")
            {
                DashboardNine form = new DashboardNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Text += " (" + cmbRoom.Text + ")";
                form.Show();
            }
            else if (cmbRoom.Text == "SMT-OUT")
            {
                DashboardNineOut form = new DashboardNineOut();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Text += " (" + cmbRoom.Text + ")";
                form.Show();
            }
            else if (cmbRoom.Text == "SMT-MAINOUT")
            {
                DashboardNineOut form = new DashboardNineOut();
                this.Hide();
                form.roomtb.Text = cmbRoom.Text;
                form.Text += " (" + cmbRoom.Text + ")";
                form.Show();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
