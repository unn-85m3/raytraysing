using System;
using System.Windows.Forms;

namespace Auxiliary.Graphics
{
	/// <summary> ���������� ������� ������. </summary>
    public struct KeyStatus
	{
		public bool W;
		
		public bool S;
		
		public bool A;
		
		public bool D;
		
		public bool Space;
		
		public bool Z;		
	}
	
    /// <summary> ��������� ����� ��� ���������� ������� ��� ������ ����������. </summary>
	public class Keyboard
	{
		#region Private Fields
		
		/// <summary> ���������� ������� ������. </summary>
		private KeyStatus status;	
				
		#endregion
		
		#region Public Fields
		
		/// <summary> �������� �������� ������. </summary>
		public float Speed = 0.5f;	
		
		#endregion
		
		#region Constructors
				
		/// <summary> ������� ��������� ������ ��� ���������� �����������. </summary>
		public Keyboard() { }
		
		/// <summary> ������� ��������� ������ ��� ���������� �����������. </summary>
		public Keyboard(float speed)
		{			
			Speed = speed;
		}		
		
		#endregion
		
		#region Public Methods
		
		/// <summary> ������ ��� ������ ����� ���������. </summary>
		public void Apply(Camera camera)
		{
			if (null == camera)
				return;
			
			if (status.W)
				camera.MoveStraight(Speed);
					
			if (status.S)
				camera.MoveStraight(-Speed);
						
			if (status.A)
				camera.MoveHorizontal(-Speed);
					
			if (status.D)
				camera.MoveHorizontal(Speed);

			if (status.Z)
				camera.MoveVertical(-Speed);
					
			if (status.Space)
				camera.MoveVertical(Speed);
		}
		
		/// <summary> ���������� ������� '������� ������'. </summary>
		public void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					status.W = true; break;
				
				case Keys.S:
					status.S = true; break;
				
				case Keys.A:
					status.A = true; break;
				
				case Keys.D:
					status.D = true; break;
					
				case Keys.Space:
					status.Space = true; break;
					
				case Keys.Z:
					status.Z = true; break;					
			}
		}
		
		/// <summary> ���������� ������� '������� ��������'. </summary>
		public void OnKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					status.W = false; break;
				
				case Keys.S:
					status.S = false; break;
				
				case Keys.A:
					status.A = false; break;
				
				case Keys.D:
					status.D = false; break;
					
				case Keys.Space:
					status.Space = false; break;
					
				case Keys.Z:
					status.Z = false; break;					
			}
		}
		
		#endregion
	}
}
