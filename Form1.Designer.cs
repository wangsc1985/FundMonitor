﻿namespace FundMonitor
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
            this.labelSz = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFunIncrease = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelSz
            // 
            this.labelSz.AutoSize = true;
            this.labelSz.Location = new System.Drawing.Point(36, 9);
            this.labelSz.Name = "labelSz";
            this.labelSz.Size = new System.Drawing.Size(47, 12);
            this.labelSz.TabIndex = 0;
            this.labelSz.Text = "3454.88";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "2.8%";
            // 
            // labelFunIncrease
            // 
            this.labelFunIncrease.AutoSize = true;
            this.labelFunIncrease.Font = new System.Drawing.Font("华文中宋", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFunIncrease.Location = new System.Drawing.Point(31, 38);
            this.labelFunIncrease.Name = "labelFunIncrease";
            this.labelFunIncrease.Size = new System.Drawing.Size(154, 40);
            this.labelFunIncrease.TabIndex = 0;
            this.labelFunIncrease.Text = "36.85%";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 86);
            this.Controls.Add(this.labelFunIncrease);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSz);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSz;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFunIncrease;
    }
}
