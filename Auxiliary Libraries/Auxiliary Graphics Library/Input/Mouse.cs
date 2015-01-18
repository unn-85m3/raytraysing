using System;
using System.Drawing;
using System.Windows.Forms;
using Auxiliary.MathTools;

namespace Auxiliary.Graphics
{
	/// <summary> ��������� ����� ��� ���������� ������� ��� ������ ����. </summary>
	public class Mouse
	{
		#region Private Fields
		
		/// <summary> ���������� ��������� ����. </summary>
		private Point point;
		
		/// <summary> ������ ����� ����� �����������. </summary>
		private Point difference;
		
		/// <summary> ������������ / �� ������������ �������� ����. </summary>
		private bool active = false;
		
		#endregion
		
		#region Public Fields
		
		/// <summary> �������� ����. </summary>
		public float Speed = 0.005f;
		
		#endregion
		
		#region Constructors
		
		/// <summary> ������� ��������� ������ ��� ��������� �������� ����. </summary>
		public Mouse() { }		
		
		/// <summary> ������� ��������� ������ ��� ��������� �������� ����. </summary>
		public Mouse(float speed)
		{
			Speed = speed;
		}
		
		#endregion
			
		#region Public Methods
		
		/// <summary> ������ ��� ������ ����� ����������. </summary>
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

		/// <summary> ���������� ������� '������� ������ ����'. </summary>
		public void OnMouseDown(MouseEventArgs e)
		{
			point = e.Location;
			
			active = !active;
		}
		
		/// <summary> ���������� ������� '�������� ��������� ����'. </summary>
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
		
		/// <summary> ������������ / �� ������������ �������� ����. </summary>
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
