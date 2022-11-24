using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NetrayaDashboard
{
    public partial class FormMainNine : MaterialForm
    {
        MySqlConnection myConn;
        string queryAbsent;

        string employee1, employee2, employee3, employee4, employee5, employee6, employee7, employee8, employee9;
        string badge1, badge2, badge3, badge4, badge5, badge6, badge7, badge8, badge9;
        string lineCode1, lineCode2, lineCode3, lineCode4, lineCode5, lineCode6, lineCode7, lineCode8, lineCode9;
        string section1, section2, section3, section4, section5, section6, section7, section8, section9;
        string time1, time2, time3, time4, time5, time6, time7, time8, time9;

        public FormMainNine()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            queryAbsent = null;
            // display top 9 data in tbl_log
            absent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Main mm = new Main();
            mm.Show();
            this.Hide();
        }

        private void FormMainNine_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        public void absent()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                timeNow.Text = DateTime.Now.ToString("HH:mm");

                if (roomtb.Text == "SMT-SA")
                {
                    queryAbsent =
                    "SELECT b.linecode, c.description AS section, b.badgeID, b.name,  MAX(a.timelog)AS timelog FROM tbl_log a, tbl_employee b, tbl_masterlinecode c " +
                    "WHERE a.rfidno = b.rfidno AND b.linecode = c.name AND(a.ipDevice = 'SMT-SA') " +
                    "AND(a.indicator = 'In') GROUP BY b.badgeID, b.name, b.linecode ORDER BY timelog DESC LIMIT 9";
                    //"SELECT b.linecode, c.description AS section, b.badgeID, b.name,  MAX(a.timelog)AS timelog FROM tbl_log a, tbl_employee b, tbl_masterlinecode c " +
                    //    "WHERE a.rfidno = b.rfidno AND b.linecode = c.name AND (a.ipDevice = 'SMT-MAINROOM' OR a.ipDevice = 'SMT-GATE') AND a.indicator = 'In' " +
                    //    "GROUP BY b.badgeID, b.name, b.linecode ORDER BY timelog DESC LIMIT 15";
                }
                else if (roomtb.Text == "SMT-DIPPING")
                {
                    queryAbsent =
                    "SELECT b.linecode, c.description AS section, b.badgeID, b.name,  MAX(a.timelog)AS timelog FROM tbl_log a, tbl_employee b, tbl_masterlinecode c " +
                    "WHERE a.rfidno = b.rfidno AND b.linecode = c.name AND(a.ipDevice = 'SMT-DIPPING') " +
                    "AND(a.indicator = 'In') GROUP BY b.badgeID, b.name, b.linecode ORDER BY timelog DESC LIMIT 9";
                }             


                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryAbsent, myConn))
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
                            panelColor(panel1, namePanel1, section1);
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
                            panelColor(panel2, namePanel2, section2);
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
                            panelColor(panel3, namePanel3, section3);
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
                            panelColor(panel4, namePanel4, section4);
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
                            panelColor(panel5, namePanel5, section5);
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
                            panelColor(panel6, namePanel6, section6);
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
                            panelColor(panel7, namePanel7, section7);
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
                            panelColor(panel8, namePanel8, section8);
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
                            panelColor(panel9, namePanel9, section9);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("displayData: " + ex.Message);
            }
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            timeNow.Text = DateTime.Now.ToString("HH:mm");
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            absent();
        }

        private string elipsisText(string name)
        {
            if (name.Length > 12)
            {
                return name = name.Substring(0, 12) + "..";
            }
            else
            {
                return name = name;
            }
        }

        private void panelColor(Panel panel, Label label, string lineSection)
        {
            switch (lineSection)
            {
                case "PROD":
                    panel.BackColor = Color.DeepSkyBlue;
                    label.ForeColor = Color.Black;
                    break;
                case "PE":
                    panel.BackColor = Color.Blue;
                    label.ForeColor = Color.White;
                    break;
                case "MGR":
                    panel.BackColor = Color.Gray;
                    label.ForeColor = Color.White;
                    break;
                case "ENG":
                    panel.BackColor = Color.MediumSeaGreen;
                    label.ForeColor = Color.Black;
                    break;
                case "PC":
                    panel.BackColor = Color.MediumPurple;
                    label.ForeColor = Color.Black;
                    break;
                case "QC":
                    panel.BackColor = Color.HotPink;
                    label.ForeColor = Color.Black;
                    break;
                case "STORE":
                    panel.BackColor = Color.Salmon;
                    label.ForeColor = Color.Black;
                    break;
                case "CS":
                    panel.BackColor = Color.Gold;
                    label.ForeColor = Color.Black;
                    break;
            }

        }
    }
}
