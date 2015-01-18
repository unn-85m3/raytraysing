using System;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> ������ '������������� ��������'. </summary>
    [Serializable]
    public class Square : Primitive
    {
        #region Public Fields

        /// <summary> ���������� (!) ������ ��������. </summary>
        public Vector2D Size = new Vector2D(5.0f, 5.0f);

        #endregion

        #region Constructor

        /// <summary> ������� ����� �������� � �������� ��������. </summary>
        public Square(Vector2D size)
            : base()
        {
            Size = size;
        }
        
        /// <summary> ������� ����� �������� � �������� ���������������. </summary>
        public Square(Transform transform)
            : base(transform) { }        

        /// <summary> ������� ����� �������� � �������� �������� � ���������������. </summary>
        public Square(Vector2D size, Transform transform)
            : base(transform)
        {
            Size = size;
        }      

        #endregion

        #region Protected Methods
        
	    /// <summary> ����������� ������ � ��������� ������������ ������������ ���������� API OpenGL. </summary>
	    protected override void DrawLocal()
	    {
	    	Gl.glBegin(Gl.GL_QUADS);
	    	{
	    		Gl.glNormal3f(0.0f, 0.0f, 1.0f);
	    		
	    		Gl.glVertex2f(-Size.X, -Size.Y);
				Gl.glVertex2f(-Size.X, Size.Y);
				Gl.glVertex2f(Size.X, Size.Y);
				Gl.glVertex2f(Size.X, -Size.Y);
	    	}
	    	Gl.glEnd();
	    }
	    
	    #endregion
        
        #region Public Methods
        
        /// <summary> ��������� ����������� ������� � �������� �����. </summary>
        public override IntersectInfo CalcIntersection(Ray ray)
        {
        	Vector3D origin = Transform.InversePosition(ray.Origin);

        	Vector3D direction = Transform.InverseDirection(ray.Direction);

            float dot = Vector3D.Dot(Vector3D.K, direction);

            if (dot != 0.0f)
            {
                float time = -Vector3D.Dot(Vector3D.K, origin) / dot;

                if (time > 0.0f)
                {
                    Vector2D point = (origin  + direction * time).XY;

                    if (Vector2D.Abs(point) < Size)
                        return new IntersectInfo(time, IntersectType.Outer);
                }
            }

            return IntersectInfo.None;
        }

        /// <summary> ��������� ������� � ������� � �������� �����. </summary>
        public override Vector3D CalcNormal(Vector3D position)
        {
        	return Vector3D.Normalize(Transform.TransformNormal(Vector3D.K));
        }

        /// <summary> ��������� ����������� ���� ������� � �������� �����. </summary>
        public override Vector3D CalcColor(Vector3D position, Vector3D normal)
        {
        	Vector3D local = Transform.InversePosition(position);

        	Vector3D color = Material.Color;
        	
        	if (null != Material.Texture)
        	{
        		color *= Material.Texture.CalcColor(local.XY / Size);
        	}
        	
        	return color;
        }
        
        #endregion
    }
}