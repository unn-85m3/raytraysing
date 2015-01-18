using System;
using System.Drawing;
using System.Drawing.Imaging;
using Tao.OpenGl;

namespace Auxiliary.Graphics
{
	/// <summary> ��������� ��������. </summary>
	public class Texture2D
	{
		#region Private Fields
		
		/// <summary> ���������� ��������. </summary>
		private int handle;
				
		#endregion
		
		#region Public Fields
		
		/// <summary> ������ ��������. </summary>
		public int Width;
		
		/// <summary> ������ ��������. </summary>
		public int Height;
		
		/// <summary> ������ ��������. </summary>
		public int Format = Gl.GL_RGB;
		
		/// <summary> ���������� ������ ��������. </summary>
		public int InternalFormat = Gl.GL_RGB8;
		
		/// <summary> ��� ��������. </summary>
		public static readonly int Target = Gl.GL_TEXTURE_2D;
		
		#endregion
	
		#region Constructor
		
		/// <summary> ������� ����� �������� �� �������� ������ � ������. </summary>
		public Texture2D(int width, int height)
		{
			Width = width;
			Height = height;
			
		    unsafe
		    {
		    	fixed (int* pointer = &handle)
		    	{
		    		Gl.glGenTextures(1, new IntPtr(pointer));
		    	}
		    }
		}
		
		/// <summary> ������� ����� �������� �� �������� ������, ������ � �������. </summary>
		public Texture2D(int width, int height, int format, int internalFormat)
			: this(width, height)
		{
			Format = format;
			InternalFormat = internalFormat;
		}
			
		#endregion
		
		#region Public Methods
		
		/// <summary> ��������� � �������� ������ �� ������������� �������. </summary>
		public void Create(IntPtr pixels)
		{				
			Gl.glBindTexture(Target, handle);
		    		
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
		
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
		  
		    Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            Gl.glTexImage2D(Target, 0, InternalFormat, Width, Height, 0, Format,
                            Gl.GL_UNSIGNED_BYTE, pixels);
		}
		
		/// <summary> ��������� � �������� ������ �� ������������� �������. </summary>
		public void Create(float[] pixels)
		{				
			Gl.glBindTexture(Target, handle);
		    		
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
		
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
		  
		    Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            Gl.glTexImage2D(Target, 0, InternalFormat, Width, Height, 0, Format,
                            Gl.GL_FLOAT, pixels);
		}
		
		public unsafe void Create()
		{	
			Create(new IntPtr());
		}
			
		public void Destroy()
		{
		    unsafe
		    {
		    	fixed (int* pointer = &handle)
		    	{
		    		Gl.glDeleteTextures(1, new IntPtr(pointer));
		    	}
		    }
		}
		
		/// <summary> ������� ��������� �������� �� ������������ �����. </summary>
		public static Texture2D LoadFromImage(string fileName)
		{
			try
			{
				Bitmap image = new Bitmap(fileName);

                Texture2D texture = new Texture2D(image.Width, image.Height, Gl.GL_BGR, Gl.GL_RGB8);

                Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);

                BitmapData data = image.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                texture.Create(data.Scan0);

                image.UnlockBits(data);
			
				return texture;
			}
			catch
			{
				return null;
			}
		}
		
		#endregion
		
		#region Properties
				
		public int Handle
		{
			get
			{
				return handle;
			}
		}
		
		#endregion
	}
}
