using System;
using System.Drawing;
using System.Collections.Generic;
using Auxiliary.MathTools;

namespace Auxiliary.Graphics
{
    /// <summary> Простой класс для представления растра. </summary>
    [Serializable]
    public class Raster
    {
        #region Private Fields

        /// <summary> Массив пикселей растра. </summary>
        private Vector3D[,] pixels = null;

        #endregion

        #region Constructor

        /// <summary> Создает новый растр. </summary>
        public Raster(int width, int height)
        {
            pixels = new Vector3D[width, height];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    pixels[i, j] = Vector3D.Zero;
                }
            }
        }

        /// <summary> Создает новый растр. </summary>
        public Raster(string fileName)
        {
            Bitmap image = new Bitmap(fileName);

            pixels = new Vector3D[image.Width, image.Height];

            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    Color color = image.GetPixel(i, j);

                    pixels[i, j] = new Vector3D(color.R / 255f, color.G / 255f, color.B / 255f);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Конвертирует растр в объект Bitmap (медленно!). </summary>
        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height);

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    bitmap.SetPixel(i, j,
                                    Color.FromArgb((int)(255f * pixels[i, j].X),
                                                   (int)(255f * pixels[i, j].Y),
                                                   (int)(255f * pixels[i, j].Z)));
                }
            }

            return bitmap;
        }

        /// <summary> Конвертирует растр в текстуру API OpenGL. </summary>
        public Texture2D ToTexture()
        {
        	List<float> data = new List<float>();

            for (int j = Height-1; j > -1; j--)
            {
                for (int i = 0; i < Width; i++)
                {
	                for (int k = 0; k < 3; k++)
	                {
	                	data.Add(pixels[i, j][k]);
	                }
                }
            }
            
            Texture2D texture = new Texture2D(Width, Height);

            texture.Create(data.ToArray());
            
            return texture;
        }
        
        /// <summary> Приводит растр к стандартному диапазону [0, 1]. </summary>
        public void Normalize()
        {
        	float length = 0f;
        	
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                	length = Math.Max(Vector3D.Length(pixels[i, j]), length);
                }
            }
            
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                	pixels[i, j] /= length;
                }
            }
        }

        /// <summary> Производит копирование растра (быстрее механизма сериализации). </summary>
        public Raster Clone()
        {
        	Raster raster = new Raster(Width, Height);
        	
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    raster[i, j] = pixels[i, j];
                }
            }
            
            return raster;
        }        
        
        #endregion

        #region Properties

        /// <summary> Ширина растра. </summary>
        public int Width
        {
            get
            {
                return pixels.GetLength(0);
            }
        }

        /// <summary> Высота растра. </summary>
        public int Height
        {
            get
            {
                return pixels.GetLength(1);
            }
        }

        /// <summary> Обеспечивает прямой доступ к пикселям растра. </summary>
        public Vector3D this[int i, int j]
        {
            get
            {
                return pixels[i, j];
            }

            set
            {
                pixels[i, j] = value;
            }
        }

        #endregion
    }
}
