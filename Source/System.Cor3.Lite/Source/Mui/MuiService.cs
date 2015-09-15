/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
namespace Mui
{
  abstract public class MuiService<TClient> : MuiService
    where TClient:Mui.Widgets.Widget
  {
    public TClient Client { get { return Parent as TClient; } }
  }
  public abstract class MuiService
  {
    public Mui.Widgets.Widget Parent { get; set; }
    
    abstract public void Register();
    abstract public void Unregister();
    virtual public void Initialize(Mui.Widgets.Widget widget){ Parent = widget; }
  }
}






