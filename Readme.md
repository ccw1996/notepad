#  实验四：文本编辑器的设计

**【实验目的】******

l  掌握界面设计过程中菜单、工具栏与状态栏的使用方法

l  掌握对话框和RichTextBox控件的使用方法。

l  掌握界面布局的步骤和技术

**【实验步骤】******

本次实验我们将建立一个单文档文本编辑器（参照记事本功能）。

 

**1.****文件存取功能******

文本编辑器都具有文件存取功能，包括“新建”、“打开”、“退出”、“保存”和“另存为”等功能。

(1)    为”新建”菜单项增加事件处理函数如下：

private void menuItemFileNew_Click(objectsender, System.EventArgs e)

{   richTextBox1.Text="";//或richTextBox1.Clear();

s_FileName="";//新建文件没有文件名。s_FileName为字段成员。

}

(2)   在设计视图中添加OpenFileDialog控件，Name属性为openFileDialog1，并为“打开”文件菜单项增加事件处理函数如下：

private voidmenuItemFileOpen_Click(object sender,System.EventArgs e)

{   if(openFileDialog1.ShowDialog()==DialogResult.OK)

{   s_FileName=openFileDialog1.FileName;

richTextBox1.LoadFile(openFileDialog1.FileName,

RichTextBoxStreamType.PlainText);

}

}

(3)   在设计视图中添加SaveFileDialog控件，Name属性为saveFileDialog1，并为“另存为”菜单项增加事件处理函数如下：

private void menuItemFileSaveAs_Click(object sender, System.EventArgs e)

{

if** (**saveFileDialog1.ShowDialog**()**==** **DialogResult.OK**)**

**    {**

**       **s_FileName** **=** **saveFileDialog1.FileName**;**

**             **richTextBox1.SaveFile**(**saveFileDialog1.FileName**,**RichTextBoxStreamType.PlainText**);**

**        **this.Text** **=** **s_FileName**;**

**    }**//注意存取文件类型应一致

                                                       

}

(4)   为“保存”文件菜单项增加事件处理处理函数如下：

private void menuItemSaveFile_Click(objectsender, System.EventArgs e)

{   if(s_FileName.Length!=0)

richTextBox1.SaveFile(s_FileName,RichTextBoxStreamType.PlainText);

    else

menuItemFileSaveAs_Click(sender,e);//调用另存为菜单项事件处理函数

}

(5)   为“退出”菜单项增加事件处理函数如下：

private voidmenuItemExit_Click(object sender,System.EventArgs e)

{   

this.Close();

Application.Exit();

}

 

**2.****文本的编辑功能******

使用RichTextBox提供的一系列方法可以实现文本的“剪切”、“复制”、“粘贴”、“撤销”和“恢复”等功能。

private void menuItemEditCut_Click(object sender, EventArgse)

       {

           richTextBox1.Cut();//剪切

       }

 

       private voidmenuItemEditCopy_Click(object sender, EventArgse)

       {

           richTextBox1.Copy();//复制

       }

 

        private void menuItemEditPaste_Click(object sender, EventArgse)

       {

           richTextBox1.Paste();//粘贴

       }

 

       private voidmenuItemEditUndo_Click(object sender, EventArgse)

       {

           richTextBox1.Undo();//撤销

       }

 

       private voidmenuItemEditRedo_Click(object sender, EventArgse)

       {

           richTextBox1.Redo();//恢复

       }

### “查找替换”功能

(6)    建立查找替换对话框。添加“Windows窗体”，输入窗体文件名称：formFindReplace.cs。

(7)    ![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image002.jpg)修改formFindReplace窗体

l  属性StartPosition=CenterParent，表示打开对话框时，对话框在父窗口的中间。

l  修改属性MaximizeBox=False，MinimizeBox=False，表示没有最大化和最小化按钮，既不能最大化和最小化。

l  FormBorderStyle=FixedDialog，窗口不能修改大小。

l  属性Text="查找和替换"。

l  修改属性TopMost=true，使该窗口打开时总在其它窗体的前边。

在窗体中增加两个Label控件，属性Text分别为"查找字符串"和"替换字符串"。两个TextBox控件，属性Text=""。两个按钮，属性Text分别为"查找下一个"和"替换查到字符"。对话框界面如右图。

(8)    为formFindReplace窗体增加变量：Form1 mainForm1;

(9)    修改formFindReplace类构造函数如下(阴影部分是所做的修改)：

public formFindReplace (Form1form1)//增加参数

