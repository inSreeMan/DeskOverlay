using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace DeskOverlay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {

        private Mutex _instanceMutex = null;

        protected override void OnExit(ExitEventArgs e)
        {
            if (_instanceMutex != null)
                _instanceMutex.ReleaseMutex();
            base.OnExit(e);
        }


        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;

        protected override void OnStartup(StartupEventArgs e)
        {
            // check that there is only one instance of the control panel running...
            bool createdNew;
            _instanceMutex = new Mutex(true, @"DeskOverlay.SMFrameworks.linwinsoft.com", out createdNew);
            if (!createdNew)
            {
                _instanceMutex = null;
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);
            getMainWindowToShow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            _notifyIcon.Icon = DeskOverlay.Properties.Resources.blackmonitor;
            _notifyIcon.Visible = true;

            CreateContextMenu();

            ShowMainWindow();
        }

        private void getMainWindowToShow()
        {
            if(JSettings.Mode == 1)
            {
                MainWindow = new TaskbarWindow() as DeskOverlay.TaskbarWindow;
            }
            else
            {
                MainWindow = new MainWindow() as DeskOverlay.MainWindow;
            }

        }

        private void getAppSettingsFromJson()
        {
            System.Windows.Forms.MessageBox.Show(JSettings.AppDataFile); 
        }

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip =
              new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        internal void ShowMainWindow()
        {
            ((iOverlayWindow)MainWindow).RefreshData();
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }
    }
}