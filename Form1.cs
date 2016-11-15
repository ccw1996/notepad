using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;


namespace notepad
{
    public partial class Form1 : Form
    {
        private int findPostion = 0;
        private string s_FileName="";
        private object s;
        private EventArgs ea;
        private StringReader streamToPrint = null;
        private Font printFont;
        public Form1()
        {
            InitializeComponent();
            新建ToolStripMenuItem_Click(s,ea);
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";//或richTextBox1.Clear();
            s_FileName = "";//新建文件没有文件名。s_FileName为字段成员。
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s_FileName = openFileDialog1.FileName;
                richTextBox1.LoadFile(openFileDialog1.FileName,
                RichTextBoxStreamType.PlainText);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (s_FileName.Length != 0)
                richTextBox1.SaveFile(s_FileName, RichTextBoxStreamType.PlainText);
            else
                menuItemFileSaveAs_Click(sender, e);//调用另存为菜单项事件处理函数
        }

        private void menuItemFileSaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s_FileName = saveFileDialog1.FileName;
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                this.Text = s_FileName;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void 恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        public void FindRichTextBoxString(string FindString)
        {
            if (findPostion >= richTextBox1.Text.Length)//已查到文本底部
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找","提示", MessageBoxButtons.OK);
                findPostion = 0;
                return;
            }//下边语句进行查找，返回找到的位置，返回-1，表示未找到，参数1是要找的字符串
             //参数2是查找的开始位置，参数3是查找的一些选项，如大小写是否匹配，查找方向等
            findPostion = richTextBox1.Find(FindString,
            findPostion, RichTextBoxFinds.MatchCase);
            if (findPostion == -1)//如果未找到
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找","提示", MessageBoxButtons.OK);
                findPostion = 0;//下次查找的开始位置
            }
            else//已找到
            {
                richTextBox1.Focus();//主窗体获得焦点
                findPostion += FindString.Length;
            }//下次查找的开始位置在此次找到字符串之后

        }

        public void ReplaceRichTextBoxString(string ReplaceString)
        {
            if (richTextBox1.SelectedText.Length != 0)//如果选取了字符串
                richTextBox1.SelectedText = ReplaceString;//替换被选的字符串
        }

        private void 查找和替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findPostion = 0;
            formFindReplace FindReplaceDialog = new formFindReplace(this);//注意this
            FindReplaceDialog.Show();//打开非模式对话框使用Show()方法   
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void printDocument1_BeginPrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            printFont = richTextBox1.Font;//打印使用的字体
            streamToPrint = new StringReader(richTextBox1.Text);//打印richTextBox1.Text
        }//如预览文件改为：streamToPrint=new StreamReader("文件的路径及文件名");

        private void printDocument1_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e)
        {
            float linesPerPage = 0;//记录每页最大行数
            float yPos = 0;//记录将要打印的一行数据在垂直方向的位置
            int count = 0;//记录每页已打印行数
            float leftMargin = e.MarginBounds.Left;//左边距
            float topMargin = e.MarginBounds.Top;//顶边距
            string line = null;//从RichTextBox中读取一段字符将存到line中
                               //每页最大行数=一页纸打印区域的高度/一行字符的高度
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            //如果当前页已打印行数小于每页最大行数而且读出数据不为null，继续打印
            while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
            {   //yPos为要打印的当前行在垂直方向上的位置
                yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black,leftMargin, yPos, new StringFormat());//打印，参见第五章
                count++;//已打印行数加1
            }
            if (line != null)//是否需要打印下一页
                e.HasMorePages = true;//需要打印下一页
            else
                e.HasMorePages = false;//不需要打印下一页
        }
        private void printDocument1_EndPrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            if (streamToPrint != null)
                streamToPrint.Close();//释放不用的资源
        }

        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog(this) == DialogResult.OK)
                printDocument1.Print();
        }

        private void 打印预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formAbout formAboutDialig = new formAbout();//注意this
            formAboutDialig.Show();//打开非模式对话框使用Show()方法   
        }
    }
}
