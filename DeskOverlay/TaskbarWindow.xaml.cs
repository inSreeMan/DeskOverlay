using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeskOverlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TaskbarWindow : Window, iOverlayWindow
    {
        public TextDataLoader TextDataLoader { get; private set; }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //[NONE]
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS

        //https://docs.microsoft.com/en-us/windows/desktop/inputdev/virtual-key-codes
        private const uint VK_MYKEY = 0x13; //PAUSE Key

        private HwndSource source;

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_MYKEY)
                            {
                                //handle global hot key here...
                                if(this.Visibility == Visibility.Visible)
                                {
                                    this.Hide();
                                }
                                else
                                {
                                    showThisOverlayWindow();
                                }
                                
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void showThisOverlayWindow()
        {
            this.RefreshData();
            this.Topmost = true;
            if (this.IsVisible)
            {
                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                }
                
                this.Activate();
            }
            else
            {
                this.Show();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            IntPtr handle = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(handle);
            source.AddHook(HwndHook);

            RegisterHotKey(handle, HOTKEY_ID, 0, VK_MYKEY); //CTRL + CAPS_LOCK
        }
        public TaskbarWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;

            var smbginfoFile = JSettings.AppDataFile; //SMPaths.GetAppDataFile("smtaskbarinfo.txt");
            TextDataLoader = new TextDataLoader(smbginfoFile);

            this.lblDisplayText.FontSize = JSettings.Fontsize;

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }


        public void RefreshData()
        {
            JSettings.RefreshData();
            this.TextDataLoader.Refresh();
            this.lblDisplayText.Text = this.TextDataLoader.Data;


            this.lblDisplayText.FontSize = JSettings.Fontsize;
            // position TopRight,MiddleRight,BottomRight
            this.lblDisplayText.SetValue(Grid.RowProperty, JSettings.DisplayGridRow);
            this.lblDisplayText.SetValue(Grid.ColumnProperty, JSettings.DisplayGridColumn);

            //this.backfill.SetValue(Grid.RowProperty, JSettings.DisplayGridRow);
            //this.backfill.SetValue(Grid.ColumnProperty, JSettings.DisplayGridColumn);

            this.lblDisplayText.TextAlignment = JSettings.TextAlignment;

            var margin = JSettings.Margin;
            if (null != margin)
            {
                this.lblDisplayText.Margin = (Thickness)margin;
            }

            var textColor = JSettings.TextColor;
            if (null != textColor)
            {
                this.lblDisplayText.Foreground = (Brush)textColor;
            }

            //if (JSettings.ShowBackfill)
            //{
            //    var fillColor = JSettings.FillColor;
            //    if (null != fillColor)
            //    {
            //        this.backfill.Fill = (Brush)fillColor;
            //    }
            //    this.backfill.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.backfill.Visibility = Visibility.Hidden;
            //}
        }
    }
}
