using System;
using Auxiliary.MathTools;

namespace Auxiliary.Raytracing
{
    /// <summary> Структура данных для представления луча. </summary>
    public class Ray
    {
        #region Public Fields

        /// <summary> Начальная точка луча. </summary>
        public Vector3D Origin;

        /// <summary> Направляющий вектор луча. </summary>
        public Vector3D Direction;

        /// <summary> Переносимая лучом интенсивность света. </summary>
        public Vector3D Intensity = Vector3D.Zero;

        #endregion

        #region Constructor
        
        /// <summary> Создает новый луч с заданным началом и направлением. </summary>
        public Ray(Vector3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
        }
    
        #endregion
    }

    /// <summary> Тип соударения луча с объектом. </summary>
    public enum IntersectType
    {
        /// <summary> Соударение изнутри объекта. </summary>
    	Inner,
        
    	/// <summary> Соударение извне объекта. </summary>
    	Outer,
        
    	/// <summary> Соударение отсутствует. </summary>
    	None
    }

    /// <summary> Структура с информацией о соударении луча с объектом. </summary>
    public class IntersectInfo
    {
        #region Public Fields

        /// <summary> Время соударения луча с объектом. </summary>
        public float Time;

        /// <summary> Тип соударения луча с объектом. </summary>
        public IntersectType Type;

        /// <summary> Стандартная информация 'Луч не пересекается с объектом'. </summary>
        public static readonly IntersectInfo None =
        	new IntersectInfo(float.PositiveInfinity, IntersectType.None);

        #endregion

        #region Constructor
       
        /// <summary> Создает информацию о соударении луча с объектом по заданному времени и типу соударения. </summary>
        public IntersectInfo(float time, IntersectType type)
        {
            Time = time;
            Type = type;
        }

        #endregion
    }
}
