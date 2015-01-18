using System;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Graphics
{
    /// <summary> ������ '������' � ������� ������������. </summary>
    [Serializable]
    public class Camera
    {
        #region Public Fields

        /// <summary> ��������� ������ � ������� ������������. </summary>
        public Vector3D Position = new Vector3D(0.0f, 0.0f, 100.0f);
        
        /// <summary> ��������� ���� �����������. </summary>
        public Viewport Viewport = new Viewport(512, 512);      

        #endregion
        
        #region Private Fields
        
        /// <summary> ���������� ������ (���� ������). </summary>
        private Vector3D orientation = Vector3D.Zero;

        /// <summary> ������ ����������� '�����'. </summary>
        private Vector3D upDirection = Vector3D.K;

        /// <summary> ������ ����������� '������'. </summary>
        private Vector3D rightDirection = Vector3D.J;

        /// <summary> ������ ����������� '������'. </summary>
        private Vector3D viewDirection = -Vector3D.I;

        #endregion

        #region Constructors

        /// <summary> ������� ����� ������ �� ���������. </summary>
        public Camera() { }

        /// <summary> ������� ����� ������ �� ��������� ���������. </summary>
        public Camera(Vector3D pos)
        {
            Position = pos;
        }

        /// <summary> ������� ����� ������ �� ��������� ��������� � ����������. </summary>
        public Camera(Vector3D pos, Vector3D orient)
            : this(pos)
        {
            SetOrientation(orient);
        }

        #endregion

        #region Private Methods

        /// <summary> ������ ����� ���������� ������ � ������������. </summary>
        private void SetOrientation(Vector3D orient)
        {
            orientation = orient;

            Matrix3D rotation = Matrix3D.MatrixRotateAxes(orientation);

            viewDirection = -rotation * Vector3D.I;
            upDirection = rotation * Vector3D.K;
            rightDirection = rotation * Vector3D.J;
        }

        #endregion

        #region Public Methods

        /// <summary> ���������� ������ ������ �� �������� ����������. </summary>
        public void MoveStraight(float delta)
        {
            Position += viewDirection * delta;
        }

        /// <summary> ���������� ������ � �������������� ����������� �� �������� ����������. </summary>
        public void MoveHorizontal(float delta)
        {
            Position += rightDirection * delta;
        }

        /// <summary> ���������� ������ � ������������ ����������� �� �������� ����������. </summary>
        public void MoveVertical(float delta)
        {
            Position += upDirection * delta;
        }
        
        /// <summary> ������������ ������ ������ ���� ���� �� �������� ����. </summary>
        public void Rotate(Vector3D delta)
        {
            SetOrientation(orientation + delta);
        }
        
        /// <summary> ��������� ��������� ������ � ��������� ��������� API OpenGL. </summary>
        public void Setup()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION); 
            
            Gl.glLoadIdentity(); 
            
            Glu.gluPerspective(Util.ToDegree(Viewport.FieldOfView),
                               Viewport.Aspect, 1.0f, 1000.0f);
            
         	Glu.gluLookAt(Position.X, Position.Y, Position.Z,
                          Position.X + ViewDirection.X,
                          Position.Y + ViewDirection.Y,
                          Position.Z + ViewDirection.Z,
                          UpDirection.X, UpDirection.Y, UpDirection.Z);
            
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
        }
        
        /// <summary> ������������� uniform-���������� �������. </summary>
        public void SetShaderData(ShaderProgram program)
        {
        	program.SetUniformVector("Camera.Position", Position);
        	
        	program.SetUniformVector("Camera.ViewDirection", viewDirection);
        	
        	program.SetUniformVector("Camera.UpDirection", upDirection);
        	
        	program.SetUniformVector("Camera.RightDirection", rightDirection);
        	
        	Viewport.SetShaderData(program);
        }

        /// <summary> ���������� ����������� ���� ��� �������� ����� ���� �����������. </summary>
        public Vector3D CalcDirection(int x, int y)
        {
            Vector2D screen = Viewport.Convert(x, y);

            Vector3D direction = viewDirection +
                                 rightDirection * screen.X * Viewport.HorizontalScale +
                                 upDirection * screen.Y * Viewport.VerticalScale;

            return Vector3D.Normalize(direction);
        }        
        
        #endregion

        #region Properties

        /// <summary> ���������� ������ (���� ������). </summary>
        public Vector3D Orientation
        {
            get
            {
                return orientation;
            }
            
            set
            {
            	orientation = value;
            	
            	SetOrientation(orientation);
            }
        }

        /// <summary> ������ ����������� '������'. </summary>
        public Vector3D ViewDirection
        {
            get
            {
                return viewDirection;
            }
        }

        /// <summary> ������ ����������� '�����'. </summary>
        public Vector3D UpDirection
        {
            get
            {
                return upDirection;
            }
        }

        /// <summary> ������ ����������� '������'. </summary>
        public Vector3D RightDirection
        {
            get
            {
                return rightDirection;
            }
        }

        #endregion
    }
}
