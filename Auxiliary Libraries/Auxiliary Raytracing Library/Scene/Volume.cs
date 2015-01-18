using System;
using Auxiliary.Graphics;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
	/// <summary> Представляет видимый объем (в виде параллелипипеда). </summary>
	[Serializable]
	public class Volume
	{
		#region Public Fields
				
		/// <summary> Половинный (!) размер видимого параллелипипеда. </summary>
		public Vector3D Size = new Vector3D(5.0f, 5.0f, 5.0f);
		
		/// <summary> Цвет для отрисовки видимого параллелипипеда. </summary>
		public Vector3D Color = new Vector3D(0.8f, 0.8f, 0.8f);
		
		/// <summary> Флаг отрисовывать / не отрисовывать. </summary>
		public bool Visible = true;		
		
		#endregion
		
		#region Constructor
		
		/// <summary> Создает новый видимый объем по умолчанию. </summary>
		public Volume() { }
		
		/// <summary> Создает новый видимый объем с заданным размером. </summary>
		public Volume(Vector3D size)
		{
			Size = size;
		}
		
		#endregion
		
		#region Public Methods
		
		/// <summary> Отрисовывет границы видимого объема стандартными средствами API OpenGL. </summary>
		public void Draw()
		{
			if (Visible)
			{
				Gl.glDisable(Gl.GL_LIGHTING);
				
				float [] current = new float[4];
				
				Gl.glGetFloatv(Gl.GL_CURRENT_COLOR, current);				
				
				Gl.glColor3f(Color.X, Color.Y, Color.Z);
				
				Gl.glBegin(Gl.GL_LINE_STRIP);
				{
					Gl.glVertex3f(-Size.X, -Size.Y, -Size.Z);
					Gl.glVertex3f(-Size.X, Size.Y, -Size.Z);
					Gl.glVertex3f(Size.X, Size.Y, -Size.Z);
					Gl.glVertex3f(Size.X, -Size.Y, -Size.Z);
				}
					Gl.glEnd();
				
				Gl.glBegin(Gl.GL_LINE_STRIP);
				{
					Gl.glVertex3f(-Size.X, Size.Y, Size.Z);
					Gl.glVertex3f(-Size.X, -Size.Y, Size.Z);
					Gl.glVertex3f(Size.X, -Size.Y, Size.Z);
					Gl.glVertex3f(Size.X, Size.Y, Size.Z);
				}
				Gl.glEnd();
				
				Gl.glBegin(Gl.GL_LINE_STRIP);
				{
					Gl.glVertex3f(-Size.X, -Size.Y, Size.Z);
					Gl.glVertex3f(-Size.X, -Size.Y, -Size.Z);
					Gl.glVertex3f(Size.X, -Size.Y, -Size.Z);
					Gl.glVertex3f(Size.X, -Size.Y, Size.Z);
				}
				Gl.glEnd();
				
				Gl.glBegin(Gl.GL_LINE_STRIP);
				{
					Gl.glVertex3f(-Size.X, Size.Y, -Size.Z);
					Gl.glVertex3f(-Size.X, Size.Y, Size.Z);
					Gl.glVertex3f(Size.X, Size.Y, Size.Z);
					Gl.glVertex3f(Size.X, Size.Y, -Size.Z);
				}
				Gl.glEnd();
				
				Gl.glColor3fv(current);
				
				Gl.glEnable(Gl.GL_LIGHTING);
			}
		}
		
        /// <summary> Устанавливает uniform-переменные шейдера. </summary>
        public void SetShaderData(ShaderProgram program)
        {
        	program.SetUniformVector("VolumeSize", Size);
        }		
		
		#endregion
	}
}
