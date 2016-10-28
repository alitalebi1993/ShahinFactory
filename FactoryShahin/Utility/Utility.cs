using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    
    class Utility
    {
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
                if (item.Text == "" && item.AccessibleDescription!="")
                {
                    BeheshtMBox.Show(item.AccessibleDescription+" "+"نمیتواند خالی بماند !", "هشدار", BeheshtMBox.Icon.Warning, BeheshtMBox.MessageType.OK);
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
}
