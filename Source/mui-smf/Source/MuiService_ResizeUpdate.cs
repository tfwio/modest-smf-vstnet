/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	public class MuiService_Resizer : MuiService<WidgetMidiList>
	{
    public override void Register()
    {
      Client.Parent.SizeChanged += parent_Resize;
    }
    
    public override void Unregister()
    {
      Client.Parent.SizeChanged -= parent_Resize;
    }
    
    void parent_Resize(object sender, EventArgs e)
    {
      if (Client==null) return;
      Client.Width  = Client.Container.Width;
      Client.Height = Client.Container.Height;
      Client.LineOffset = Client.LineOffset.Contain(5 - Client.MaxVisibleRows, 127);
    }
	}
}








