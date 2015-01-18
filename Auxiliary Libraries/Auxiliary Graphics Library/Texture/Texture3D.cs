using System;
using Tao.OpenGl;

namespace Auxiliary.Graphics
{
	/// <summary> ���������� ��������. </summary>
	public class Texture3D
	{
		#region Private Fields
		
		/// <summary> ������������� ��������. </summary>
		private int handle;
				
		#endregion
		
		#region Public Fields
		
		/// <summary> ������ ��������. </summary>
		public int Width;
	
		/// <summary> ������ ��������. </summary>
		public int Height;
		
		/// <summary> ������� ��������. </summary>
		public int Depth;		
		
		/// <summary> ������ ������� ��������. </summary>
		public int TexelFormat = Gl.GL_RGBA;
		
		/// <summary> ���������� ������������� �������. </summary>
		public int InternalFormat = Gl.GL_RGBA8;
		
		/// <summary> ��� �������� � OpenGL. </summary>
		public static int Target = Gl.GL_TEXTURE_3D;
		
		#endregion
		
		#region Constructors
		
		/// <summary> ������� ����� �������� �� �������� ������, ������ � �������. </summary>
		public Texture3D(int width, int height, int depth)
		{
			Width = width;
			Height = height;
			Depth = depth;
			
		    unsafe
		    {
		    	fixed (int* pointer = &handle)
		    	{
		    		Gl.glGenTextures(1, new IntPtr(pointer));
		    	}
		    }
		}
		
		/// <summary> ������� ����� �������� �� �������� ������, ������, ������� � �������. </summary>
		public Texture3D(int width, int height, int depth, int texelFormat, int internalFormat)
			: this(width, height, depth)
		{
			TexelFormat = texelFormat;
			InternalFormat = internalFormat;
		}
			
		#endregion
		
		#region Public Methods
		
		/// <summary> ������������� ������ ���������� ��������. </summary>
		public void SetTextureFilter(int minFilter, int magFilter)
		{
			Gl.glBindTexture(Target, handle);
			
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MAG_FILTER, magFilter);
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MIN_FILTER, minFilter);			
		}
		
		/// <summary> ������������� ��������� ��� ������ �� ������� ���������. </summary>
		public void SetTextureWrap(int wrapS, int wrapT)
		{
			Gl.glBindTexture(Target, handle);
			
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_S, wrapS);
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_T, wrapT);			
		}		
		
		/// <summary> ��������� ������ ���������� ������. </summary>
		public void UploadData()
		{	
			UploadData(null);
		}			
			
		/// <summary> ��������� ���������� ������ -- ������ ��������. </summary>
		public void UploadData(float[] pixels)
		{				
			Gl.glBindTexture(Target, handle);
		    			    
		    Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
		    
		    Gl.glTexImage3D(Target, 0, InternalFormat, Width, Height, Depth, 0, TexelFormat,
			                Gl.GL_FLOAT, pixels);
		}
		
		/// <summary> ������� ��� ������ � ��������. </summary>
		public void UnloadData()
		{
		    unsafe
		    {
		    	fixed (int* pointer = &handle)
		    	{
		    		Gl.glDeleteTextures(1, new IntPtr(pointer));
		    	}
		    }
		}		
		
		#endregion
		
		#region Properties
		
		/// <summary> ������������� ��������. </summary>
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
