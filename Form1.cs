using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FiveMPortugal_Login
{
    public partial class Form1 : Form

    {
        string DATABASEIP = "localhost";
        string DATABASEUSER = "root";
        string DATABASEPASS = "";
        string DATABASENAME = "essentialmode";

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        string externalip = new System.Net.WebClient().DownloadString("http://icanhazip.com");

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void Form1_Shown(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://discord.io/mack");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstring = @"server=" + DATABASEIP + ";userid=" + DATABASEUSER + ";password=" + DATABASEPASS + ";database=" + DATABASENAME;
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();

                string query = "SELECT * FROM loginlauncher_users WHERE username ='" + textBox1.Text + "'AND password ='" + textBox3.Text + "'";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "table_name");
                DataTable dt = ds.Tables["table_name"];



                if (dt.Rows.Count >= 1)
                {

         

                    Console.WriteLine(dt.Rows[0][2].ToString());
                    if (dt.Rows[0][2].ToString() == ""){
                        MySqlCommand comm = conn.CreateCommand();
                        comm.CommandText = "UPDATE loginlauncher_users SET externalip = @ip WHERE username = @username;";
                        comm.Parameters.Add("ip", MySqlDbType.VarChar).Value = externalip;
                        comm.Parameters.Add("username", MySqlDbType.VarChar).Value = textBox1.Text;

                        comm.ExecuteNonQuery();

                    }
                    else
                    {


                    }

                    bool IsAllowed = false;

                    if (dt.Rows[0][2].ToString() == externalip.ToString())
                    {
                        IsAllowed = true;

                    }
                    else
                    {

                    }

                    MySqlCommand comm2 = conn.CreateCommand();
                    comm2.CommandText = "UPDATE loginlauncher_users SET IsAllowed = @IsAllowed WHERE username = @username;";
                    comm2.Parameters.Add("IsAllowed", MySqlDbType.Bit).Value = IsAllowed;
                    comm2.Parameters.Add("username", MySqlDbType.VarChar).Value = textBox1.Text;

                    comm2.ExecuteNonQuery();


                    foreach (DataRow row in dt.Rows)
                    {
                        FiveMPortugal.Properties.Settings.Default.name = row[0].ToString();
                        this.Opacity = 0;
                        this.Visible = false;
                        this.ShowInTaskbar = false;
                        this.ShowIcon = false;

                        Form2 f2 = new Form2();
                        f2.Show();

                    }
                }
                else {  }

            }
            catch (Exception)
            {
               
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }






        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connstring = @"server=" + DATABASEIP + ";userid=" + DATABASEUSER + ";password=" + DATABASEPASS + ";database=" + DATABASENAME;
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();

                string query = "SELECT * FROM loginlauncher_users WHERE username ='" + textBox1.Text + "'AND password ='" + textBox3.Text + "' AND IsAdmin =1";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "table_name");
                DataTable dt = ds.Tables["table_name"];

                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("התחברת כמנהל בהצלחה", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Opacity = 0;
                    this.Visible = false;
                    this.ShowInTaskbar = false;
                    this.ShowIcon = false;

                }
                else { MessageBox.Show("!הפרטים שהוזנו לא תקינים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
            catch (Exception)
            {
                MessageBox.Show("!אירעה שגיאה במסד הנתונים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
