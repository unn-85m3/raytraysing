using System;
using Library.MathTools;

namespace Library.Raytracing
{
    /// <summary> Описывает параметры преобразования вершин и нормалей объекта. </summary>
    [Serializable]
    public class Transform
    {
        #region Public Fields

        /// <summary> Положение объекта в мировом пространстве. </summary>
        public Vector3D Position = Vector3D.Zero;

        #endregion

        #region Private Fields

        /// <summary> Углы поворота вокруг каждой оси. </summary>
        private Vector3D orientation = Vector3D.Zero;
        
        /// <summary> Коэффициенты масштабирования для каждой оси. </summary>
        private Vector3D scale = Vector3D.Unit;        

        /// <summary> Матрица поворота (по умолчанию -- единичная). </summary>
        private Matrix3D rotateMatrix = Matrix3D.Identity;
        
        /// <summary> Матрица масштабирования (по умолчанию -- единичная). </summary>
        private Matrix3D scaleMatrix = Matrix3D.Identity;
        
        /// <summary> Матрица обратного поворота (по умолчанию -- единичная). </summary>
        private Matrix3D inverseRotateMatrix = Matrix3D.Identity;
        
        /// <summary> Матрица обратного масштабирования (по умолчанию -- единичная). </summary>
        private Matrix3D inverseScaleMatrix = Matrix3D.Identity;          
        
        #endregion

        #region Constructor

        /// <summary> Создает новое преобразование (тождественное). </summary>
        public Transform() { }
        
        /// <summary> Создает новое преобразование по заданному смещению. </summary>
        public Transform(Vector3D position)
        {
        	Position = position;
        }
        
        /// <summary> Создает новое преобразование по заданному смещению и повороту. </summary>
        public Transform(Vector3D position, Vector3D orient)
        	: this(position)
        {
            orientation = orient;

            rotateMatrix = Matrix3D.MatrixRotateAxes(orientation);
            
            inverseRotateMatrix = Matrix3D.Inverse(rotateMatrix);
        }
        
        /// <summary> Создает новое преобразование по заданному смещению, повороту и масштабированию. </summary>
        public Transform(Vector3D position, Vector3D orient, Vector3D scal)
        	: this(position, orient)
        {
            scale = scal;

            scaleMatrix = Matrix3D.MatrixScale(scale);
            
            inverseScaleMatrix = Matrix3D.Inverse(scaleMatrix);
        }        

        #endregion
        
        #region Public Methods
        
        /// <summary> Осуществляет преобразование заданной точки. </summary>
        public Vector3D TransformPosition(Vector3D position)
        {
        	return rotateMatrix * scaleMatrix * position + Position;
        }
        
        /// <summary> Осуществляет преобразование заданного вектора. </summary>
        public Vector3D TransformDirection(Vector3D direction)
        {
        	return rotateMatrix * scaleMatrix * direction;
        }
        
        /// <summary> Осуществляет преобразование заданной нормали. </summary>
        public Vector3D TransformNormal(Vector3D normal)
        {        	
        	return rotateMatrix * inverseScaleMatrix * normal;
        }         
        
        /// <summary> Осуществляет обратное преобразование заданной точки. </summary>
        public Vector3D InversePosition(Vector3D position)
        {
        	return inverseScaleMatrix * inverseRotateMatrix * (position - Position);
        }
        
        /// <summary> Осуществляет обратное преобразование заданного вектора. </summary>
        public Vector3D InverseDirection(Vector3D direction)
        {
        	return inverseScaleMatrix * inverseRotateMatrix * direction;
        }               
        
        #endregion
        
        #region Properties

        /// <summary> Углы поворота вокруг каждой оси. </summary>
        public Vector3D Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                orientation = value;

                rotateMatrix = Matrix3D.MatrixRotateAxes(orientation);
                
                inverseRotateMatrix = Matrix3D.Inverse(rotateMatrix);
            }
        }
        
        /// <summary> Коэффициенты масштабирования для каждой оси. </summary>
        public Vector3D Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;

                scaleMatrix = Matrix3D.MatrixScale(scale);
                
                inverseScaleMatrix = Matrix3D.Inverse(scaleMatrix);
            }
        }        

        #endregion        
    }
}