{

//Windows窗体设计器支持所必需的

    InitializeComponent();

//TODO:在InitializeComponent调用后添加任何构造函数代码

    mainForm1=form1;//新增语句,这里Form1是主窗体的属性Name的值

}//有了Form1，可以在formFindReplace窗体中调用主窗体的公有方法

(10) 为主窗体Form1增加方法如下，该方法将被formFindReplace窗体类调用。

public void FindRichTextBoxString(stringFindString)

{  }   //以后步骤将在此方法中增加查找语句

(11) formFindReplace窗体中查找下一个按钮单击事件处理函数如下：

private void btnFindNext_Click(objectsender, System.EventArgs e)

{   if(textBox1.Text.Length!=0)//如果查找字符串不为空,调用主窗体查找方法

mainForm1.FindRichTextBoxString(textBox1.Text);//上步增加的方法

else

MessageBox.Show("查找字符串不能为空","提示",MessageBoxButtons.OK);

}//MessageBox时对话框

(12) 为主窗体Form1增加方法如下，该方法将被formFindReplace窗体类调用。

public void ReplaceRichTextBoxString(string ReplaceString )

{}    //以后步骤将在此方法中增加替换语句

(13) 为”替换查到字符”按钮单击事件增加事件处理函数如下：

private void btnReplace_Click(objectsender, System.EventArgs e)

{ if(textBox1.Text.Length!=0)//如果查找字符串不为空,调用主窗体替换方法

mainForm1.ReplaceRichTextBoxString(textBox1.Text,textBox2.Text);

else//方法mainForm1.ReplaceRichTextBoxString见(16)中定义

MessageBox.Show("替换字符串不能为空","提示",MessageBoxButtons.OK);

}

(14) 为Form1窗体增加变量：int findPostion=0，记录查找位置。

(15) 为Form1窗体”编辑”菜单项中增加子菜单项：”查找和替换”。为”查找和替换”菜单项单击事件增加事件处理函数如下：

private void menuItemFindReplace_Click(object sender, System.EventArgs e)

{   findPostion=0;

formFindReplace FindReplaceDialog=new formFindReplace (this);//注意this

    FindReplaceDialog.Show();//打开非模式对话框使用Show()方法

}

(16) 为在前边定义的Form1主窗体的FindRichTextBoxString方法增加语句如下：

public void FindRichTextBoxString(stringFindString)

{   if(findPostion>=richTextBox1.Text.Length)//已查到文本底部

{   MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",

"提示",MessageBoxButtons.OK);

findPostion=0;

return;

}//下边语句进行查找，返回找到的位置，返回-1，表示未找到，参数1是要找的字符串

//参数2是查找的开始位置，参数3是查找的一些选项，如大小写是否匹配，查找方向等

findPostion=richTextBox1.Find(FindString,

findPostion,RichTextBoxFinds.MatchCase);

if(findPostion==-1)//如果未找到

{   MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",

"提示", MessageBoxButtons.OK);

findPostion=0;//下次查找的开始位置

}

else//已找到

{   richTextBox1.Focus();//主窗体获得焦点

findPostion+=FindString.Length;

}//下次查找的开始位置在此次找到字符串之后

}

(17) 为在前边定义的Form1主窗体的ReplaceRichTextBoxString方法增加语句如下：

public void ReplaceRichTextBoxString(string ReplaceString)

{   if(richTextBox1.SelectedText.Length!=0)//如果选取了字符串

    richTextBox1.SelectedText=ReplaceString;//替换被选的字符串

![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image004.jpg)}

(18) 编译，运行，输入若干字符，选中菜单项：编辑/查找和替换，打开对话框，注意该对话框可以在不关闭的情况下，转到主窗体，并且总是在其它窗体的前边，因此它是一个典型的非模式对话框。在对话框中输入查找和替换的字符，单击标题为查找下一个的按钮，可以找到所选字符，并被选中，单击标题为替换所选字符按钮，可以看到查找到的字符被替换。运行效果如右图：

 

**3.****修改字体属性******

格式菜单中有字体字号和颜色两种功能。

(19) 字体字号设置

在设计视图中添加FontDialog控件到窗体，属性Name=fontDialog1。增加顶级菜单项：格式，为格式顶级菜单项的弹出菜单增加菜单项：字体，属性Name分别为mainMenuModel和menuItemModelFont，为字体菜单项增加事件处理函数如下：

private void menuItemModelFont_Click(objectsender, System.EventArgs e)

{   if(fontDialog1.ShowDialog()==DialogResult.OK)

richTextBox1.SelectionFont=fontDialog1.Font;

}

(20)颜色设置

同学自己完成吧。（添加ColorDialog控件）

**4.****打印******

