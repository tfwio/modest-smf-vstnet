/*
 * User: xo
 * Date: 8/5/2017
 * Time: 4:22 PM
 */
namespace on.FFmeta.nplayer
{
  partial class Configurator
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox cbDriverType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbDriver_SubType;
    private System.Windows.Forms.Button button1;
    private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel blCurrent;
    
    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    
    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
    {
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cbDriver_SubType = new System.Windows.Forms.ComboBox();
      this.cbDriverType = new System.Windows.Forms.ComboBox();
      this.button1 = new System.Windows.Forms.Button();
      this.blCurrent = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.cbDriver_SubType);
      this.groupBox1.Controls.Add(this.cbDriverType);
      this.groupBox1.Location = new System.Drawing.Point(127, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(361, 87);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Audio Device Selection";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(46, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(62, 21);
      this.label2.TabIndex = 1;
      this.label2.Text = "Device-ID";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(46, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(62, 21);
      this.label1.TabIndex = 1;
      this.label1.Text = "Driver";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cbDriver_SubType
      // 
      this.cbDriver_SubType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDriver_SubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriver_SubType.FormattingEnabled = true;
      this.cbDriver_SubType.Location = new System.Drawing.Point(114, 46);
      this.cbDriver_SubType.Name = "cbDriver_SubType";
      this.cbDriver_SubType.Size = new System.Drawing.Size(241, 21);
      this.cbDriver_SubType.TabIndex = 0;
      // 
      // cbDriverType
      // 
      this.cbDriverType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDriverType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDriverType.FormattingEnabled = true;
      this.cbDriverType.Location = new System.Drawing.Point(114, 19);
      this.cbDriverType.Name = "cbDriverType";
      this.cbDriverType.Size = new System.Drawing.Size(241, 21);
      this.cbDriverType.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Location = new System.Drawing.Point(405, 150);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(82, 37);
      this.button1.TabIndex = 1;
      this.button1.Text = "Save";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.Click_Button_Save);
      // 
      // blCurrent
      // 
      this.blCurrent.BackColor = System.Drawing.Color.Transparent;
      this.blCurrent.BaseStylesheet = null;
      this.blCurrent.Location = new System.Drawing.Point(127, 106);
      this.blCurrent.Name = "blCurrent";
      this.blCurrent.Size = new System.Drawing.Size(131, 20);
      this.blCurrent.TabIndex = 2;
      this.blCurrent.Text = "currentAudioDevice";
      // 
      // Configurator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(503, 199);
      this.Controls.Add(this.blCurrent);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "Configurator";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Configurator";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
  }
}
