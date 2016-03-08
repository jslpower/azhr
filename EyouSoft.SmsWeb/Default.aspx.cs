using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*using System.Runtime.InteropServices;*/

namespace EyouSoft.SmsWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //toDoc();
            //toXls();

            /*string s = Request.QueryString["s"];
            if (s == "1")
            {
                toDoc();
            }
            else if (s == "2")
            {
                toXls();
            }*/
        }

        /*void toDoc()
        {
            object missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word._Application docApp = new Microsoft.Office.Interop.Word.Application();
            docApp.Visible = false;

            //模板文件
            object dotFileName = EyouSoft.Toolkit.Utils.GetMapPath("dot.doc");
            Microsoft.Office.Interop.Word._Document doc = docApp.Documents.Add(ref dotFileName, ref missing, ref missing, ref missing);

            //写入内容
            docApp.Selection.InsertFile(EyouSoft.Toolkit.Utils.GetMapPath("myword.doc"), ref missing, ref missing, ref missing, ref missing);

            //保存文件
            object savefileName = EyouSoft.Toolkit.Utils.GetMapPath("myword1.doc");
            doc.SaveAs(ref savefileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            doc.Close(ref missing, ref missing, ref missing);
            docApp.Quit(ref missing, ref missing, ref missing);

            doc = null;
            docApp = null;
        }

        void toXls()
        {
            Microsoft.Office.Interop.Excel.ApplicationClass xlsApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            if (xlsApp == null)
            {
                Console.WriteLine("未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook workbook = xlsApp.Workbooks.Open(EyouSoft.Toolkit.Utils.GetMapPath("kis.xls"), Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets[2] as Microsoft.Office.Interop.Excel.Worksheet;

            for (int i = 0; i < 10; i++)
            {
                worksheet.Cells[i + 2, 1] = "ABC";
                worksheet.Cells[i + 2, 2] = "2012-05-13";
                worksheet.Cells[i + 2, 3] = i + 10;
            }

            workbook.Saved = true;
            workbook.SaveCopyAs(EyouSoft.Toolkit.Utils.GetMapPath("kis0001.xls"));
            workbook.Close(true, Type.Missing, Type.Missing);

            xlsApp.Visible = false;
            xlsApp.Quit();

            IntPtr hwnd = new IntPtr(xlsApp.Hwnd);
            int processId = 0;
            GetWindowThreadProcessId(hwnd, out processId);
            System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessById(processId);
            process.Kill();

            worksheet = null;
            workbook = null;
            xlsApp = null;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID); */
    }
}
