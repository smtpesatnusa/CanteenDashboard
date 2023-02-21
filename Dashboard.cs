using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NetrayaDashboard
{
    public partial class Dashboard : Form
    {
        MySqlConnection myConn;
        readonly Helper help = new Helper();
        string employee1, employee2, employee3, employee4, employee5, employee6, employee7, employee8, employee9, employee10, employee11, employee12, employee13, employee14, employee15, employee16;
        string badge1, badge2, badge3, badge4, badge5, badge6, badge7, badge8, badge9, badge10, badge11, badge12, badge13, badge14, badge15, badge16;
        string lineCode1, lineCode2, lineCode3, lineCode4, lineCode5, lineCode6, lineCode7, lineCode8, lineCode9, lineCode10, lineCode11, lineCode12, lineCode13, lineCode14, lineCode15, lineCode16;
        string section1, section2, section3, section4, section5, section6, section7, section8, section9, section10, section11, section12, section13, section14, section15, section16;
        string time1, time2, time3, time4, time5, time6, time7, time8, time9, time10, time11, time12, time13, time14, time15, time16;


        public Dashboard()
        {
            InitializeComponent();
        }

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            currentDate.Text = DateTime.Now.ToString("dddd, dd-MM-yyyy");
            currentTime.Text = DateTime.Now.ToString("HH:mm");
            //absent();
            EmployeeData();
        }
        private void timerScroll_Tick(object sender, EventArgs e)
        {
            //Scroll late
            if (dataGridViewLate.CurrentRow == null) return;
            if (dataGridViewLate.CurrentRow.Index + 1 >= 0 && dataGridViewLate.CurrentRow.Index + 1 < dataGridViewLate.RowCount)
            {
                dataGridViewLate.CurrentCell = dataGridViewLate.Rows[dataGridViewLate.CurrentRow.Index + 1].Cells[0];
                dataGridViewLate.Rows[dataGridViewLate.CurrentCell.RowIndex].Selected = true;
            }
            if (dataGridViewLate.CurrentRow.Index + 1 == dataGridViewLate.RowCount)
            {
                dataGridViewLate.CurrentCell = dataGridViewLate.Rows[0].Cells[0];
            }            
        }
        private void timerRefreshLate_Tick(object sender, EventArgs e)
        {
            // refresh late and reupdate latelist
            refreshLate();
            lateList();
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
            //// display top 16 data in tbl_log
            //absent();
            //lateList();
            EmployeeData();
        }
        class EmployeeDetail
        {
            public string badgeId { get; set; }
            public string rfidNo { get; set; }
            public string name { get; set; }
            public string level { get; set; }
            public string linecode { get; set; }
            public string descr { get; set; }
            public string timelog { get; set; }
            public int sequence { get; set; }
        }

        public void EmployeeData()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);
                myConn.Open();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

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
                            employee.Add(dt.Rows[i]["rfidNo"].ToString(), new EmployeeDetail
                            {
                                badgeId = dt.Rows[i]["badgeId"].ToString(),
                                name = dt.Rows[i]["name"].ToString(),
                                level = dt.Rows[i]["level"].ToString(),
                                linecode = dt.Rows[i]["linecode"].ToString()+"("+ dt.Rows[i]["description"].ToString() + ")",
                                descr = dt.Rows[i]["description"].ToString(),
                                timelog = dt.Rows[i]["timelog"].ToString(),
                                sequence = Convert.ToInt32(dt.Rows[i]["sequence"].ToString())
                            });
                        }
                    }
                }

                // select rfid log
                string queryInEmployee = "SELECT * FROM clockIn_Mainroom";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryInEmployee, myConn))
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
                            string query = "SELECT rfidNo FROM tbl_employee WHERE rfidNo = '"+rfidno+"'";
                            using (MySqlDataAdapter adpt1 = new MySqlDataAdapter(query, myConn))
                            {
                                DataTable dt1 = new DataTable();
                                adpt1.Fill(dt1);

                                // jika rfid tsb ada di data employee update data array
                                if (dt1.Rows.Count > 0)
                                {
                                    employee[rfidno].timelog = timelogs;
                                    employee[rfidno].sequence = sequences;
                                }
                            }                            
                        }
                    }
                }

                foreach (var k in employee.OrderByDescending(x => x.Value.sequence).Take(16))
                {
                    //Console.WriteLine(k);
                    Console.WriteLine(k.Value.badgeId + " " + k.Value.name + " " + k.Value.linecode + " " + k.Value.timelog + " " + k.Value.sequence);
                }

                stopwatch.Stop();
                Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");

                //employee.OrderByDescending(item => item.Value.sequence);

                //for (int i = 0; i < employee.Count; i++)
                //{
                //    Console.WriteLine($"Employee {i} is {employee.ElementAt(i).Value.badgeId} {employee.ElementAt(i).Value.rfidNo} {employee.ElementAt(i).Value.name} {employee.ElementAt(i).Value.level} " +
                //        $"{employee.ElementAt(i).Value.linecode} {employee.ElementAt(i).Value.descr} {employee.ElementAt(i).Value.timelog}");
                //}
            }
            catch (Exception ex)
            {
                myConn.Close();
                MessageBox.Show("displayData: " + ex.Message);
            }
        }

        public void InMainroom()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                string queryInEmployee = "SELECT * FROM clockIn_Mainroom";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryInEmployee, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string rfidno = dt.Rows[i]["rfidNo"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }


        private void dataGridViewLate_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewLate.Columns[1].DefaultCellStyle.Format = "HH:mm";
            dataGridViewLate.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;          
        }

        public void refreshLate()
        {
            // reset datagridview
            DataGridView[] dgv = { dataGridViewLate };
            for (int i = 0; i < dgv.Length; i++)
            {
                // remove data in datagridview result
                dgv[i].DataSource = null;
                dgv[i].Refresh();

                while (dgv[i].Columns.Count > 0)
                {
                    dgv[i].Columns.RemoveAt(0);
                }
                dgv[i].Update();
                dgv[i].Refresh();
            }
        }


        public void lateList()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                string queryClockIn = "SELECT CONCAT(b.name,'(',b.badgeId,')') AS employee, a.intime FROM tbl_attendance a, tbl_employee b " +
                    "WHERE a.date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND a.islate = '1' AND a.emplid = b.id ORDER BY a.intime";

                //"SELECT b.badgeId, b.name, b.linecode, a.intime FROM tbl_attendance a, tbl_employee b " +
                //"WHERE a.date = '"+ DateTime.Now.ToString("yyyy-MM-dd") +"' AND a.islate = '1' AND a.emplid = b.id ORDER BY a.intime";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryClockIn, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    dataGridViewLate.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        // add image in datagridview table
                        DataGridViewImageColumn image = new DataGridViewImageColumn();
                        dataGridViewLate.Columns.Add(image);
                        image.Name = "employeePict";
                        image.ImageLayout = DataGridViewImageCellLayout.Zoom;                        

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string employee = dt.Rows[i]["employee"].ToString();
                            string[] words = employee.Split('(');
                            string badges = words[1].Replace(")", "");
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badges + ".jpg"))
                            {
                                dataGridViewLate[2, i].Value = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badges + ".jpg");
                            }
                            else
                            {
                                dataGridViewLate[2, i].Value = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\default.png");
                            }
                            //Console.WriteLine(badges);
                        }
                        // auto size image column after populate data
                        image.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("displayData: " + ex.Message);
            }
        }

        public void absent()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                string queryClockIn = "SELECT * FROM in_Mainroom";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryClockIn, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge1 + ".jpg"))
                            {
                                pictureBox1.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge1 + ".jpg");
                            }
                            else
                            {
                                pictureBox1.Image = Properties.Resources._default;
                            }
                            HeaderColor(header1,clock1,badge1,"0");                            
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge2 + ".jpg"))
                            {
                                pictureBox2.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge2 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge3 + ".jpg"))
                            {
                                pictureBox3.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge3 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge4 + ".jpg"))
                            {
                                pictureBox4.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge4 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge5 + ".jpg"))
                            {
                                pictureBox5.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge5 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge6 + ".jpg"))
                            {
                                pictureBox6.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge6 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge7 + ".jpg"))
                            {
                                pictureBox7.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge7 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge8 + ".jpg"))
                            {
                                pictureBox8.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge8 + ".jpg");
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
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge9 + ".jpg"))
                            {
                                pictureBox9.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge9 + ".jpg");
                            }
                            else
                            {
                                pictureBox9.Image = Properties.Resources._default;
                            }
                            HeaderColor(header9, clock9, badge9, "0");
                        }
                        if (r > 9)
                        {
                            employee10 = dt.Rows[9]["name"].ToString();
                            badge10 = dt.Rows[9]["badgeID"].ToString();
                            lineCode10 = dt.Rows[9]["linecode"].ToString();
                            section10 = dt.Rows[9]["section"].ToString();
                            time10 = Convert.ToDateTime(dt.Rows[9]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel10.Text = elipsisText(employee10);
                            badgeId10.Text = badge10;
                            linesection10.Text = lineCode10 + " (" + section10 + ")";
                            clockIn10.Text = time10;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge10 + ".jpg"))
                            {
                                pictureBox10.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge10 + ".jpg");
                            }
                            else
                            {
                                pictureBox10.Image = Properties.Resources._default;
                            }
                            HeaderColor(header10, clock10, badge10, "0");
                        }
                        if (r > 10)
                        {
                            employee11 = dt.Rows[10]["name"].ToString();
                            badge11 = dt.Rows[10]["badgeID"].ToString();
                            lineCode11 = dt.Rows[10]["linecode"].ToString();
                            section11 = dt.Rows[10]["section"].ToString();
                            time11 = Convert.ToDateTime(dt.Rows[10]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel11.Text = elipsisText(employee11);
                            badgeId11.Text = badge11;
                            linesection11.Text = lineCode11 + " (" + section11 + ")";
                            clockIn11.Text = time11;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge11 + ".jpg"))
                            {
                                pictureBox11.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge11 + ".jpg");
                            }
                            else
                            {
                                pictureBox11.Image = Properties.Resources._default;
                            }
                            HeaderColor(header11, clock11, badge11, "0");
                        }
                        if (r > 11)
                        {
                            employee12 = dt.Rows[11]["name"].ToString();
                            badge12 = dt.Rows[11]["badgeID"].ToString();
                            lineCode12 = dt.Rows[11]["linecode"].ToString();
                            section12 = dt.Rows[11]["section"].ToString();
                            time12 = Convert.ToDateTime(dt.Rows[11]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel12.Text = elipsisText(employee12);
                            badgeId12.Text = badge12;
                            linesection12.Text = lineCode12 + " (" + section12 + ")";
                            clockIn12.Text = time12;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge12 + ".jpg"))
                            {
                                pictureBox12.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge12 + ".jpg");
                            }
                            else
                            {
                                pictureBox12.Image = Properties.Resources._default;
                            }
                            HeaderColor(header12, clock12, badge12, "0");
                        }
                        if (r > 12)
                        {
                            employee13 = dt.Rows[12]["name"].ToString();
                            badge13 = dt.Rows[12]["badgeID"].ToString();
                            lineCode13 = dt.Rows[12]["linecode"].ToString();
                            section13 = dt.Rows[12]["section"].ToString();
                            time13 = Convert.ToDateTime(dt.Rows[12]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel13.Text = elipsisText(employee13);
                            badgeId13.Text = badge13;
                            linesection13.Text = lineCode13 + " (" + section13 + ")";
                            clockIn13.Text = time13;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge13 + ".jpg"))
                            {
                                pictureBox13.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge13 + ".jpg");
                            }
                            else
                            {
                                pictureBox13.Image = Properties.Resources._default;
                            }
                            HeaderColor(header13, clock13, badge13, "0");
                        }
                        if (r > 13)
                        {
                            employee14 = dt.Rows[13]["name"].ToString();
                            badge14 = dt.Rows[13]["badgeID"].ToString();
                            lineCode14 = dt.Rows[13]["linecode"].ToString();
                            section14 = dt.Rows[13]["section"].ToString();
                            time14 = Convert.ToDateTime(dt.Rows[13]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel14.Text = elipsisText(employee14);
                            badgeId14.Text = badge14;
                            linesection14.Text = lineCode14 + " (" + section14 + ")";
                            clockIn14.Text = time14;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge14 + ".jpg"))
                            {
                                pictureBox14.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge14 + ".jpg");
                            }
                            else
                            {
                                pictureBox14.Image = Properties.Resources._default;
                            }
                            HeaderColor(header14, clock14, badge14, "0");
                        }
                        if (r > 14)
                        {
                            employee15 = dt.Rows[14]["name"].ToString();
                            badge15 = dt.Rows[14]["badgeID"].ToString();
                            lineCode15 = dt.Rows[14]["linecode"].ToString();
                            section15 = dt.Rows[14]["section"].ToString();
                            time15 = Convert.ToDateTime(dt.Rows[14]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel15.Text = elipsisText(employee15);
                            badgeId15.Text = badge15;
                            linesection15.Text = lineCode15 + " (" + section15 + ")";
                            clockIn15.Text = time15;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge15 + ".jpg"))
                            {
                                pictureBox15.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge15 + ".jpg");
                            }
                            else
                            {
                                pictureBox15.Image = Properties.Resources._default;
                            }
                            HeaderColor(header15, clock15, badge15, "0");
                        }
                        if (r > 15)
                        {
                            employee16 = dt.Rows[15]["name"].ToString();
                            badge16 = dt.Rows[15]["badgeID"].ToString();
                            lineCode16 = dt.Rows[15]["linecode"].ToString();
                            section16 = dt.Rows[15]["section"].ToString();
                            time16 = Convert.ToDateTime(dt.Rows[15]["timelog"].ToString()).ToString("HH:mm"); ;
                            namePanel16.Text = elipsisText(employee16);
                            badgeId16.Text = badge16;
                            linesection16.Text = lineCode16 + " (" + section16 + ")";
                            clockIn16.Text = time16;
                            if (File.Exists(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge16 + ".jpg"))
                            {
                                pictureBox16.Image = Image.FromFile(@"\\192.168.192.254\SystemSupport\Netraya\EmplFoto\" + badge16 + ".jpg");
                            }
                            else
                            {
                                pictureBox16.Image = Properties.Resources._default;
                            }
                            HeaderColor(header16, clock16, badge16, "0");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }
        private void dataGridViewLate_Paint(object sender, PaintEventArgs e)
        {
            help.norecord_dgv(dataGridViewLate, e);
        }

        //private string elipsisText(string name)
        //{
        //    if (name.Length > 13)
        //    {
        //        return name = name.Substring(0, 13) + "..";
        //    }
        //    else
        //    {
        //        return name = name;
        //    }
        //}

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

                string query = "SELECT a.islate FROM tbl_attendance a , tbl_employee b WHERE a.emplid = b.id AND " +
                    "DATE = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND b.badgeId='"+badgeEmployee+"'";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, myConn))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            isLate = dt.Rows[i]["islate"].ToString();
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
