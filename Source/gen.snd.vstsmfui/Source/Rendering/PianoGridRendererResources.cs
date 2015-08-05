// oio * 10/31/2012 * 12:27 PM
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace modest100.Rendering
{
  class PianoGridRendererResources : IDisposable
	{
		public Pen GridPen { get; set; }
		public Pen GridRowMid { get; set; }
		public Pen GridRowHeavy { get; set; }
		public Pen GridBar { get; set; }
		public Pen GridRowDiv { get; set; }

		void IDisposable.Dispose()
		{
		  if (GridPen!=null) GridPen.Dispose();
		  if (GridRowMid!=null) GridRowMid.Dispose();
		  if (GridRowHeavy!=null) GridRowHeavy.Dispose();
		  if (GridBar!=null) GridBar.Dispose();
		  if (GridRowDiv!=null) GridRowDiv.Dispose();
		}
		
		static Pen DefaultPenStyle(Color clr, float width = 1)
		{
			Pen p = new Pen(clr, width);
			
			p.Alignment = PenAlignment.Left;
			p.StartCap = LineCap.Round;
			p.EndCap = LineCap.Round;
			return p;
		}

		static public readonly PianoGridRendererResources Default = new PianoGridRendererResources() {
			GridPen = DefaultPenStyle(Color.Silver, 1),
			GridRowMid = DefaultPenStyle(Color.FromArgb(127, Color.Red)),
			GridRowHeavy = DefaultPenStyle(Color.Gray),
			GridBar = DefaultPenStyle(Color.FromArgb(0, 0, 0)),
			GridRowDiv = DefaultPenStyle(Color.FromArgb(127, 0, 127, 255))
		};
	}
}


