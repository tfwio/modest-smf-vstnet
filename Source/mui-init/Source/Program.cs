/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Windows.Forms;

namespace ren_mbqt_layout
{
  
  /// <summary>
  /// Class with program entry point.
  /// </summary>
  internal sealed class Program
  {
    public static MuiForm AppForm;
    /// <summary>
    /// Program entry point.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      
      Application.Run(AppForm = new MuiForm());
    }
    
  }
}
