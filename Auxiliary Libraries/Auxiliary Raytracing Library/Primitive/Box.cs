using System;
using System.Collections.Generic;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> Объект 'Параллелепипед'. </summary>
    [Serializable]
    public class Box : Primitive
    {
        #region Public Fields

        /// <summary> Половинный (!) размер параллелепипеда. </summary>
        public Vector3D Size = new Vector3D(1.0f, 1.0f, 1.0f);

        #endregion

        #region Constructor

        /// <summary> Создает новый параллелепипед. </summary>
        public Box()
        	: base() { }
        
        /// <summary> Создает новый параллелепипед с заданным размером. </summary>
        public Box(Vector3D size)
            : base()
        {
            Size = size;
        }        

        /// <summary> Создает новый параллелепипед с заданным преобразованием. </summary>
        public Box(Transform transform)
            : base(transform) { }
        
        /// <summary> Создает новый параллелепипед с заданным размером и преобразованием. </summary>
        public Box(Vector3D size, Transform transform)
            : base(transform)
        {
            Size = size;
        }   

        #endregion

        #region Protected Methods
        
	    /// <summary> Отрисовывет объект в локальном пространстве стандартными средствами API OpenGL. </summary>
	    protected override void DrawLocal()
	    {
	    	Gl.glBegin(Gl.GL_QUADS);
	    	{
				Gl.glNormal3f(-1.0f, 0.0f, 0.0f);
				Gl.glVertex3f(-Size.X, -Size.Y, -Size.Z);
				Gl.glVertex3f(-Size.X, Size.Y, -Size.Z);
				Gl.glVertex3f(-Size.X, Size.Y, Size.Z);
				Gl.glVertex3f(-Size.X, -Size.Y, Size.Z);	    		
	    		
	    		Gl.glNormal3f(1.0f, 0.0f, 0.0f);
	    		Gl.glVertex3f(Size.X, -Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, Size.Z);
				Gl.glVertex3f(Size.X, -Size.Y, Size.Z);	    		

				Gl.glNormal3f(0.0f, -1.0f, 0.0f);
				Gl.glVertex3f(-Size.X, -Size.Y, Size.Z);
				Gl.glVertex3f(-Size.X, -Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, -Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, -Size.Y, Size.Z);
				
				Gl.glNormal3f(0.0f, 1.0f, 0.0f);
				Gl.glVertex3f(-Size.X, Size.Y, -Size.Z);
				Gl.glVertex3f(-Size.X, Size.Y, Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, -Size.Z);				
				
				Gl.glNormal3f(0.0f, 0.0f, -1.0f);
				Gl.glVertex3f(-Size.X, -Size.Y, -Size.Z);
				Gl.glVertex3f(-Size.X, Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, -Size.Z);
				Gl.glVertex3f(Size.X, -Size.Y, -Size.Z);
				
				Gl.glNormal3f(0.0f, 0.0f, 1.0f);
				Gl.glVertex3f(-Size.X, Size.Y, Size.Z);
				Gl.glVertex3f(-Size.X, -Size.Y, Size.Z);
				Gl.glVertex3f(Size.X, -Size.Y, Size.Z);
				Gl.glVertex3f(Size.X, Size.Y, Size.Z);
	    	}
	    	Gl.glEnd();
	    }
	    
	    #endregion        
        
        #region Public Methods
        
        /// <summary> Вычисляет пересечение объекта с заданным лучом. </summary>
        public override IntersectInfo CalcIntersection(Ray ray)
        {
        	Vector3D origin = Transform.InversePosition(ray.Origin);

        	Vector3D direction = Transform.InverseDirection(ray.Direction);

        	float [] candidates = new float[] { -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f };

            int index = 0;

            if (direction.X != 0.0f)
            {
                candidates[index++] = (-Size.X - origin.X) / direction.X;
                candidates[index++] = (Size.X - origin.X) / direction.X;
            }

            if (direction.Y != 0.0f)
            {
                candidates[index++] = (-Size.Y - origin.Y) / direction.Y;
                candidates[index++] = (Size.Y - origin.Y) / direction.Y;
            }

            if (direction.Z != 0.0f)
            {
                candidates[index++] = (-Size.Z - origin.Z) / direction.Z;
                candidates[index++] = (Size.Z - origin.Z) / direction.Z;
            }

            float time = Single.PositiveInfinity;

            foreach (float candidate in candidates)
            {
                if (candidate > 0.0f)
                {
                    Vector3D point = origin + candidate * direction;

                    if (point > -Size - Vector3D.Epsilon && point < Size + Vector3D.Epsilon)
                    {
                        if (candidate < time)
                            time = candidate;
                    }
                }
            }

            if (Single.IsPositiveInfinity(time))
            {
                return IntersectInfo.None;
            }
            else
            {
            	if (origin >= -Size && origin <= Size)
            	{
            		return new IntersectInfo(time, IntersectType.Inner);
            	}
                else
                {
                	return new IntersectInfo(time, IntersectType.Outer);
                }
            }
        }

        /// <summary> Вычисляет нормаль к объекту в заданной точке. </summary>
        public override Vector3D CalcNormal(Vector3D position)
        {
        	Vector3D local = Transform.InversePosition(position);

            if (Math.Abs(local.X + Size.X) < Util.Epsilon)
            	return Vector3D.Normalize(Transform.TransformNormal(-Vector3D.I));
            else
                if (Math.Abs(local.X - Size.X) < Util.Epsilon)
            		return Vector3D.Normalize(Transform.TransformNormal(Vector3D.I));
                else
                    if (Math.Abs(local.Y + Size.Y) < Util.Epsilon)
                		return Vector3D.Normalize(Transform.TransformNormal(-Vector3D.J));
                    else
                        if (Math.Abs(local.Y - Size.Y) < Util.Epsilon)
                    		return Vector3D.Normalize(Transform.TransformNormal(Vector3D.J));
                        else
                            if (Math.Abs(local.Z + Size.Z) < Util.Epsilon)
                        		return Vector3D.Normalize(Transform.TransformNormal(-Vector3D.K));
                            else
                                if (Math.Abs(local.Z - Size.Z) < Util.Epsilon)
                            		return Vector3D.Normalize(Transform.TransformNormal(Vector3D.K));

            return Vector3D.Zero;
        }

        /// <summary> Вычисляет собственный цвет объекта в заданной точке. </summary>
        public override Vector3D CalcColor(Vector3D position, Vector3D normal)
        {
        	Vector3D color = Material.Color;
        	
        	if (null != Material.Texture)
        	{        	
	        	Vector3D local = Transform.InversePosition(position);
	
	        	if (Math.Abs(Math.Abs(local.X) - Size.X) < Util.Epsilon)
	        		color *= Material.Texture.CalcColor((local / Size).YZ);
	            else
	            	if (Math.Abs(Math.Abs(local.Y) - Size.Y) < Util.Epsilon)
	            		color *= Material.Texture.CalcColor((local / Size).XZ);
	                else
	                	if (Math.Abs(Math.Abs(local.Z) - Size.Z) < Util.Epsilon)
	                		color *= Material.Texture.CalcColor((local / Size).XY);
        	}
        	
        	return color;
        }
        
        #endregion
    }
}