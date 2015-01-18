using System;

namespace Auxiliary.Graphics
{
	/// <summary> Простой класс для подсчета числа кадров секунду (FPS). </summary>
	public class FrameCounter
	{
		#region Private Fields
		
		/// <summary> Предыдущий 'тик' системных часов. </summary>
		private static int lastTick;
        
		/// <summary> Предыдущее число кадров в секунду (FPS). </summary>
		private static float lastFrameRate;
        
		/// <summary> Текущее число кадров в секунду (FPS). </summary>
		private static float frameRate;
		
		/// <summary> Интервал времени для подсчета числа кадров (больше -- точнее). </summary>
		private static int interval = 1000;		

		#endregion
		
		#region Public Methods
		
		/// <summary> Подсчитывает число кадров в секунду (FPS). </summary>
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
