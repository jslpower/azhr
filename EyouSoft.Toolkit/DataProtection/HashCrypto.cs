using System;
using System.Text;
using System.Security.Cryptography;
namespace EyouSoft.Toolkit.DataProtection
{
	/// <summary>
	/// ����ģ��
	/// Author:��־�� 2007-11-06 
	/// </summary>
    [Serializable]
	public class HashCrypto:IDisposable,IHashCrypto
	{
		private bool _IsDispose = false;
		#region ��ʼ��
		public HashCrypto()
		{}
		/// <summary>
		/// ��������
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if(!this._IsDispose)
			{
				if(disposing)
				{					
					GC.Collect();
				}
			}
			_IsDispose = true;

		}
		~HashCrypto()      
		{
			Dispose(false);
		}
		#endregion
		#region �����ͱ���
		
		private string _Key = null;
		private string _IV = null;

		#endregion		

		#region ���캯��
		/// <summary>
		/// ���� ��Կ
		/// </summary>
		public string Key
		{
			set{_Key=value;}
			get{return _Key;}
		}
		/// <summary>
		/// ��ʼ����
		/// </summary>
		public string IV
		{
			set{ _IV=value;}
			get{return _IV;}
		}
		#endregion

		#region ȫ�ַ���

		/// <summary>
		/// 32λMD5 ����
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		public string MD5Encrypt(string inputString)
		{
			if(inputString==null || inputString=="")
			{
				throw new System.Exception("Ҫ���ܵ��ַ�������Ϊ��");
			}
			try
			{
				MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
				return BitConverter.ToString(hashMD5.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-","").ToLower();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("���ܹ����з�������:" + e.Message);
			}
			
		}
		/// <summary>
		/// 16λMD5 ����
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <param name="hashLength">���ܳ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		public string MD5Encrypt16(string inputString)
		{
			if(inputString==null || inputString=="")
			{
				throw new System.Exception("Ҫ���ܵ��ַ�������Ϊ��");
			}
			return MD5Encrypt(inputString).Substring(8,16);
		}
		/// <summary>
		/// SHA�㷨��Ĭ��ΪSHA1
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		public string SHAEncrypt(string inputString)
		{
			if(inputString==null || inputString=="")
			{
				throw new System.Exception("Ҫ���ܵ��ַ�������Ϊ��");
			}
			try
			{
				SHA1CryptoServiceProvider hashSHA = new SHA1CryptoServiceProvider();
				return BitConverter.ToString(hashSHA.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-","").ToLower();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("���ܹ����з�������:" + e.Message);
			}
		}
		/// <summary>
		/// ����SHA�㷨 Ĭ�� 256
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <param name="HashLength">���ܳ��� �ɷ�Ϊ 128,256,384,512 �⼸�ֳ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		public string SHAEncrypt(string inputString,int HashLength)
		{
			try
			{
				switch(HashLength)
				{
					case 128:
						return SHAEncrypt(inputString);
					case 384:
						SHA384Managed hashSHA384 = new SHA384Managed();
						return BitConverter.ToString(hashSHA384.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-","").ToLower();
					case 512:
						SHA512Managed hashSHA512 = new SHA512Managed();
						return BitConverter.ToString(hashSHA512.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-","").ToLower();
					default:
						SHA256Managed hashSHA256 = new SHA256Managed();
						return BitConverter.ToString(hashSHA256.ComputeHash(Encoding.Default.GetBytes(inputString))).Replace("-","").ToLower();
				}
			}
			catch (System.Exception e)
			{
				throw new System.Exception("SHA���ܹ����з�������:" + e.Message);
			}
		}
		/// <summary>
		/// DES ����
		/// </summary>
		/// <param name="Values">Ҫ���ܵ��ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string DESEncrypt(string Values)
		{
			try
			{
				DESCryptoServiceProvider DesHash = new DESCryptoServiceProvider();
				DesHash.Mode = CipherMode.CBC;
				byte[] byt;
				if(null == this._Key)
				{
					DesHash.GenerateKey();
					_Key = Encoding.ASCII.GetString(DesHash.Key);
				}
				else
				{
					if(_Key.Length>8)
					{
						_Key = _Key.Substring(0,8);
					}
					else
					{
						for(int i=8;i<_Key.Length;i--)
							_Key += "0";
					}
				}
				if(null == this._IV)
				{
					DesHash.GenerateIV();
					_IV = Encoding.ASCII.GetString(DesHash.IV);
				}
				else
				{
					if(_IV.Length>8)
					{
						_IV = _IV.Substring(0,8);
					}
					else
					{
						for(int i=8;i<_IV.Length;i--)
							_IV += "0";
					}
				}
				//return _Key + "��" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "��" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
				byt = Encoding.UTF8.GetBytes(Values);
				ICryptoTransform ct = DesHash.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				DesHash.Clear();
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Write);
				cs.Write(byt,0,byt.Length);
				cs.FlushFinalBlock();
				cs.Close();
				return Convert.ToBase64String(ms.ToArray());
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// DES ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string DeDESEncrypt(string Values)
		{
			try
            {
                

                byte[] buffer = Convert.FromBase64String(Values);
				DESCryptoServiceProvider DesHash = new DESCryptoServiceProvider();
				DesHash.Mode = CipherMode.CBC;

                #region ����key ,iv
                if (null == this._Key)
                {
                    DesHash.GenerateKey();
                    _Key = Encoding.ASCII.GetString(DesHash.Key);
                }
                else
                {
                    if (_Key.Length > 8)
                    {
                        _Key = _Key.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _Key.Length; i--)
                            _Key += "0";
                    }
                }
                if (null == this._IV)
                {
                    DesHash.GenerateIV();
                    _IV = Encoding.ASCII.GetString(DesHash.IV);
                }
                else
                {
                    if (_IV.Length > 8)
                    {
                        _IV = _IV.Substring(0, 8);
                    }
                    else
                    {
                        for (int i = 8; i < _IV.Length; i--)
                            _IV += "0";
                    }
                }
                #endregion


				System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
				ICryptoTransform ct = DesHash.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				DesHash.Clear();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Read);
				System.IO.StreamReader sw = new System.IO.StreamReader(cs);
				return sw.ReadToEnd();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// 3 DES���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string TripleDesEncrypt(string Values)
		{
			try
			{
				TripleDES des3 = new TripleDESCryptoServiceProvider();
				des3.Mode = CipherMode.CBC;
				byte[] byt;
				//Key �� IV Ϊ 16��24�ֽ�
				if(null == this._Key)
				{
					des3.GenerateKey();
					_Key = Encoding.ASCII.GetString(des3.Key);
				}
				else
				{
					if(_Key.Length != 16 && _Key.Length != 24)
						throw new System.Exception("�������ݳ���,��ϸԭ��Key�ĳ��Ȳ�Ϊ 16 �� 24 byte.");
				}
				if(null == this._IV)
				{
					des3.GenerateIV();
					_IV = Encoding.ASCII.GetString(des3.IV);
				}
				else
				{
					if(_IV.Length != 8)
						throw new System.Exception("�������ݳ���,��ϸԭ��IV�ĳ��Ȳ�Ϊ 8 byte.");
				}
				//return _Key + "��" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "��" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
				byt = Encoding.UTF8.GetBytes(Values);
				ICryptoTransform ct = des3.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				des3.Clear();
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Write);
				cs.Write(byt,0,byt.Length);
				cs.FlushFinalBlock();
				cs.Close();
				return Convert.ToBase64String(ms.ToArray());
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// 3 DES ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string DeTripleDesEncrypt(string Values)
		{
			try
			{
				byte[] buffer = Convert.FromBase64String(Values);
				TripleDES des3 = new TripleDESCryptoServiceProvider();
				des3.Mode = CipherMode.CBC;
				System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
				ICryptoTransform ct = des3.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				des3.Clear();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Read);
				System.IO.StreamReader sw = new System.IO.StreamReader(cs);
				return sw.ReadToEnd();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// Rijndael���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string RijndaelEncrypt(string Values)
		{
			try
			{
				Rijndael rijndael = new RijndaelManaged();
				rijndael.Mode = CipherMode.CBC;
				byte[] byt;
				//Key �� IV Ϊ 16��24�ֽ�
				if(null == this._Key)
				{
					rijndael.GenerateKey();
					_Key = Encoding.ASCII.GetString(rijndael.Key);
				}
				else
				{
					if(_Key.Length != 32)
						throw new System.Exception("�������ݳ���,��ϸԭ��Key�ĳ��Ȳ�Ϊ 32 byte.");
				}
				if(null == this._IV)
				{
					rijndael.GenerateIV();
					_IV = Encoding.ASCII.GetString(rijndael.IV);
				}
				else
				{
					if(_IV.Length != 16)
						throw new System.Exception("�������ݳ���,��ϸԭ��IV�ĳ��Ȳ�Ϊ 16 byte.");
				}
				//return _Key + "��" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "��" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
				byt = Encoding.UTF8.GetBytes(Values);
				ICryptoTransform ct = rijndael.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				rijndael.Clear();
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Write);
				cs.Write(byt,0,byt.Length);
				cs.FlushFinalBlock();
				cs.Close();
				return Convert.ToBase64String(ms.ToArray());
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// Rijndael ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string DeRijndaelEncrypt(string Values)
		{
			try
			{
				byte[] buffer = Convert.FromBase64String(Values);
				Rijndael rijndael = new RijndaelManaged();
				rijndael.Mode = CipherMode.CBC;
				System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
				ICryptoTransform ct = rijndael.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				rijndael.Clear();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Read);
				System.IO.StreamReader sw = new System.IO.StreamReader(cs);
				return sw.ReadToEnd();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// RC2 ���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string RC2Encrypt(string Values)
		{
			try
			{
				RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
				rc2CSP.Mode = CipherMode.CBC;
				byte[] byt;
				//Key �� IV Ϊ 16��24�ֽ�
				if(null == this._Key)
				{
					rc2CSP.GenerateKey();
					_Key = Encoding.ASCII.GetString(rc2CSP.Key);
				}
				else{
					if(_Key.Length != 16)
						throw new System.Exception("�������ݳ���,��ϸԭ��Key�ĳ��Ȳ�Ϊ 16 byte.");
				}
				if(null == this._IV)
				{
					rc2CSP.GenerateIV();
					_IV = Encoding.ASCII.GetString(rc2CSP.IV);
				}
				else
				{
					if(_IV.Length != 8)
						throw new System.Exception("�������ݳ���,��ϸԭ��IV�ĳ��Ȳ�Ϊ 8 byte.");
				}
				//return _Key + "��" + Encoding.ASCII.GetBytes(_Key).Length.ToString() + "<br>"+ _IV + "��" + Encoding.ASCII.GetBytes(this._IV).Length.ToString();
				byt = Encoding.UTF8.GetBytes(Values);
				ICryptoTransform ct = rc2CSP.CreateEncryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				rc2CSP.Clear();
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Write);
				cs.Write(byt,0,byt.Length);
				cs.FlushFinalBlock();
				cs.Close();
				return Convert.ToBase64String(ms.ToArray());
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		/// <summary>
		/// RC2 ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		public string DeRC2Encrypt(string Values)
		{
			try
			{
				byte[] buffer = Convert.FromBase64String(Values);
				RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
				rc2CSP.Mode = CipherMode.CBC;
				System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer);
				ICryptoTransform ct = rc2CSP.CreateDecryptor(Encoding.ASCII.GetBytes(this._Key),Encoding.ASCII.GetBytes(this._IV));
				rc2CSP.Clear();
				CryptoStream cs = new CryptoStream(ms,ct,CryptoStreamMode.Read);
				System.IO.StreamReader sw = new System.IO.StreamReader(cs);
				return sw.ReadToEnd();
			}
			catch (System.Exception e)
			{
				throw new System.Exception("�������ݳ���,��ϸԭ��" + e.Message);
			}
		}
		#endregion
		
	}
}
