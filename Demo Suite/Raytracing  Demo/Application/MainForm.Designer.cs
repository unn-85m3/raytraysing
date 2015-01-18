namespace SceneEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.режимToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRender = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelView = new System.Windows.Forms.Panel();
            this.panelOpenGL = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.toolStripViewport = new System.Windows.Forms.ToolStrip();
            this.labelViewport = new System.Windows.Forms.ToolStripLabel();
            this.buttonSceneProperties = new System.Windows.Forms.ToolStripButton();
            this.buttonCamera = new System.Windows.Forms.ToolStripButton();
            this.buttonVolume = new System.Windows.Forms.ToolStripButton();
            this.buttonAxes = new System.Windows.Forms.ToolStripButton();
            this.labelMouseActive = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelOrientation = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.panelView.SuspendLayout();
            this.toolStripViewport.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.режимToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(531, 24);
            this.menuStrip.TabIndex = 3;
            // 
            // режимToolStripMenuItem
            // 
            this.режимToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditor,
            this.menuItemRender});
            this.режимToolStripMenuItem.Name = "режимToolStripMenuItem";
            this.режимToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.режимToolStripMenuItem.Text = "Режим";
            // 
            // menuItemEditor
            // 
            this.menuItemEditor.Name = "menuItemEditor";
            this.menuItemEditor.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuItemEditor.Size = new System.Drawing.Size(152, 22);
            this.menuItemEditor.Text = "Редактор";
            this.menuItemEditor.Click += new System.EventHandler(this.MenuItemEditorClick);
            // 
            // menuItemRender
            // 
            this.menuItemRender.Name = "menuItemRender";
            this.menuItemRender.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.menuItemRender.Size = new System.Drawing.Size(152, 22);
            this.menuItemRender.Text = "Рендеринг";
            this.menuItemRender.Click += new System.EventHandler(this.MenuItemRenderClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.White;
            this.imageList.Images.SetKeyName(0, "shere.bmp");
            this.imageList.Images.SetKeyName(1, "rect.bmp");
            this.imageList.Images.SetKeyName(2, "box.bmp");
            this.imageList.Images.SetKeyName(3, "surface.bmp");
            this.imageList.Images.SetKeyName(4, "light.bmp");
            // 
            // panelView
            // 
            this.panelView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelView.Controls.Add(this.panelOpenGL);
            this.panelView.Controls.Add(this.toolStripViewport);
            this.panelView.Location = new System.Drawing.Point(9, 35);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(516, 541);
            this.panelView.TabIndex = 12;
            // 
            // panelOpenGL
            // 
            this.panelOpenGL.AccumBits = ((byte)(0));
            this.panelOpenGL.AutoCheckErrors = false;
            this.panelOpenGL.AutoFinish = false;
            this.panelOpenGL.AutoMakeCurrent = true;
            this.panelOpenGL.AutoSwapBuffers = true;
            this.panelOpenGL.BackColor = System.Drawing.Color.Black;
            this.panelOpenGL.ColorBits = ((byte)(32));
            this.panelOpenGL.DepthBits = ((byte)(16));
            this.panelOpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpenGL.Location = new System.Drawing.Point(0, 25);
            this.panelOpenGL.Name = "panelOpenGL";
            this.panelOpenGL.Size = new System.Drawing.Size(512, 512);
            this.panelOpenGL.StencilBits = ((byte)(0));
            this.panelOpenGL.TabIndex = 8;
            this.panelOpenGL.Load += new System.EventHandler(this.panelOpenGL_Load);
            this.panelOpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelOpenGLPaint);
            this.panelOpenGL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PanelOpenGLKeyDown);
            this.panelOpenGL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PanelOpenGLKeyUp);
            this.panelOpenGL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelOpenGLMouseDown);
            this.panelOpenGL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelOpenGLMouseMove);
            // 
            // toolStripViewport
            // 
            this.toolStripViewport.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripViewport.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripViewport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelViewport,
            this.buttonSceneProperties,
            this.buttonCamera,
            this.buttonVolume,
            this.buttonAxes,
            this.labelMouseActive,
            this.toolStripButton1});
            this.toolStripViewport.Location = new System.Drawing.Point(0, 0);
            this.toolStripViewport.Name = "toolStripViewport";
            this.toolStripViewport.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripViewport.Size = new System.Drawing.Size(512, 25);
            this.toolStripViewport.TabIndex = 4;
            // 
            // labelViewport
            // 
            this.labelViewport.Name = "labelViewport";
            this.labelViewport.Size = new System.Drawing.Size(100, 22);
            this.labelViewport.Text = "Окно просмотра";
            // 
            // buttonSceneProperties
            // 
            this.buttonSceneProperties.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonSceneProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSceneProperties.Image = ((System.Drawing.Image)(resources.GetObject("buttonSceneProperties.Image")));
            this.buttonSceneProperties.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSceneProperties.ImageTransparentColor = System.Drawing.Color.Red;
            this.buttonSceneProperties.Name = "buttonSceneProperties";
            this.buttonSceneProperties.Size = new System.Drawing.Size(23, 22);
            this.buttonSceneProperties.ToolTipText = "Параметры визуализации";
            // 
            // buttonCamera
            // 
            this.buttonCamera.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonCamera.Image = ((System.Drawing.Image)(resources.GetObject("buttonCamera.Image")));
            this.buttonCamera.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonCamera.ImageTransparentColor = System.Drawing.Color.Red;
            this.buttonCamera.Name = "buttonCamera";
            this.buttonCamera.Size = new System.Drawing.Size(23, 22);
            this.buttonCamera.ToolTipText = "Камера по умолчанию";
            this.buttonCamera.Click += new System.EventHandler(this.ButtonCameraClick);
            // 
            // buttonVolume
            // 
            this.buttonVolume.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonVolume.Checked = true;
            this.buttonVolume.CheckOnClick = true;
            this.buttonVolume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonVolume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonVolume.Image = ((System.Drawing.Image)(resources.GetObject("buttonVolume.Image")));
            this.buttonVolume.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonVolume.ImageTransparentColor = System.Drawing.Color.Red;
            this.buttonVolume.Name = "buttonVolume";
            this.buttonVolume.Size = new System.Drawing.Size(23, 22);
            this.buttonVolume.ToolTipText = "Отобразить / скрыть ограничивающий объем";
            this.buttonVolume.CheckedChanged += new System.EventHandler(this.ButtonVolumeCheckedChanged);
            // 
            // buttonAxes
            // 
            this.buttonAxes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonAxes.Checked = true;
            this.buttonAxes.CheckOnClick = true;
            this.buttonAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonAxes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAxes.Image = ((System.Drawing.Image)(resources.GetObject("buttonAxes.Image")));
            this.buttonAxes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAxes.ImageTransparentColor = System.Drawing.Color.Red;
            this.buttonAxes.Name = "buttonAxes";
            this.buttonAxes.Size = new System.Drawing.Size(23, 22);
            this.buttonAxes.ToolTipText = "Отобразить / скрыть оси";
            this.buttonAxes.CheckedChanged += new System.EventHandler(this.ButtonAxesCheckedChanged);
            // 
            // labelMouseActive
            // 
            this.labelMouseActive.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.labelMouseActive.ForeColor = System.Drawing.Color.Brown;
            this.labelMouseActive.Name = "labelMouseActive";
            this.labelMouseActive.Size = new System.Drawing.Size(23, 22);
            this.labelMouseActive.Text = "8";
            this.labelMouseActive.Visible = false;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 20;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerRunWorkerCompleted);
            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.progressBar);
            this.panelStatus.Controls.Add(this.labelOrientation);
            this.panelStatus.Controls.Add(this.labelPosition);
            this.panelStatus.Controls.Add(this.labelTime);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(0, 587);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(531, 21);
            this.panelStatus.TabIndex = 13;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(360, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(71, 21);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 5;
            // 
            // labelOrientation
            // 
            this.labelOrientation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelOrientation.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelOrientation.Location = new System.Drawing.Point(180, 0);
            this.labelOrientation.Name = "labelOrientation";
            this.labelOrientation.Size = new System.Drawing.Size(180, 21);
            this.labelOrientation.TabIndex = 4;
            this.labelOrientation.Text = "---";
            this.labelOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPosition
            // 
            this.labelPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPosition.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelPosition.Location = new System.Drawing.Point(0, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(180, 21);
            this.labelPosition.TabIndex = 3;
            this.labelPosition.Text = "---";
            this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTime
            // 
            this.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTime.Location = new System.Drawing.Point(431, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(100, 21);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "---";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(531, 608);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raytracing Tutorial";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelView.ResumeLayout(false);
            this.panelView.PerformLayout();
            this.toolStripViewport.ResumeLayout(false);
            this.toolStripViewport.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label labelOrientation;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditor;
        private System.Windows.Forms.ToolStripMenuItem режимToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem menuItemRender;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.ToolStripLabel labelMouseActive;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton buttonCamera;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton buttonVolume;
        private System.Windows.Forms.ToolStripButton buttonSceneProperties;
        private System.Windows.Forms.ToolStripButton buttonAxes;
        private System.Windows.Forms.ToolStripLabel labelViewport;
        private System.Windows.Forms.ToolStrip toolStripViewport;
        private Tao.Platform.Windows.SimpleOpenGlControl panelOpenGL;
        private System.Windows.Forms.MenuStrip menuStrip;

        #endregion

        private System.Windows.Forms.ToolStripButton toolStripButton1;


    }
}

