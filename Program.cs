using System;
using System.Windows.Forms;
using System.Drawing;

namespace TaskbarOnlyWinForm
{
    class TaskBarOnlyApp:Form
    {
        private NotifyIcon trayIcon;
        private bool isShieldIcon = true;
        private Icon shieldIcon = new Icon(SystemIcons.Shield, 40, 40);
        private Icon warningIcon = new Icon(SystemIcons.Warning, 40, 40);


        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            Opacity       = 0;     // See through, just in case.
            base.OnLoad(e);
        }

        [STAThread]
        static void Main()
        {
             Application.Run(new TaskBarOnlyApp());
        }

        public TaskBarOnlyApp(){
            SetupNotifyIcon();
        }
        private void SetupNotifyIcon(){
            trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = shieldIcon;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += (s, e) => OnDoubleClick(s,e);
            trayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Do Something?").Click += (s, e) => OnClick(s,e);
            trayIcon.ContextMenuStrip.Items.Add($"Exit").Click += (s, e) => OnExit(s,e);
            trayIcon.ShowBalloonTip(500, "Status", "Started Up!", ToolTipIcon.Info);
        }
        private void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("You did something!","Congrats!");
        }
        private void OnDoubleClick(object sender, EventArgs e)
        {
            isShieldIcon = !isShieldIcon;
            trayIcon.Icon = isShieldIcon ? shieldIcon : warningIcon;
        }
        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
