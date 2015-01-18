using System;
using Auxiliary.MathTools;
using Auxiliary.Graphics;

namespace Auxiliary.Raytracing
{
    /// <summary> Режим наложения текстуры. </summary>
    [Serializable]
    public enum MapMode
    {
    	/// <summary> Обрезка текстуры. </summary>
        Clamp,
        
        /// <summary> Размножение текстуры. </summary>
        Repeat
    }

    /// <summary> Абстрактная двумерная текстура. </summary>
    [Serializable]
    public abstract class AbstractTexture
    {
        #region Public Fields

        /// <summary> Режим наложения текстуры. </summary>
        public MapMode MapMode = MapMode.Repeat;

        /// <summary> Коэффициенты масштабирования текстуры. </summary>
        public Vector2D Scale = Vector2D.Unit;

        #endregion

        #region Constructor
    
        /// <summary> Создает новую абстрактную текстуру. </summary>
        public AbstractTexture() { }
        
        /// <summary> Создает новую абстрактную текстуру с заданным масштабированием. </summary>
        public AbstractTexture(Vector2D scale)
        {
            Scale = scale;
        }        
        
        /// <summary> Создает новую абстрактную текстуру с заданным масштабированием и наложением. </summary>
        public AbstractTexture(Vector2D scale, MapMode mode)
        	: this(scale)
        {
            MapMode = mode;
        }

        #endregion

        #region Protected Methods

        /// <summary> Производит обработку текстурных координат. </summary>
        protected void ProcessTexCoord(ref Vector2D texcoord)
        {
            if (MapMode.Clamp == MapMode)
            {
                texcoord = Vector2D.Clamp(Scale * texcoord, 0.0f, 1.0f);
            }
            else
            {
                texcoord = Vector2D.Fract(Scale * texcoord);
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Осуществляет поиск по текстуре. </summary>
        public abstract Vector3D CalcColor(Vector2D texcoord);

        #endregion
    }

    /// <summary> Двумерная текстура, загружаемая из графического файла. </summary>
    [Serializable]
    public class ImageTexture : AbstractTexture
    {
        #region Private Fields

        /// <summary> Массив текселей. </summary>
        private Raster raster = null;

        #endregion

        #region Constructor
        
        /// <summary> Создает текстуру из графичекого файла. </summary>
        public ImageTexture(string fileName)
        {
            raster = new Raster(fileName);
        }
        
        /// <summary> Создает текстуру из графичекого файла. </summary>
        public ImageTexture(string path, Vector2D scale)
            : base(scale)
        {
            raster = new Raster(path);
        }        

        /// <summary> Создает текстуру из графичекого файла. </summary>
        public ImageTexture(string path, MapMode mode, Vector2D scale)
            : base(scale, mode)
        {
            raster = new Raster(path);
        }

        #endregion
        
        #region Public Methods
        
        /// <summary> Осуществляет поиск по текстуре. </summary>
        public override Vector3D CalcColor(Vector2D texcoord)
        {
        	ProcessTexCoord(ref texcoord);

            texcoord *= new Vector2D(raster.Width - 1,
        	                         raster.Height - 1);
        	
        	float u = texcoord.X;
        	float v = texcoord.Y;
        	
        	int x = (int)Math.Floor(u);
		  	int y = (int)Math.Floor(v);
		  	
		  	float u_ratio = u - x;
		  	float v_ratio = v - y;
		  	
		  	float u_opposite = 1 - u_ratio;
		  	float v_opposite = 1 - v_ratio;
		  	
		  	Vector3D result = (raster[x, y]   * u_opposite  + raster[x+1, y]   * u_ratio) * v_opposite + 
		                   (raster[x, y+1] * u_opposite  + raster[x+1, y+1] * u_ratio) * v_ratio;      
		  	
		  	return result;

//            Vector2D ratio = Vector2D.Fract(texcoord);
//
//            int x = Convert.ToInt32(texcoord.X);           
//            int y = Convert.ToInt32(texcoord.Y);
//
//            Vector3D result = Vector3D.Mix(Vector3D.Mix(raster[x, y], raster[x + 1, y], ratio.X),
//                                           Vector3D.Mix(raster[x, y + 1], raster[x + 1, y + 1], ratio.X),
//                                           ratio.Y);
//
//            return result;
        }        
    
        #endregion
    }
    
    /// <summary> Процедурная текстура 'Шахматная доска'. </summary>
    [Serializable]
    public class ChessBoardTexture : AbstractTexture
    {
        #region Public Fields

        /// <summary> Первый цвет ячеек. </summary>
        public Vector3D FirstColor = new Vector3D(0.0f, 0.0f, 0.0f);
        
        /// <summary> Второй цвет ячеек. </summary>
        public Vector3D SecondColor = new Vector3D(1.0f, 1.0f, 1.0f);
        
        #endregion

        #region Constructor
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом. </summary>
        public ChessBoardTexture(Vector2D scale)
        	: base(scale) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами и масштабом. </summary>
        public ChessBoardTexture(Vector3D firstColor, Vector3D secondColor, Vector2D scale)        
        	: base(scale)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }        
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом и наложением. </summary>
        public ChessBoardTexture(Vector2D scale, MapMode mode)
        	: base(scale, mode) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами, масштабом и наложением. </summary>
        public ChessBoardTexture(Vector3D firstColor, Vector3D secondColor, Vector2D scale, MapMode mode)
            : base(scale, mode)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }

