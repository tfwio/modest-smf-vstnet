/* tfooo 11/12/2005 4:19 PM */
using System.Drawing.Drawing2D;

namespace System.Drawing
{
	public class GraphicsPens : IDisposable
	{
		void IDisposable.Dispose()
		{
			if (GridPen!=null) GridPen.Dispose();
			if (GridRowMid!=null) GridRowMid.Dispose();
			if (GridRowHeavy!=null) GridRowHeavy.Dispose();
			if (GridBar!=null) GridBar.Dispose();
			if (GridRowDiv!=null) GridRowDiv.Dispose();
			if (SemiBlack!=null) SemiBlack.Dispose();
			if (SemiBlackBrush!=null) SemiBlackBrush.Dispose();
			if (AnotherBrush!=null) AnotherBrush.Dispose();
		}
		public Pen GridPen = new Pen(Color.Silver, 1){ Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round };
		public Pen GridRowMid = new Pen(Color.FromArgb(127, Color.Red), 1) { Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round};
		public Pen GridRowHeavy=new Pen(Color.Gray, 1){Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round};
		public Pen GridBar=new Pen(Color.FromArgb(0,0,0), 1){Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round};
		public Pen GridRowDiv=new Pen(Color.FromArgb(127,0, 127, 255), 1){Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round};
		public Pen SemiBlack=new Pen(Color.FromArgb(200, Color.Black), 1){Alignment=PenAlignment.Left,StartCap=LineCap.Round,EndCap=LineCap.Round};
		public Brush SemiBlackBrush = new SolidBrush(Color.FromArgb(200, Color.Black));
		public Brush AnotherBrush = new SolidBrush(Color.FromArgb(48, Color.DodgerBlue));
		
	}
}
