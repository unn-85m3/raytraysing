using System;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> ��������� �������� ��������� ��������� �����. </summary>
    [Serializable]
	public class Light
	{
        #region Public Fields

        /// <summary> ������������� �������� (ambiant) �����. </summary>
        public Vector3D AmbiantIntensity = new Vector3D(0.2f, 0.2f, 0.2f);

        /// <summary> ������������� ���������� (diffuse) �����. </summary>
        public Vector3D DiffuseIntensity = new Vector3D(1.0f, 1.0f, 1.0f);

        /// <summary> ������������� ����������� (specular) �����. </summary>
        public Vector3D SpecularIntensity = new Vector3D(0.3f, 0.3f, 0.3f);
        
        /// <summary> ��������� ��������� �����. </summary>
        public Vector3D Position = new Vector3D(5.0f, 5.0f, 5.0f);
        
        /// <summary> ����� ���������������� ��������� ����� � API OpenGL. </summary>
        public int Number = 0;   

        #endregion
        
		#region Constructor
		
		/// <summary> ������� �������� �������� ����� � �������� �������. </summary>
		public Light(int number)
		{
			Number = number;
		}
		
		/// <summary> ������� �������� �������� ����� � �������� ������� � ����������. </summary>
		public Light(int number, Vector3D position)
			: this(number)
		{
			Position = position;
		}
		
		/// <summary> ������� �������� �������� ����� � �������� �������, ���������� � ���������������. </summary>
		public Light(int number,
		             Vector3D position,
		             Vector3D ambiantIntensity,
		             Vector3D diffuseIntensity,
		             Vector3D specularIntensity)
			: this(number, position)
		{
			AmbiantIntensity = ambiantIntensity;
			DiffuseIntensity = diffuseIntensity;
			SpecularIntensity = specularIntensity;
		}
		
		#endregion

		#region Public Methods
        
        /// <summary> ��������� �������� ��������� ����� � ��������� ��������� API OpenGL. </summary>
        public void Setup()
        {
        	Gl.glEnable(Gl.GL_LIGHT0 + Number);
        	
        	Gl.glLightfv(Gl.GL_LIGHT0 + Number, Gl.GL_POSITION,
        	             new float[] { Position.X, Position.Y, Position.Z, 1.0f });
        	
        	Gl.glLightfv(Gl.GL_LIGHT0 + Number, Gl.GL_AMBIENT, AmbiantIntensity.ToArray());
        	
        	Gl.glLightfv(Gl.GL_LIGHT0 + Number, Gl.GL_DIFFUSE, DiffuseIntensity.ToArray());
        	
        	Gl.glLightfv(Gl.GL_LIGHT0 + Number, Gl.GL_SPECULAR, SpecularIntensity.ToArray());
        }
        
		/// <summary> ������������ �������� ����� ������������ ���������� API OpenGL. </summary>
		public void Draw()
		{
			Gl.glDisable(Gl.GL_LIGHTING);
			
			Gl.glBegin(Gl.GL_POINTS);
			{
				Gl.glColor3f(DiffuseIntensity.X, DiffuseIntensity.Y, DiffuseIntensity.Z);
			
				Gl.glVertex3f(Position.X, Position.Y, Position.Z);
			}
			Gl.glEnd();
			
			Gl.glEnable(Gl.GL_LIGHTING);
		}
		
        #endregion
	}
}
