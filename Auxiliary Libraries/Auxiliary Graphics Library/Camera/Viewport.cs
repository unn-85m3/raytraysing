using System;
using Auxiliary.MathTools;

namespace Auxiliary.Graphics
{
    /// <summary> Параметры окна отображения. </summary>
    [Serializable]
    public class Viewport
    {
        #region Private Fields

        /// <summary> Ширина окна отображения. </summary>
        private int width = 512;

        /// <summary> Высота окна отображения. </summary>
        private int height = 512;

        /// <summary> Угол раствора камеры (в радианах). </summary>
        private float fieldOfView = Util.PI / 6.0f;

        /// <summary> Соотношение сторон окна отображения. </summary>
        private float aspect;

        /// <summary> Коэффициент масштабирования по горизонтали. </summary>
        private float horizontalScale;

        /// <summary> Коэффициент масштабирования по вертикали. </summary>
        private float verticalScale;

        #endregion

        #region Constructor

        /// <summary> Создает новые параметры окна отображения. </summary>
        public Viewport(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        /// <summary> Создает новые параметры окна отображения. </summary>
        public Viewport(int width, int height, float fieldOfView)
        	: this(width, height)
        {
            FieldOfView = fieldOfView;
        }

        #endregion

        #region Private Methods

        /// <summary> Обрабатывает новые настройки окна отображения (ширина, высота, угол раствора). </summary>
        private void ApplySettings()
        {
            aspect = (float)width / height;

            horizontalScale = (float)Math.Tan(Aspect * fieldOfView / 2.0f);

            verticalScale = (float)Math.Tan(fieldOfView / 2.0f);
        }

        #endregion

        #region Public Methods

        /// <summary> Преобразует оконные координаты в область [-1, 1] x [-1, 1]. </summary>
        public Vector2D Convert(float x, float y)
        {
        	return new Vector2D(2 * (x / width) - 1, 1 - 2 * (y / height));
        }
        
        /// <summary> Устанавливает uniform-переменные шейдера. </summary>
        public void SetShaderData(ShaderProgram program)
        {
        	program.SetUniformVector("ViewportScale", new Vector2D(HorizontalScale, VerticalScale));
        }        

        #endregion

        #region Properties

        /// <summary> Ширина окна отображения. </summary>
        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;

                ApplySettings();
            }
        }

        /// <summary> Высота окна отображения. </summary>
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;

                ApplySettings();
            }
        }

        /// <summary> Угол раствора камеры (в радианах). </summary>
        public float FieldOfView
        {
            get
            {
                return fieldOfView;
            }

            set
            {
                fieldOfView = value;

                ApplySettings();
            }
        }

        // <summary> Соотношение сторон окна отображения. </summary>
        public float Aspect
        {
        	get
        	{
        		return aspect;
        	}
        }

        /// <summary> Коэффициент масштабирования по горизонтали. </summary>
        public float HorizontalScale
        {
        	get
        	{
        		return horizontalScale;
        	}
        }

        /// <summary> Коэффициент масштабирования по вертикали. </summary>
        public float VerticalScale
        {
        	get
        	{
        		return verticalScale;
        	}
        }        
        
        #endregion
    }
}