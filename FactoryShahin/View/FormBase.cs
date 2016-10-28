using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Globalization;
using System.Runtime.InteropServices;
using FarsiCalendarComponent;
using Utility;
using System.Drawing.Drawing2D;
using System.IO;

namespace BaseForms
{
    public partial class FormBase : Form
    {
        public int _SelectedID = 0;
        string _Where = "1=1";
        public enum FormState { Selection, ListSelection, Register }
        FormState FState = FormState.Register;
        string _Query = "";
        Type _Type;
        //Type _DBCONTEXT;
        DbContext _DbContext;
        object _ParentObject;
        bool _Visible;
        public FormBase(/*Type DBCONTEXT, */DbContext DbContext, Type TType, string Where, object ParentObject, bool Visible, string FormName)
        {
            InitializeComponent();
            _DbContext = DbContext;
            _Type = TType;
            _ParentObject = ParentObject;
            this.Text = this.Text = FormName;
            _Visible = Visible;
            BtnCancel.BackgroundImage = FactoryShahin.Properties.Resources.Back;
            if (Where != "")
                _Where = Where;
            else
                _Where = "1=1";
            Initial();
        }
        public FormBase(DbContext DBCONTEXT, Type TType, string FormName, FormState STate)
        {
            InitializeComponent();
            _DbContext = DBCONTEXT;
            _Type = TType;
            this.Text = FormName;
            FState = STate;
            BtnCancel.BackgroundImage = FactoryShahin.Properties.Resources.Back;
            Initial();

        }
        private void GetBaseData()
        {

            GridView.AutoGenerateColumns = false;
            var list = _DbContext.Set(_Type).Select("new (" + _Query + ")").Where(_Where).ToListAsync();
            list.Wait();
            GridView.DataSource = list.Result;
        }
        public string GetAttributeFrom(string propertyName, object instance)
        {
            var attrType = typeof(DisplayNameAttribute);
            var property = instance.GetType().GetProperty(propertyName);
            DisplayNameAttribute DisplayAttribute = property.GetCustomAttributes(attrType, false).FirstOrDefault() as DisplayNameAttribute;
            if (DisplayAttribute != null)
            {
                return DisplayAttribute.DisplayName;
            }
            else
            {
                return "";
            }
        }
        private void Initial()
        {

            InitialGridViewe();
            InitialHederAndContext();
            SetNameToText();
            GetBaseData();
        }
        private void InitialGridViewe()
        {

            var Entity = Activator.CreateInstance(_Type);
            foreach (PropertyInfo item in Entity.GetType().GetProperties())
            {
                string HeaderText = "";
                string DataPropertyName = item.Name;
                string Name = item.Name;
                string btnText = "";
                if (_DbContext.GetType().GetProperties().ToList().FirstOrDefault(p => p.Name.Contains(item.PropertyType.Name)) == null)
                {
                    HeaderText = GetAttributeFrom(item.Name, Entity);
                }
                else
                {
                    btnText = GetAttributeFrom(item.Name, Entity);
                    if (btnText != "")
                    {
                        var C = Activator.CreateInstance(item.PropertyType);
                        string Cname = C.GetType().GetProperties().FirstOrDefault(p => p.Name.Contains("Name")).Name;
                        HeaderText = GetAttributeFrom(Cname, C);
                        DataPropertyName = DataPropertyName + "." + Cname;
                        Name = Cname;
                    }
                }
                if (HeaderText == "" && item.Name.Contains("ID"))
                {
                    HeaderText = "IDUnVisible";
                }
                if (HeaderText != "")
                {
                    DataGridViewColumn GVC;
                    if (Name.Contains("Date"))
                    {
                        GVC = new DataGridViewFarsiDatePickerColumn();
                        GVC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    else
                    {
                        GVC = new DataGridViewTextBoxColumn();
                    }
                    GVC.HeaderCell.Tag = btnText;
                    GVC.Tag = item.PropertyType;
                    GVC.DataPropertyName = GVC.Name = Name;
                    _Query = _Query + DataPropertyName + ",";
                    GVC.HeaderText = HeaderText;
                    GridView.Columns.Add(GVC);
                    if (HeaderText != "IDUnVisible")
                    {
                        GVC.Width = 135;
                        this.Width = this.Width + 135;
                    }
                    else
                    {
                        GVC.Visible = false;
                    }
                }
            }
            _Query = _Query.Substring(0, _Query.Length - 1);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            if (_Visible)
            {
                DataGridViewImageColumn GVIE = new DataGridViewImageColumn();
                GVIE.ImageLayout = DataGridViewImageCellLayout.Zoom;
                GVIE.Image = new Bitmap(FactoryShahin.Properties.Resources.Edit, new Size(40, 40));
                GVIE.Width = 60;
                GVIE.Name = "Edit";
                GVIE.HeaderText = "ویرایش";
                GridView.Columns.Add(GVIE);
            }
            DataGridViewImageColumn GVIR = new DataGridViewImageColumn();
            GVIR.Image = new Bitmap(FactoryShahin.Properties.Resources.Delete, new Size(40, 40));
            GVIR.ImageLayout = DataGridViewImageCellLayout.Zoom;
            GVIR.Width = 60;
            GVIR.Name = "Remove";
            GVIR.HeaderText = "حذف";
            GridView.Columns.Add(GVIR);
            if (FState == FormState.Selection)
            {
                DataGridViewImageColumn GVIS = new DataGridViewImageColumn();
                GVIS.ImageLayout = DataGridViewImageCellLayout.Zoom;
                GVIS.Image = new Bitmap(FactoryShahin.Properties.Resources.Pass, new Size(40, 40));
                GVIS.Width = 60;
                GVIS.Name = "Select";
                GVIS.HeaderText = "انتخاب";
                GridView.Columns.Add(GVIS);
                this.Width = this.Width + 60;
            }
            if (FState == FormState.ListSelection)
            {
                DataGridViewCheckBoxColumn GVCheck = new DataGridViewCheckBoxColumn();
                GVCheck.Width = 60;
                GVCheck.Name = "Select";
                GVCheck.HeaderText = "انتخاب";
                GridView.Columns.Add(GVCheck);
                this.Width = this.Width + 60;
            }
        }
        private void InitialHederAndContext()
        {
            var ConAlignment = ContentAlignment.MiddleLeft;
            foreach (DataGridViewColumn item in GridView.Columns)
            {
                if (item.HeaderText == "ویرایش" || item.HeaderText == "حذف" || item.HeaderText == "انتخاب")
                {
                    ConAlignment = ContentAlignment.MiddleCenter;
                }
                else
                {
                    {
                        Control Control;
                        if (item.DataPropertyName.Contains("Date"))
                        {
                            Control = new ArisaFarsiDatePicker();
                            var DatePicker = Control as ArisaFarsiDatePicker;
                            DatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
                            DatePicker.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold);
                            DatePicker.GeoDate = new System.DateTime(2016, 3, 29, 0, 0, 0, 0);
                            DatePicker.Location = new System.Drawing.Point(7, 25);
                            DatePicker.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
                            DatePicker.MaximumSize = new System.Drawing.Size(2166, 52);
                            DatePicker.Name = "DatePickerDate";
                            DatePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                            DatePicker.Size = new System.Drawing.Size(259, 41);
                            DatePicker.TabIndex = 83;
                            if (item.Name.Contains("Uniq"))
                            {
                                (Control as ArisaFarsiDatePicker).IsUniq = true;
                            }
                        }
                        else
                            if (item.DataPropertyName.Contains("Image"))
                            {
                                Control = new ArisaButtonImage();
                                Control.RightToLeft = RightToLeft.Yes;
                                var btn = (Control as Button);
                                btn.FlatStyle = FlatStyle.Flat;
                                btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                                btn.Text = item.HeaderText;
                                btn.ImageAlign = ContentAlignment.TopRight;
                                btn.Click += Control_Click;
                            }
                            else
                            {
                                if (_DbContext.GetType().GetProperties().ToList().FirstOrDefault(p => p.Name.Contains((item.Tag as Type).Name)) != null)
                                {
                                    Control = new Button();
                                    var btn = (Control as Button);
                                    btn.FlatStyle = FlatStyle.Flat;
                                    btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                                    btn.Text = item.HeaderCell.Tag.ToString();
                                    btn.ImageAlign = ContentAlignment.TopRight;
                                    btn.Click += Control_Click;
                                }
                                else
                                {
                                    Control = new ArisaTextBox();
                                    Control.ForeColor = System.Drawing.Color.LightSlateGray;
                                    Control.AccessibleDefaultActionDescription = "First";
                                    Control.Enter += Tbox_Enter;
                                    if ((item.Tag as Type) == typeof(Int32) || (item.Tag as Type) == typeof(Int64))
                                        Control.KeyPress += Tbox_KeyPressInt32;
                                    if (item.Name.Contains("Price"))
                                    {
                                        Control.TextChanged += Tbox_TextChanged;
                                    }
                                    if (item.Name.Contains("Uniq"))
                                    {
                                        (Control as ArisaTextBox).IsUniq = true;
                                    }
                                }
                            }
                        Control.AccessibleDescription = item.DataPropertyName;
                        Control.Name = item.HeaderText;
                        Control.Tag = (item.Tag as Type);
                        Control.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                        Control.Margin = new Padding(2, 0, 0, 0);
                        Control.Size = new Size(item.Width - 1, PanelHeder.Height);
                        Control.Visible = item.Visible;
                        PanelContent.Controls.Add(Control);
                    }
                }
                Label l = new Label();
                l.Margin = new Padding(0);
                l.Size = new Size(item.Width, PanelHeder.Height);
                l.Text = item.HeaderText;
                l.ForeColor = System.Drawing.Color.White;
                l.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                l.BackgroundImage = FactoryShahin.Properties.Resources.image;
                l.BackgroundImageLayout = ImageLayout.Stretch;
                l.TextAlign = ConAlignment;
                l.Visible = item.Visible;
                PanelHeder.Controls.Add(l);
            }
            PanelContent.Controls.Add(btnRegister);
        }
        private void Control_Click(object sender, EventArgs e)
        {
            Button BtnSelected = (sender as Button);
            Type T = BtnSelected.Tag as Type;
            if (T == typeof(byte[]))
            {
                GetImage(BtnSelected as ArisaButtonImage);
                return;
            }
            FormBase F = new FormBase(_DbContext, T, BtnSelected.Text + " انتخاب ", FormState.Selection);
            F.ShowDialog();
            if (F._SelectedID != 0)
            {
                BtnSelected.AccessibleName = F._SelectedID.ToString();
                BtnSelected.Image = new Bitmap(FactoryShahin.Properties.Resources.Pass, new Size(20, 20));
            }
            F.Dispose();
        }

