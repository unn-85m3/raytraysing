using System;
using System.IO;
using System.Text;

using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Graphics
{
	/// <summary> Вспомогательный класс для работы с шейдерами. </summary>
	public class ShaderProgram
	{
        #region Protected Fields

        /// <summary> Идентификатор вершинного шейдера. </summary>
        protected int vertexHandle;

        /// <summary> Идентификатор фрагментного шейдера. </summary>
        protected int fragmentHandle;

        /// <summary> Идентификатор программного объекта. </summary>
        protected int programHandle;

        /// <summary> Информационные журналы шейдерных и программного объекта. </summary>
        protected string log;

        #endregion

        #region Constructor

        /// <summary> Создает пустую шейдерную программу и шейдерные объекты. </summary>
        public ShaderProgram()
        {
            vertexHandle = Gl.glCreateShader(Gl.GL_VERTEX_SHADER);
            fragmentHandle = Gl.glCreateShader(Gl.GL_FRAGMENT_SHADER);

            programHandle = Gl.glCreateProgram();
        }

        #endregion

        #region Private Methods

        /// <summary> Загружает шейдер указанного типа из нескольких файлов. </summary>
        private bool LoadSource(int shaderHandle, string[] fileNames)
        {
            log += "Opening shader source files...\n";
            
            string[] lines = new string[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                log += "File: " + Path.GetFileName(fileNames[i]) + "...";

                StreamReader sr = new StreamReader(fileNames[i]);

                try
                {
                    lines[i] = sr.ReadToEnd();

                    sr.Close();

                    log += "Ok\n";
                }
                catch
                {
                    sr.Close();

                    log += "Failed\n";

                    return false;
                }
            }
            
            Gl.glShaderSource(shaderHandle, lines.Length, lines, null);

            return CompileShader(shaderHandle);
        }
        
        /// <summary> Загружает в шейдерный объект исходный код. </summary>
        private bool CompileShader(int shaderHandle)
        {
            log += "Compiling shader source code...";

            Gl.glCompileShader(shaderHandle);

            int status = 0;

            unsafe { Gl.glGetShaderiv(shaderHandle, Gl.GL_COMPILE_STATUS, new IntPtr(&status)); }

            if (0 != status)
            	log += "Ok\n";
            else           
            	log += "Failed\n";

            int capacity = 0;

            unsafe { Gl.glGetShaderiv(shaderHandle, Gl.GL_INFO_LOG_LENGTH, new IntPtr(&capacity)); }

            StringBuilder info = new StringBuilder(capacity);

            unsafe { Gl.glGetShaderInfoLog(shaderHandle, Int32.MaxValue, null, info); }

            if (0 == info.Length)
            	log += "Info log is empty\n";
            else
                log += info;
            
            if (0 == status)
            	return false;
            
            log += "Attaching shader to program object...";

            Gl.glAttachShader(programHandle, shaderHandle);
            
            log += "Ok\n";

            return true;
        }

        #endregion

        #region Public Methods
        
        #region Методы для управления шейдерными объектами

        /// <summary> Загружает вершинный шейдер из указанного файла. </summary>
        public bool LoadVertexShader(string fileName)
        {        
        	return LoadVertexShader(new string[] { fileName });
        }

        /// <summary> Загружает фрагментный шейдер из указанного файла. </summary>
        public bool LoadFragmentShader(string fileName)
        {
        	return LoadFragmentShader(new string[] { fileName });
        }
        
        /// <summary> Загружает вершинный шейдер из указанных файлов. </summary>
        public bool LoadVertexShader(string[] fileNames)
        {
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            log += "+++                     VERTEX SHADER                     +++\n";
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            
            return LoadSource(vertexHandle, fileNames);
        }

        /// <summary> Загружает фрагментный шейдер из указанных файлов. </summary>
        public bool LoadFragmentShader(string[] fileNames)
        {
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            log += "+++                    FRAGMENT SHADER                    +++\n";
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            
            return LoadSource(fragmentHandle, fileNames);
        }

        /// <summary> Создает шейдерную программу. </summary>
        public bool BuildProgram()
        {
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            log += "+++                    BUILDING PROGRAM                   +++\n";
            log += "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            
            log += "Linking program object...";
            
            Gl.glLinkProgram(programHandle);

            int status = 0;

            unsafe { Gl.glGetProgramiv(programHandle, Gl.GL_LINK_STATUS, new IntPtr(&status)); }

            if (0 != status)
            	log += "Ok\n";
            else
            	log += "Failed\n";

            int capacity = 0;
            
            unsafe { Gl.glGetProgramiv(programHandle, Gl.GL_INFO_LOG_LENGTH, new IntPtr(&capacity)); }

            StringBuilder info = new StringBuilder(capacity);

            unsafe { Gl.glGetProgramInfoLog(programHandle, Int32.MaxValue, null, info); }

            if (0 == info.Length)
            	log += "Info log is empty\n";
            else
            	log += info;
            
            return 0 != status;
        }

        /// <summary> Уничтожает шейдерную программу и шейдерные объекты. </summary>
        public void Destroy()
        {
            unsafe
            {
                Gl.glDeleteShader(vertexHandle);
                Gl.glDeleteShader(fragmentHandle);

                Gl.glDeleteProgram(programHandle);
            }
        }

        /// <summary> Устанавливает шейдерную программу как текущее состояние OpenGL. </summary>
        public void Bind()
        {
            Gl.glUseProgram(programHandle);
        }

        /// <summary> Возвращает OpenGL к стандартной функциональности. </summary>
        public void Unbind()
        {
            Gl.glUseProgram(0);
        }

        #endregion
        
        #region Методы для ввода / вывода данных шейдернх объектов

        /// <summary> Возвращает адрес uniform-переменной по ее имени. </summary>
        public int GetUniformLocation(string name)
        {
            return Gl.glGetUniformLocation(programHandle, name);
        }

        /// <summary> Возвращает адрес attribute-переменной по ее имени. </summary>
        public int GetAttributeLocation(string name)
        {
            return Gl.glGetAttribLocation(programHandle, name);
        }


        /// <summary> Возвращает значение uniform-переменной с заданным именем. </summary>
        public Vector4D GetUniformVector(string name)
        {
            float[] values = new float[4];

            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return Vector4D.Zero;

            Gl.glGetUniformfv(programHandle, location, values);

            return new Vector4D(values);
        }

        /// <summary> Возвращает значение uniform-переменной с заданным адресом. </summary>
        public Vector4D GetUniformVector(int location)
        {
            float[] values = new float[4];

            Gl.glGetUniformfv(programHandle, location, values);

            return new Vector4D(values);
        }

        /// <summary> Возвращает значение attribute-переменной с заданным именем. </summary>
        public Vector4D GetAttributeVector(string name)
        {
            int location = Gl.glGetAttribLocation(programHandle, name);

            if (location < 0)
                return Vector4D.Zero;

            float[] values = new float[4];

            Gl.glGetVertexAttribfv(location, Gl.GL_CURRENT_VERTEX_ATTRIB, values);

            return new Vector4D(values);
        }

        // <summary> Возвращает значение attribute-переменной с заданным адресом. </summary>
        public Vector4D GetAttributeVector(int location)
        {
            float[] values = new float[4];

            Gl.glGetVertexAttribfv(location, Gl.GL_CURRENT_VERTEX_ATTRIB, values);

            return new Vector4D(values);
        }
        

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformInteger(string name, int value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform1i(location, value);

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformInteger(int location, int value)
        {
            Gl.glUniform1i(location, value);

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformFloat(string name, float value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform1f(location, value);

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformFloat(int location, float value)
        {
            Gl.glUniform1f(location, value);

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformVector(string name, Vector2D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform2fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformVector(int location, Vector2D value)
        {
            Gl.glUniform2fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformVector(string name, Vector3D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform3fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformVector(int location, Vector3D value)
        {
            Gl.glUniform3fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformVector(string name, Vector4D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform4fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformVector(int location, Vector4D value)
        {
            Gl.glUniform4fv(location, 1, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformMatrix(string name, Matrix2D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniformMatrix2fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformMatrix(int location, Matrix2D value)
        {
            Gl.glUniformMatrix2fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformMatrix(string name, Matrix3D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniformMatrix3fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformMatrix(int location, Matrix3D value)
        {
            Gl.glUniformMatrix3fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetUniformMatrix(string name, Matrix4D value)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniformMatrix4fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetUniformMatrix(int location, Matrix4D value)
        {
            Gl.glUniformMatrix4fv(location, 1, Gl.GL_TRUE, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным именем. </summary>
        public bool SetTexture(string name, int textureUnit)
        {
            int location = Gl.glGetUniformLocation(programHandle, name);

            if (location < 0)
                return false;

            Gl.glUniform1i(location, textureUnit);

            return true;
        }

        /// <summary> Устанавливает значение uniform-переменной с заданным адресом. </summary>
        public bool SetTexture(int location, int textureUnit)
        {
            Gl.glUniform1i(location, textureUnit);

            return true;
        }

        /// <summary> Устанавливает имя attribute-переменной с заданным адресом. </summary>
        public bool SetAttributeName(int location, string name)
        {
            Gl.glBindAttribLocation(programHandle, location, name);

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным именем. </summary>
        public bool SetAttributeFloat(string name, float value)
        {
            int index = Gl.glGetAttribLocation(programHandle, name);

            if (index < 0)
                return false;

            Gl.glVertexAttrib1f(index, value);

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным адресом. </summary>
        public bool SetAttributeFloat(int location, float value)
        {
            Gl.glVertexAttrib1f(location, value);

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным именем. </summary>
        public bool SetAttributeVector(string name, Vector2D value)
        {
            int index = Gl.glGetAttribLocation(programHandle, name);

            if (index < 0)
                return false;

            Gl.glVertexAttrib2fv(index, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным адресом. </summary>
        public bool SetAttributeVector(int location, Vector2D value)
        {
            Gl.glVertexAttrib2fv(location, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным именем. </summary>
        public bool SetAttributeVector(string name, Vector3D value)
        {
            int index = Gl.glGetAttribLocation(programHandle, name);

            if (index < 0)
                return false;

            Gl.glVertexAttrib3fv(index, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным адресом. </summary>
        public bool SetAttributeVector(int location, Vector3D value)
        {
            Gl.glVertexAttrib3fv(location, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным именем. </summary>
        public bool SetAttributeVector(string name, Vector4D value)
        {
            int index = Gl.glGetAttribLocation(programHandle, name);

            if (index < 0)
                return false;

            Gl.glVertexAttrib4fv(index, value.ToArray());

            return true;
        }

        /// <summary> Устанавливает значение attribute-переменной с заданным адресом. </summary>
        public bool SetAttributeVector(int location, Vector4D value)
        {
            Gl.glVertexAttrib4fv(location, value.ToArray());

            return true;
        }        
        
        #endregion

        #endregion

        #region Properties

        /// <summary> Информационный журнал программного и шейдерных объектов. </summary>
        public string Log
        {
            get
            {
                return log;
            }
        }

        /// <summary> Идентификатор вершинного шейдера. </summary>
        public int VertexShaderHandle
        {
            get
            {
                return vertexHandle;
            }
        }

        /// <summary> Идентификатор фрагментного шейдера. </summary>
        public int FragmentShaderHandle
        {
            get
            {
                return fragmentHandle;
            }
        }

        /// <summary> Идентификатор программного объекта. </summary>
        public int ProgramHandle
        {
            get
            {
                return programHandle;
            }
        }
        
        #endregion
	}
}
