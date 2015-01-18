using System;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> ������ '�����'. </summary>
    [Serializable]
    public class Sphere : Primitive
    {
        #region Public Fields

        /// <summary> ������ �����. </summary>
        public float Radius = 1.0f;   

        #endregion

        #region Constructor

        /// <summary> ������� ����� ����� � �������� ��������. </summary>
        public Sphere(float radius)
            : base()
        {
            Radius = radius;
        }
        
        /// <summary> ������� ����� ����� � �������� ���������������. </summary>
        public Sphere(Transform transform)
            : base(transform) { }          

        /// <summary> ������� ����� ����� � �������� �������� � ���������������. </summary>
        public Sphere(float radius, Transform tranform)
            : base(tranform)
        {
            Radius = radius;
        }    
        
        #endregion

        #region Protected Methods
        
	    /// <summary> ����������� ������ � ��������� ������������ ������������ ���������� API OpenGL. </summary>
	    protected override void DrawLocal()
	    {
	    	Glu.GLUquadric sphere = Glu.gluNewQuadric();
	    	
	    	Glu.gluSphere(sphere, Radius, 80, 80);
	    	
	    	Glu.gluDeleteQuadric(sphere);
	    }
	    
	    #endregion        
        
        #region Public Methods
        
        /// <summary> ��������� ����������� ������� � �������� �����. </summary>
        public override IntersectInfo CalcIntersection(Ray ray)
        {
        	Vector3D origin = Transform.InversePosition(ray.Origin);

        	Vector3D direction = Transform.InverseDirection(ray.Direction);

            float a = Vector3D.Dot(direction, direction);

            float b = Vector3D.Dot(direction, origin);

            float c = Vector3D.Dot(origin, origin) - Radius * Radius;

            float det = b * b - a * c;

            if (det > 0.0f)
            {
                det = (float) Math.Sqrt(det);

                float min = (-b - det) / a;
                float max = (-b + det) / a;

                if (max > 0.0f)
                {
                    if (min > 0.0f)
                    {
                        return new IntersectInfo(min, IntersectType.Outer);
                    }
                    else
                    {
                        return new IntersectInfo(max, IntersectType.Inner);
                    }
                }
            }

            return IntersectInfo.None;
        }

        /// <summary> ��������� ������� � ������� � �������� �����. </summary>
        public override Vector3D CalcNormal(Vector3D position)
        {
        	Vector3D local = Transform.InversePosition(position);
        	
        	return Vector3D.Normalize(Transform.TransformNormal(local));
        }

        /// <summary> ��������� ����������� ���� ������� � �������� �����. </summary>
        public override Vector3D CalcColor(Vector3D position, Vector3D normal)
        {
        	
        	Vector3D ip = Vector3D.Normalize(Transform.InversePosition(position));
        	
        	
        	//Vector3D texcoords = Vector3D.Asin(ip) / Util.PI + Vector3D.Unit / 2;
        	
        	float u = (float)Math.Atan(Math.Sqrt(ip.X * ip.X + ip.Y * ip.Y) / ip.Z);
        	float v = (float)Math.Atan(ip.Y / ip.X);
        	
        	
        	Vector3D color = Material.Color;
        	
        	if (null != Material.Texture)
        	{
        		color *= Material.Texture.CalcColor(new Vector2D(u / Util.PI, 0.5f * v / Util.PI));
        	}
        	
        	return color;        	
        }
        
        #endregion
    }
}
