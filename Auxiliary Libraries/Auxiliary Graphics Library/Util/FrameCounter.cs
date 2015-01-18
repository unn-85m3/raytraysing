using System;

namespace Auxiliary.Graphics
{
	/// <summary> ������� ����� ��� �������� ����� ������ ������� (FPS). </summary>
	public class FrameCounter
	{
		#region Private Fields
		
		/// <summary> ���������� '���' ��������� �����. </summary>
		private static int lastTick;
        
		/// <summary> ���������� ����� ������ � ������� (FPS). </summary>
		private static float lastFrameRate;
        
		/// <summary> ������� ����� ������ � ������� (FPS). </summary>
		private static float frameRate;
		
		/// <summary> �������� ������� ��� �������� ����� ������ (������ -- ������). </summary>
		private static int interval = 1000;		

		#endregion
		
		#region Public Methods
		
		/// <summary> ������������ ����� ������ � ������� (FPS). </summary>
        public static float CalculateFrameRate()
        {
            if (System.Environment.TickCount - lastTick >= interval)
            {
                lastFrameRate = frameRate;
                
                lastTick = System.Environment.TickCount;
                
                frameRate = 0;
            }
            
            frameRate++;
            
            return 1000 * lastFrameRate / interval;
        }
        
        #endregion
	}
}
