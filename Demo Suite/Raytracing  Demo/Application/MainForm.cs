﻿using System;
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

        /// <summary> Источник света. </summary>
        private Light light = null;

        /// <summary> Стенки. </summary>
        private Auxiliary.Raytracing.Primitive backSquare = null;
        private Auxiliary.Raytracing.Primitive leftSquare = null;
		
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
            OpenScene("E:/GitHub/scn_main.scn");
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

        /// <summary> Загружает сцену из файла. </summary>
        private void OpenScene(string path)
        {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();

                    try
                    {
                        this.scene = (Scene)formatter.Deserialize(fileStream);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка! Не удается загрузить сцену из файла.");
                    }

                    fileStream.Close();

                    this.scene.Volume.Visible = false;
                    this.scene.Axes.Visible = false;

                    backSquare = this.scene.Primitives[0];
                    leftSquare = this.scene.Primitives[1];

                    light = this.scene.Lights[0];


                    this.scene.Lights[1].AmbiantIntensity.X = 0f;
                    this.scene.Lights[1].AmbiantIntensity.Y = 0f;
                    this.scene.Lights[1].AmbiantIntensity.Z = 0f;
                    this.scene.Lights[1].DiffuseIntensity.X = 0f;
                    this.scene.Lights[1].DiffuseIntensity.Y = 0f;
                    this.scene.Lights[1].DiffuseIntensity.Z = 0f; 
                    this.scene.Lights[1].SpecularIntensity.X = 0f;
                    this.scene.Lights[1].SpecularIntensity.Y = 0f;
                    this.scene.Lights[1].SpecularIntensity.Z = 0f;


                    buttonStart.Enabled = false;
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
        	scene.Camera.Position = new Vector3D(35.0f, 15.0f, 11.0f);
        	
        	scene.Camera.Orientation = new Vector3D(0.0f, 0.32f, -0.32f);       	
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

            backgroundWorker.RunWorkerAsync();
        }

       
        
        private void MenuItemExitClick(object sender, EventArgs e)
        {
        	Close();
        }
        
        #endregion
        
        #region Настройка визуализации
        
        #endregion
        
        #region Визуализация сцены
        
        private void PanelOpenGLPaint(object sender, PaintEventArgs e)
        {
            DrawScene();
        } 

        private void trackBarReflectivity_Scroll(object sender, EventArgs e)
        {
            float tmp = (1.0f / trackBarReflectivity.Maximum) * Convert.ToSingle(trackBarReflectivity.Value);
            backSquare.Material.ReflectCoeff.X = 0.2f * tmp;
            backSquare.Material.ReflectCoeff.Y = 0.2f * tmp;
            backSquare.Material.ReflectCoeff.Z = 0.2f * tmp;

            leftSquare.Material.ReflectCoeff.X = 0.2f * tmp;
            leftSquare.Material.ReflectCoeff.Y = 0.2f * tmp;
            leftSquare.Material.ReflectCoeff.Z = 0.2f * tmp;

        }

        private void trackBarLight_Scroll(object sender, EventArgs e)
        {
            float tmp = (1.0f / trackBarLight.Maximum) * Convert.ToSingle(trackBarLight.Value);
            light.AmbiantIntensity.X = 0.5f * tmp;
            light.AmbiantIntensity.Y = 0.5f * tmp;
            light.AmbiantIntensity.Z = 0.5f * tmp;
            light.DiffuseIntensity.X = 1.0f * tmp;
            light.DiffuseIntensity.Y = 1.0f * tmp;
            light.DiffuseIntensity.Z = 1.0f * tmp;
            light.SpecularIntensity.X = 0.3f * tmp;
            light.SpecularIntensity.Y = 0.3f * tmp;
            light.SpecularIntensity.Z = 0.3f * tmp;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            scene.Lights[0] = light;
            scene.Primitives[0] = backSquare;
            scene.Primitives[1] = leftSquare;
            backgroundWorker.RunWorkerAsync();
            buttonStart.Enabled = false;
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

            buttonStart.Enabled = true;
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
 