        private void GetImage(ArisaButtonImage BtnSelected)
        {
            OpenFileDialog OF = new OpenFileDialog();
            OF.Filter = "All Files (*.jpg)|*.jpg";
            OF.FileName = "";
            if (OF.ShowDialog(this) == DialogResult.OK)
            {
                //string S = OF.FileName;
                //FTP F = new FTP("192.168.48.129", "Ftp_User", "M@123456");
                BtnSelected.FileStream = new FileStream(OF.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                //F.UploadFile(fs, @"//mansour4");
            }
        }
        private void SetNameToText()
        {
            PanelContent.Controls.OfType<TextBox>().ToList().ForEach(x =>
            {
                if (x.Name != "IDUnVisible")
                    x.Text = x.Name;
                else
                    x.Text = "0";
            });
        }
        private void Tbox_KeyPressInt32(object sender, KeyPressEventArgs e)
        {
            var TextBox = sender as TextBox;
            long meghdar = Utility.Utility.ConvertCommaPrice(TextBox.Text);
            if (TextBox.Text.Length >= 8)
            {
                TextBox.Text = TextBox.Text.Substring(0, 8);
                TextBox.Select(TextBox.Text.Length, 0);
            }
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && meghdar.ToString().Length > 9)
            {
                e.KeyChar = (char)0;
            }
        }

