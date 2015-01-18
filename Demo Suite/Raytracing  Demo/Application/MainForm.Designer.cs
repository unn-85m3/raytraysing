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
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelView = new System.Windows.Forms.Panel();
            this.panelOpenGL = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelOrientation = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.trackBarLight = new System.Windows.Forms.TrackBar();
            this.trackBarReflectivity = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panelView.SuspendLayout();
            this.panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReflectivity)).BeginInit();
            this.SuspendLayout();
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
            this.panelView.Location = new System.Drawing.Point(12, 12);
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
            this.panelOpenGL.Location = new System.Drawing.Point(0, 0);
            this.panelOpenGL.Name = "panelOpenGL";
            this.panelOpenGL.Size = new System.Drawing.Size(512, 537);
            this.panelOpenGL.StencilBits = ((byte)(0));
            this.panelOpenGL.TabIndex = 8;
            this.panelOpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelOpenGLPaint);
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
            this.panelStatus.Location = new System.Drawing.Point(0, 557);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(632, 21);
            this.panelStatus.TabIndex = 13;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(360, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(172, 21);
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
            this.labelTime.Location = new System.Drawing.Point(532, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(100, 21);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "---";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarLight
            // 
            this.trackBarLight.Location = new System.Drawing.Point(529, 39);
            this.trackBarLight.Maximum = 100;
            this.trackBarLight.Name = "trackBarLight";
            this.trackBarLight.Size = new System.Drawing.Size(91, 45);
            this.trackBarLight.TabIndex = 9;
            this.trackBarLight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarLight.Value = 100;
            this.trackBarLight.Scroll += new System.EventHandler(this.trackBarLight_Scroll);
            // 
            // trackBarReflectivity
            // 
            this.trackBarReflectivity.Location = new System.Drawing.Point(529, 90);
            this.trackBarReflectivity.Maximum = 100;
            this.trackBarReflectivity.Name = "trackBarReflectivity";
            this.trackBarReflectivity.Size = new System.Drawing.Size(91, 45);
            this.trackBarReflectivity.TabIndex = 14;
            this.trackBarReflectivity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarReflectivity.Value = 100;
            this.trackBarReflectivity.Scroll += new System.EventHandler(this.trackBarReflectivity_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(531, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Интенсивность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(531, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Зеркальность";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(535, 125);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 17;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(632, 578);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarReflectivity);
            this.Controls.Add(this.trackBarLight);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelView);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raytracing Tutorial";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.panelView.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReflectivity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label labelOrientation;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panelStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Timer timer;
        private Tao.Platform.Windows.SimpleOpenGlControl panelOpenGL;

        #endregion

        private System.Windows.Forms.TrackBar trackBarLight;
        private System.Windows.Forms.TrackBar trackBarReflectivity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;


    }
}

