using System;
using System.Collections.Generic;
using Auxiliary.MathTools;
using Tao.OpenGl;

namespace Auxiliary.Raytracing
{
    /// <summary> Описывает свойства материала объекта (на основе модели Уиттеда). </summary>
    [Serializable]
    public class Material
    {
        #region Public Fields

        /// <summary> Коэффициент отражения фонового (ambiant) света. </summary>
        public Vector3D Ambiant = new Vector3D(0.2f, 0.2f, 0.2f);

        /// <summary> Коэффициент отражения диффузного (diffuse) света. </summary>
        public Vector3D Diffuse = new Vector3D(0.5f, 0.5f, 0.5f);

        /// <summary> Коэффициент отражения зеркального (specular) света. </summary>
        public Vector3D Specular = new Vector3D(0.5f, 0.5f, 0.5f);

        /// <summary> Вклад отраженного луча. </summary>
        public Vector3D ReflectCoeff = new Vector3D(0.0f, 0.0f, 0.0f);

        /// <summary> Вклад преломленного луча. </summary>
        public Vector3D RefractCoeff = new Vector3D(0.0f, 0.0f, 0.0f);

        /// <summary> Коэффициент резкости бликов. </summary>
        public float Shininess = 64.0f;

        /// <summary> Коэффициент преломления материала. </summary>
        public float RefractIndex = 1.5f;

        /// <summary> Собственный цвет поверхности. </summary>
        public Vector3D Color = new Vector3D(1.0f, 1.0f, 1.0f);
        
        /// <summary> Текстура для наложения на объект. </summary>
        public AbstractTexture Texture = null;

        #endregion

        #region Constructors

        /// <summary> Создает новый материал по умолчанию. </summary>
        public Material() { }
        
        /// <summary> Создает новый материал с заданным цветом. </summary>
        public Material(Vector3D color)
        {
            Color = color;
        }        

        /// <summary> Создает новый материал с заданным цветом и коэффициентами Фонга. </summary>
        public Material(Vector3D color, Vector3D ambiant, Vector3D diffuse, Vector3D specular, float shininess)
        	: this(color)
        {
            Ambiant = ambiant;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }
        
        /// <summary> Создает новый материал с заданным цветом и коэффициентами Уиттеда. </summary>
        public Material(Vector3D color, Vector3D ambiant, Vector3D diffuse, Vector3D specular, float shininess,
                        Vector3D reflectCoeff, Vector3D refractCoeff, float refractIndex)
        	: this(color, ambiant, diffuse, specular, shininess)
        {
            ReflectCoeff = reflectCoeff;
            RefractCoeff = refractCoeff;
            RefractIndex = refractIndex;
        }    

        #endregion
        
        #region Public Methods
        
        /// <summary> Загружает свойства материала (не все) в параметры состояния API OpenGL. </summary>
        public void Setup()
        {
        	Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT, Ambiant.ToArray());

            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, (Diffuse * Color).ToArray());

            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, Specular.ToArray());

            Gl.glMaterialf(Gl.GL_FRONT_AND_BACK, Gl.GL_SHININESS, Shininess);
        }
        
        #endregion
    }
}
