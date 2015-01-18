using System;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
	/// <summary> Вспомогательный класс для отрисовки осей системы координат. </summary>
	[Serializable]
	public class Axes
	{
		#region Public Fields
				
		/// <summary> Флаг отрисовывать / не отрисовывать. </summary>
		public bool Visible = true;			
				
		#endregion
		
		#region Constructor
		
		/// <summary> Создает объект для отрисовки координатных осей. </summary>
		public Axes() { }
		
		#endregion
		
		#region Public Methods
		
		/// <summary> Отрисовывет координатные оси стандартными средствами API OpenGL. </summary>
		public void Draw(Volume volume)
		{
			if (Visible)
			{
				Gl.glDisable(Gl.GL_LIGHTING);
				
				float [] current = new float[4];
				
				Gl.glGetFloatv(Gl.GL_CURRENT_COLOR, current);
				
				Glu.GLUquadric quadric = Glu.gluNewQuadric();
				
				{
					Gl.glColor3f(1.0f, 0.0f, 0.0f);
					
					Gl.glBegin(Gl.GL_LINES);
					{
						Gl.glVertex3f(0.0f, 0.0f, 0.0f);
						Gl.glVertex3f(volume.Size.X, 0.0f, 0.0f);	
					}
					Gl.glEnd();
					
					Gl.glPushMatrix();
					{
						Gl.glRotatef(90.0f, 0.0f, 1.0f, 0.0f);					
						Gl.glTranslatef(0.0f, 0.0f, volume.Size.X);					
						Glu.gluCylinder(quadric, 0.1f, 0.0f, 0.5f, 8, 8);
					}
					Gl.glPopMatrix();
				}
				
				{
					Gl.glColor3f(0.0f, 1.0f, 0.0f);
					
					Gl.glBegin(Gl.GL_LINES);
					{
						Gl.glVertex3f(0.0f, 0.0f, 0.0f);
						Gl.glVertex3f(0.0f, volume.Size.Y, 0.0f);
					}
					Gl.glEnd();
					
					Gl.glPushMatrix();
					{
						Gl.glRotatef(-90.0f, 1.0f, 0.0f, 0.0f);					
						Gl.glTranslatef(0.0f, 0.0f, volume.Size.Y);					
						Glu.gluCylinder(quadric, 0.1f, 0.0f, 0.5f, 8, 8);
					}
					Gl.glPopMatrix();				
				}
				
				{
					Gl.glColor3f(0.0f, 0.0f, 1.0f);
					
					Gl.glBegin(Gl.GL_LINES);
					{
						Gl.glVertex3f(0.0f, 0.0f, 0.0f);
						Gl.glVertex3f(0.0f, 0.0f, volume.Size.Z);
					}
					Gl.glEnd();
					
					Gl.glPushMatrix();
					{
						Gl.glRotatef(90.0f, 0.0f, 0.0f, 1.0f);					
						Gl.glTranslatef(0.0f, 0.0f, volume.Size.Z);					
						Glu.gluCylinder(quadric, 0.1f, 0.0f, 0.5f, 8, 8);
					}
					Gl.glPopMatrix();				
				}
				
				Glu.gluDeleteQuadric(quadric);
				
				Gl.glColor3fv(current);
				
				Gl.glEnable(Gl.GL_LIGHTING);
			}
		}
		
		#endregion		
	}
}
