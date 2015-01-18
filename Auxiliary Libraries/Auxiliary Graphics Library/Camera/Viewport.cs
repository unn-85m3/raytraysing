using System;
using Auxiliary.MathTools;

namespace Auxiliary.Graphics
{
    /// <summary> ��������� ���� �����������. </summary>
    [Serializable]
    public class Viewport
    {
        #region Private Fields

        /// <summary> ������ ���� �����������. </summary>
        private int width = 512;

        /// <summary> ������ ���� �����������. </summary>
        private int height = 512;

        /// <summary> ���� �������� ������ (� ��������). </summary>
        private float fieldOfView = Util.PI / 6.0f;

        /// <summary> ����������� ������ ���� �����������. </summary>
        private float aspect;

        /// <summary> ����������� ��������������� �� �����������. </summary>
        private float horizontalScale;

        /// <summary> ����������� ��������������� �� ���������. </summary>
        private float verticalScale;

        #endregion

        #region Constructor

        /// <summary> ������� ����� ��������� ���� �����������. </summary>
        public Viewport(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        /// <summary> ������� ����� ��������� ���� �����������. </summary>
        public Viewport(int width, int height, float fieldOfView)
        	: this(width, height)
        {
            FieldOfView = fieldOfView;
        }

        #endregion

        #region Private Methods

        /// <summary> ������������ ����� ��������� ���� ����������� (������, ������, ���� ��������). </summary>
        private void ApplySettings()
        {
            aspect = (float)width / height;

            horizontalScale = (float)Math.Tan(Aspect * fieldOfView / 2.0f);

            verticalScale = (float)Math.Tan(fieldOfView / 2.0f);
        }

        #endregion

        #region Public Methods

        /// <summary> ����������� ������� ���������� � ������� [-1, 1] x [-1, 1]. </summary>
        public Vector2D Convert(float x, float y)
        {
        	return new Vector2D(2 * (x / width) - 1, 1 - 2 * (y / height));
        }
        
        /// <summary> ������������� uniform-���������� �������. </summary>
        public void SetShaderData(ShaderProgram program)
        {
        	program.SetUniformVector("ViewportScale", new Vector2D(HorizontalScale, VerticalScale));
        }        

        #endregion

        #region Properties

        /// <summary> ������ ���� �����������. </summary>
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

        /// <summary> ������ ���� �����������. </summary>
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

        /// <summary> ���� �������� ������ (� ��������). </summary>
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

        // <summary> ����������� ������ ���� �����������. </summary>
        public float Aspect
        {
        	get
        	{
        		return aspect;
        	}
        }

        /// <summary> ����������� ��������������� �� �����������. </summary>
        public float HorizontalScale
        {
        	get
        	{
        		return horizontalScale;
        	}
        }

        /// <summary> ����������� ��������������� �� ���������. </summary>
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