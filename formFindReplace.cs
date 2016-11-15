using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
    public partial class formFindReplace : Form
    {
        Form1 mainForm1;
        public formFindReplace(Form1 form1)
        {
            InitializeComponent();
            mainForm1 = form1;
        }

        private void formFindReplace_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)//如果查找字符串不为空,调用主窗体查找方法
                mainForm1.FindRichTextBoxString(textBox1.Text);//上步增加的方法
            else
                MessageBox.Show("查找字符串不能为空", "提示", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)//如果查找字符串不为空,调用主窗体替换方法
                mainForm1.ReplaceRichTextBoxString(textBox2.Text);
            else//方法mainForm1.ReplaceRichTextBoxString见(16)中定义
                MessageBox.Show("替换字符串不能为空", "提示", MessageBoxButtons.OK);

        }
    }
}
