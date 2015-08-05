using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using gen.snd.Midi;
namespace modest100.Forms
{
	class MidiPianoViewRenderer
	{
		#region Fields
		internal static MidiPianoViewSettings ui_view_settings;

		public static Font NoteFont = new Font("Ubuntu Mono", 10, FontStyle.Bold, GraphicsUnit.Point);

		public static int TextYOffset = 32, TextXOffset = 8, TextXOffsetE = 0;

		#endregion
		public class GraphicsPens : IDisposable
		{
			void IDisposable.Dispose()
			{
				if (GridPen != null)
					GridPen.Dispose();
				if (GridRowMid != null)
					GridRowMid.Dispose();
				if (GridRowHeavy != null)
					GridRowHeavy.Dispose();
				if (GridBar != null)
					GridBar.Dispose();
				if (GridRowDiv != null)
					GridRowDiv.Dispose();
				if (SemiBlack != null)
					SemiBlack.Dispose();
				if (SemiBlackBrush != null)
					SemiBlackBrush.Dispose();
				if (AnotherBrush != null)
					AnotherBrush.Dispose();
			}

			public Pen GridPen = new Pen(Color.Silver, 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Pen GridRowMid = new Pen(Color.FromArgb(127, Color.Red), 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Pen GridRowHeavy = new Pen(Color.Gray, 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Pen GridBar = new Pen(Color.FromArgb(0, 0, 0), 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Pen GridRowDiv = new Pen(Color.FromArgb(127, 0, 127, 255), 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Pen SemiBlack = new Pen(Color.FromArgb(200, Color.Black), 1) {
				Alignment = PenAlignment.Left,
				StartCap = LineCap.Round,
				EndCap = LineCap.Round
			};

			public Brush SemiBlackBrush = new SolidBrush(Color.FromArgb(200, Color.Black));

			public Brush AnotherBrush = new SolidBrush(Color.FromArgb(48, Color.DodgerBlue));
		}

		static public GraphicsPens DefaultPens {
			get {
				return new GraphicsPens();
			}
		}

		static public MidiTimeState TimeState {
			get;
			set;
		}

		static MidiPianoViewRenderer()
		{
			TimeState = new MidiTimeState();
			TimeState.InitializeSnappingBoundaries(4, 4, 4);
		}

		#region GetBackgroundGrid_Image, GetBackgroundGrid_Image, GetBackgroundGrid_X
		/// BackgroundImg Rendering Process
		public static Image GetBackgroundGrid_Image(int off)
		{
			Image img = new Bitmap(ui_view_settings.Width, ui_view_settings.Height, PixelFormat.Format32bppArgb);
			
			using (Graphics g = Graphics.FromImage(img)) {
				g.Clear(Color.White);
				GetBackgroundGrid_X(g, 0);
				GetBackgroundGrid_Y(g, off);
				GetBackgroundGrid_Ebony(g, off);
			}
			return img;
		}

		// BackgroundImg Rendering Process
		// This isn't used.  Why is it here?
		public static void GetBackgroundGrid_Image(Graphics g, Image background)
		{
			g.DrawImage(background, 0, 0);
		}

		/// BackgroundImg Rendering Process
		public static void GetBackgroundGrid_X(Graphics g, int off)
		{
			FloatRect grid = ui_view_settings.ClientRect;
			using (GraphicsPens pens = DefaultPens)
				foreach (int i in GetInt32XEnumeration()) {
					if (IsGutterXMod(i, off, TimeState.PxTpb))
						g.DrawLine(pens.GridBar, i, grid.Top, i, grid.Bottom);
					else
						if (IsGutterXMod(i, off, TimeState.PxTpn))
							g.DrawLine(pens.GridRowMid, i, grid.Top, i, grid.Bottom);
						else
							if (IsGutterXMod(i, off, TimeState.PxPpn))
								g.DrawLine(pens.GridRowDiv, i, grid.Top, i, grid.Bottom);
							else
								g.DrawLine(pens.GridPen, i, grid.Top, i, grid.Bottom);
				}
		}

		#endregion
		#region Client GetClientLocationAtY, GetClientSizeAtY
		static FloatPoint GetClientLocationAtY(float i)
		{
			return new FloatPoint(0, i);
		}

		static FloatPoint GetClientSizeAtY(float i)
		{
			return new FloatPoint(ui_view_settings.ClientPadding.Left, i);
		}

		#endregion
		#region GetBackgroundGrid_Y, GetBackgroundGrid_Ebony
		/// BackgroundImg Rendering Process
		public static void GetBackgroundGrid_Y(Graphics g, int off)
		{
			using (GraphicsPens pens = DefaultPens)
				foreach (float i in EnumerateSingleClientRectGutterTopPerRowY()) {
					int value = GetGutterY(i, off, ui_view_settings.ClientPadding.Top) + 127;
					value = 127 - value;
					if (value < -1 || value > 127)
						continue;
					if (IsGutterYModFIX(i, off, ui_view_settings.ClientPadding.Top, 12)) {
						DrawLine(g, pens.GridPen, GetClientSizeAtY(i), GetClientLocationAtY(i));
						DrawLine(g, pens.GridRowHeavy, ui_view_settings.Rect.Left, i, ui_view_settings.Rect.Right, i);
					}
					else {
						g.DrawLine(pens.GridPen, 0, i, ui_view_settings.Rect.Right, i);
					}
				}
		}

		/// 
		static public void GetBackgroundGrid_Ebony(Graphics g, int off, int i, GraphicsPens pens)
		{
			int corner = 7;
			
			FloatRect rect = RectangleMe2(i);
			
			RoundURectRenderer r = new RoundURectRenderer(new RectangleDoubleUnit(rect), new FloatRectCorners(0, corner, corner, 0), 1);
			
			g.SmoothingMode = SmoothingMode.Default;
			
			if (IsEbony_ClientPadded(i, off, 12)) {
				g.DrawPath(pens.SemiBlack, r.Path);
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.FillPath(pens.SemiBlackBrush, r.Path);
			}
			
			g.SmoothingMode = SmoothingMode.Default;
			r = null;
			if (IsEbony_ClientPadded(i, off, 12))
				g.FillRectangle(pens.AnotherBrush, RectangleMe(i));
		}

		/// y-plane top to bottom
		static public void GetBackgroundGrid_Ebony(Graphics g, float off, GraphicsPens pens)
		{
			for (float i = ui_view_settings.Rect.Top; i <= ui_view_settings.Rect.Bottom; i += ui_view_settings.NodeHeight)
				GetBackgroundGrid_Ebony(g, Convert.ToInt32(off), Convert.ToInt32(i), pens);
		}

		public static void GetBackgroundGrid_Ebony(Graphics g, int off)
		{
			using (GraphicsPens pens = DefaultPens)
				GetBackgroundGrid_Ebony(g, off, pens);
		}

		#endregion
		#region DrawLine
		static void DrawLine(Graphics g, Pen c, int i)
		{
			g.DrawLine(c, GetClientSizeAtY(i), GetClientLocationAtY(i));
		}

		static void DrawLine(Graphics g, Pen c, float x, float y, float w, float h)
		{
			g.DrawLine(c, x, y, w, h);
		}

		static void DrawLine(Graphics g, Pen c, FloatPoint from, FloatPoint to)
		{
			g.DrawLine(c, from, to);
		}

		#endregion
		#region IsGutterXMod, IsGutterYMod, GetGutterX, GetGutterY, IsGutterYModFIX, IsEbony_ClientPadded
		static bool IsGutterXMod(float value, int offset, int against)
		{
			return (GetGutterX(value, offset)) % against == 0;
		}

		static bool IsGutterYMod(float value, int offset, int gutter, int against)
		{
			return GetGutterY(value, offset, gutter) % against == 0;
		}

		static int GetGutterX(float value, int off)
		{
			return (int)((value - ui_view_settings.ClientPadding.Left) / ui_view_settings.NodeWidth) + off;
		}

		static int GetGutterY(float value, int off, int guttertop)
		{
			return (int)((value - guttertop) / ui_view_settings.NodeHeight) - off;
		}

		/// <summary>see GetBackgroundGrid_Y</summary>
		static bool IsGutterYModFIX(float value, int offset, int gutter, int against)
		{
			int a = GetGutterY(value, offset, gutter) + 127;
			a = 128 - a;
			return a % against == 0;
		}

		static bool IsEbony_ClientPadded(float value, int off, int against)
		{
			int a = GetGutterY(value, off, ui_view_settings.ClientPadding.Top) + 127;
			a = 127 - a;
			switch (a % against) {
				case 1:
				case 3:
				case 6:
				case 8:
				case 10:
					return true;
				default:
					return false;
			}
		}

		#endregion
		
		
		/// Surface Layer
		public static void NoteText(Graphics g, int off)
		{
			FloatRect r = ui_view_settings.Rect;
			using (SolidBrush b = new SolidBrush(Color.Black))
				foreach (int i in EnumerateInt32ClientRectGutterTopPerRowY()) {
					int value = GetGutterY(i, off, ui_view_settings.ClientPadding.Top) + 127;
					value = 127 - value;
					if (value < 0 || value > 127)
						continue;
					FloatPoint position = new FloatPoint(TextXOffset, i);
					string fmt = "{0:000} {1,-2}{2}";
					if (IsEbony_ClientPadded(i, off, 12))
						position.X += TextXOffsetE;
					string strvalue = string.Format(fmt, value, MidiReader.SmfStringFormatter.GetKeySharp(value), MidiReader.SmfStringFormatter.GetOctave(value));
					g.DrawString(strvalue, NoteFont, b, position);
				}
		}

		#region Enumerators
		public static IEnumerable<int> EnumerateInt32ClientRectGutterTopPerRowY()
		{
			FloatRect r = ui_view_settings.ClientRect;
			for (float i = r.Top; i <= r.Bottom; i += ui_view_settings.NodeHeight)
				yield return Convert.ToInt32(i);
			r = null;
		}

		public static IEnumerable<float> EnumerateSingleClientRectGutterTopPerRowY()
		{
			FloatRect r = ui_view_settings.ClientRect;
			for (float i = r.Top; i <= r.Bottom; i += ui_view_settings.NodeHeight)
				yield return i;
			r = null;
		}

		public static IEnumerable<KeyValuePair<int, float>> EnumerateYAxis(int off)
		{
			foreach (float i in EnumerateSingleClientRectGutterTopPerRowY()) {
				int value = GetGutterY(i, off, ui_view_settings.ClientPadding.Top) + 127;
				value = 127 - value;
				if (value < -1 || value > 127)
					continue;
				yield return new KeyValuePair<int, float>(value, i);
			}
		}

		public static IEnumerable<int> GetInt32XEnumeration()
		{
			FloatRect grid = ui_view_settings.ClientRect;
			lock (grid) {
				for (float i = grid.Left; i <= grid.Right; i += ui_view_settings.NodeSize.Width)
					yield return Convert.ToInt32(i);
			}
			grid = null;
		}

		#endregion
		#region Rectangle Utility
		static RectangleDoubleUnit RectangleMe(float i)
		{
			return new RectangleDoubleUnit(ui_view_settings.ClientPadding.Left, i, ui_view_settings.Rect.Width, ui_view_settings.NodeSize.Height);
		}

		/// <summary>
		/// FloatRect(0, i, ui.NodeSize.Width*4, ui.NodeSize.Height)
		/// </summary>
		static FloatRect RectangleMe2(float i, int multiplier=4)
		{
			return new FloatRect(0, i, ui_view_settings.NodeSize.Width * multiplier, ui_view_settings.NodeSize.Height);
		}
	#endregion
	}
}


