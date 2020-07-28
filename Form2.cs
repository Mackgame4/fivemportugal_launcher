using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiveMPortugal_Login
{
    public partial class Form2 : Form
    {
        string FIVEMIP = "31.220.40.228";
        string FIVEMPORT = "30120";

        /////////////////////////////////

        string TSIP = "teamspeak.servername";
        string TSPORT = "999";

        /////////////////////////////////

        string DISCORD = "https://discord.io/mack";
        string FORUM = "https://discord.io/mack";
        string PLUGINS = "https://discord.io/mack";

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
        public Form2()
        {
            InitializeComponent();
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
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        public bool GetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead($"http://{FIVEMIP}:{FIVEMPORT}/players.json"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        
        public int pCount()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString($"http://{FIVEMIP}:{FIVEMPORT}/players.json");
                int player = json.Length - json.Replace("{", "").Length;
                return player;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string externalip = new System.Net.WebClient().DownloadString("http://icanhazip.com");
            label1.Text = FiveMPortugal.Properties.Settings.Default.name;
            if(GetConnection() == false)
            {
                Console.WriteLine($"The Connection Is: {GetConnection()}");
                label5.Text = "OFF";
                label5.ForeColor = Color.FromArgb(200, 25, 25);
            }
            label8.Text = $"{pCount()} players";
            /*string queueCount = new System.Net.WebClient().DownloadString("http://barbaronn.xyz/api/queue.json");
            if (queueCount == "0")
            {
                label10.Text = "None";
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"fivem://connect/{FIVEMIP}:{FIVEMPORT}");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{DISCORD}");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"ts3server://{TSIP}?port={TSPORT}");
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{FORUM}");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"{PLUGINS}");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