        private void Tbox_TextChanged(object sender, EventArgs e)
        {
            TextBox Tbox = sender as TextBox;
            if (Tbox.Text != "" && Tbox.Text != Tbox.Name)
            {
                Tbox.Text = Utility.Utility.ConvertPrice(Utility.Utility.ConvertCommaPrice(Tbox.Text).ToString());
                Tbox.Select(Tbox.Text.Length, 0);
            }
        }


        private void Tbox_Enter(object sender, EventArgs e)
        {
            TextBox Tbox = sender as TextBox;
            if (Tbox.AccessibleDefaultActionDescription == "First" && Tbox.ForeColor == System.Drawing.Color.LightSlateGray)
            {
                Tbox.AcceptsReturn = false;
                Tbox.Text = "";
                Tbox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void Register()
        {
            var Entity = Activator.CreateInstance(_Type);
            GetValue(Entity);
            _DbContext.Entry(Entity).State = EntityState.Added;
            _DbContext.SaveChanges();
        }
        private void GetValue(object Entity)
        {

            foreach (TextBox Tbox in PanelContent.Controls.OfType<TextBox>())
            {
                if (!Tbox.AccessibleDescription.Contains("Price"))
                    Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == Tbox.AccessibleDescription).SetValue(Entity, Convert.ChangeType(Tbox.Text, Type.GetType(Tbox.Tag.ToString())));
                else
                    Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == Tbox.AccessibleDescription).SetValue(Entity, Utility.Utility.ConvertCommaPrice(Tbox.Text));
            }
            foreach (ArisaButtonImage AB in PanelContent.Controls.OfType<ArisaButtonImage>())
            {
                MemoryStream mp = new MemoryStream();
                AB.FileStream.CopyTo(mp);
                byte[] m = mp.ToArray();
                Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == AB.AccessibleDescription).SetValue(Entity, m);
            }
            foreach (ArisaFarsiDatePicker DatePicker in PanelContent.Controls.OfType<ArisaFarsiDatePicker>())
            {
                Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == DatePicker.AccessibleDescription).SetValue(Entity, DatePicker.GeoDate.Value);
            }
            //foreach (Button Btn in PanelContent.Controls.OfType<Button>().Where(x => x.Tag != null))
            //{
            //    Entity.GetType().GetProperties().FirstOrDefault(p => p.Name == (Btn.Tag as Type).Name + "ID").SetValue(Entity, Convert.ToInt32(Btn.AccessibleName));
            //}

        }

        public void SetDataSourcToComboBox(ComboBox Combo, Type TP)
        {
            //var Entity = Activator.CreateInstance(TP);
            //DBCONTEXT db = new DBCONTEXT();
            //var list = db.Set<T>().ToList();
            //Combo.DataSource = list;
            //Combo.ValueMember = Entity.GetType().GetProperties().Where(p => p.Name.Contains("ID")).FirstOrDefault().Name;
            //Combo.DisplayMember = Entity.GetType().GetProperties().Where(p => p.Name.Contains("Name")).FirstOrDefault().Name;
        }
        private void Remove(int ID)
        {
            var Entity = Activator.CreateInstance(_Type);
            Entity.GetType().GetProperties().Where(p => p.Name.Contains("ID")).FirstOrDefault().SetValue(Entity, ID);
            _DbContext.Entry(Entity).State = EntityState.Deleted;
            _DbContext.SaveChanges();

        }
        private void Edit()
        {
            //if (_Type != typeof(Process))
            //{
                var Entity = Activator.CreateInstance(_Type);
                GetValue(Entity);
                _DbContext.Entry(Entity).State = EntityState.Modified;
                _DbContext.SaveChanges();
                btnRegister.BackgroundImage = FactoryShahin.Properties.Resources.tick2;
                btnRegister.AccessibleName = "Register";
                PanelContent.Controls.Remove(BtnCancel);
            //}
            //else
            //{
            //    var Entity = GridView.CurrentRow.DataBoundItem;
            //    _DbContext.Entry(Entity).State = EntityState.Modified;
            //    GetValue(Entity);
            //    _DbContext.SaveChanges();
            //    btnRegister.BackgroundImage = FactoryShahin.Properties.Resources.tick2;
            //    btnRegister.AccessibleName = "Register";
            //    PanelContent.Controls.Remove(BtnCancel);
            //}
            GridView.Tag = null;
        }
        private void SetValue()
        {
            PanelContent.Controls.Add(BtnCancel);
            PanelContent.Controls.OfType<TextBox>().ToList().ForEach(x => { x.Text = GridView.CurrentRow.Cells[x.AccessibleDescription].Value.ToString(); x.ForeColor = System.Drawing.Color.Black; });
            btnRegister.BackgroundImage = FactoryShahin.Properties.Resources.Edited;
            btnRegister.AccessibleName = "Edit";
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Register();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (Check() && CheckUniq())
            {
                if (btnRegister.AccessibleName == "Register")
                    Register();
                else
                    Edit();
                Clear();
                GetBaseData();
            }
        }
        private bool CheckUniq()
        {
            foreach (var C in PanelContent.Controls.OfType<ArisaTextBox>().Where(x => x.IsUniq).ToList())
            {
                if (GridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(x => x.Cells[C.AccessibleDescription].Value.ToString() == C.Text && (GridView.Tag != null ? x.Index != (GridView.Tag as DataGridViewRow).Index : true)) != null)
                {
                    Utility.BeheshtMBox.Show("اطلاعات وارد شده تکراری است !", "هشدار", Utility.BeheshtMBox.Icon.Warning, Utility.BeheshtMBox.MessageType.OK);
                    return false;
                }
            }
            foreach (var C in PanelContent.Controls.OfType<ArisaFarsiDatePicker>().Where(x => x.IsUniq).ToList())
            {
                if (GridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(x => Convert.ToDateTime((x.Cells[C.AccessibleDescription] as FarsiCalendarComponent.DataGridViewFarsiDatePickerCell).Value).Date == C.GeoDate.Value) != null)
                {
                    Utility.BeheshtMBox.Show("اطلاعات وارد شده تکراری است !", "هشدار", Utility.BeheshtMBox.Icon.Warning, Utility.BeheshtMBox.MessageType.OK);
                    return false;
                }
            }
            return true;
        }
        private void Clear()
        {
            foreach (var item in PanelContent.Controls.OfType<TextBox>().Where(x => x.Visible).ToList())
            {
                item.Text = item.Name;
                item.ForeColor = Color.LightSlateGray;
            }
            //foreach (ArisaButtonEntity AB in PanelContent.Controls.OfType<ArisaButtonEntity>())
            //{
            //    AB.Text = AB.EntityName;
            //    AB.Image = null;
            //}
            //foreach (ArisaButtonImage AB in PanelContent.Controls.OfType<ArisaButtonImage>())
            //{
            //    AB.EntityImage = null;
            //    AB.Image = null;
            //}
        }
        private bool Check()
        {
            var list = PanelContent.Controls.OfType<TextBox>().Where(x => x.Visible).ToList();
            foreach (var item in list)
            {
                if (item.Text == "" || (item.ForeColor == System.Drawing.Color.LightSlateGray))
                {
                    item.Focus();
                    Utility.BeheshtMBox.Show(" لطفا  " + item.Name + " را وارد کنید ! ", "اطلاع", Utility.BeheshtMBox.Icon.Info, Utility.BeheshtMBox.MessageType.OK);
                    return false;
                }
            }
            //foreach (ArisaButtonEntity AB in PanelContent.Controls.OfType<ArisaButtonEntity>())
            //{
            //    if (AB.Image == null)
            //    {
            //        AB.Focus();
            //        Utility.BeheshtMBox.Show(" لطفا  " + AB.Name + " را انتخاب کنید ! ", "اطلاع", Utility.BeheshtMBox.Icon.Info, Utility.BeheshtMBox.MessageType.OK);
            //        return false;
            //    }
            //}
            foreach (ArisaButtonImage AB in PanelContent.Controls.OfType<ArisaButtonImage>())
            {
                if (AB.FileStream == null)
                {
                    AB.Focus();
                    Utility.BeheshtMBox.Show(" لطفا " + AB.Name + " را انتخاب کنید ! ", "اطلاع", Utility.BeheshtMBox.Icon.Info, Utility.BeheshtMBox.MessageType.OK);
                    return false;
                }
            }

            return true;
        }
        private void CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.ColumnIndex == GridView.Columns["Remove"].Index)
            {
                RemoveEntity((Int32)GridView.CurrentRow.Cells[0].Value);
                GetBaseData();
            }
            if (e.ColumnIndex == GridView.Columns["Edit"].Index)
            {
                GridView.Tag = GridView.CurrentRow;
                SetValue();
            }
            if (FState == FormState.Selection)
                if (e.ColumnIndex == GridView.Columns["Select"].Index)
                {
                    _SelectedID = (Int32)GridView.CurrentRow.Cells[_Type.Name + "ID"].Value;
                    this.Close();
                }
        }

        private void RemoveEntity(int ID)
        {
            if (BeheshtMBox.Show("آیا مایل به حذف می باشید ؟", "سوال", BeheshtMBox.Icon.Question, BeheshtMBox.MessageType.YesNo) == BeheshtMBox.Result.Yes)
            {
                Remove(ID);
            }
        }
        private void EditEntity()
        {
            Edit();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            PanelContent.Controls.Remove(BtnCancel);
            btnRegister.BackgroundImage = FactoryShahin.Properties.Resources.tick2;
            btnRegister.Name = "Register";
            GridView.Tag = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 0x84:
        //            base.WndProc(ref m);
        //            if ((int)m.Result == 0x1)
        //                m.Result = (IntPtr)0x2;
        //            return;
        //    }

        //    base.WndProc(ref m);
        //}
        //private bool _dragging = false;
        //private Point _offset;
        //private Point _start_point = new Point(0, 0);


        //private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{

        //}

        //public const int WmNclbuttondown = 0xA1;
        //public const int HtCaption = 0x2;

        //[DllImport("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        //[DllImportAttribute("user32.dll")]
        //public static extern bool ReleaseCapture();


        //private void panel1_MouseDown(object sender, MouseEventArgs e)
        //{
        //if (e.Button == MouseButtons.Left)
        //{
        //    ReleaseCapture();
        //    SendMessage(Handle, WmNclbuttondown, HtCaption, 0);
        //}
        //}
        public const int WmNclbuttondown = 0xA1;
        public const int HtCaption = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WmNclbuttondown, HtCaption, 0);
            }

        }
        //private void panel1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    _dragging = true;  // _dragging is your variable flag
        //    _start_point = new Point(e.X, e.Y);
        //}

        //private void panel1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    _dragging = false;
        //}

        //private void panel1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (_dragging)
        //    {
        //        Point p = PointToScreen(e.Location);
        //        Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
        //    }
        //}
        ////////
        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HT_CAPTION = 0x2;

        //[DllImportAttribute("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //[DllImportAttribute("user32.dll")]
        //public static extern bool ReleaseCapture();

        //private void FormMain_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        ReleaseCapture();
        //        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        //    }
        //}
        ////////
    }
    public class ArisaTextBoxColoumn : DataGridViewTextBoxColumn
    {
        public ArisaTextBoxColoumn() { }
        public Type Type;
        public string PropertyName;
        public string ButtonText;
    }
    public class ArisaButtonImage : Button
    {
        public ArisaButtonImage() { }
        public Type Type;
        public string PropertyName;
        public string HeaderText;
        public string ButtonText;
        public FileStream FileStream;

    }
    public class ArisaTextBox : TextBox
    {
        public ArisaTextBox()
        {
            IsUniq = false;
        }
        public Type Type;
        public string PropertyName;
        public string HeaderText;
        public string ButtonText;
        public bool IsUniq;

    }
    public class ArisaFarsiDatePicker : FarsiCalendarComponent.FarsiDatePicker
    {
        public ArisaFarsiDatePicker()
        {
            IsUniq = false;
            base.GeoDate = DateTime.Now;
            base.CalendarControl.HeaderBackColor = System.Drawing.Color.White;
            base.CalendarControl.SelectedBackColor = System.Drawing.Color.Silver;
        }
        public bool IsUniq;
    }
    public class ArisaDatePicker : DataGridViewFarsiDatePickerColumn
    {
        public ArisaDatePicker() { }
        public Type Type;
        public string PropertyName;

    }
    public class ArisaPictureBox : PictureBox
    {
        public ArisaPictureBox()
        {
            this.BackColor = Color.DarkGray;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                this.Region = new Region(gp);
            }
        }
    }
}
