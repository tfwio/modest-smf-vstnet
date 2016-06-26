using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
namespace on.trig
{
	using Point = System.Drawing.DoublePoint;

	public interface IApiDraw
	{
	  void textTo(string message);
	  
		void curveTo(double a0, double a1, double b0, double b1);

		void curveTo(Point point0, Point point1);

		void moveTo(Point point);

		void moveTo(double X, double Y);

		void lineTo(Point point);

		void lineTo(double X, double Y);
	}
}







