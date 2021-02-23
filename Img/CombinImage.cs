using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.Img
{
    class CombinImage
    {
		static void Main(string[] args)
		{
			Combin();
		}


		private static void Combin()
		{
			Image img = Image.FromFile(@"coupon_2.png");
			
			using (Bitmap b = new Bitmap(img))
			{
				Font f = new Font("Arial", 70F);
				Graphics g = Graphics.FromImage(b);
				SolidBrush whiteBrush = new SolidBrush(Color.White);
				SolidBrush blackBrush = new SolidBrush(Color.Black);

				RectangleF canvas = new RectangleF(0, 0, 5, 5);
				g.FillRectangle(whiteBrush, canvas);
				g.DrawString("Hello World", f, blackBrush, canvas);
				b.Save(@"coupon_23434.png", ImageFormat.Jpeg);
			}
		}

	}
}
