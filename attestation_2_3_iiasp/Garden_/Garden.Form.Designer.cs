namespace attestation_2_3_iiasp.Garden_
{
    partial class GardenForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label_money = new System.Windows.Forms.Label();
            this.label_moneyy = new System.Windows.Forms.Label();
            this.pictureBox_Garden = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Garden)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox_Garden);
            this.splitContainer1.Size = new System.Drawing.Size(197, 120);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.flowLayoutPanel1.Controls.Add(this.label_money);
            this.flowLayoutPanel1.Controls.Add(this.label_moneyy);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(197, 25);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label_money
            // 
            this.label_money.AutoSize = true;
            this.label_money.Location = new System.Drawing.Point(3, 0);
            this.label_money.Name = "label_money";
            this.label_money.Size = new System.Drawing.Size(39, 13);
            this.label_money.TabIndex = 0;
            this.label_money.Text = "Money";
            // 
            // label_moneyy
            // 
            this.label_moneyy.AutoSize = true;
            this.label_moneyy.Location = new System.Drawing.Point(48, 0);
            this.label_moneyy.Name = "label_moneyy";
            this.label_moneyy.Size = new System.Drawing.Size(25, 13);
            this.label_moneyy.TabIndex = 1;
            this.label_moneyy.Text = "000";
            // 
            // pictureBox_Garden
            // 
            this.pictureBox_Garden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Garden.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Garden.Name = "pictureBox_Garden";
            this.pictureBox_Garden.Size = new System.Drawing.Size(197, 91);
            this.pictureBox_Garden.TabIndex = 0;
            this.pictureBox_Garden.TabStop = false;
            this.pictureBox_Garden.SizeChanged += new System.EventHandler(this.pictureBox_Garden_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 120);
            this.panel1.TabIndex = 1;
            // 
            // GardenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 120);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "GardenForm";
            this.Text = "Garden";
            this.Load += new System.EventHandler(this.GardenForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GardenForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GardenForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Garden)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox_Garden;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label_money;
        private System.Windows.Forms.Label label_moneyy;
        private System.Windows.Forms.Panel panel1;

    }
}

