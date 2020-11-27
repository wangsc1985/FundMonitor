namespace FundMonitor
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelSz = new System.Windows.Forms.Label();
            this.labelSzIncrease = new System.Windows.Forms.Label();
            this.labelFunIncrease = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBoxVolume = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSz
            // 
            this.labelSz.BackColor = System.Drawing.Color.Transparent;
            this.labelSz.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSz.ForeColor = System.Drawing.Color.White;
            this.labelSz.Location = new System.Drawing.Point(12, 91);
            this.labelSz.Name = "labelSz";
            this.labelSz.Size = new System.Drawing.Size(100, 17);
            this.labelSz.TabIndex = 0;
            this.labelSz.Text = "0";
            this.labelSz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSzIncrease
            // 
            this.labelSzIncrease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSzIncrease.BackColor = System.Drawing.Color.Transparent;
            this.labelSzIncrease.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSzIncrease.ForeColor = System.Drawing.Color.White;
            this.labelSzIncrease.Location = new System.Drawing.Point(144, 91);
            this.labelSzIncrease.Name = "labelSzIncrease";
            this.labelSzIncrease.Size = new System.Drawing.Size(70, 17);
            this.labelSzIncrease.TabIndex = 0;
            this.labelSzIncrease.Text = "0%";
            this.labelSzIncrease.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFunIncrease
            // 
            this.labelFunIncrease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFunIncrease.BackColor = System.Drawing.Color.Transparent;
            this.labelFunIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFunIncrease.ForeColor = System.Drawing.Color.White;
            this.labelFunIncrease.Location = new System.Drawing.Point(12, 37);
            this.labelFunIncrease.Name = "labelFunIncrease";
            this.labelFunIncrease.Size = new System.Drawing.Size(202, 42);
            this.labelFunIncrease.TabIndex = 0;
            this.labelFunIncrease.Text = "0";
            this.labelFunIncrease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFunIncrease.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelFunIncrease_MouseClick);
            this.labelFunIncrease.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelFunIncrease_MouseDown);
            this.labelFunIncrease.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelFunIncrease_MouseMove);
            this.labelFunIncrease.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelFunIncrease_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(134, 9);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(80, 17);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "00:00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTime.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelTime_MouseClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("华文中宋", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "点击还原";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // pictureBoxVolume
            // 
            this.pictureBoxVolume.Image = global::FundMonitor.Properties.Resources.volume_on;
            this.pictureBoxVolume.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxVolume.InitialImage")));
            this.pictureBoxVolume.Location = new System.Drawing.Point(13, 9);
            this.pictureBoxVolume.Name = "pictureBoxVolume";
            this.pictureBoxVolume.Size = new System.Drawing.Size(17, 17);
            this.pictureBoxVolume.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxVolume.TabIndex = 2;
            this.pictureBoxVolume.TabStop = false;
            this.pictureBoxVolume.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxVolume_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(226, 117);
            this.Controls.Add(this.pictureBoxVolume);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelFunIncrease);
            this.Controls.Add(this.labelSzIncrease);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVolume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSz;
        private System.Windows.Forms.Label labelSzIncrease;
        private System.Windows.Forms.Label labelFunIncrease;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBoxVolume;
    }
}

