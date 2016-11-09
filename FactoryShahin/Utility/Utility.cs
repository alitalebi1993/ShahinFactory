using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    class Utility
    {
        public static void SetDataSourcToComboBox<T>(ComboBox Combo) where T : class,new()
        {
            T Entity =new T();
            FactoryShahin.ShahinFactoryDb db = new FactoryShahin.ShahinFactoryDb();
            var list = db.Set<T>().ToList();
            Combo.DataSource = list;
            Combo.ValueMember = Entity.GetType().GetProperties().Where(p => p.Name.Contains("ID")).FirstOrDefault().Name;
            Combo.DisplayMember = Entity.GetType().GetProperties().Where(p => p.Name.Contains("Name")).FirstOrDefault().Name;
        }
        public static string ConvertDateToHijrie2(DateTime date)
        {
            PersianCalendar P = new PersianCalendar();
            string str = "" + (P.GetDayOfMonth(date) >= 10 ? "" : "0") + P.GetDayOfMonth(date) + "/" + (P.GetMonth(date) >= 10 ? "" : "0") + P.GetMonth(date) + "/" + P.GetYear(date);
            return str;

        }
        public static Image ConvertByteToImage(byte[] Bytes)
        {
            MemoryStream MS = new MemoryStream(Bytes);
            return Image.FromStream(MS);
        }
        public static bool CheckTextBox(List<System.Windows.Forms.TextBox> list)
        {
            foreach (var item in list)
            {
                if (item.Text == "" && item.AccessibleDescription != "" && item.AccessibleDescription!=null)
                {
                    MessageBox.Show(item.AccessibleDescription + " " + "نمیتواند خالی بماند !", "هشدار",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;

        }

        public static int CreatedNewID()
        {
            try
            {
                String str = Guid.NewGuid().ToString("N"), strID = "";
                int i = 0;
                while (strID.Length < 9)
                {
                    if (str[i] >= '0' && str[i] < '9')
                        strID += str[i];
                    i++;
                    if (i == str.Length)
                    {
                        str = Guid.NewGuid().ToString("N");
                        i = 0;
                    }
                }
                return Convert.ToInt32(strID);
            }
            catch (Exception)
            {
                return -1;
            }

        }
        public static string ConvertDateToHijrie3(DateTime date)
        {
            PersianCalendar P = new PersianCalendar();
            string str = "" + P.GetYear(date).ToString().Substring(2) + "/" + (P.GetMonth(date) >= 10 ? "" : "0") + P.GetMonth(date) + "/" + (P.GetDayOfMonth(date) >= 10 ? "" : "0") + P.GetDayOfMonth(date);
            return str;

        }
        public static string ConvertDateToHijrie(DateTime date)
        {
            PersianCalendar P = new PersianCalendar();
            string str = "" + P.GetYear(date) + "-" + (P.GetMonth(date) >= 10 ? "" : "0") + P.GetMonth(date) + "-" + (P.GetDayOfMonth(date) >= 10 ? "" : "0") + P.GetDayOfMonth(date);
            return str;

        }
        /// <summary>
        /// این تابع بین قیمت ها ویرگول می گذارد 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertPrice(String str)
        {
            int j = str.Length - 1;
            String st = "";
            for (int i = 0; i < str.Length; i++)
            {
                st += str[j];
                if ((i + 1) % 3 == 0 && j != 0)
                    st += ',';
                j--;
            }
            char[] a = st.ToCharArray().Reverse().ToArray();
            return new String(a);
        }
        /// <summary>
        /// این تابع بین قیمت ها ویرگول می گذارد 
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string ConvertPrice(long Num)
        {
            String str = Num.ToString();
            String st = "";
            int j = str.Length - 1;
            for (int i = 0; i < str.Length; i++)
            {
                st += str[j];
                if ((i + 1) % 3 == 0 && j != 0)
                    st += ',';
                j--;
            }
            char[] a = st.ToCharArray().Reverse().ToArray();
            return new String(a);
        }
        /// <summary>
        /// این تابع مقدار عدد رشته ای را که ویرگول دارد را به عدد صحیح تبدیل میکند
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ConvertCommaPrice(String str)
        {
            try
            {
                String st = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] >= '0' && str[i] <= '9')
                        st += str[i];
                }
                return Convert.ToInt64(st);
            }
            catch (FormatException)
            {
                return 0;
            }
        }
    }
    public class MyGridview : DataGridView
    {
        public MyGridview()
        {
            this.AutoGenerateColumns = false;
            this.BackgroundColor = System.Drawing.Color.White;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.RowHeadersVisible = false;
            this.AllowUserToAddRows = false;
            this.EnableHeadersVisualStyles = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.CellPainting += MyGridview_CellPainting;

        }

        void MyGridview_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                Image m = new Bitmap(FactoryShahin.Properties.Resources.image, new Size(e.CellBounds.Width, e.CellBounds.Height));
                e.Graphics.DrawImage(m, e.CellBounds);
                e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }
    }
}
