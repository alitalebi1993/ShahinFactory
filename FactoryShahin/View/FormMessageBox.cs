using System;
using System.Drawing;
using System.Windows.Forms;
using Utility;
using System.Runtime.InteropServices;

namespace View
{
    public partial class FormMessageBox : Form
    {
        string _Title, _Text;
        int _formHeight;
        BeheshtMBox.MessageType _MT;
        BeheshtMBox.Icon _Icon;
        public BeheshtMBox.Result Result = BeheshtMBox.Result.No;
        public FormMessageBox(string PTitle, string PText, BeheshtMBox.MessageType PMT, BeheshtMBox.Icon PIcon)
        {
            InitializeComponent();
            _Title = PTitle;
            _Text = PText;
            _MT = PMT;
            _Icon = PIcon;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FormMessageBox_Load(object sender, EventArgs e)
        {
            ColorForm();
            ConfigMessage();
            SetLocation();
            Resizing();
            SendKeys.Send("{TAB}");
        }

        private void SetLocation()
        {
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - _formHeight / 2);
        }

        private void ConfigMessage()
        {
            int LblTextLeft = LblText.Left;
            LblTitle.Text = _Title;
            LblText.Text = _Text;
            _formHeight = LblText.Size.Height + 120;
            Width = LblText.Size.Width + 100;
            LblTitle.MaximumSize = new Size(LblText.Size.Width, 24);
            LblTitle.Location = new Point(Width / 2 - LblTitle.Width / 2, LblTitle.Location.Y);
            if (LblText.Left != LblTextLeft)
                LblText.Left = LblTextLeft - 5;
            if (_MT == BeheshtMBox.MessageType.YesNo)
            {
                BtnCancel.Visible = true;
                BtnCancel.Select();
            }
            else
            {
                BtnOK.Select();
            }
            switch (_Icon)
            {
                case BeheshtMBox.Icon.Info: PicBoxIcon.Image = FactoryShahin.Properties.Resources.Info;
                    break;
                case BeheshtMBox.Icon.Warning: PicBoxIcon.Image = FactoryShahin.Properties.Resources.Warning;
                    break;
                case BeheshtMBox.Icon.Question: PicBoxIcon.Image = FactoryShahin.Properties.Resources.Question;
                    break;
            }
        }

        private void Resizing()
        {
            Height = 0;
            Timer T = new Timer();
            T.Interval = 7;
            T.Enabled = true;
            T.Tick += (s, e) =>
                {
                    if (Height >= _formHeight)
                    {
                        Height = _formHeight;
                        T.Enabled = false;
                        T.Dispose();
                    }
                    else
                    {
                        Height += 10;
                    }
                };
        }

        private void ColorForm()
        {
            BackColor = Color.FromArgb(255, 100, 100, 100);
            PanelMain.BackColor = Color.FromArgb(255, 130, 130, 130);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Result = BeheshtMBox.Result.No;
            FormClosing -= FormMessageBox_FormClosing;
            CloseForm();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            CkeckResult();
            FormClosing -= FormMessageBox_FormClosing;
            CloseForm();
        }

        private void CloseForm()
        {
            Timer T = new Timer();
            T.Interval = 7;
            T.Enabled = true;
            T.Tick += (s, e) =>
            {
                if (Height <= 10)
                {
                    Height = 0;
                    T.Enabled = false;
                    T.Dispose();
                    Close();
                }
                else
                {
                    Height -= 10;
                }
            };
        }

        private void FormMessageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void FormMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (BtnCancel.Focus())
                {
                    Result = BeheshtMBox.Result.No;
                    CloseForm();
                    return;
                }
                CkeckResult();
            }

        }

        private void CkeckResult()
        {
            if (_MT == BeheshtMBox.MessageType.YesNo)
                Result = BeheshtMBox.Result.Yes;
            else
                Result = BeheshtMBox.Result.OK;
        }
    }
}
