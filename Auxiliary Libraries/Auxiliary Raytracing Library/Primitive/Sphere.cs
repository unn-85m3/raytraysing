using System;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> Объект 'Сфера'. </summary>
    [Serializable]
    public class Sphere : Primitive
    {
        #region Public Fields

        /// <summary> Радиус сферы. </summary>
        public float Radius = 1.0f;   

        #endregion

        #region Constructor

        /// <summary> Создает новую сферу с заданным радиусом. </summary>
        public Sphere(float radius)
            : base()
        {
            Radius = radius;
        }
        
        /// <summary> Создает новую сферу с заданным преобразованием. </summary>
        public Sphere(Transform transform)
            : base(transform) { }          

        /// <summary> Создает новую сферу с заданным радиусом и преобразованием. </summary>
        public Sphere(float radius, Transform tranform)
            : base(tranform)
        {
            Radius = radius;
        }    
        
        #endregion

        #region Protected Methods
        
	    /// <summary> Отрисовывет объект в локальном пространстве стандартными средствами API OpenGL. </summary>
	    protected override void DrawLocal()
	    {
	    	Glu.GLUquadric sphere = Glu.gluNewQuadric();
	    	
	    	Glu.gluSphere(sphere, Radius, 80, 80);
	    	
	    	Glu.gluDeleteQuadric(sphere);
	    }
	    
	    #endregion        
        
        #region Public Methods
        
        /// <summary> Вычисляет пересечение объекта с заданным лучом. </summary>
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

        /// <summary> Вычисляет нормаль к объекту в заданной точке. </summary>
        public override Vector3D CalcNormal(Vector3D position)
        {
        	Vector3D local = Transform.InversePosition(position);
        	
        	return Vector3D.Normalize(Transform.TransformNormal(local));
        }

        /// <summary> Вычисляет собственный цвет объекта в заданной точке. </summary>
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
