using System;
using Tao.OpenGl;

namespace Auxiliary.Graphics
{
	/// <summary> Трехмерная текстура. </summary>
	public class Texture3D
	{
		#region Private Fields
		
		/// <summary> Идентификатор текстуры. </summary>
		private int handle;
				
		#endregion
		
		#region Public Fields
		
		/// <summary> Ширина текстуры. </summary>
		public int Width;
	
		/// <summary> Высота текстуры. </summary>
		public int Height;
		
		/// <summary> Глубина текстуры. </summary>
		public int Depth;		
		
		/// <summary> Формат текселя текстуры. </summary>
		public int TexelFormat = Gl.GL_RGBA;
		
		/// <summary> Внутреннее представление текселя. </summary>
		public int InternalFormat = Gl.GL_RGBA8;
		
		/// <summary> Тип текстуры в OpenGL. </summary>
		public static int Target = Gl.GL_TEXTURE_3D;
		
		#endregion
		
		#region Constructors
		
		/// <summary> Создает новую текстуру по заданной ширине, высоте и глубине. </summary>
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
		
		/// <summary> Создает новую текстуру по заданной ширине, высоте, глубине и формату. </summary>
		public Texture3D(int width, int height, int depth, int texelFormat, int internalFormat)
			: this(width, height, depth)
		{
			TexelFormat = texelFormat;
			InternalFormat = internalFormat;
		}
			
		#endregion
		
		#region Public Methods
		
		/// <summary> Устанавливает способ фильтрации текстуры. </summary>
		public void SetTextureFilter(int minFilter, int magFilter)
		{
			Gl.glBindTexture(Target, handle);
			
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MAG_FILTER, magFilter);
			Gl.glTexParameteri(Target, Gl.GL_TEXTURE_MIN_FILTER, minFilter);			
		}
		
		/// <summary> Устанавливает поведение при выходе за границы координат. </summary>
		public void SetTextureWrap(int wrapS, int wrapT)
		{
			Gl.glBindTexture(Target, handle);
			
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_S, wrapS);
		    Gl.glTexParameteri(Target, Gl.GL_TEXTURE_WRAP_T, wrapT);			
		}		
		
		/// <summary> Загружает пустые текстурные данные. </summary>
		public void UploadData()
		{	
			UploadData(null);
		}			
			
		/// <summary> Загружает текстурные данные -- массив текселей. </summary>
		public void UploadData(float[] pixels)
		{				
			Gl.glBindTexture(Target, handle);
		    			    
		    Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
		    
		    Gl.glTexImage3D(Target, 0, InternalFormat, Width, Height, Depth, 0, TexelFormat,
			                Gl.GL_FLOAT, pixels);
		}
		
		/// <summary> Удаляет все данные о текстуре. </summary>
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
		
		/// <summary> Идентификатор текстуры. </summary>
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
