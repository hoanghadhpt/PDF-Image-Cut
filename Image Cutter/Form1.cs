using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AxAcroPDFLib;

namespace Mosaic_Image_Cut
{
	// Token: 0x02000002 RID: 2
	public partial class Form1 : Form
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Form1()
		{
			this.InitializeComponent();
			this.axAcroPDF1.PreviewKeyDown += this.Form1_OnPreviewKeyDown;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B8 File Offset: 0x000002B8
		private void Form1_OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs previewKeyDownEventArgs)
		{
			bool flag = !this._textboxEnable;
			if (flag)
			{
				this.textBox1.Visible = true;
			}
			bool flag2 = !this.textBox1.Focused;
			if (flag2)
			{
				this.textBox1.Focus();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FE File Offset: 0x000002FE
		private void Form1_Load(object sender, EventArgs e)
		{
            comboBox1.SelectedIndex = 0;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002101 File Offset: 0x00000301
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.axAcroPDF1.Dispose();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FE File Offset: 0x000002FE
		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FE File Offset: 0x000002FE
		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		public int RowCount
		{
			get
			{
				return this.dataGridView1.Rows.Count;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		private void button1_Click(object sender, EventArgs e)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.textBox1.Text);
			string directoryName = Path.GetDirectoryName(this.textBox1.Text);
			img = Clipboard.GetImage();
            bool flag = this.img == null;
            if (flag)
            {
                MessageBox.Show("No Image Here");
            }
            else
            {

                Bitmap bitmap = null;

                if (this.img.Size.Width > 480 || this.img.Size.Height > 480)
                {
                    float scaleHeight = (float)480 / (float)img.Height;
                    float scaleWidth = (float)480 / (float)img.Width;
                    float scale = Math.Min(scaleHeight, scaleWidth);
                    Bitmap resizedImage = new Bitmap(img,
                        (int)(img.Width * scale), (int)(img.Height * scale));
                    bitmap = HoangHaResize.Crop(resizedImage);
                }
                else
                {
                    bitmap = new Bitmap(this.img);
                    bitmap = HoangHaResize.Crop(bitmap);
                }

                if (comboBox1.SelectedIndex == 0)
                {
                    this.pictureBox1.Image = bitmap;
                    bitmap.Save(string.Concat(new object[]
                    {
                        directoryName,
                        "\\",
                        fileNameWithoutExtension,
                        ".",
                        this.RowCount + 1,
                        ".PNG"
                    }));
                }
                else
                {
                    this.pictureBox1.Image = bitmap;
                    bitmap.Save(string.Concat(new object[]
                    {
                        directoryName,
                        "\\",
                        fileNameWithoutExtension,
                        "_",
                        this.RowCount + 1,
                        ".jpg"
                    }));
                }
                if (comboBox1.SelectedIndex == 0)
                {
                    this.dataGridView1.Rows.Add(new object[]
                    {
                        this.RowCount + 1,
                        string.Concat(new object[]
                        {
                            fileNameWithoutExtension,
                            ".",
                            this.RowCount + 1,
                            ".PNG"
                        }),
                        bitmap.Width + "x" + bitmap.Height,
                        string.Concat(new object[]
                        {
                            directoryName,
                            "\\",
                            fileNameWithoutExtension,
                            ".",
                            this.RowCount + 1,
                            ".PNG"
                        })
                    });
                }
                else
                {
                    this.dataGridView1.Rows.Add(new object[]
                    {
                        this.RowCount + 1,
                        string.Concat(new object[]
                        {
                            fileNameWithoutExtension,
                            "_",
                            this.RowCount + 1,
                            ".jpg"
                        }),
                        bitmap.Width + "x" + bitmap.Height,
                        string.Concat(new object[]
                        {
                            directoryName,
                            "\\",
                            fileNameWithoutExtension,
                            "_",
                            this.RowCount + 1,
                            ".jpg"
                        })
                    });
                }
				Clipboard.Clear();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022BB File Offset: 0x000004BB
		private void button3_Click(object sender, EventArgs e)
		{
			base.Close();
		}

        
        // Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
        private void sslCertField_DragDrop(object sender, DragEventArgs e)
		{
			bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop, false);
			if (dataPresent)
			{
				e.Effect = DragDropEffects.All;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022FC File Offset: 0x000004FC
		private void sslCertField_DragEnter(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			bool flag = array != null && array.Length != 0;
			if (flag)
			{
				bool flag2 = array[0].Substring(array[0].Length - 4, 4) == ".pdf" || array[0].Substring(array[0].Length - 4, 4) == ".PDF";
				if (flag2)
				{
					this.textBox1.Text = array[0];

				}
				else
				{
					MessageBox.Show("Invalid file.");
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002394 File Offset: 0x00000594
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			bool flag = File.Exists(this.textBox1.Text);
			if (flag)
			{
				this.axAcroPDF1.src = this.textBox1.Text;
				Clipboard.Clear();
				this.dataGridView1.Rows.Clear();
				this.axAcroPDF1.setShowToolbar(true);
				this.axAcroPDF1.setShowScrollbars(true);
                if (comboBox1.SelectedIndex == 0)
                {
                    this.axAcroPDF1.setZoom(75f);
                }
                else
                {
                    this.axAcroPDF1.setZoom(100f);
                }
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002414 File Offset: 0x00000614
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = keyData == (Keys)131153;
			bool result;
			if (flag)
			{
				MessageBox.Show("Shortcut Keys Work!", "Yay!");
				result = true;
			}
			else
			{
				result = base.ProcessCmdKey(ref msg, keyData);
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020FE File Offset: 0x000002FE
		private void axAcroPDF1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002450 File Offset: 0x00000650
		private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.F3;
			if (flag)
			{
				this.button1.PerformClick();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000247C File Offset: 0x0000067C
		private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Right;
			if (flag)
			{
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A0 File Offset: 0x000006A0
		private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Right;
			if (flag)
			{
				this.dataGridView1.Rows[e.RowIndex].Selected = true;
				this.rowIndex = e.RowIndex;
				this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
				this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
				this.contextMenuStrip1.Show(Cursor.Position);
				this.todelete = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020FE File Offset: 0x000002FE
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002574 File Offset: 0x00000774
		private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = !this.dataGridView1.Rows[this.rowIndex].IsNewRow;
			if (flag)
			{
				File.Delete(this.todelete);
				this.dataGridView1.Rows.RemoveAt(this.rowIndex);
				this.todelete = "";
			}
		}

		// Token: 0x04000001 RID: 1
		private DateTime _lastRenav = DateTime.MinValue;

		// Token: 0x04000002 RID: 2
		private bool _textboxEnable = false;

		// Token: 0x04000003 RID: 3
		public Image img;

		// Token: 0x04000004 RID: 4
		private int i = 0;

		// Token: 0x04000005 RID: 5
		private string todelete = "";

		// Token: 0x04000006 RID: 6
		private int rowIndex = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                this.axAcroPDF1.setZoom(75f);
            }
            else
            {
                this.axAcroPDF1.setZoom(100f);
            }
        }
    }
}
