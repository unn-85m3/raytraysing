using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Auxiliary.Graphics;
using Auxiliary.MathTools;
using Auxiliary.Raytracing;

namespace Texture_Exporter
{
	/// <summary> Утилита для экспорта процедурных текстур в отдельные файлы. </summary>
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Procedure Texture Exporter\n");
			
			{
				AbstractTexture texture = new ChessBoardTexture1(Vector3D.Unit, Vector3D.Zero, Vector2D.Unit);
				
	        	using (FileStream fileStream = new FileStream("..//..//..//..//Support//Procedure Textures//ChessBoard1.prt",
				                                              FileMode.Create))
				{
					IFormatter formatter = new BinaryFormatter();
					
					try
					{
						formatter.Serialize(fileStream, texture);
					}
					catch
					{
						Console.Write("Error!");
					}
					
					fileStream.Close();
	        	}
			}
			
			Console.Write("Finished!");
			
			Console.ReadKey(true);
		}
	}
}