using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CanteenDashboard
{
    public partial class DashboardNine : Form
    {
        MySqlConnection myConn;
        readonly Helper help = new Helper();
        string employee1, employee2, employee3, employee4, employee5, employee6, employee7, employee8, employee9;
        string badge1, badge2, badge3, badge4, badge5, badge6, badge7, badge8, badge9;
        string lineCode1, lineCode2, lineCode3, lineCode4, lineCode5, lineCode6, lineCode7, lineCode8, lineCode9;
        string section1, section2, section3, section4, section5, section6, section7, section8, section9;
        string time1, time2, time3, time4, time5, time6, time7, time8, time9;

        string queryAbsent;

        public DashboardNine()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            currentDate.Text = DateTime.Now.ToString("dddd, dd-MM-yyyy");
            currentTime.Text = DateTime.Now.ToString("HH:mm");
            if (DateTime.Now.ToString("HH:mm") == "23:59")
            {
                EmployeeData();
            }

            // get top 9 data empployee from log
            logData();
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MaterialDialog materialDialog = new MaterialDialog(this, title, message, "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == "OK")
            {
                Application.ExitThread();
            }
            else
            {
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(), 750);
                SnackBarMessage.Show(this);
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            currentDate.Text = DateTime.Now.ToString("dddd, dd-MM-yyyy");
            currentTime.Text = DateTime.Now.ToString("HH:mm");
            queryAbsent = null;

            // store employee data to hashmap
            EmployeeData();

            // get top 9 data empployee from log
            logData();
        }

        //class EmployeeDetail
        //{
        //    public string badgeId { get; set; }
        //    public string rfidNo { get; set; }
        //    public string name { get; set; }
        //    public string level { get; set; }
        //    public string linecode { get; set; }
        //    public string section { get; set; }
        //    public string timelog { get; set; }
        //    public int sequence { get; set; }
        //}

        public void EmployeeData()
        {
            try
            {               
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);
                myConn.Open();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                string view = roomtb.Text;                
                queryAbsent = "SELECT * FROM "+view;

                // arrayemployeeata
                var employee = new Dictionary<string, EmployeeDetail>();
                string queryDetailEmployee = "SELECT * FROM detail_employee";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryDetailEmployee, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // get all data in data table to array
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //GlobalVariables.employee.Add(dt.Rows[i]["rfidNo"].ToString(), new EmployeeDetail
                            employee.Add(dt.Rows[i]["rfidNo"].ToString(), new EmployeeDetail
                            {
                                badgeId = dt.Rows[i]["badgeId"].ToString(),
                                name = dt.Rows[i]["name"].ToString(),
                                level = dt.Rows[i]["level"].ToString(),
                                linecode = dt.Rows[i]["linecode"].ToString(),
                                section = dt.Rows[i]["dept"].ToString(),
                                timelog = dt.Rows[i]["timelog"].ToString(),
                                sequence = Convert.ToInt32(dt.Rows[i]["sequence"].ToString())
                            });
                        }
                    }
                }

                //foreach (var kvp in employee)
                //{
                //    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value.name);
                //}

                GlobalVariables.employee = employee;

                stopwatch.Stop();
                Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");
            }
            catch (Exception ex)
            {
                myConn.Close();
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }

        public void logData()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);
                myConn.Open();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                string view = roomtb.Text;
                queryAbsent = "SELECT * FROM " + view;

                // reset sequence to 0
                foreach (var employeeItem in GlobalVariables.employee)
                {
                    GlobalVariables.employee[employeeItem.Key].sequence = 0;
                    //Console.WriteLine("Key = {0}, Value = {1}", employeeItem.Key, employeeItem.Value.name);
                }

                // select rfid log
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryAbsent, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int sequences = dt.Rows.Count - i;
                            string rfidno = dt.Rows[i]["rfidNo"].ToString();
                            string timelogs = DateTime.Parse(dt.Rows[i]["timelog"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                            // check rfid tsb di data employee
                            string query = "SELECT rfidNo FROM tbl_employee WHERE rfidNo = '" + rfidno + "'";
                            using (MySqlDataAdapter adpt1 = new MySqlDataAdapter(query, myConn))
                            {
                                DataTable dt1 = new DataTable();
                                adpt1.Fill(dt1);

                                // jika rfid tsb ada di data employee update data array
                                if (dt1.Rows.Count > 0)
                                {
                                    // update jika hanya sequence awalnya 0
                                    if (GlobalVariables.employee[rfidno].sequence == 0)
                                    {
                                        GlobalVariables.employee[rfidno].timelog = timelogs;
                                        GlobalVariables.employee[rfidno].sequence = sequences;
                                    }
                                }
                            }
                        }
                    }
                }

                //convert dictionary to datatable
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("badgeID", typeof(string));
                dt2.Columns.Add("name", typeof(string));
                dt2.Columns.Add("linecode", typeof(string));
                dt2.Columns.Add("section", typeof(string));
                dt2.Columns.Add("timelog", typeof(string));
                dt2.Columns.Add("Sequence", typeof(int));

                foreach (var item in GlobalVariables.employee)
                {
                    DataRow dr = dt2.NewRow();
                    dr["badgeID"] = item.Value.badgeId;
                    dr["name"] = item.Value.name;
                    dr["linecode"] = item.Value.linecode;
                    dr["section"] = item.Value.section;
                    dr["timelog"] = item.Value.timelog;
                    dr["Sequence"] = Convert.ToInt32(item.Value.sequence);
                    dt2.Rows.Add(dr);
                }

                //Sorting the Table by sequence
                dt2.DefaultView.Sort = "Sequence desc";
                dt2 = dt2.DefaultView.ToTable(true);

                // display datatable to diplay
                datatabletoview(dt2);

                stopwatch.Stop();
                Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");
                myConn.Close();
            }
            catch (Exception ex)
            {
                myConn.Close();
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }

        public void datatabletoview(DataTable dt )
        {
            if (dt.Rows.Count > 0)
            {
                int r = dt.Rows.Count;
                if (r > 0)
                {
                    employee1 = dt.Rows[0]["name"].ToString();
                    badge1 = dt.Rows[0]["badgeID"].ToString();
                    lineCode1 = dt.Rows[0]["linecode"].ToString();
                    section1 = dt.Rows[0]["section"].ToString();
                    time1 = Convert.ToDateTime(dt.Rows[0]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel1.Text = elipsisText(employee1);
                    badgeId1.Text = badge1;
                    linesection1.Text = lineCode1 + " (" + section1 + ")";
                    clockIn1.Text = time1;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge1 + ".jpg"))
                    {
                        pictureBox1.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge1 + ".jpg");
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources._default;
                    }
                    HeaderColor(header1, clock1, badge1, "0");
                }
                if (r > 1)
                {
                    employee2 = dt.Rows[1]["name"].ToString();
                    badge2 = dt.Rows[1]["badgeID"].ToString();
                    lineCode2 = dt.Rows[1]["linecode"].ToString();
                    section2 = dt.Rows[1]["section"].ToString();
                    time2 = Convert.ToDateTime(dt.Rows[1]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel2.Text = elipsisText(employee2);
                    badgeId2.Text = badge2;
                    linesection2.Text = lineCode2 + " (" + section2 + ")";
                    clockIn2.Text = time2;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge2 + ".jpg"))
                    {
                        pictureBox2.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge2 + ".jpg");
                    }
                    else
                    {
                        pictureBox2.Image = Properties.Resources._default;
                    }
                    HeaderColor(header2, clock2, badge2, "0");
                }
                if (r > 2)
                {
                    employee3 = dt.Rows[2]["name"].ToString();
                    badge3 = dt.Rows[2]["badgeID"].ToString();
                    lineCode3 = dt.Rows[2]["linecode"].ToString();
                    section3 = dt.Rows[2]["section"].ToString();
                    time3 = Convert.ToDateTime(dt.Rows[2]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel3.Text = elipsisText(employee3);
                    badgeId3.Text = badge3;
                    linesection3.Text = lineCode3 + " (" + section3 + ")";
                    clockIn3.Text = time3;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge3 + ".jpg"))
                    {
                        pictureBox3.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge3 + ".jpg");
                    }
                    else
                    {
                        pictureBox3.Image = Properties.Resources._default;
                    }
                    HeaderColor(header3, clock3, badge3, "0");
                }
                if (r > 3)
                {
                    employee4 = dt.Rows[3]["name"].ToString();
                    badge4 = dt.Rows[3]["badgeID"].ToString();
                    lineCode4 = dt.Rows[3]["linecode"].ToString();
                    section4 = dt.Rows[3]["section"].ToString();
                    time4 = Convert.ToDateTime(dt.Rows[3]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel4.Text = elipsisText(employee4);
                    badgeId4.Text = badge4;
                    linesection4.Text = lineCode4 + " (" + section4 + ")";
                    clockIn4.Text = time4;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge4 + ".jpg"))
                    {
                        pictureBox4.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge4 + ".jpg");
                    }
                    else
                    {
                        pictureBox4.Image = Properties.Resources._default;
                    }
                    HeaderColor(header4, clock4, badge4, "0");
                }
                if (r > 4)
                {
                    employee5 = dt.Rows[4]["name"].ToString();
                    badge5 = dt.Rows[4]["badgeID"].ToString();
                    lineCode5 = dt.Rows[4]["linecode"].ToString();
                    section5 = dt.Rows[4]["section"].ToString();
                    time5 = Convert.ToDateTime(dt.Rows[4]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel5.Text = elipsisText(employee5);
                    badgeId5.Text = badge5;
                    linesection5.Text = lineCode5 + " (" + section5 + ")";
                    clockIn5.Text = time5;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge5 + ".jpg"))
                    {
                        pictureBox5.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge5 + ".jpg");
                    }
                    else
                    {
                        pictureBox5.Image = Properties.Resources._default;
                    }
                    HeaderColor(header5, clock5, badge5, "0");
                }
                if (r > 5)
                {
                    employee6 = dt.Rows[5]["name"].ToString();
                    badge6 = dt.Rows[5]["badgeID"].ToString();
                    lineCode6 = dt.Rows[5]["linecode"].ToString();
                    section6 = dt.Rows[5]["section"].ToString();
                    time6 = Convert.ToDateTime(dt.Rows[5]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel6.Text = elipsisText(employee6);
                    badgeId6.Text = badge6;
                    linesection6.Text = lineCode6 + " (" + section6 + ")";
                    clockIn6.Text = time6;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge6 + ".jpg"))
                    {
                        pictureBox6.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge6 + ".jpg");
                    }
                    else
                    {
                        pictureBox6.Image = Properties.Resources._default;
                    }
                    HeaderColor(header6, clock6, badge6, "0");
                }
                if (r > 6)
                {
                    employee7 = dt.Rows[6]["name"].ToString();
                    badge7 = dt.Rows[6]["badgeID"].ToString();
                    lineCode7 = dt.Rows[6]["linecode"].ToString();
                    section7 = dt.Rows[6]["section"].ToString();
                    time7 = Convert.ToDateTime(dt.Rows[6]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel7.Text = elipsisText(employee7);
                    badgeId7.Text = badge7;
                    linesection7.Text = lineCode7 + " (" + section7 + ")";
                    clockIn7.Text = time7;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge7 + ".jpg"))
                    {
                        pictureBox7.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge7 + ".jpg");
                    }
                    else
                    {
                        pictureBox7.Image = Properties.Resources._default;
                    }
                    HeaderColor(header7, clock7, badge7, "0");
                }
                if (r > 7)
                {
                    employee8 = dt.Rows[7]["name"].ToString();
                    badge8 = dt.Rows[7]["badgeID"].ToString();
                    lineCode8 = dt.Rows[7]["linecode"].ToString();
                    section8 = dt.Rows[7]["section"].ToString();
                    time8 = Convert.ToDateTime(dt.Rows[7]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel8.Text = elipsisText(employee8);
                    badgeId8.Text = badge8;
                    linesection8.Text = lineCode8 + " (" + section8 + ")";
                    clockIn8.Text = time8;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge8 + ".jpg"))
                    {
                        pictureBox8.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge8 + ".jpg");
                    }
                    else
                    {
                        pictureBox8.Image = Properties.Resources._default;
                    }
                    HeaderColor(header8, clock8, badge8, "0");
                }
                if (r > 8)
                {
                    employee9 = dt.Rows[8]["name"].ToString();
                    badge9 = dt.Rows[8]["badgeID"].ToString();
                    lineCode9 = dt.Rows[8]["linecode"].ToString();
                    section9 = dt.Rows[8]["section"].ToString();
                    time9 = Convert.ToDateTime(dt.Rows[8]["timelog"].ToString()).ToString("HH:mm"); ;
                    namePanel9.Text = elipsisText(employee9);
                    badgeId9.Text = badge9;
                    linesection9.Text = lineCode9 + " (" + section9 + ")";
                    clockIn9.Text = time9;
                    if (File.Exists(@"\\192.168.20.253\Netraya\EmplFoto\" + badge9 + ".jpg"))
                    {
                        pictureBox9.Image = Image.FromFile(@"\\192.168.20.253\Netraya\EmplFoto\" + badge9 + ".jpg");
                    }
                    else
                    {
                        pictureBox9.Image = Properties.Resources._default;
                    }
                    HeaderColor(header9, clock9, badge9, "0");
                }
            }
        }

        private string elipsisText(string name)
        {
            if (name.Contains(" "))
            {
                string[] words = name.Split(' ');
                name = words[0] + '\n' + words[1];
            }

            return name;
        }

        // get color late or not
        private void HeaderColor(Panel panel, Label label, string badgeEmployee, string isLate)
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                string query = "SELECT a.isOver FROM tbl_attendance a , tbl_employee b WHERE a.emplid = b.id AND " +
                    "DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND b.badgeId='" + badgeEmployee + "'";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isLate = dt.Rows[i]["isOver"].ToString();
                        }
                    }
                }

                // change header based on color
                switch (isLate)
                {
                    case "1":
                        panel.BackColor = Color.FromArgb(203, 34, 48);
                        label.ForeColor = Color.White;
                        break;
                    case "0":
                        panel.BackColor = Color.FromArgb(33, 206, 163);
                        label.ForeColor = Color.DimGray;
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }
    }
}
