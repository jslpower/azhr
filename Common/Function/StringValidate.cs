using System;
using System.Text;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;

namespace EyouSoft.Common.Function
{
	/// <summary>
	/// StringValidate ��ժҪ˵����
	/// </summary>
	public class StringValidate
	{
		private string _UploadFileExt = "txt,gif,bmp,png,jpg,jpeg,doc,pdf,xls,rar,zip,tif";
		private static Regex RegInteger = new Regex("^[0-9]+$");
		private static Regex RegNumber = new Regex("^\\d+$");
		private static Regex RegNumberSign = new Regex("^[+-]?\\d+$");
		private static Regex RegIntegerSign = new Regex("^[+-]?[0-9]+$");
		private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]*$");
		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]*$"); //�ȼ���^[+-]?\d+[.]?\d+$
		private static Regex RegEmail = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
		private static Regex RegChinese = new Regex("[\u4e00-\u9fa5]");
		private static Regex RegPhone = new Regex("^((\\(\\d{2,3}\\))|(\\d{3}\\-))?(\\(0\\d{2,3}\\)|0\\d{2,3}-)?[1-9]\\d{6,7}(\\-\\d{1,4})?$");
		private static Regex RegURL = new Regex("^http:\\/\\/[A-Za-z0-9]+\\.[A-Za-z0-9]+[\\/=\\?%\\-&_~`@[\\]\\':+!]*([^<>\\\"\\\"])*$");
		private static Regex RegHtml = new Regex("<\\/*[^<>]*>");
		private static Regex RegLink = new Regex("<\\/a*[^<>]*>");
		private static Regex RegGUID = new Regex(@"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
        private static Regex RegIP = new Regex("^(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})$");
        /// <summary>
        /// С��ͨ����
        /// </summary>
        private static Regex RegPHSNo = new Regex(@"^0(([1-9]\d)|([3-9]\d{2}))\d{8}$");
        private static Regex RegMobileNo = new Regex(@"^(13|15|18|14)\d{9}$"); 
		private static readonly string DigitText = "��Ҽ��������½��ƾ�";
		private static readonly string PositionText = "Բʰ��Ǫ�f�|�׾������";
		private static readonly string OtherText = "�ֽ�����";
		public StringValidate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region model
		/// <summary>
		/// �ϴ��ļ�����ĸ�ʽ
		/// </summary>
		public string UploadFileExt
		{
			set{_UploadFileExt = value;}
			get{return _UploadFileExt;}
		}
		#endregion
		#region ����Ҵ�д���ת��
		/// <summary>
		/// �ֽǴ���
		/// </summary>
		/// <param name="num"></param>
		/// <param name="stack"></param>
		private static void GetFractionStack(int num, Stack stack)
		{
			int fen, jiao = Math.DivRem(num, 10, out fen);
			if (fen != 0)
			{
				stack.Push(OtherText[0]);
				stack.Push(DigitText[fen]);
			}
			if (jiao != 0)
			{
				stack.Push(OtherText[1]);
				stack.Push(DigitText[jiao]);
			}
		}
		/// <summary>
		/// �������ִ���
		/// </summary>
		/// <param name="num"></param>
		/// <param name="position"></param>
		/// <param name="stack"></param>
		private static void GetIntegerStack(decimal num, int position, Stack stack)
		{
			if (num < 10000M)
			{
				int _num = Decimal.ToInt32(num);
				for (int i = 0, mod_10 = 0; i < 4; i++)
				{
					bool behindZero = mod_10 == 0;
					_num = Math.DivRem(_num, 10, out mod_10);
					if (mod_10 == 0)
					{
						if (behindZero)
							if (_num == 0)
								break;
							else
								continue;
					}
					else if (i > 0)
						stack.Push(PositionText[i]);
					stack.Push(DigitText[mod_10]);
				}
			}
			else
			{
				GetIntegerStack(Decimal.Remainder(num, 10000M), position, stack);

				int mask = -1, offset = 4;
				while ((position & (0x1 << ++mask)) == 0) ;
				mask += offset;
				while ((char)stack.Peek() == PositionText[offset++])
					stack.Pop();
				stack.Push(PositionText[mask]);

				GetIntegerStack(Decimal.Divide(num, 10000M), position + 1, stack);
			}
		}
		/// <summary>
		/// ���ת��
		/// </summary>
		/// <param name="input">������</param>
		/// <returns></returns>
		public static string MoneyFormatter(Decimal input)
		{
			Stack stack = new Stack(60);

			bool isNegate = input < Decimal.Zero;
			input = Decimal.Add(isNegate ? Decimal.Negate(input) : input, 0.005M);
			decimal integer = Decimal.Truncate(input);
			int fraction = Decimal.ToInt32(Decimal.Multiply(Decimal.Subtract(input, integer), 100M));
			if (fraction == 0)
				stack.Push(OtherText[2]);
			else
				GetFractionStack(fraction, stack);
			if (integer != Decimal.Zero)
			{
				stack.Push(PositionText[0]);
				GetIntegerStack(integer, 1, stack);
				if ((char)stack.Peek() == DigitText[0])
					stack.Pop();
			}
			else if (fraction == 0)
			{
				stack.Push(PositionText[0]);
				stack.Push(DigitText[0]);
			}
			if (isNegate)
				stack.Push(OtherText[3]);
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			foreach (object _s in stack)
				sb.Append(_s.ToString());
			return sb.ToString();
		}
		#endregion
		#region �ļ�������
		/// <summary>
		/// ��ʱ�������������ļ���
		/// </summary>
		/// <param name="FileExt">�ļ���׺��</param>
		/// <returns>�ļ���</returns>
		public static string CreateFileName(string FileExt)
		{
			//int RandNum;
			string PhotoName;
//			Random rnd = new Random();
//			RandNum = rnd.Next(1,10000);//����һ��10000���ڵ������
			PhotoName = DateTime.Now.ToString("yyyyMMddhhmmss") + Guid.NewGuid().ToString().Replace("-","") + "." + FileExt;
			return PhotoName;
		}
		/// <summary>
		/// ���ص��յ���������
		/// </summary>
		/// <returns></returns>
		public static string TodayUploadDirectory()
		{
			string FolderName;
			FolderName = DateTime.Now.ToString("yyMMdd");
			return FolderName;
		}
		/// <summary>
		/// ����ļ���׺��
		/// </summary>
		/// <param name="FileExt"></param>
		public bool CheckFileExt(string FileExt)
		{
			string tmpFileExt = _UploadFileExt;
			string[] strFileExt = tmpFileExt.Split(',');
			foreach(string fFileExt in strFileExt)
			{
				if(FileExt.ToLower().Trim() == fFileExt.Trim())
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// ����һ���ļ�
		/// </summary>
		/// <param name="FilePathName"></param>
		public bool CreateFile(string FilePathName)
		{
			try
			{
				FileInfo CreateFile = new FileInfo(FilePathName); //�����ļ� 
				if (!CreateFile.Exists)
				{
					FileStream FS = CreateFile.Create();
					FS.Close();
					CreateFile = null;
					return true;
				}
				else
				{
					CreateFile = null;
					return false;
				}				
			}
			catch
			{
				return false;
			}
		}
		/**/
		/// <summary> 
		/// ɾ�������ļ��м������ļ��к��ļ� 
		/// </summary> 
		/// <param name="FolderPathName"></param> 
		public void DeleParentFolder(string FolderPathName)
		{
			try
			{
				DirectoryInfo DelFolder = new DirectoryInfo(FolderPathName);
				if (DelFolder.Exists)
				{
					DelFolder.Delete();
				}
			}
			catch
			{
			}
		}
		/// <summary>
		/// �����ı����ļ�
		/// </summary>
		/// <param name="FilePathName"></param>
		/// <param name="WriteWord"></param>
		public void WriteTextToFile(string FilePathName, string WriteWord)
		{
			try
			{
				//�����ļ� 
				CreateFile(FilePathName);
				//�õ�ԭ���ļ������� 
				FileStream FileRead = new FileStream(FilePathName, FileMode.Open, FileAccess.ReadWrite);
				using(StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.UTF8))
				{
					using(StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.UTF8))
					{
						FileWrite.Write(WriteWord);
					}
				}
				FileRead.Close();
			}
			catch
			{
				// throw; 
			}
		}
		/// <summary> 
		/// ���ļ���׷������ 
		/// </summary> 
		/// <param name="FilePathName"></param> 
		public void AppendText(string FilePathName, string WriteWord)
		{
			try
			{
				//�����ļ� 
				CreateFile(FilePathName);
				//�õ�ԭ���ļ������� 
				FileStream FileRead = new FileStream(FilePathName, FileMode.Open, FileAccess.ReadWrite);
				using(StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default))
				{
					string OldString = FileReadWord.ReadToEnd().ToString();
					WriteWord = OldString + WriteWord;
					//���µ���������д�� 
					using(StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default))
					{
						FileWrite.Write(WriteWord);
					}
				}
				FileRead.Close();
			}
			catch
			{
				// throw; 
			}
		}

		/**/
		/// <summary> 
		/// ��ȡ�ļ������� 
		/// </summary> 
		/// <param name="FilePathName"></param> 
		public string ReadFileData(string FilePathName)
		{
			string TxtString = "";
			try
			{

				FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.Read);
				using(StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default))
				{
					TxtString = FileReadWord.ReadToEnd().ToString();
				}
				FileRead.Close();
				return TxtString;
			}
			catch
			{
				throw;
			}
		}
		/// <summary>
		/// ɾ���ļ�
		/// </summary>
		/// <param name="FilePath"></param>
		/// <returns></returns>
		public void FileDel(string FilePath)
		{
			try 
			{
				FileInfo objFile = new FileInfo(FilePath);
				if(objFile.Exists)//�������
				{
					//ɾ���´������ļ�.
					objFile.Delete();
				}

			} 
			catch 
			{
			}
		}
		/// <summary>
		/// ɾ���ļ�
		/// </summary>
		/// <param name="FilePath"></param>
		/// <returns></returns>
		public static bool FileDelete(string FilePath)
		{
			try 
			{
				FileInfo objFile = new FileInfo(FilePath);
				if(objFile.Exists)//�������
				{
					//ɾ���´������ļ�.
					objFile.Delete();
					return true;
				}

			} 
			catch 
			{
				return false;
			}
			return false;
		}
		/// <summary>
		/// ����Ŀ¼
		/// </summary>
		/// <param name="DirectoryName">Ŀ¼��</param>
		/// <returns>��������, 1:Ŀ¼�Ѵ���,2:Ŀ¼����ʧ��,0:Ŀ¼�����ɹ�</returns>
		public static int CreateDirectory(string DirectoryName)
		{
			try
			{
				if(Directory.Exists(DirectoryName))
				{
					return 1;
				}//Ŀ¼�Ƿ����
				DirectoryInfo di = Directory.CreateDirectory(DirectoryName);
				return 0;
			}
			catch
			{
				return 2;
			}
		}

		#endregion
		#region ���ú���
		/// <summary>
		/// ȡ��ʱ����
		/// </summary>
		/// <param name="D1"></param>
		/// <param name="D2"></param>
		/// <param name="DateFormat"></param>
		/// <returns></returns>
		public static double DateDiff(DateTime StartDate,DateTime EndDate,string DateFormat)
		{
			double intDateDiff = 0;
			TimeSpan tmpSpan = EndDate - StartDate;
			switch(DateFormat.ToLower())
			{
				case "yyyy":
				case "yy":
				case "year":
					intDateDiff = EndDate.Year - StartDate.Year;
					break;
				case "m":
				case "mm":
				case "month":
					intDateDiff = (EndDate.Year - StartDate.Year)*12 + (EndDate.Month - StartDate.Month);
					break;
				case "h":
				case "hh":
				case "hour":
					intDateDiff = tmpSpan.TotalHours;
					break;
				case "n":
				case "mi":
				case "minute":					
					intDateDiff = tmpSpan.TotalMinutes;
					break;
				case "s":
				case "ss":
				case "second":
					intDateDiff = tmpSpan.TotalSeconds;
					break;
				case "ms":
				case "millisecond":
					intDateDiff = tmpSpan.TotalMilliseconds;
					break;
				default:
				case "d":
				case "dd":
				case "day":
					intDateDiff = tmpSpan.TotalDays;
					break;
			}
			return intDateDiff;
		}
		/// <summary>
		/// �Զ���SPLIT
		/// </summary>
		/// <param name="Content"></param>
		/// <param name="SplitString"></param>
		/// <returns></returns>
		public static string[] Split(string Content,string SplitString)
		{
			if(Content != null && Content != String.Empty)
			{
				string[] ResultString = Regex.Split(Content,SplitString,RegexOptions.IgnoreCase);
				return ResultString;
			}
			else{
				string[] ResultString = {null};
				return ResultString;
			}			
		}
		/// <summary>
		/// ����HTML����
		/// </summary>
		/// <param name="HtmlCode"></param>
		/// <returns></returns>
		public static string LoseHtml(string HtmlCode){
			string tmpStr = "";
			if(null != HtmlCode && String.Empty != HtmlCode){
				tmpStr = RegHtml.Replace(HtmlCode,"");
			}
			return tmpStr;
		}
		/// <summary>
		/// �������Ӵ���
		/// </summary>
		/// <param name="HtmlCode"></param>
		/// <returns></returns>
		public static string LoseLink(string HtmlCode)
		{
			string tmpStr = "";
			if(null != HtmlCode && String.Empty != HtmlCode)
			{				
				tmpStr = RegLink.Replace(HtmlCode,"");
			}
			return tmpStr;
		}
		
		/// <summary>
		/// ת�����Ϊ��д
		/// </summary>
		/// <param name="numVal"></param>
		/// <returns></returns>
		public static string ConvertNumAmtToChinese(decimal numVal)
		{
			decimal org = Math.Round(numVal,2);
			string orgData = org.ToString();
			int length = orgData.Length;
			int j = 0;
			string ret = string.Empty;
			string temp = string.Empty;
			//9,123,456,789,123.12
			for (int i=length-1;i>=0;i--)
			{
				temp = "";
				j++;
				switch (orgData[i])
				{
					case '.' : temp = "Ԫ";
						break;
					case '0' : temp = "��";
						break;
					case '1' : temp = "Ҽ";
						break;
					case '2' : temp = "��";
						break;
					case '3' : temp = "��";
						break;
					case '4' : temp = "��";
						break;
					case '5' : temp = "��";
						break;
					case '6' : temp = "½";
						break;
					case '7' : temp = "��";
						break;
					case '8' : temp = "��";
						break;
					case '9' : temp = "��";
						break;
					default : break;
				}
				switch(j)
				{
					case 1  : temp = temp + "��";
						break;
					case 2  : temp = temp + "��";
						break;
					case 3  : temp = temp + "";
						break;
					case 4  : temp = temp + "";
						break;
					case 5  : temp = temp + "ʰ";
						break;
					case 6  : temp = temp + "��";
						break;
					case 7  : temp = temp + "Ǫ";
						break;
					case 8  : temp = temp + "��";
						break;
					case 9  : temp = temp + "ʰ";
						break;
					case 10 : temp = temp + "��";
						break;
					case 11 : temp = temp + "Ǫ";
						break;
					case 12 : temp = temp + "��";
						break;
					case 13 : temp = temp + "ʰ";
						break;
					case 14 : temp = temp + "��";
						break;
					case 15 : temp = temp + "Ǫ";
						break;
					case 16 : temp = temp + "��";
						break;
					default: break;
				}    
				ret = temp + ret;
			}
   
			ret = ret.Replace("��ʰ","��");
			ret = ret.Replace("���","��");
			ret = ret.Replace("��Ǫ","��");
			ret = ret.Replace("������","��");
			ret = ret.Replace("����","��");
			ret = ret.Replace("������","��");
			ret = ret.Replace("���","��");
			ret = ret.Replace("���","��");
			ret = ret.Replace("����������Ԫ","��Ԫ");
			ret = ret.Replace("��������Ԫ","��Ԫ");
			ret = ret.Replace("��������","��");
			ret = ret.Replace("������Ԫ","��Ԫ");
			ret = ret.Replace("����Ԫ","��Ԫ");
			ret = ret.Replace("����","��");
			ret = ret.Replace("����","��");
			ret = ret.Replace("��Ԫ","Ԫ");
			ret = ret.Replace("����","��");
			return ret;
		}

		/// <summary>
		/// ����ַ����Ƿ��������е�һ��
		/// </summary>
		/// <param name="inputData"></param>
		/// <param name="arrData"></param>
		/// <returns></returns>
		public static bool IsStringExists(string inputData,string[] arrData)
		{
			if(null == inputData || string.Empty == inputData)
			{
				return false;
			}
			foreach(string tmpStr in arrData)
			{
				if(inputData == tmpStr)
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// ȡ�ÿͻ���IP����
		/// </summary>
		/// <returns>�ͻ���IP</returns>
		public static string GetRemoteIP()
		{
			string Remote_IP = "";
			try
			{
				if(HttpContext.Current.Request.ServerVariables["HTTP_VIA"]!=null)
				{ 
					Remote_IP=HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); 
				}
				else
				{ 
					Remote_IP=HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); 
				} 
			}
			catch
			{
			}
			return Remote_IP;
		}
		/// <summary>
		/// ���ַ�������HTML����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string HtmlEncode(string inputData)
		{
			return HttpUtility.HtmlEncode(inputData);
		}
		/// <summary>
		/// ��ʽ����ʾHTML
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string SafeHtmlEndcode(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("\"","&quot;");
				str = str.Replace("\n","\\n");
				str = str.Replace("\r","\\r");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string TextToHtml(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("\n","<br>").Replace(" ","&nbsp;").Replace("&quot;","\"");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// ��ʽ�� ' ��
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string CheckSql(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("'", "&#39");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// ȡ�õ����ݰ�ȫ��
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string SafeRequest(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("'", "&#39");
				str = str.Replace("<", "&lt;");
				str = str.Replace(">", "&gt;");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// ��ʽ���ַ�
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Encode(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("&", "&amp;");
				str = str.Replace("'", "&#39");
				str = str.Replace("\"", "&quot;");
				str = str.Replace(" ", "&nbsp;");
				str = str.Replace("<", "&lt;");
				str = str.Replace(">", "&gt;");
				str = str.Replace("\n", "<br>");
				str = str.Replace("\r", "</p><p>");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// ����ʽ���ַ�
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Decode(string str)
		{
			if(str != null && str != string.Empty)
			{
				str = str.Replace("<br>", "\n");
				str = str.Replace("&gt;", ">");
				str = str.Replace("&lt;", "<");
				str = str.Replace("&nbsp;", " ");
				str = str.Replace("&quot;", "\"");
				str = str.Replace("&#39", "'");
				str = str.Replace("&amp;", "&");
				str = str.Replace("</p><p>", "\r");
			}
			else
			{
				str = "";
			}
			return str;
		}
		/// <summary>
		/// ת���ַ�Ϊ bit ��
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		private static int ConvertStringToBit(string inputData)
		{
			if(null == inputData || "" == inputData)
			{
				return 0;
			}
			if("1" == inputData)
			{
				return 1;
			}
			return 0;
		}
		/// <summary>
		/// ȡ�ù̶����ȵ��ַ���
		/// </summary>
		/// <param name="sqlInput"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string StringTrimMid(string sqlInput, int maxLength)
		{
			if ((sqlInput != null) && (sqlInput != string.Empty))
			{
				sqlInput = sqlInput.Trim();
				if (sqlInput.Length > maxLength)
				{
					sqlInput = sqlInput.Substring(0, maxLength) + "...";
				}
			}
			return sqlInput;
		}
		/// <summary>
		/// label ��ֵ
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="txtInput"></param>
		public static void SetLabel(System.Web.UI.WebControls.Label lbl, string txtInput)
		{
			lbl.Text = HtmlEncode(txtInput);
		}
		/// <summary>
		/// label ��ֵ ����,����
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="inputObj"></param>
		public static void SetLabel(System.Web.UI.WebControls.Label lbl, object inputObj)
		{
			SetLabel(lbl, inputObj.ToString());
		}		
		/// <summary>
		/// �����·ݶ����
		/// </summary>
		/// <param name="ArrayMonthList"></param>
		/// <returns></returns>
		public static System.Data.DataTable CreateMonthTable(string[] ArrayMonthList)
		{
			System.Data.DataTable dt=new System.Data.DataTable();
			System.Data.DataRow dr;
			dt.Columns.Add(new System.Data.DataColumn("ReportMonth", typeof(DateTime)));
			foreach(string monthStr in ArrayMonthList)
			{
				dr = dt.NewRow();
				dr[0]=DateTime.Parse(DateTime.Now.Year + "-" + monthStr + "-1");
				dt.Rows.Add(dr);
			}
			return dt;
		}
		/// <summary>
		/// �����·ݶ����
		/// </summary>
		/// <param name="YearStr"></param>
		/// <param name="ArrayMonthList"></param>
		/// <returns></returns>
		public static System.Data.DataTable CreateMonthTable(string YearStr,string[] ArrayMonthList)
		{
			System.Data.DataTable dt=new System.Data.DataTable();
			System.Data.DataRow dr;
			dt.Columns.Add(new System.Data.DataColumn("ReportMonth", typeof(DateTime)));
			foreach(string monthStr in ArrayMonthList)
			{
				dr = dt.NewRow();
				dr[0]=DateTime.Parse(YearStr + "-" + monthStr + "-1");
				dt.Rows.Add(dr);
			}
			return dt;
		}
		/// <summary>
		/// ������ݶ����
		/// </summary>
		/// <param name="ArrayYearList"></param>
		/// <returns></returns>
		public static System.Data.DataTable CreateYearTable(string[] ArrayYearList)
		{
			System.Data.DataTable dt=new System.Data.DataTable();
			System.Data.DataRow dr;
			dt.Columns.Add(new System.Data.DataColumn("ReportYear", typeof(DateTime)));
			foreach(string YearStr in ArrayYearList)
			{
				dr = dt.NewRow();
				dr[0]=DateTime.Parse(YearStr + "-1-1");
				dt.Rows.Add(dr);
			}
			return dt;
		}
		/// <summary>
		/// SELECTѡ���ж�
		/// </summary>
		/// <param name="str1"></param>
		/// <param name="str2"></param>
		/// <returns></returns>
		public static string ItemIsEqual(string str1,string str2)
		{
			if(str1==str2)
			{
				return " selected";
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>
		/// ɾ����ַ����
		/// </summary>
		/// <param name="urlParams">��ַ����</param>
		/// <param name="UrlString">��Ҫɾ������</param>
		/// <returns></returns>
		public static string DeleteUrlString(NameValueCollection urlParams,string UrlString)
		{
			NameValueCollection newCol = new NameValueCollection(urlParams);
			NameValueCollection col = new NameValueCollection();
			string[] newColKeys = newCol.AllKeys;	
			StringBuilder sb = new StringBuilder();
			int i;
			for (i = 0; i < newColKeys.Length; i++)
			{
				if (newColKeys[i] != null)
				{
					newColKeys[i] = newColKeys[i].ToLower();					
				}			
			}
			for (i = 0; i < newCol.Count; i++)
			{
				if (newColKeys[i] != UrlString.ToLower() )
				{					
					sb.Append(newColKeys[i]);
					sb.Append("=");				
					sb.Append(System.Web.HttpContext.Current.Server.UrlEncode(newCol[i]));
					sb.Append("&");
				}
			}
			return sb.ToString();
		}
		#endregion
		#region �����жϺ���
		/// <summary>
		/// �����ж�
		/// </summary>
		/// <param name="inputData"></param>
		/// <param name="strRegex"></param>
		/// <returns></returns>
		public static bool IsRegexMatch(string inputData,string strRegex)
		{
			Regex strTmp = new Regex(strRegex);
			Match match1 = strTmp.Match(inputData);
			return match1.Success;
		}

        /// <summary>
        /// �ж��Ƿ�IP
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static bool IsIP(string IP)
        {
            if (!string.IsNullOrEmpty(IP))
            {
                Match match1 = RegIP.Match(IP);
                return match1.Success;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// �ж��Ƿ�Ϊ����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsNumber(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegNumber.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// �ж��Ƿ�Ϊ����������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsNumberSign(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegNumberSign.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}			
		}
		/// <summary>
		/// �ж��Ƿ����޷�������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsInteger(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegInteger.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// �ж��Ƿ��� �з�������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsIntegerSign(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegIntegerSign.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}			
		}
		/// <summary>
		/// �ж��Ƿ�Ϊ�޷��Ÿ�����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsDecimal(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegDecimal.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}
		}
		//�ж��Ƿ�Ϊ�з��Ÿ�����
		public static bool IsDecimalSign(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegDecimalSign.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}			
		}
		/// <summary>
		/// �ж��Ƿ��ǵ绰����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsPhone(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegPhone.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}				
		}

        /// <summary>
        /// �ж��Ƿ����ֻ������С��ͨ����
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsMobileOrPHS(string inputData)
        {
            return IsMobilePhone(inputData) || IsPHS(inputData);
        }

		/// <summary>
		/// �ж��Ƿ����ֻ�����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsMobilePhone(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegMobileNo.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}
        }
        /// <summary>
        /// �ж��Ƿ���С��ͨ����
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsPHS(string inputData)
        {
            if (inputData != null)
            {
                Match match = RegPHSNo.Match(inputData);
                return match.Success;
            }
            else
            {
                return false;
            }
        }
		/// <summary>
		/// �Ƕ��Ƿ�Ϊ����
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsChineseExist(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegChinese.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}				
		}
		/// <summary>
		/// �ж��Ƿ�ΪEMAIL��ʽ
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsEmail(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegEmail.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}	
			
		}
		/// <summary>
		/// �ж��Ƿ���URL��ʽ
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsUrl(string inputData)
		{
			if(inputData != null)
			{
				Match match1 = RegURL.Match(inputData);
				return match1.Success;
			}
			else
			{
				return false;
			}	
			
		}
		/// <summary>
		/// �ж��Ƿ�������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsDateTime(string inputData){
			try
			{
				System.DateTime tmpDate = System.DateTime.Parse(inputData);
				return true;
			}
			catch{
				return false;
			}
		}
		/// <summary>
		/// �ж��Ƿ���������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsIntegerArray(string inputData)
		{
			try
			{
				string[] tmpList = inputData.Split(',');
				foreach(string _str in tmpList)
				{
					if(!IsInteger(_str))
						return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// �ж��Ƿ�GUID
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsGUID(string inputData)
		{
			try
			{
				Match m = RegGUID.Match(inputData);
				if(m.Success)
				{
					//����ת��
					//Guid guid = new Guid(str);
					return true;
				}
				else
				{
					//����ת��
					return false;
				}
			}
			catch
			{
				return false;
			}
		}
		public static string BuildUrlString(NameValueCollection urlParams,string Params)
		{
			NameValueCollection newCol = new NameValueCollection(urlParams);
			NameValueCollection col = new NameValueCollection();
			string[] newColKeys = newCol.AllKeys;			
			int i;
			for (i = 0; i < newColKeys.Length; i++)
			{
				if (newColKeys[i] != null)
				{
					newColKeys[i] = newColKeys[i].ToLower();					
				}			
			}
			StringBuilder sb = new StringBuilder();
			for (i = 0; i < newCol.Count; i++)
			{
				if (newColKeys[i] != Params.ToLower() )
				{					
					sb.Append(newColKeys[i]);
					sb.Append("=");				
					sb.Append(System.Web.HttpContext.Current.Server.UrlEncode(newCol[i]));
					sb.Append("&");
				}
			}
			return sb.ToString();
		}

		public static string BuildUrlString(NameValueCollection urlParams,string[] Params) {
			NameValueCollection newCol = new NameValueCollection(urlParams);
			string[] newColKeys = newCol.AllKeys;			
			int i;
			for (i = 0; i < newColKeys.Length; i++) {
				if(newColKeys[i]!=null && newColKeys[i]!=string.Empty){
					newColKeys[i] = newColKeys[i].ToLower();		
				}
			}
			bool isEqual = false;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (i = 0; i < newColKeys.Length; i++) {
				if(newColKeys[i]==string.Empty || newColKeys[i]==null){
					continue;
				}
				for(int j=0;j<Params.Length;j++){
					if(newColKeys[i]==Params[j].ToLower()){
						isEqual = true;
						break;
					}
				}
				if (isEqual==false ) {					
					sb.Append(newColKeys[i]);
					sb.Append("=");				
					sb.Append(System.Web.HttpContext.Current.Server.UrlEncode(newCol[newColKeys[i]]));
					sb.Append("&");
				}
				isEqual = false;
			}
			return sb.ToString();
		}
		#endregion

        /// <summary>
        /// ȡ�ÿͻ���IP����
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string RemoteIP(System.Web.HttpRequest Request)
        {
            string Remote_IP = "";
            try
            {
                if (Request.ServerVariables["HTTP_VIA"] != null)
                {
                    Remote_IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    Remote_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
            }
            catch { }
            return Remote_IP;
        }

        /// <summary>
        /// ��ý���int��Ч����֤(����Ϊ����)���ֵ,��Ϊ����Ч��ֵ,�򷵻�0
        /// </summary>
        /// <param name="strValue">Ҫ������֤���ַ���ֵ</param>
        public static int GetIntValue(string strValue)
        {
            return GetIntValue(strValue, 0);
        }

        /// <summary>
        /// ���ַ���ת��Ϊ���� ��ֵ�������ֽ�����ָ��ֵ
        /// </summary>
        /// <param name="s">Ҫת�������ֵ��ַ���</param>
        /// <param name="defaultValue">ָ��ֵ</param>
        public static int GetIntValue(string s, int defaultValue)
        {
            if (!String.IsNullOrEmpty(s) && StringValidate.IsIntegerSign(s))
            {
                return Convert.ToInt32(s.Trim());
            }
            else
            {
                return defaultValue;
            }
        }

	}
}
