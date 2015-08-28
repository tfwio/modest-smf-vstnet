/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Windows.Forms;
namespace Mui
{
  abstract public class WidgetLayout
  {
    protected WidgetGroup Parent { get; set; }
    
    abstract public void LayoutInitialize();
    abstract public void LayoutInitialize(WidgetGroup widgetGroup);
    abstract public void LayoutRefresh();
    abstract public void LayoutComplete();
    
    
    
  }
  
}






