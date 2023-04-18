using MaterialSkin.Controls;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CanteenDashboard
{
    public partial class Main : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();        
        string room;

        public Main()
        {
            InitializeComponent();            
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }


        private void selectBtn_Click(object sender, EventArgs e)
        {
            showDashboard();
        }

        private void showDashboard()
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
            //menampilkan data combobox 
            help.displayCmbList("SELECT * FROM tbl_masterroom WHERE dept = 'CT' ORDER BY id", "name", "query", cmbRoom);

            // cek file jika ada detail room auto kebuka
            string configFile = "C:\\Config\\file.txt";
            FileInfo file = new FileInfo(configFile);
            // cek apakah file exist
            if (file.Exists)
            {
                room = File.ReadAllText(@"" + configFile + "", Encoding.UTF8);
                //jika ada datanya cek apakah ada di dropdown cika ada auto select
                if (room != "")
                {
                    cmbRoom.SelectedIndex = cmbRoom.FindString(room);
                    Thread.Sleep(3000);
                }
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (cmbRoom.Text != "")
            {
                selectBtn.PerformClick();
            }            
        }
    }
}
