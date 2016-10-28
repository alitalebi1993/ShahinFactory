using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Entity.Infrastructure;
using Utility;

namespace System
{
    public static class ErrorHandler
    {
        public static string ErrorDescription { get; private set; }
        public static string ErrorDeteialDescription { get; private set; }
        public static void GetError(Exception ex)
        {
            ErrorDeteialDescription = ex.ToString();
            ErrorDescription = " خطا ";
            if (ex.GetType() == typeof(FormatException))
            {
                ErrorDescription = " خطای فرمت " + "\n\n" + ErrorDeteialDescription;
            }
            else if (ex.GetType() == typeof(DivideByZeroException))
            {
                ErrorDescription = " خطای تقسیم بر صفر " + "\n\n" + ErrorDeteialDescription;
            }
            else if (ex.GetType() == typeof(System.Data.Entity.Core.EntityCommandExecutionException))
            {
                SqlException exsql = ex.InnerException as SqlException;
                GetSqlServerError(exsql);
            }
            else if (ex.GetType() == typeof(DbUpdateException))
            {
                SqlException exsql = ex.InnerException.InnerException as SqlException;
                GetSqlServerError(exsql);
            }
            else if (ex.GetType() == typeof(System.Data.Entity.Core.EntityException))
            {
                SqlException exsql = ex.InnerException as SqlException;
                GetSqlServerError(exsql);
            }
            else if (ex.GetType() == typeof(SqlException))
            {
                SqlException exsql = ex as SqlException;
                GetSqlServerError(exsql);
            }
            else if (ex.GetType() == typeof(InvalidOperationException))
            {
                ErrorDescription = " به دلیل اینکه اطلاعات انتخابی در قسمت های دیگر استفاده شده است، قابل تغییر نیست ";
            }
            else
                ErrorDescription += "\n\n" + ErrorDeteialDescription;
            ShowError();
        }

        private static void GetSqlServerError(SqlException exsql)
        {

            switch (exsql.Number)
            {
                case 2627: { ErrorDescription = " اطلاعات تکراری است "; break; }
                case 2: { ErrorDescription = " پایگاه داده متوقف است "; break; }
                case 4060: { ErrorDescription = " اجازه دسترسی ندارید "; break; }
                case 229: { ErrorDescription = " اجازه دسترسی ندارید "; break; }
                case 547: { ErrorDescription = " به دلیل اینکه اطلاعات انتخابی در قسمت های دیگر استفاده شده است، قابل تغییر نیست "; break; }
                default:
                    { ErrorDescription = "خطای پایگاه داده " + "\n\n" + ErrorDeteialDescription; break; }
            }
        }

        private static void ShowError()
        {
            MessageBox.Show(ErrorDescription, " خطا ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
