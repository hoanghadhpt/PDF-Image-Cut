using System;
using System.Drawing;

namespace Mosaic_Image_Cut
{
	// Token: 0x02000003 RID: 3
	public static class HoangHaResize
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000033EC File Offset: 0x000015EC
		public static Size ResizeKeepAspect(this Size src, int maxWidth, int maxHeight, bool enlarge = false)
		{
			maxWidth = (enlarge ? maxWidth : Math.Min(maxWidth, src.Width));
			maxHeight = (enlarge ? maxHeight : Math.Min(maxHeight, src.Height));
			decimal d = Math.Min(maxWidth / src.Width, maxHeight / src.Height);
			return new Size((int)Math.Round(src.Width * d), (int)Math.Round(src.Height * d));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003484 File Offset: 0x00001684
		public static Bitmap Crop(Bitmap bmp)
		{
			int w = bmp.Width;
			int h = bmp.Height;
			Func<int, bool> func = delegate(int row)
			{
				for (int m = 0; m < w; m++)
				{
					bool flag17 = bmp.GetPixel(m, row).R != byte.MaxValue;
					bool flag18 = flag17;
					if (flag18)
					{
						return false;
					}
				}
				return true;
			};
			Func<int, bool> func2 = delegate(int col)
			{
				for (int m = 0; m < h; m++)
				{
					bool flag17 = bmp.GetPixel(col, m).R != byte.MaxValue;
					bool flag18 = flag17;
					if (flag18)
					{
						return false;
					}
				}
				return true;
			};
			int num = 0;
			for (int i = 0; i < h; i++)
			{
				bool flag = func(i);
				bool flag2 = !flag;
				if (flag2)
				{
					break;
				}
				num = i;
			}
			int num2 = 0;
			for (int j = h - 1; j >= 0; j--)
			{
				bool flag3 = func(j);
				bool flag4 = !flag3;
				if (flag4)
				{
					break;
				}
				num2 = j;
			}
			int num3 = 0;
			int num4 = 0;
			for (int k = 0; k < w; k++)
			{
				bool flag5 = func2(k);
				bool flag6 = !flag5;
				if (flag6)
				{
					break;
				}
				num3 = k;
			}
			for (int l = w - 1; l >= 0; l--)
			{
				bool flag7 = func2(l);
				bool flag8 = !flag7;
				if (flag8)
				{
					break;
				}
				num4 = l;
			}
			bool flag9 = num4 == 0;
			bool flag10 = flag9;
			if (flag10)
			{
				num4 = w;
			}
			bool flag11 = num2 == 0;
			bool flag12 = flag11;
			if (flag12)
			{
				num2 = h;
			}
			int num5 = num4 - num3;
			int num6 = num2 - num;
			bool flag13 = num5 == 0;
			bool flag14 = flag13;
			if (flag14)
			{
				num3 = 0;
				num5 = w;
			}
			bool flag15 = num6 == 0;
			bool flag16 = flag15;
			if (flag16)
			{
				num = 0;
				num6 = h;
			}
			Bitmap result;
			try
			{
				Bitmap bitmap = new Bitmap(num5, num6);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(bmp, new RectangleF(0f, 0f, (float)num5, (float)num6), new RectangleF((float)num3, (float)num, (float)num5, (float)num6), GraphicsUnit.Pixel);
				}
				result = bitmap;
			}
			catch (Exception innerException)
			{
				throw new Exception(string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", new object[]
				{
					num,
					num2,
					num3,
					num4,
					num5,
					num6
				}), innerException);
			}
			return result;
		}
	}
}
