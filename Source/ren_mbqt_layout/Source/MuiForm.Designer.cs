/* oio * 8/3/2015 * Time: 6:39 AM
 */
namespace ren_mbqt_layout
{
  partial class MuiForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    
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
      this.SuspendLayout();
      // 
      // MuiForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(640, 401);
      this.Font = new System.Drawing.Font("FreeSans", 12F, System.Drawing.FontStyle.Bold);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "MuiForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "MUI";
      this.ResumeLayout(false);

    }

  }
}
