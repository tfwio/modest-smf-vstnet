/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
  abstract public partial class WidgetGroup
  {
    /// <summary>
    /// Not used.
    /// <para>
    /// Overrides layout engine to 'DockLayout' if set or not 'None'.
    /// </para>
    /// (when implemented)
    /// </summary>
    public DockStyle    Dock { get; set; }
    
    /// <summary>
    /// Not used.
    /// <para>
    /// This allows for the default 'SimpleLayout' engine,
    /// which is responsible for resizing our widgets.
    /// </para>
    /// <para>
    /// Generally, this should always be set.
    /// If Dock is set, then it will override settings here.
    /// </para>
    /// (when implemented)
    /// </summary>
    public AnchorStyles Anchor { get; set; }
    
    /// <summary>
    /// Not used.
    /// <para>Affects layout.</para>
    /// (when implemented)
    /// </summary>
    public AlignHorizontal AlignH { get; set; }
    
    /// <summary>
    /// Not used.
    /// <para>Affects layout.</para>
    /// (when implemented)
    /// </summary>
    public AlignVertical AlignV { get; set; }
    
  }
}








