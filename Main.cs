using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CanteenDashboard
{
    public partial class Main : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();

        public Main()
        {
            InitializeComponent();

            //menampilkan data combobox 
            help.displayCmbList("SELECT * FROM tbl_masterroom ORDER BY id ", "name", "query", cmbRoom);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }


        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (cmbRoom.Text != "System.Data.DataRowView" || cmbRoom.Text != "")
            {
                DashboardNine form = new DashboardNine();
                this.Hide();
                form.roomtb.Text = cmbRoom.SelectedValue.ToString();
                form.Text += " (" + cmbRoom.SelectedValue.ToString() + ")";
                form.Show();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
