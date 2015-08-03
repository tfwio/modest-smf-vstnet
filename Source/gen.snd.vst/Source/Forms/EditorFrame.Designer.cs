namespace gen.snd.Vst.Forms
{
    partial class EditorFrame
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
        	this.comboBox1 = new System.Windows.Forms.ComboBox();
        	this.octaveUpDown = new System.Windows.Forms.NumericUpDown();
        	this.lblOct = new System.Windows.Forms.Label();
        	this.panel2 = new System.Windows.Forms.Panel();
        	this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        	this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        	this.button13 = new System.Windows.Forms.ToolStripButton();
        	this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        	this.button1 = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.octaveUpDown)).BeginInit();
        	this.tableLayoutPanel1.SuspendLayout();
        	this.toolStrip1.SuspendLayout();
        	this.flowLayoutPanel1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// comboBox1
        	// 
        	this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBox1.FormattingEnabled = true;
        	this.comboBox1.Location = new System.Drawing.Point(178, 0);
        	this.comboBox1.Margin = new System.Windows.Forms.Padding(0);
        	this.comboBox1.Name = "comboBox1";
        	this.comboBox1.Size = new System.Drawing.Size(203, 21);
        	this.comboBox1.TabIndex = 2;
        	// 
        	// octaveUpDown
        	// 
        	this.octaveUpDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
        	this.octaveUpDown.CausesValidation = false;
        	this.octaveUpDown.InterceptArrowKeys = false;
        	this.octaveUpDown.Location = new System.Drawing.Point(54, 3);
        	this.octaveUpDown.Maximum = new decimal(new int[] {
        	        	        	9,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.octaveUpDown.Name = "octaveUpDown";
        	this.octaveUpDown.Size = new System.Drawing.Size(61, 16);
        	this.octaveUpDown.TabIndex = 0;
        	this.octaveUpDown.TabStop = false;
        	// 
        	// lblOct
        	// 
        	this.lblOct.AutoSize = true;
        	this.lblOct.Location = new System.Drawing.Point(3, 0);
        	this.lblOct.Name = "lblOct";
        	this.lblOct.Padding = new System.Windows.Forms.Padding(3, 5, 0, 0);
        	this.lblOct.Size = new System.Drawing.Size(45, 18);
        	this.lblOct.TabIndex = 1;
        	this.lblOct.Text = "Octave";
        	// 
        	// panel2
        	// 
        	this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panel2.Location = new System.Drawing.Point(0, 23);
        	this.panel2.Name = "panel2";
        	this.panel2.Size = new System.Drawing.Size(381, 21);
        	this.panel2.TabIndex = 1;
        	// 
        	// tableLayoutPanel1
        	// 
        	this.tableLayoutPanel1.AutoSize = true;
        	this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        	this.tableLayoutPanel1.ColumnCount = 3;
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        	this.tableLayoutPanel1.Controls.Add(this.comboBox1, 2, 0);
        	this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
        	this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
        	this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
        	this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        	this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
        	this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        	this.tableLayoutPanel1.RowCount = 1;
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        	this.tableLayoutPanel1.Size = new System.Drawing.Size(381, 23);
        	this.tableLayoutPanel1.TabIndex = 2;
        	// 
        	// toolStrip1
        	// 
        	this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
        	this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
        	this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
        	this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        	this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.button13});
        	this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
        	this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        	this.toolStrip1.Name = "toolStrip1";
        	this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
        	this.toolStrip1.Size = new System.Drawing.Size(23, 22);
        	this.toolStrip1.TabIndex = 0;
        	this.toolStrip1.Text = "toolStrip1";
        	// 
        	// button13
        	// 
        	this.button13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        	this.button13.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.button13.Name = "button13";
        	this.button13.Size = new System.Drawing.Size(23, 19);
        	this.button13.Text = "?";
        	// 
        	// flowLayoutPanel1
        	// 
        	this.flowLayoutPanel1.AutoSize = true;
        	this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.flowLayoutPanel1.Controls.Add(this.lblOct);
        	this.flowLayoutPanel1.Controls.Add(this.octaveUpDown);
        	this.flowLayoutPanel1.Controls.Add(this.button1);
        	this.flowLayoutPanel1.Location = new System.Drawing.Point(23, 0);
        	this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
        	this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(155, 0);
        	this.flowLayoutPanel1.Name = "flowLayoutPanel1";
        	this.flowLayoutPanel1.Size = new System.Drawing.Size(155, 23);
        	this.flowLayoutPanel1.TabIndex = 1;
        	this.flowLayoutPanel1.WrapContents = false;
        	// 
        	// button1
        	// 
        	this.button1.AutoSize = true;
        	this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
        	this.button1.Location = new System.Drawing.Point(118, 0);
        	this.button1.Margin = new System.Windows.Forms.Padding(0);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(32, 23);
        	this.button1.TabIndex = 2;
        	this.button1.Text = "!";
        	this.button1.UseVisualStyleBackColor = true;
        	// 
        	// EditorFrame
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(381, 44);
        	this.Controls.Add(this.panel2);
        	this.Controls.Add(this.tableLayoutPanel1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        	this.MaximizeBox = false;
        	this.Name = "EditorFrame";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "EditorFrame";
        	((System.ComponentModel.ISupportInitialize)(this.octaveUpDown)).EndInit();
        	this.tableLayoutPanel1.ResumeLayout(false);
        	this.tableLayoutPanel1.PerformLayout();
        	this.toolStrip1.ResumeLayout(false);
        	this.toolStrip1.PerformLayout();
        	this.flowLayoutPanel1.ResumeLayout(false);
        	this.flowLayoutPanel1.PerformLayout();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripButton button13;
        private System.Windows.Forms.NumericUpDown octaveUpDown;
        private System.Windows.Forms.Label lblOct;
        private System.Windows.Forms.Panel panel2;

        #endregion
    }
}