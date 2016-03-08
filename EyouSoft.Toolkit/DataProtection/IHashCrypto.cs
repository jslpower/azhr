using System;

namespace EyouSoft.Toolkit.DataProtection
{
	/// <summary>
	/// ���ܽ��ܻ���ӿ��ļ�
	/// Author:��־�� 2007-11-06
	/// </summary>
	public interface IHashCrypto
	{
		#region ���캯��
		/// <summary>
		/// ���� ��Կ
		/// </summary>
		string Key
		{
			set;
			get;
		}
		/// <summary>
		/// ��ʼ����
		/// </summary>
		string IV
		{
			set;
			get;
		}
		#endregion
		#region ȫ�ַ���
		/// <summary>
		/// 32λMD5 ����
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		string MD5Encrypt(string inputString);
		/// <summary>
		/// 16λMD5 ����
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <param name="hashLength">���ܳ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		string MD5Encrypt16(string inputString);
		/// <summary>
		/// SHA�㷨��Ĭ��ΪSHA1
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		string SHAEncrypt(string inputString);
		/// <summary>
		/// ����SHA�㷨 Ĭ�� 256
		/// </summary>
		/// <param name="inputString">Ҫ���ܵ��ַ���</param>
		/// <param name="HashLength">���ܳ��� �ɷ�Ϊ 128,256,384,512 �⼸�ֳ���</param>
		/// <returns>���ؼ��ܺ���ַ���</returns>
		string SHAEncrypt(string inputString,int HashLength);
		/// <summary>
		/// DES ����
		/// </summary>
		/// <param name="Values">Ҫ���ܵ��ַ���</param>
		/// <returns>Ҫ���ܵ��ַ���</returns>
		string DESEncrypt(string Values);
		/// <summary>
		/// DES ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string DeDESEncrypt(string Values);
		/// <summary>
		/// 3 DES���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string TripleDesEncrypt(string Values);
		/// <summary>
		/// 3 DES ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string DeTripleDesEncrypt(string Values);
		/// <summary>
		/// Rijndael���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string RijndaelEncrypt(string Values);
		/// <summary>
		/// Rijndael ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string DeRijndaelEncrypt(string Values);
		/// <summary>
		/// RC2 ���ݼ���
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string RC2Encrypt(string Values);
		/// <summary>
		/// RC2 ����
		/// </summary>
		/// <param name="Values">���ܺ���ַ���</param>
		/// <returns>���ܺ���ַ���</returns>
		string DeRC2Encrypt(string Values);
		#endregion
	}
}
