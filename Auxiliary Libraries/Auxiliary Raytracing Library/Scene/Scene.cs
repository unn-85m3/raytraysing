using System;
using System.Collections.Generic;
using Auxiliary.Graphics;

namespace Auxiliary.Raytracing
{
    /// <summary> Класс для представления сцены с объектами и источниками света. </summary>
    [Serializable]
    public class Scene
    {
        #region Public Fields

        /// <summary> Список источников света. </summary>
        public List<Light> Lights = new List<Light>();

        /// <summary> Список объектов сцены. </summary>
        public List<Primitive> Primitives = new List<Primitive>();
        
        /// <summary> Камера для 'съемки' сцены. </summary>
        public Camera Camera = new Camera();
        
        /// <summary> Видимый объем в виде параллелипипеда. </summary>
        public Volume Volume = new Volume();
        
        /// <summary> Оси системы координат. </summary>
        public Axes Axes = new Axes();
        
        #endregion

        #region Constructor

        /// <summary> Создает новую сцену. </summary>
        public Scene() { }

        #endregion
        
        #region Public Methods
        
        /// <summary> Отрисовывает сцену стандартными средствами API OpenGL. </summary>
        public void Draw()
        {
        	foreach (Light light in Lights)
        	{
        		light.Setup();
        		
        		light.Draw();
        	}
        	
        	foreach (Primitive primirive in Primitives)
        	{
        		if (primirive.Visible)
        		{
        			primirive.Draw();
        		}        		
        	}
        	
        	Volume.Draw();
        	
        	Axes.Draw(Volume);
        }	        
        
        #endregion
    }
}