        #endregion
        
        #region Public Methods
        
        /// <summary> Осуществляет поиск по текстуре. </summary>
        public override Vector3D CalcColor(Vector2D texcoord)
        {
            ProcessTexCoord(ref texcoord);
            
            Vector2D border = Vector2D.Unit / 2.0f;
            
            if (texcoord < border || texcoord > border)
            {
            	return FirstColor;
            }
            else
            {
            	return SecondColor;
            }
        }
    
        #endregion    	
    }
    
    [Serializable]
    public class ChessBoardTexture1 : AbstractTexture
    {
        #region Public Fields

        /// <summary> Первый цвет ячеек. </summary>
        public Vector3D FirstColor = new Vector3D(0.0f, 0.0f, 0.0f);
        
        /// <summary> Второй цвет ячеек. </summary>
        public Vector3D SecondColor = new Vector3D(1.0f, 1.0f, 1.0f);
        
        #endregion

        #region Constructor
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом. </summary>
        public ChessBoardTexture1(Vector2D scale)
        	: base(scale) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами и масштабом. </summary>
        public ChessBoardTexture1(Vector3D firstColor, Vector3D secondColor, Vector2D scale)        
        	: base(scale)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }        
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом и наложением. </summary>
        public ChessBoardTexture1(Vector2D scale, MapMode mode)
        	: base(scale, mode) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами, масштабом и наложением. </summary>
        public ChessBoardTexture1(Vector3D firstColor, Vector3D secondColor, Vector2D scale, MapMode mode)
            : base(scale, mode)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }

        #endregion
        
        #region Public Methods
        
        /// <summary> Осуществляет поиск по текстуре. </summary>
        public override Vector3D CalcColor(Vector2D texcoord)
        {
            ProcessTexCoord(ref texcoord);
            
            Vector2D border = Vector2D.Unit / 2.0f;
            
           
            if (texcoord.Y < 0.5f)
            {
            	return FirstColor;
            }
            else
            {
            	return SecondColor;
            }
        }
    
        #endregion    	
    }
    
    [Serializable]
    public class ChessBoardTexture2 : AbstractTexture
    {
        #region Public Fields

        /// <summary> Первый цвет ячеек. </summary>
        public Vector3D FirstColor = new Vector3D(0.0f, 0.0f, 0.0f);
        
        /// <summary> Второй цвет ячеек. </summary>
        public Vector3D SecondColor = new Vector3D(1.0f, 1.0f, 1.0f);
        
        #endregion

        #region Constructor
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом. </summary>
        public ChessBoardTexture2(Vector2D scale)
        	: base(scale) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами и масштабом. </summary>
        public ChessBoardTexture2(Vector3D firstColor, Vector3D secondColor, Vector2D scale)        
        	: base(scale)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }        
        
        /// <summary> Создает текстуру шахматной доски с заданным масштабом и наложением. </summary>
        public ChessBoardTexture2(Vector2D scale, MapMode mode)
        	: base(scale, mode) { }
        
        /// <summary> Создает текстуру шахматной доски с заданными цветами, масштабом и наложением. </summary>
        public ChessBoardTexture2(Vector3D firstColor, Vector3D secondColor, Vector2D scale, MapMode mode)
            : base(scale, mode)
        {
        	FirstColor = firstColor;
        	SecondColor = secondColor;
        }

        #endregion
        
        #region Public Methods
        
        /// <summary> Осуществляет поиск по текстуре. </summary>
        public override Vector3D CalcColor(Vector2D texcoord)
        {
            ProcessTexCoord(ref texcoord);
            
            Vector2D border = Vector2D.Unit / 2.0f;
            
            texcoord -= border;
            
            if (texcoord.X * texcoord.X * texcoord.X * texcoord.X +  texcoord.Y * texcoord.Y * texcoord.Y * texcoord.Y  < 0.06f)
            {
            	return FirstColor;
            }
            else
            {
            	return SecondColor;
            }
        }
    
        #endregion    	
    }   
}