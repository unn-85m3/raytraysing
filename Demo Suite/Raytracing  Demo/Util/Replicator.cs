using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SceneEditor
{
	/// <summary> ��������������� ����� ��� ����������� ��������. </summary>
	public class Replicator
	{
		#region Public Methods
		
		/// <summary> ���������� ������ (��������) ����������� �������. </summary>
		public static object Copy(object value)
		{
			object result = null;
			
			using (MemoryStream memoryStream = new MemoryStream())
			{
				IFormatter formatter = new  BinaryFormatter();
				
				formatter.Serialize(memoryStream, value);
				
				memoryStream.Position = 0;
				
				result = formatter.Deserialize(memoryStream);
				
				memoryStream.Close();
			}
			
			return result;
		}
		
		#endregion
	}
}
