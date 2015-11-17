namespace SimShaker
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.trackX = new System.Windows.Forms.TrackBar();
            this.trackY = new System.Windows.Forms.TrackBar();
            this.trackZ = new System.Windows.Forms.TrackBar();
            this.lblCG = new System.Windows.Forms.Label();
            this.trackDelta = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.lblDelta = new System.Windows.Forms.Label();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDelta)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "CG";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackX
            // 
            this.trackX.Location = new System.Drawing.Point(26, 113);
            this.trackX.Maximum = 100;
            this.trackX.Minimum = 1;
            this.trackX.Name = "trackX";
            this.trackX.Size = new System.Drawing.Size(247, 45);
            this.trackX.TabIndex = 1;
            this.trackX.Value = 50;
            this.trackX.Scroll += new System.EventHandler(this.track_Scroll);
            // 
            // trackY
            // 
            this.trackY.Location = new System.Drawing.Point(291, 2);
            this.trackY.Maximum = 100;
            this.trackY.Minimum = 1;
            this.trackY.Name = "trackY";
            this.trackY.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackY.Size = new System.Drawing.Size(45, 200);
            this.trackY.TabIndex = 2;
            this.trackY.Value = 50;
            this.trackY.Scroll += new System.EventHandler(this.track_Scroll);
            // 
            // trackZ
            // 
            this.trackZ.BackColor = System.Drawing.Color.Gainsboro;
            this.trackZ.Location = new System.Drawing.Point(372, 2);
            this.trackZ.Maximum = 25;
            this.trackZ.Minimum = 10;
            this.trackZ.Name = "trackZ";
            this.trackZ.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackZ.Size = new System.Drawing.Size(45, 200);
            this.trackZ.TabIndex = 3;
            this.trackZ.Value = 18;
            this.trackZ.Scroll += new System.EventHandler(this.track_Scroll);
            // 
            // lblCG
            // 
            this.lblCG.AutoSize = true;
            this.lblCG.Location = new System.Drawing.Point(110, 22);
            this.lblCG.Name = "lblCG";
            this.lblCG.Size = new System.Drawing.Size(10, 13);
            this.lblCG.TabIndex = 4;
            this.lblCG.Text = "-";
            // 
            // trackDelta
            // 
            this.trackDelta.Location = new System.Drawing.Point(513, 113);
            this.trackDelta.Maximum = 2500;
            this.trackDelta.Minimum = -1000;
            this.trackDelta.Name = "trackDelta";
            this.trackDelta.Size = new System.Drawing.Size(176, 45);
            this.trackDelta.TabIndex = 5;
            this.trackDelta.Scroll += new System.EventHandler(this.trackDelta_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(513, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Delta Weight";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblDelta
            // 
            this.lblDelta.AutoSize = true;
            this.lblDelta.Location = new System.Drawing.Point(608, 22);
            this.lblDelta.Name = "lblDelta";
            this.lblDelta.Size = new System.Drawing.Size(10, 13);
            this.lblDelta.TabIndex = 7;
            this.lblDelta.Text = "-";
            // 
            // txtReport
            // 
            this.txtReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReport.Location = new System.Drawing.Point(13, 208);
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReport.Size = new System.Drawing.Size(701, 414);
            this.txtReport.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(513, 179);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Delta Weight";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 634);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtReport);
            this.Controls.Add(this.lblDelta);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackDelta);
            this.Controls.Add(this.lblCG);
            this.Controls.Add(this.trackZ);
            this.Controls.Add(this.trackY);
            this.Controls.Add(this.trackX);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackDelta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar trackX;
        private System.Windows.Forms.TrackBar trackY;
        private System.Windows.Forms.TrackBar trackZ;
        private System.Windows.Forms.Label lblCG;
        private System.Windows.Forms.TrackBar trackDelta;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblDelta;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Button button3;
    }
}

