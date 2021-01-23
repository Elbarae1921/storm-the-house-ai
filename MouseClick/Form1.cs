using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace MouseClick
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public Form1()
        {
            InitializeComponent();
        }

        /*private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue == (char)Keys.F1)
            {
                MessageBox.Show("You pressed A");
            }
        }*/

        public void TakeScreenshot()
        {
            Bitmap screenshot = new Bitmap(SystemInformation.VirtualScreen.Width,
                               SystemInformation.VirtualScreen.Height,
                               PixelFormat.Format32bppArgb);
            Graphics screenGraph = Graphics.FromImage(screenshot);
            screenGraph.CopyFromScreen(SystemInformation.VirtualScreen.X,
                                       SystemInformation.VirtualScreen.Y,
                                       0,
                                       0,
                                       SystemInformation.VirtualScreen.Size,
                                       CopyPixelOperation.SourceCopy);

            screenshot.Save("Screenshot.png", ImageFormat.Png);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n;
            bool stop = false;
            int found = 0;
            WindowState = FormWindowState.Minimized;
            Thread.Sleep(1000);
            int k = 0;
            while (k < 600)
            {
                TakeScreenshot();
                k++;
                Bitmap img = new Bitmap("Screenshot.png");
                for (int i = 871; i > 377; i--)
                {
                    for (int j = 704; j > 581; j--)
                    {
                        //Color pixel = img.GetPixel(i, j);
                        if (img.GetPixel(700, 700).GetBrightness() > 0.9)
                        {
                            LeftMouseClick(524, 400);
                            Thread.Sleep(500);
                            LeftMouseClick(524, 400);
                            Thread.Sleep(500);
                            LeftMouseClick(524, 400);
                            Thread.Sleep(500);
                            LeftMouseClick(524, 400);
                            Thread.Sleep(500);
                            for(n = 0; n < 50; n++)
                            {
                                LeftMouseClick(439, 400);
                                Thread.Sleep(50);
                            }
                            LeftMouseClick(700, 700);
                            stop = true;
                            break;
                        }
                        else if (img.GetPixel(i, j).GetBrightness() < 0.02)
                        {
                            LeftMouseClick(i , j );
                            LeftMouseClick(i-2 , j-2 );
                            found+=2;
                            j -= 21;
                            i -= 16;
                        }
                    }
                    if(stop == true)
                    {
                        stop = false;
                        break;
                    }
                }
                img.Dispose();
                if (File.Exists(@"Screenshot.png"))
                {
                    File.Delete(@"Screenshot.png");
                }
                if(found >= 7)
                {
                    SendKeys.SendWait(" ");
                    found = 0;
                }
                Thread.Sleep(1000);
            }

            
            
            

            /*WindowState = FormWindowState.Minimized;
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromMilliseconds(5000))
            {
                LeftMouseClick(590, 347);
                Thread.Sleep(0);
            }
            s.Stop();*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
