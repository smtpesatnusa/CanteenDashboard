﻿using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NetrayaDashboard
{
    public partial class FormMain : MaterialForm
    {
        MySqlConnection myConn;

        string employee1, employee2, employee3, employee4, employee5, employee6, employee7, employee8, employee9, employee10, employee11, employee12, employee13, employee14, employee15;
        string badge1, badge2, badge3, badge4, badge5, badge6, badge7, badge8, badge9, badge10, badge11, badge12, badge13, badge14, badge15;
        string lineCode1, lineCode2, lineCode3, lineCode4, lineCode5, lineCode6, lineCode7, lineCode8, lineCode9, lineCode10, lineCode11, lineCode12, lineCode13, lineCode14, lineCode15;
        string section1, section2, section3, section4, section5, section6, section7, section8, section9, section10, section11, section12, section13, section14, section15;
        string time1, time2, time3, time4, time5, time6, time7, time8, time9, time10, time11, time12, time13, time14, time15;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            // display top 12 data in tbl_log
            absent();
        }

        public void absent()
        {
            try
            {
                string koneksi = ConnectionDB.strProvider;
                myConn = new MySqlConnection(koneksi);

                dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                timeNow.Text = DateTime.Now.ToString("HH:mm");

                string queryTotalOntime = "SELECT b.linecode, c.description AS section, b.badgeID, b.name,  MAX(a.timelog)AS timelog FROM tbl_log a, tbl_employee b, tbl_masterlinecode c " +
                    "WHERE a.rfidno = b.rfidno AND b.linecode = c.name AND (a.ipDevice = 'SMT-MAINROOM' OR a.ipDevice = 'SMT-GATE') AND a.indicator = 'In' " +
                    "GROUP BY b.badgeID, b.name, b.linecode ORDER BY timelog DESC LIMIT 15";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryTotalOntime, myConn))
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
                            panelColor(panel2, section1);
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
                            panelColor(panel3, section2);
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
                            panelColor(panel4, section3);
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
                            panelColor(panel5, section4);
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
                            panelColor(panel6, section5);
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
                            panelColor(panel7, section6);
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
                            panelColor(panel8, section7);
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
                            panelColor(panel9, section8);
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
                            panelColor(panel10, section9);
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
                            panelColor(panel11, section10);
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
                            panelColor(panel12, section11);
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
                            panelColor(panel13, section12);
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
                            linesection13.Text =  lineCode13 + " (" + section13 + ")";
                            clockIn13.Text = time13;
                            panelColor(panel14, section13);
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
                            label24.Text = time14;
                            panelColor(panel15, section14);
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
                            label26.Text = time15;
                            panelColor(panel16, section15);
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
            if (name.Length > 13)
            {
                return name = name.Substring(0, 13) + "..";
            }
            else
            {
                return name = name;
            }
        }

        private void panelColor(Panel panel, string lineSection)
        {
            if (lineSection.Contains("PROD"))
            {
                panel.BackColor = Color.DeepSkyBlue;
            }
            if (lineSection.Contains("PE"))
            {
                panel.BackColor = Color.Blue;
            }
            if (lineSection.Contains("MGR"))
            {
                panel.BackColor = Color.Gray;
            }
            if (lineSection.Contains("ENG"))
            {
                panel.BackColor = Color.LightSeaGreen;
            }
            if (lineSection.Contains("PC"))
            {
                panel.BackColor = Color.MediumPurple;
            }
            if (lineSection.Contains("QC"))
            {
                panel.BackColor = Color.HotPink;
            }
            if (lineSection.Contains("STORE"))
            {
                panel.BackColor = Color.Salmon;
            }
        }
    }
}