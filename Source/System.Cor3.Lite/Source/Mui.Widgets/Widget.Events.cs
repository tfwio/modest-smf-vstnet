/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	
	public abstract partial class Widget
	{
    
    protected event EventHandler DoubleClick;
    protected virtual void OnDoubleClick(EventArgs e)
    {
      var handler = DoubleClick;
      if (handler != null) handler(this, e);
    }
    
    public event EventHandler Click;
    protected virtual void OnClick(EventArgs e)
    {
      var handler = Click;
      if (handler != null) handler(this, e);
    }

    public event MouseEventHandler MouseDown;
    protected virtual void OnMouseDown(MouseEventArgs e)
    {
      var handler = MouseDown;
      if (handler != null) handler(this, e);
    }

    public event MouseEventHandler MouseUp;
    protected virtual void OnMouseUp(MouseEventArgs e)
    {
      var handler = MouseUp;
      if (handler != null) handler(this, e);
    }

    public event MouseEventHandler MouseMove;
    protected virtual void OnMouseMove(MouseEventArgs e)
    {
      var handler = MouseMove;
      if (handler != null) handler(this, e);
    }
    
    protected event EventHandler<WheelArgs> Wheel;
    protected virtual void OnWheel(WheelArgs e)
    {
      var handler = Wheel;
      if (handler != null) handler(this, e);
    }
	  
    #region ParentEvents

    public event EventHandler ParentDoubleClick {
      add    { Parent.DoubleClick += value; }
      remove { Parent.DoubleClick -= value; }
    }
    
    public event EventHandler ParentClick {
      add    { Parent.Click += value; }
      remove { Parent.Click -= value; }
    }
    virtual protected void WidgetButton_ParentClick(object sender, EventArgs e)
    {
      if (HasClientMouse) {
        OnClick(e);
        using (var rgn = new Region(this.Bounds))
          Parent.Invalidate(rgn);
      }
    }
    
    public event MouseEventHandler ParentMouseDown {
      add    { Parent.MouseDown += value; }
      remove { Parent.MouseDown -= value; }
    }
    virtual protected void WidgetButton_ParentMouseDown(object sender, MouseEventArgs e)
    {
      if (HasClientMouse)
      {
        if (e.Button == MouseButtons.Left) OnMouseDown(e);
        this.HasMouseDown = true;
        OnMouseDown(e);
      }
      using (var rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
    }
    
    public event MouseEventHandler ParentMouseUp {
      add    { Parent.MouseUp += value; }
      remove { Parent.MouseUp -= value; }
    }
    virtual protected void WidgetButton_ParentMouseUp(object sender, MouseEventArgs e)
    {
      this.HasMouseDown = false;
      if (HasClientMouse) using (var rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
      if (HasMouseDown || HasClientMouse) { OnMouseUp(e); }
    }
    
    public event MouseEventHandler ParentMouseMove {
      add    { Parent.MouseMove += value; }
      remove { Parent.MouseMove -= value; }
    }
    virtual protected void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
      if (HasClientMouse || HasClientMouseDown)
      {
        OnMouseMove(e);
        Parent.Invalidate(this.Bounds);
      }
    }
    
    public event EventHandler<WheelArgs> ParentWheel {
      add    { Parent.Wheel += value; }
      remove { Parent.Wheel -= value; }
    }
    
    #endregion
	}
}