**PrintDocument****组件**是用于完成打印的类，其常用属性、方法和事件如下：

l 属性DocumentName：字符串类型，记录打印文档时显示的文档名（例如，在打印状态对话框或打印机队列中显示）。

l 方法Print：开始文档的打印。

l 事件BeginPrint：在调用Print方法后，在打印文档的第一页之前发生。

l 事件PrintPage：需要打印新的一页时发生。

l 事件EndPrint：在文档的最后一页打印后发生。

若要打印，首先创建PrintDocument组件的对象。然后使用页面设置对话框PageSetupDialog设置页面打印方式，这些设置作为要打印的所有页的默认设置。使用打印对话框PrintDialog设置对文档进行打印的打印机的参数。在打开两个对话框前，首先设置对话框的属性Document为指定的PrintDocument类对象，修改的设置将保存到PrintDocument组件对象中。第三步是调用PrintDocument.Print方法来实际打印文档。当调用该方法后，引发下列事件：BeginPrint、PrintPage、EndPrint。其中每打印一页都引发PrintPage事件，打印多页，要多次引发PrintPage事件。完成一次打印，可以引发一个或多个PrintPage事件。

程序员应为这3个事件编写事件处理函数。BeginPrint事件处理函数进行打印初始化，一般设置在打印时所有页的相同属性或共用的资源，例如所有页共同使用的字体、建立要打印的文件流等。PrintPage事件处理函数负责打印一页数据。EndPrint事件处理函数进行打印善后工作。这些处理函数的第2个参数System.Drawing.Printing.PrintEventArgs e提供了一些附加信息，主要有：

l e.Cancel：布尔变量，设置为true，将取消这次打印作业。

l e.Graphics：所使用的打印机的设备环境，参见第五章。

l e.HasMorePages：布尔变量。PrintPage事件处理函数打印一页后，仍有数据未打印，退出事件处理函数前设置HasMorePages=true，退出PrintPage事件处理函数后，将再次引发PrintPage事件，打印下一页。

l e.MarginBounds：打印区域的大小，是Rectangle结构，元素包括左上角坐标：Left和Top，宽和高：Width和Height。单位为1/100英寸。

l e.MarginBounds：打印纸的大小，是Rectangle结构。单位为1/100英寸。

l e.PageSettings：PageSettings类对象，包含用对话框PageSetupDialog设置的页面打印方式的全部信息。可用帮助查看PageSettings类的属性。

下边为这3个事件编写事件处理函数，具体步骤如下：

(21) 在最后一个using语句之后增加语句：

using System.IO;

using System.Drawing.Printing;

(22) 本例打印或预览RichTextBox中的内容，增加变量：StringReader streamToPrint=null。如果打印或预览文件，改为：StreamReader streamToPrint，流的概念参见第六章。增加打印使用的字体的变量：Font printFont。

(23) 放PrintDocument控件到窗体，属性name为printDocument1。

(24) 为printDocument1增加BeginPrint事件处理函数如下：

private void printDocument1_BeginPrint(object sender,

System.Drawing.Printing.PrintEventArgs e)

{   printFont=richTextBox1.Font;//打印使用的字体

streamToPrint=new StringReader(richTextBox1.Text);//打印richTextBox1.Text

}//如预览文件改为：streamToPrint=new StreamReader("文件的路径及文件名");

(25) printDocument1的PrintPage事件处理函数如下。streamToPrint.ReadLine()读入一段数据，可能打印多行。本事件处理函数将此段数据打印在一行上，因此方法必须改进。

private void printDocument1_PrintPage(object sender,

System.Drawing.Printing.PrintPageEventArgs e)

{   float linesPerPage=0;//记录每页最大行数

    float yPos=0;//记录将要打印的一行数据在垂直方向的位置

    int count=0;//记录每页已打印行数

    float leftMargin=e.MarginBounds.Left;//左边距

    float topMargin=e.MarginBounds.Top;//顶边距

    string line=null;//从RichTextBox中读取一段字符将存到line中

    //每页最大行数=一页纸打印区域的高度/一行字符的高度

    linesPerPage=e.MarginBounds.Height/printFont.GetHeight(e.Graphics);

    //如果当前页已打印行数小于每页最大行数而且读出数据不为null，继续打印

    while(count<linesPerPage&&((line=streamToPrint.ReadLine())!=null))

    {   //yPos为要打印的当前行在垂直方向上的位置

yPos=topMargin+(count*printFont.GetHeight(e.Graphics));

        e.Graphics.DrawString(line,printFont,Brushes.Black,

leftMargin,yPos,new StringFormat());//打印，参见第五章

        count++;//已打印行数加1

    }

    if(line!=null)//是否需要打印下一页

        e.HasMorePages=true;//需要打印下一页

    else

        e.HasMorePages=false;//不需要打印下一页

}

