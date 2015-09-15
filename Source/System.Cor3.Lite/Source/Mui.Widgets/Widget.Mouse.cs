/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	public abstract partial class Widget
	{
    // disable once AccessToStaticMemberViaDerivedType
    protected internal FloatPoint Mouse { get { return Parent.PointToClient(Form.MousePosition); } }
    
    virtual public bool HasClientMouseDown {
      get { return HasClientMouse && Parent.MouseD != null; }
    }
    
    protected void GetHasMouse(){
      Bounds.Contains(Parent.PointToClient(Parent.MouseM));
    }
    
    
    
    
    
	}
}






