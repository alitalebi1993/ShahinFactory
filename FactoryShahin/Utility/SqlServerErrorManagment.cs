using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQLEXEPTION
{
    public static class ErrorHandler
    {

        public static void SetError(ThreadExceptionEventArgs ErrorException, string PItemError, string PAction)
        {
            ErrorDescription = "";
            ItemError = PItemError;
            Action = PAction;
            ErrorDescriptionDetial = ErrorException.Exception.ToString();
            ErrorMessage = ErrorException.Exception.Message;

            if (ErrorException.GetType() == typeof(DbUpdateException))
            {
                ErrorDescription = SqlServerErrorManagment.ShowError(ErrorException.Exception as DbUpdateException, ItemError);
            }
            DisplayError();
        }

        public static string ErrorDescription { get; private set; }
        public static string ErrorDescriptionDetial { get; private set; }
        public static string ErrorMessage { get; private set; }

        private static string ItemError { get; set; }

        private static string Action { get; set; }

        public static void DisplayError()
        {
            string str = "";
            if (Action != string.Empty)
                str += " خطا در انجام عملیات " + Action + "\n";
            if (ItemError != string.Empty)
                str += " برروی " + ItemError + "\n";
            if (ErrorDescription != string.Empty)
                str += "علت خطا: " + ErrorDescription + "\n";

            System.Windows.Forms.MessageBox.Show(str, " خطا ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

        }

        public static void DisplayError(string p)
        {
            System.Windows.Forms.MessageBox.Show(p, " خطا ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
    public static class SqlServerErrorManagment
    {
        public static string ShowError(DbUpdateException ex, string EntityName)
        {
            int ErrNumber = (ex.InnerException.InnerException as SqlException).Number;
            if (ErrNumber == 2627)
            {
                return " اطلاعات " + EntityName + " تکراری است ";
            }
            if (ErrNumber == 547)
            {
                return " به دلیل اینکه اطلاعات " + EntityName + " در قسمت های دیگر استفاده شده است، اطلاعات قابل تغییر نیست ";
            }
            if (ErrNumber == 2)
            {
                return "ارتباط با اسکیو ال سرور بر قرار نمی شود";
            }
            return "خطا از بانک اطلاعاتی";
        }
    }
}
