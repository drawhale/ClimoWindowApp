using Climo.Controller;
using Climo.Model;
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

namespace Climo
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _SAVE_TYPE = "local";    // local || db
        
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, System.Windows.Forms.Keys vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int WM_HOTKEY = 0x0312;

        // custom
        const int HOTKEY_ID_COPY = 31197;
        const int HOTKEY_ID_PASTE = 31198;
        const int HOTKEY_ID_PASTE1 = 31199;
        const int HOTKEY_ID_PASTE2 = 31200;
        const int HOTKEY_ID_PASTE3 = 31201;
        const int HOTKEY_ID_PASTE4 = 31202;
        const int HOTKEY_ID_PASTE5 = 31203;
        const int HOTKEY_ID_COPY1 = 31204;
        const int HOTKEY_ID_COPY2 = 31205;
        const int HOTKEY_ID_COPY3 = 31206;
        const int HOTKEY_ID_COPY4 = 31207;
        const int HOTKEY_ID_COPY5 = 31208;

        public enum KeyModifiers {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        private HwndSource _source;
        System.Windows.Forms.NotifyIcon _NotifyIcon;

        public static ClimoCollection ClimoItems { get; set; }

        private static IClimoController _ClimoContorller { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitNotifyIcon();

            if (_SAVE_TYPE == "db")
            {
                _ClimoContorller = new ClimoController(System.Configuration.ConfigurationManager.AppSettings["API_URL"].ToString());
            }
            else
            {
                _ClimoContorller = new ClimoLocalController();
            }
            
            InitClimoCollection();

            this.DataContext = this;
        }

        // Override event
        protected override void OnSourceInitialized(EventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(WndProc);
            RegisterHotKey();

            base.OnSourceInitialized(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_NotifyIcon != null)
            {
                _NotifyIcon.Visible = false;
            }

            UnregisterHotKey();

            base.OnClosing(e);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (_NotifyIcon != null && WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
            
            base.OnStateChanged(e);
        }
        // -- Override event
        private void InitClimoCollection()
        {
            List<ClimoItem> list = _ClimoContorller.GetList();
            ClimoItems = list.Count > 0 ? new ClimoCollection(5, list) : new ClimoCollection(5);
        }

        private void InitNotifyIcon()
        {
            try
            {
                _NotifyIcon = new System.Windows.Forms.NotifyIcon();
                _NotifyIcon.Icon = new System.Drawing.Icon("Main.ico");
                _NotifyIcon.Visible = true;
                _NotifyIcon.DoubleClick +=
                    delegate (object sender, EventArgs args)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    };
            }
            catch (Exception ex)
            {
                _NotifyIcon = null;
                Console.WriteLine(ex.Message);
            }
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID_COPY:
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(Clipboard.GetText()));
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_COPY1:
                            System.Threading.Thread.Sleep(500);
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(0, Clipboard.GetText()));
                                ClimoItems.ItemPasteIndex = 0;
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_COPY2:
                            System.Threading.Thread.Sleep(500);
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(1, Clipboard.GetText()));
                                ClimoItems.ItemPasteIndex = 1;
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_COPY3:
                            System.Threading.Thread.Sleep(500);
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(2, Clipboard.GetText()));
                                ClimoItems.ItemPasteIndex = 2;
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_COPY4:
                            System.Threading.Thread.Sleep(500);
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(3, Clipboard.GetText()));
                                ClimoItems.ItemPasteIndex = 3;
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_COPY5:
                            System.Threading.Thread.Sleep(500);
                            System.Windows.Forms.SendKeys.SendWait("^c");
                            System.Threading.Thread.Sleep(100);

                            if (Clipboard.GetText().Trim() != "")
                            {
                                _ClimoContorller.AddItem(ClimoItems.AddItem(4, Clipboard.GetText()));
                                ClimoItems.ItemPasteIndex = 45;
                                ReloadClimoCollection();
                            }

                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE:
                            SetClipboardByIndex(ClimoItems.ItemPasteIndex);
                            ClimoItems.IncrementPasteIndex();

                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE1:
                            SetClipboardByIndex(0);
                            ClimoItems.ItemPasteIndex = 1;

                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE2:
                            SetClipboardByIndex(1);
                            ClimoItems.ItemPasteIndex = 2;

                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE3:
                            SetClipboardByIndex(2);
                            ClimoItems.ItemPasteIndex = 3;

                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE4:
                            SetClipboardByIndex(3);
                            ClimoItems.ItemPasteIndex = 4;

                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                        case HOTKEY_ID_PASTE5:
                            SetClipboardByIndex(4);
                            ClimoItems.ItemPasteIndex = 0;
                            
                            System.Windows.Forms.SendKeys.SendWait("^v");
                            handled = true;
                            break;
                    }
                    break;
            }

            return IntPtr.Zero;
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY, KeyModifiers.Control, System.Windows.Forms.Keys.Q);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE, KeyModifiers.Control, System.Windows.Forms.Keys.W);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE1, KeyModifiers.Control, System.Windows.Forms.Keys.F1);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE2, KeyModifiers.Control, System.Windows.Forms.Keys.F2);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE3, KeyModifiers.Control, System.Windows.Forms.Keys.F3);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE4, KeyModifiers.Control, System.Windows.Forms.Keys.F4);
            RegisterHotKey(helper.Handle, HOTKEY_ID_PASTE5, KeyModifiers.Control, System.Windows.Forms.Keys.F5);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY1, KeyModifiers.Shift, System.Windows.Forms.Keys.F1);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY2, KeyModifiers.Shift, System.Windows.Forms.Keys.F2);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY3, KeyModifiers.Shift, System.Windows.Forms.Keys.F3);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY4, KeyModifiers.Shift, System.Windows.Forms.Keys.F4);
            RegisterHotKey(helper.Handle, HOTKEY_ID_COPY5, KeyModifiers.Shift, System.Windows.Forms.Keys.F5);
        }
        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE1);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE2);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE3);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE4);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_PASTE5);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY1);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY2);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY3);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY4);
            UnregisterHotKey(helper.Handle, HOTKEY_ID_COPY5);
        }

        private static void SetClipboardByIndex(int Index)
        {
            if (ClimoItems.Count > Index)
            {
                Clipboard.SetText(ClimoItems[Index].Text);
            }
            else
            {
                Clipboard.SetText("");
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string Text = ((TextBlock)sender).Text;
            Clipboard.SetText(Text);
        }

        private void TextLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = (((Label)sender).DataContext as ClimoItem).Index;
            ClimoItem removedItem = ClimoItems.RemoveItem(index);
            if (_SAVE_TYPE == "db")
            {
                _ClimoContorller.RemoveItem(removedItem.ID);
            }
            else
            {
                _ClimoContorller.RemoveItem(index);
            }

            ReloadClimoCollection();
        }

        private static void ReloadClimoCollection()
        {
            if(_SAVE_TYPE == "db")
            {
                List<ClimoItem> list = _ClimoContorller.GetList();

                foreach (ClimoItem item in ClimoItems)
                {
                    if (item.ID == 0)
                    {
                        item.ID = list.Find(x => x.Index == item.Index).ID;
                    }
                    else
                    {
                        item.Index = list.Find(x => x.ID == item.ID).Index;
                    }
                }
            }
        }
    }
}
