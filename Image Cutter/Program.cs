using System;
using System.Windows.Forms;

namespace Mosaic_Image_Cut
{
	// Token: 0x02000004 RID: 4
	internal static class Program
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00003728 File Offset: 0x00001928
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