(26) 为printDocument1增加EndPrint事件处理函数如下：

private void printDocument1_EndPrint (objectsender,

System.Drawing.Printing.PrintEventArgs e)

{   if(streamToPrint!=null)

        streamToPrint.Close();//释放不用的资源

}

###  打印设置对话框控件PageSetupDialog

Windows窗体的PageSetupDialog控件是一个页面设置对话框，用于在Windows应用程序中设置打印页面的详细信息，对话框的外观如图4.8.2。

![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image006.jpg)

图4.8.2

用户使用此对话框能够设置纸张大小(类型)、纸张来源、纵向与横向打印、上下左右的页边距等。在打开对话框前，首先设置其属性Document为指定的PrintDocument类对象，用来把页面设置保存到PrintDocument类对象中。为文本编辑器增加页面设置功能的具体步骤如下：

(27) 为文件顶级菜单项的弹出菜单增加菜单项：页面设置。

(28) 放PageSetupDialog控件到窗体，属性name为pageSetupDialog1。

(29) 为页面设置菜单项增加单击事件处理函数如下：

private void menuItem5_Click(objectsender,System.EventArgs e)

{   pageSetupDialog1.Document=printDocument1;

pageSetupDialog1.ShowDialog();

}

(30) 打开对话框pageSetupDialog1后，如果单击了确定按钮，PageSetupDialog对话框中所做的的页面设置被保存到PrintDocument类对象printDocument1中，如果单击了取消按钮，不保存这些修改，维持原来的值。当调用PrintDocument.Print方法来实际打印文档时，引发PrintPage事件，该事件处理函数的第二个参数e提供了这些设置信息。

### 打印预览对话框**PrintPreviewDialog**

**用****PrintPreviewDialog**类可以在屏幕上显示PrintDocument的打印效果，既打印预览。实现打印预览的具体步骤如下：

(31) 为文件顶级菜单项的弹出菜单增加菜单项：打印预览。

(32) 放PrintPreviewDialog控件到窗体，属性name为printPreviewDialog1。

(33) 为打印预览菜单项增加单击事件处理函数如下：

private void menuItemPrintView_Click(objectsender,System.EventArgs e)

{   printPreviewDialog1.Document=printDocument1;

printPreviewDialog1.ShowDialog();

}

(34) 编译，运行，输入若干字符，试验一下预览的效果，预览的效果如图4.8.3。

![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image008.jpg)

图4.8.3

### 用打印对话框PrintDialog实现打印

PrintDialog组件是类库中预先定义的对话框，用来设置对文档进行打印的打印机的参数，包括打印机名称、要打印的页(全部打印或指定页的范围)、打印的份数以及是否打印到文件等。在打开对话框前，首先设置其属性Document为指定的PrintDocument类对象，打开PrintDialog对话框后，修改的设置将保存到PrintDocument类的对象中。PrintDialog对话框的外观如图4.8.4。

![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image010.jpg)

图4.8.4

增加打印功能的具体步骤如下：

(35) 在设计视图中添加PrintDialog控件，属性Name=printDialog1。

(36) 为文件顶级菜单项的弹出菜单增加菜单项：打印。

(37) 为“打印”菜单项增加单击事件处理函数如下： 

private void menuItemPrint_Click(objectsender, System.EventArgs e)

{   printDialog1.Document=printDocument1;

    if(printDialog1.ShowDialog(this)==DialogResult.OK)

        printDocument1.Print();

}

(38) 编译，运行，输入若干字符，试验一下打印效果。

 

**5.****实现****About****对话框******

(39) “添加”——“Windows窗体”——“关于”框，输入窗体文件名称：formAbout.cs。

(40) 修改formAbout属性 

l  StartPosition=CenterParent，表示打开对话框时，对话框在父窗口的中间。

l  MaximizeBox=False，MinimizeBox=False，表示没有最大化和最小化按钮，既不能最大化和最小化。

l  FormBorderStyle=FixedDialog，窗口不能修改大小。

l  Text="关于记事本"。

可以在窗体中增加各种控件，显示作者、版权等信息。“确定”按钮用来关闭formabout窗体。图为Microsoft的记事本，仅作参考。

亲，也可以用photoshop做一个自己的logo哦！（双11购物后遗症，全民皆亲！）

![img](file:///C:/Users/johnny/AppData/Local/Temp/msohtmlclip1/01/clip_image012.jpg)