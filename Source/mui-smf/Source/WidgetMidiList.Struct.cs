/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
using System.Linq;
namespace mui_smf
{
  partial class WidgetMidiList
  {
    protected internal struct RowInfoH
    {
      public int Index  { get; set; }
      public int X  { get; set; }
      public int XO { get; set; }
      
      static readonly int testQ = 1;
      static readonly int testN = 4;
      static readonly int testB = Math.Pow(testN,2).ToInt32();
      static readonly int testM = Math.Pow(testN,3).ToInt32();
      
      public bool IsQ { get { return Index - 1 % testQ == testQ; } }
      public bool IsN { get { return Index - 1 % testN == testN ; } }
      public bool IsB { get { return Index - 1 % testM == testM ; } }
      public bool IsM { get { return Index - 1 % testB == testB ; } }
    }
    protected internal class RowInfoV
    {
      public int RowIndex { get; set; }
      public bool IsIvory { get; set; }
      public bool CanDo { get; set; }
      public float Top { get; set; }
    }
  }
}






