using System;
using System.Collections.Generic;
using Auxiliary.Graphics;

namespace Auxiliary.Raytracing
{
    /// <summary> ����� ��� ������������� ����� � ��������� � ����������� �����. </summary>
    [Serializable]
    public class Scene
    {
        #region Public Fields

        /// <summary> ������ ���������� �����. </summary>
        public List<Light> Lights = new List<Light>();

        /// <summary> ������ �������� �����. </summary>
        public List<Primitive> Primitives = new List<Primitive>();
        
        /// <summary> ������ ��� '������' �����. </summary>
        public Camera Camera = new Camera();
        
        /// <summary> ������� ����� � ���� ���������������. </summary>
        public Volume Volume = new Volume();
        
        /// <summary> ��� ������� ���������. </summary>
        public Axes Axes = new Axes();
        
        #endregion

        #region Constructor

        /// <summary> ������� ����� �����. </summary>
        public Scene() { }

        #endregion
        
        #region Public Methods
        
        /// <summary> ������������ ����� ������������ ���������� API OpenGL. </summary>
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
