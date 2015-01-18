using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

using Auxiliary.Graphics;
using Auxiliary.MathTools;
using Auxiliary.Raytracing;
using Tao.OpenGl;

namespace SceneEditor
{
	/// <summary> Главное окно приложения. </summary>
    public partial class MainForm : Form
    {
        #region Private Fields
        
        /// <summary> Управляет камерой при помощи мыши. </summary>
        private Mouse mouse = new Mouse();
		
		/// <summary> Управляет камерой при помощи клавиатуры. </summary>
		private Keyboard keyboard = new Keyboard();
		
		/// <summary> Сцена с объектами и источниками света. </summary>
		private Scene scene = new Scene();
		
		/// <summary> Точечный рисунок для сохранения изображения. </summary>
		private Raster raster = null;
		
		/// <summary> Режим работы окна просмотра. </summary>
    	private ViewMode mode = ViewMode.Editor;
		
        #endregion
        
        #region Constructor
        
        /// <summary> Создает главное окно приложения. </summary>
        public MainForm()
        {
            InitializeComponent();
            InitScene();
        }

        private void InitScene()
        {
            OpenScene();
        }        
        
        #endregion
        
        #region Private Methods
        
        
        
        #region Сохранение и загрузка сцены и отдельных объектов
        
        /// <summary> Загружает сцену из файла. </summary>
        private void OpenScene()
        {
        	OpenFileDialog dialog = new OpenFileDialog();
        	
        	dialog.Filter = "Файлы сцен (*.scn) | *.scn";
        	
        	if (DialogResult.OK == dialog.ShowDialog())
        	{
        		using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open))
				{
					IFormatter formatter = new BinaryFormatter();
					
					try
					{					
						scene = (Scene) formatter.Deserialize(fileStream);
					}
					catch
					{
						MessageBox.Show("Ошибка! Не удается загрузить сцену из файла.");
					}
					
					fileStream.Close();
				}
	        	
	        	
        	}
        }
        
        /// <summary> Сохраняет сцену в файл. </summary>
        private void SaveScene()
        {
        	SaveFileDialog dialog = new SaveFileDialog();
        	
        	dialog.Filter = "Файлы сцен (*.scn) | *.scn";
        	
        	if (DialogResult.OK == dialog.ShowDialog())
        	{        	
	        	using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Create))
				{
					IFormatter formatter = new BinaryFormatter();
					
					try
					{
						formatter.Serialize(fileStream, scene);
					}
					catch
					{
						MessageBox.Show("Ошибка! Не удается сохранить сцену в файл.");
					}
					
					fileStream.Close();
	        	}
        	}        	
        }
        
        /// <summary> Загружает объект из файла. </summary>
        private void OpenPrimitive()
        {
        	OpenFileDialog dialog = new OpenFileDialog();
        	
        	dialog.Filter = "Файлы объектов (*.prim) | *.prim";
        	
        	if (DialogResult.OK == dialog.ShowDialog())
        	{
        		using (FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open))
				{
					IFormatter formatter = new BinaryFormatter();
					
					try
					{					
						Primitive primitive = (Primitive) formatter.Deserialize(fileStream);
						
						scene.Primitives.Add(primitive);
						
						
					}
					catch
					{
						MessageBox.Show("Ошибка! Не удается загрузить объект из файла.");
					}
					
					fileStream.Close();
        		}
        	}
        }
        
            
        
        /// <summary> Сохраняет сгенерированное изображение в файл. </summary>
        private void SaveImage()
        {
        	if (null != raster)
        	{
	        	SaveFileDialog dialog = new SaveFileDialog();
	        	
	        	dialog.Filter = "Точечный рисунок (*.bmp) | *.bmp";
	        	
	        	if (DialogResult.OK == dialog.ShowDialog())
	        	{        	
	        		raster.ToBitmap().Save(dialog.FileName);
	        	}
        	}
        	else
        	{
        		MessageBox.Show("Ошибка! Изображение недоступно.");
        	}
        }
        
        #endregion
        
        #region Настройка визуализации
                
        /// <summary> Настраивает параметры состояния API OpenGL. </summary>
        private void InitOpenGL()
        {
        	Gl.glShadeModel(Gl.GL_SMOOTH);
        	
        	Gl.glEnable(Gl.GL_POINT_SMOOTH);
    
        	Gl.glPointSize(5.0f);
        }           

        /// <summary> Устанавлиает положение и ориентацию камеры по умолчанию. </summary>
        private void InitCamera()
        {
        	scene.Camera.Position = new Vector3D(29.0f, 11.0f, 11.0f);
        	
        	scene.Camera.Orientation = new Vector3D(0.0f, 0.36f, -0.38f);       	
        }
        
        /// <summary> Перключает режимы 'редактирование сцены / просмотр результата'. </summary>
        private void SwitchMode(ViewMode mode)
        {
        	this.mode = mode;
        	
        	if (ViewMode.Editor == mode)
        	{        		
        		// Инициализируем параметры состояния API OpenGL
        		{
        			Gl.glEnable(Gl.GL_LIGHTING);
        			
        			Gl.glEnable(Gl.GL_DEPTH_TEST);
        			
        			Gl.glDisable(Gl.GL_TEXTURE_2D);
        		}
        		
        		// Устанавливаем размеры окна вывода
        		Gl.glViewport(0, 0, panelOpenGL.Width, panelOpenGL.Height);
        	}
        	else
        	{
        		// Инициализируем параметры состояния API OpenGL
        		{
        			Gl.glDisable(Gl.GL_LIGHTING);
	        		
	        		Gl.glDisable(Gl.GL_DEPTH_TEST);
	        		
	        		Gl.glEnable(Gl.GL_TEXTURE_2D);
        		}
        		
        		// Устанавливаем размеры окна вывода
        		Gl.glViewport(0, 0, panelOpenGL.Width, panelOpenGL.Height);
        		
        		// Устанавливаем матрицу проекции
        		{
        			Gl.glMatrixMode(Gl.GL_PROJECTION);
        			            
		            Gl.glLoadIdentity();              
		            
		            Gl.glOrtho(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f);
		            
		            Gl.glMatrixMode(Gl.GL_MODELVIEW);
		            
		            Gl.glLoadIdentity();
        		}        		
        	}
        }
        
        #endregion
        
        #region Визуализация сцены
        
        /// <summary> Отрисовывает сцену в выбранном режиме. </summary>
        private void DrawScene()
        {        	
        	if (ViewMode.Editor == mode)
        	{
	        	Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
	        	
	        	mouse.Apply(scene.Camera);
	        	
	        	keyboard.Apply(scene.Camera);        	            
	        	            
	        	scene.Camera.Setup();
	        	
	        	scene.Draw();
	        	
	        	labelPosition.Text = "Положение: " + scene.Camera.Position.ToString();
	        	
	        	labelOrientation.Text = "Ориентация: " + scene.Camera.Orientation.ToString();
        	}
        	else
        	{
        		Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
			
        		Gl.glColor3f(1.0f, 1.0f, 1.0f);
				
        		Gl.glBegin(Gl.GL_QUADS);
				{
					Gl.glTexCoord2f(0.0f, 0.0f);
					Gl.glVertex2f(-1.0f, -1.0f);
					
					Gl.glTexCoord2f(0.0f, 1.0f);
					Gl.glVertex2f(-1.0f, 1.0f);
					
					Gl.glTexCoord2f(1.0f, 1.0f);
					Gl.glVertex2f(1.0f, 1.0f);
					
					Gl.glTexCoord2f(1.0f, 0.0f);
					Gl.glVertex2f(1.0f, -1.0f);
				}
				Gl.glEnd();      		
        	}
        } 
 
        #endregion
        
        #endregion
        
        #region Event Handlers
        
        #region Общие операции
        
        private void MainFormLoad(object sender, EventArgs e)
        {
        	panelOpenGL.InitializeContexts();
        	
        	InitOpenGL();
        	
        	InitCamera();
        	
        	SwitchMode(ViewMode.Editor);
        }

       
        
        private void MenuItemExitClick(object sender, EventArgs e)
        {
        	Close();
        }
        
        #endregion
        
        #region Настройка визуализации
        
        private void ButtonAxesCheckedChanged(object sender, EventArgs e)
        {
        	scene.Axes.Visible = buttonAxes.Checked;
        }
        
        private void ButtonVolumeCheckedChanged(object sender, EventArgs e)
        {
        	scene.Volume.Visible = buttonVolume.Checked;
        }        
        
        private void ButtonCameraClick(object sender, EventArgs e)
        {
        	InitCamera();
        } 
        
      
        
        #endregion
        
        #region Визуализация сцены
        
        private void PanelOpenGLPaint(object sender, PaintEventArgs e)
        {
        	DrawScene();
        }
        
        private void PanelOpenGLKeyDown(object sender, KeyEventArgs e)
        {
        	keyboard.OnKeyDown(e);
        }
        
        private void PanelOpenGLKeyUp(object sender, KeyEventArgs e)
        {
        	keyboard.OnKeyUp(e);
        }
        
        private void PanelOpenGLMouseDown(object sender, MouseEventArgs e)
        {
        	mouse.OnMouseDown(e);
        	
        	labelMouseActive.Visible = mouse.Active;
        }
        
        private void PanelOpenGLMouseMove(object sender, MouseEventArgs e)
        {
        	mouse.OnMouseMove(e);
        }

        private void MenuItemEditorClick(object sender, EventArgs e)
        {
        	SwitchMode(ViewMode.Editor);
        }        
        
        private void MenuItemRenderClick(object sender, EventArgs e)
        {
        	backgroundWorker.RunWorkerAsync();	        	
        }        

        private void BackgroundWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        	raster = new Raster(scene.Camera.Viewport.Width, scene.Camera.Viewport.Height);
        			
        	Engine.Render(scene, raster, backgroundWorker);        			     	
        }
        
        private void BackgroundWorkerProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        	progressBar.Value = e.ProgressPercentage;
        	
        	labelTime.Text = e.UserState.ToString();
        }        

        private void BackgroundWorkerRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        	Texture2D texture = raster.ToTexture();
        			
        	Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.Handle);
        	
        	SwitchMode(ViewMode.Result);
        }
        
        private void TimerTick(object sender, EventArgs e)
        {
        	panelOpenGL.Invalidate();
        }
        
        #endregion
        
        #region Редактирование сцены
    
            
        
        private void ListViewPrimitivesItemChecked(object sender, ItemCheckedEventArgs e)
        {
        	scene.Primitives[e.Item.Index].Visible = e.Item.Checked;
        }
        
       
        
        #endregion
        
        #region Сохранение и загрузка сцены и отдельных объектов
        
        private void ButtonOpenPrimitiveClick(object sender, EventArgs e)
        {
        	OpenPrimitive();
        }
        
      
        
        private void MenuItemOpenSceneClick(object sender, EventArgs e)
        {
        	OpenScene();
        }
        
        private void MenuItemOpenPrimitiveClick(object sender, EventArgs e)
        {
        	OpenPrimitive();
        }
       
        
        private void MenuItemSaveSceneClick(object sender, EventArgs e)
        {
        	SaveScene();
        }
        
       
        private void MenuItemSaveImageClick(object sender, EventArgs e)
        {
        	SaveImage();
        }        
        
        #endregion

        private void panelOpenGL_Load(object sender, EventArgs e)
        {

        }
        
        #endregion       

       
    }
    
    /// <summary> Режим работы окна просмотра. </summary>
    public enum ViewMode
    {
    	/// <summary> Режим редактирования сцены. </summary>
    	Editor,
    	
    	/// <summary> Режим просмотра результата рендеринга. </summary>
    	Result
    }
}
 