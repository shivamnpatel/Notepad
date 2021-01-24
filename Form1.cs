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
namespace Notepad
{
    public partial class Form1 : Form
    {
        private string filestr;
        public Form1()
        {
            InitializeComponent();
            filestr = "";
        }

        // EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //statement that execute on click of exit button   
            //and chekcing whether textbox modified or not if modified          
            //then prompt user to save or not          
            if (MainRichTextBox.Modified == true)
            {
                DialogResult dr = MessageBox.Show("Do you want to save the file before exiting", "unsaved file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string filename = sfd.FileName;
                    String filter = "Text Files|*.txt|All Files|*.*";
                    sfd.Filter = filter;
                    sfd.Title = "Save";

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        //Write all of the text in txtBox to the specified file
                        System.IO.File.WriteAllText(filename, MainRichTextBox.Text);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MainRichTextBox.Modified = false;
                    Application.Exit();
                }
            }
        }

        // NEW 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Text = "";
            MainRichTextBox.Clear();
        }
        //OPEN
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                StreamReader read = new StreamReader(openFileDialog1.FileName);
                MainRichTextBox.Text = read.ReadToEnd();
                read.Close();
                filestr = openFileDialog1.FileName;
            }

        }

        //SAVE
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filestr == "")
            {
                DialogResult dr = saveFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    StreamWriter writefile = new StreamWriter(saveFileDialog1.FileName);
                    writefile.Write(MainRichTextBox.Text);
                    writefile.Close();
                    filestr = saveFileDialog1.FileName;

                }
                
            }
            else
            {
                StreamWriter writefile = new StreamWriter(filestr);
                writefile.Write(MainRichTextBox.Text);
                writefile.Close();
            }
           
        }
    
        //FONT
        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MainRichTextBox.Font = fontDialog1.Font;
                    
            }
        }

        //FORE COLOR
        private void foreColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MainRichTextBox.ForeColor = colorDialog1.Color;
            }
        }

        //BACK COLOR
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MainRichTextBox.BackColor = colorDialog1.Color;
            }
        }

        //WORDWRAP ON
        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.WordWrap = true;
        }

        //WORDWRAP OFF
        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.WordWrap = false;
        }

       //PRINT
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument prntDoc = new System.Drawing.Printing.PrintDocument();
            //Declare print as a new PrintDialog
            PrintDialog print = new PrintDialog();
            //Declare prntDoc_PrintPage as a new EventHandler for prntDoc's Print Page
            prntDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prntDoc_PrintPage);
            //Set prntDoc to the PrintDialog's Document
            print.Document = prntDoc;
            //Show the PrintDialog
            if (print.ShowDialog(this) == DialogResult.OK)
            {
                //Print the Page
                prntDoc.Print();
            }
            MessageBox.Show("Successfull");
        }

        private void prntDoc_PrintPage(Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Declare g as Graphics equal to the PrintPageEventArgs Graphics
            Graphics g = e.Graphics;
            //Draw the Text in txtBox to the Document
            g.DrawString(MainRichTextBox.Text, MainRichTextBox.Font, Brushes.Black, 0, 0);
        }

        // PRINT PREVIEW
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument prntDoc = new System.Drawing.Printing.PrintDocument();
            PrintPreviewDialog preview = new PrintPreviewDialog();
            //Declare prntDoc_PrintPage as a new EventHandler for prntDoc's Print Page
            prntDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prntDoc_PrintPage);
            //Set the PrintPreview's Document equal to prntDoc
            preview.Document = prntDoc;
            //Show the PrintPreview Dialog
            if (preview.ShowDialog(this) == DialogResult.OK)
            {
                //Generate the PrintPreview
                prntDoc.Print();
            }
        }


        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = true;
        }

        // UNDO
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            redoToolStripMenuItem.Enabled= true;
            undoToolStripMenuItem.Enabled = false;
        }

        // REDO
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = false;
            
        }

        //ABOUT US
        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutUsToolStripMenuItem.Enabled = true;
            MessageBox.Show("This Notepad is developed by Shivam Patel.\n Use of this application without permission is not allowed.\nLegal actions will be taken if used without permission.");
           
        }

        // CUT
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        // COPY
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        // PASTE
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Paste();
        }

       
    }
}
