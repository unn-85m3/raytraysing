using System;
using System.Collections.Generic;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
	/// <summary> Базовый абстрактный класс для всех объектов сцены. </summary>
    [Serializable]
    public abstract class Primitive
    {
        #region Public Fields

        /// <summary> Преобразование объекта. </summary>
        public Transform Transform = new Transform();
        
	    /// <summary> Материал объекта (описывает свойства поверхности). </summary>
	    public Material Material = new Material();
        
        /// <summary> Имя объекта (для работы с графическим интерфейсом). </summary>
        public string Name = "Объект";        
        
		/// <summary> Флаг отрисовывать / не отрисовывать. </summary>
		public bool Visible = true;	        

        #endregion

        #region Constructor

        /// <summary> Создает новый объект сцены по умолчанию. </summary>
        public Primitive() { }

        /// <summary> Создает новый объект сцены с заданным преобразованием. </summary>
        public Primitive(Transform transform)
        {
            Transform = transform;
        }

        #endregion
        
        #region Protected Methods
        
        /// <summary> Отрисовывет объект в локальных координатах стандартными средствами API OpenGL. </summary>
        protected abstract void DrawLocal();
        
        #endregion

        #region Public Methods
	    
        /// <summary> Вычисляет пересечение объекта с заданным лучом. </summary>
        public abstract IntersectInfo CalcIntersection(Ray ray);

        /// <summary> Вычисляет нормаль к объекту в заданной точке. </summary>
        public abstract Vector3D CalcNormal(Vector3D position);

        /// <summary> Вычисляет собственный цвет объекта в заданной точке. </summary>
        public abstract Vector3D CalcColor(Vector3D position, Vector3D normal);
        
	    /// <summary> Отрисовывет объект стандартными средствами API OpenGL. </summary>
	    public void Draw()
	    {
	    	Material.Setup();
	    	   	   	
	    	Gl.glPushMatrix();	    	
	    	
	    	Gl.glTranslatef(Transform.Position.X, Transform.Position.Y, Transform.Position.Z);
	    	
	    	Gl.glRotatef(Util.ToDegree(-Transform.Orientation.Z), 0.0f, 0.0f, 1.0f);
	    	
	    	Gl.glRotatef(Util.ToDegree(-Transform.Orientation.Y), 0.0f, 1.0f, 0.0f);
	    	
	    	Gl.glRotatef(Util.ToDegree(-Transform.Orientation.X), 1.0f, 0.0f, 0.0f);

	    	Gl.glScalef(Transform.Scale.X, Transform.Scale.Y, Transform.Scale.Z);
	    	
	    	DrawLocal();
	    	
	    	Gl.glPopMatrix();
	    }       
        
        #endregion
    }
}
