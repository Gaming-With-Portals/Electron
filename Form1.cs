using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
namespace WindowsSpotlight
{
    public partial class Form1 : Form
    {

           [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        bool IsOpen = false;
        public Form1()
        {

            InitializeComponent();

            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft");
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (reg != null)
            {
                if (reg.GetValue("Electron") == null)
                    MessageBox.Show("First time startup detected!", "Electron by Gaming With Portals.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reg.SetValue("Electron", Application.ExecutablePath.ToString());
            }
            else { 
                Debug.WriteLine("Value does exist");
            }
            //MessageBox.Show("Electron by Gaming With Portals.", "First time startup detected!", MessageBoxButtons.OK, MessageBoxIcon.Information);





            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.Opacity = 0;
            this.TopMost = false;
        }

        private void Form1_Deactivate(Object sender, EventArgs e)
        {

            MessageBox.Show("You are in the Form.Deactivate event.");
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Electrode.BalloonTipIcon = ToolTipIcon.Info;

        }


        private void Electrode_MouseDoubleClick(object sender, MouseEventArgs e)
        {



        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Electrode_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsOpen == false)
            {
                this.Opacity = 60;
                this.TopMost = true;
                IsOpen = true;
                this.Focus();
                textBox1.Focus();
            }
            else
            {
                IsOpen = false;
                this.Opacity = 0;
                this.TopMost = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.google.com/search?q=" + textBox1.Text);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    System.Diagnostics.Process.Start("https://www.google.com/search?q=" + textBox1.Text);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            IsOpen = false;
            this.Opacity = 0;
            this.TopMost = false;
        }

        private void Form1_Deactivate_1(object sender, EventArgs e)
        {
            IsOpen = false;
            this.Opacity = 0;
            this.TopMost = false;

        }
    }
}
