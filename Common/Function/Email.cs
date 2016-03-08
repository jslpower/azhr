using System;
namespace Adpost.Common.Function
{
	/// <summary>
	/// Email ��ժҪ˵����
	/// </summary>
	public class Email
	{
		private string _ToEmail;//�ռ���
		private string _FromEmail;//������
		private string _EmailSubject;//�ʼ�����
		private string _EmailBody;//�ʼ�����
		private string _SmtpServer;
		private bool _IsAuth = false;//�Ƿ�֧�ַ�������֤
		private string _EmailUserName;//��������֤���û�
		private string _EmailPassword;//��������֤���û�����
		public Email()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ģ��
		/// <summary>
		/// �����ռ��˵�ַ
		/// </summary>
		public string ToEmail
		{
			set{_ToEmail = value;}
			get{return _ToEmail;}
		}
		/// <summary>
		/// ������EMAIL
		/// </summary>
		public string FromEmail
		{
			set{_FromEmail = value;}
			get{return _FromEmail;}
		}
		/// <summary>
		/// �ʼ�����
		/// </summary>
		public string EmailSubject
		{
			set{_EmailSubject = value;}
			get{return _EmailSubject;}
		}
		/// <summary>
		/// �ʼ�����
		/// </summary>
		public string EmailBody
		{
			set{_EmailBody = value;}
			get{return _EmailBody;}
		}
		/// <summary>
		/// �����Ƿ��û���֤
		/// </summary>
		public bool IsAuth
		{
			set{ _IsAuth=value;}
			get{return _IsAuth;}
		}
		/// <summary>
		/// SMTP������
		/// </summary>
		public string SmtpServer
		{
			set{_SmtpServer = value;}
			get{return _SmtpServer;}
		}
		/// <summary>
		/// ��֤�û���
		/// </summary>
		public string EmailUserName
		{
			set{_EmailUserName = value;}
			get{return _EmailUserName;}
		}
		/// <summary>
		/// ��֤����
		/// </summary>
		public string EmailPassword
		{
			set{_EmailPassword = value;}
			get{return _EmailPassword;}
		}
		#endregion
		#region ����
		/// <summary>
		/// �����ʼ� 
		/// </summary>
		/// <returns>true�����ͳɹ���flase������ʧ��</returns>
		public bool SendEmail()
		{
			//����MailMessage����
			System.Web.Mail.MailMessage mailMsg = new System.Web.Mail.MailMessage();
			//�����ռ��˵��ʼ���ַ
			mailMsg.To = _ToEmail;
			//���÷����ߵ��ʼ���ַ
			mailMsg.From = _FromEmail;
			//�����ʼ�����
			mailMsg.Subject = _EmailSubject;
			//�����ʼ�����
			mailMsg.Body = _EmailBody;
			//����֧�ַ�������֤
			if(_IsAuth == true)
			{
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
				//�����û���
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",_EmailUserName);
				//�����û�����
				mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword",_EmailPassword);
			}
			try
			{
				//���÷����ʼ�������
				System.Web.Mail.SmtpMail.SmtpServer = _SmtpServer;
				//�����ʼ�
				System.Web.Mail.SmtpMail.Send(mailMsg);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
