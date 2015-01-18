using System;
using System.Drawing;
using System.Windows.Forms;
using Auxiliary.MathTools;

namespace Auxiliary.Graphics
{
	/// <summary> Служебный класс для управления камерой при помощи мыши. </summary>
	public class Mouse
	{
		#region Private Fields
		
		/// <summary> Координаты указателя мыши. </summary>
		private Point point;
		
		/// <summary> Дельта между двумя положениями. </summary>
		private Point difference;
		
		/// <summary> Обрабатывать / не обрабатывать движение мыши. </summary>
		private bool active = false;
		
		#endregion
		
		#region Public Fields
		
		/// <summary> Скорость мыши. </summary>
		public float Speed = 0.005f;
		
		#endregion
		
		#region Constructors
		
		/// <summary> Создает экземпляр класса для обработки движения мыши. </summary>
		public Mouse() { }		
		
		/// <summary> Создает экземпляр класса для обработки движения мыши. </summary>
		public Mouse(float speed)
		{
			Speed = speed;
		}
		
		#endregion
			
		#region Public Methods
		
		/// <summary> Задает для камеры новую ориентацию. </summary>
		public void Apply(Camera camera)
		{
			if (null == camera || !active)
				return;
									
        	if (difference.X != 0)
        	{
        		camera.Rotate(Vector3D.K * difference.X * Speed);
        	}
        	
        	if (difference.Y != 0)
        	{
        		camera.Rotate(Vector3D.J * difference.Y * Speed);
        	}
        	
        	difference = Point.Empty;
		}				

		/// <summary> Обработчик события 'Нажатие кнопки мыши'. </summary>
		public void OnMouseDown(MouseEventArgs e)
		{
			point = e.Location;
			
			active = !active;
		}
		
		/// <summary> Обработчик события 'Движение указателя мыши'. </summary>
		public void OnMouseMove(MouseEventArgs e)
		{
			if (!active)
				return;
									
        	if (e.Location.X != point.X)
        	{
        		difference.X += e.Location.X - point.X;
        	}
        	
        	if (e.Location.Y != point.Y)
        	{
        		difference.Y += e.Location.Y - point.Y;
        	}
        	
			point = e.Location;
		}
		
		#endregion
	
		#region Properties
		
		/// <summary> Обрабатывать / не обрабатывать движение мыши. </summary>
		public bool Active
		{
			get
			{
				return active;
			}
		}
		
		#endregion
	}
}
