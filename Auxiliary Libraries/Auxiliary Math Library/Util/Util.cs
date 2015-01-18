using System;

namespace Auxiliary.MathTools
{
    /// <summary> Вспомогательные функции. </summary>
    public class Util
    {
    	#region Public Fields
    	
    	public static readonly float PI = (float) Math.PI;
    	
        /// <summary> Отличная от нуля малая величина. </summary>
        public static readonly float Epsilon = 1e-3f;
    	
    	#endregion
    	
    	#region Public Methods
    	
    	public static float ToDegree(float radian)
    	{
    		return 180.0f * radian / PI;
    	}
    	
    	public static float ToRadian(float degree)
    	{
    		return PI * degree / 180.0f;
    	}    	
    	
    	public static float Clamp(float arg, float min, float max)
        {
            if (arg < min)
            {
                return min;
            }
            else
            {
                if (arg > max)
                {
                    return max;
                }
                else
                {
                    return arg;
                }
            }
        }

        public static float Step(float arg, float edge)
        {
            if (arg < edge)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static float SmoothStep(float arg, float left, float right)        	
        {
            float result = Clamp((arg - left) / (right - left), 0, 1);

            return result * result * (3 - 2 * result);
        }
        
        #endregion
    }
}
