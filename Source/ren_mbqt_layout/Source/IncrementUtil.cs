/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
namespace ren_mbqt_layout
{
	public class IncrementUtil
	{
		public float PFactor = 30F;
		public float PIncrement = 0.01F;
		
		public float PMax = 1F;
		public float PMin = 0F;

		readonly FloatPoint POrigin = new FloatPoint(30, 30);

		public float PValue = 0;
    	public FloatPoint PCoords = new FloatPoint(0, 0);

		public void IncrementY()
		{
			PValue += PIncrement;
			PCoords.Y = PValue * PFactor;
			if (PValue > PMax) PValue = PMin;
		}
	}
}


