using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private bool DarkTheme = false;
        bool textWasChanged = false;
        bool isSelecting = false;
        string currentFilePath = null;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new LightMenuColorTable());



        }
        //
        // The "File" button
        //
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        //
        // The "New" button
        //
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (textWasChanged)
            {
                if (!SaveChoise())
                    return;
            }
            richTextBox1.Clear();
            textWasChanged = false;
        }
        //
        // The "Open" button
        //
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textWasChanged)
            {
                if (!SaveChoise())
                    return;
            }
            OpenFile();
            textWasChanged = false;
        }
        //
        // The "Save" button
        //
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(currentFilePath))
            SaveDocument();
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                textWasChanged = false;
            }
        }
        //
        // The "Save as" button
        //
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }
        //
        // The "Exit" button
        //
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textWasChanged)
            {
                if (!SaveChoise())
                    return;
            }

            Application.Exit();
        }
        //
        // The counter of symbols, words and lines
        //
        void UpdateTextInfo()
        {
            string text = richTextBox1.Text;
            int linesCount = richTextBox1.Lines.Length;
            int charCount = text.Length;
            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int wordCount = words.Length;

            toolStripStatusLabel1.Text = $"Symbols: {charCount} | Words: {wordCount} | Lines: {linesCount}";
        }
        // 
        // The button of counter symbols, words and lines
        //
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        //
        // The switch theme button
        //
        private void button1_Click(object sender, EventArgs e)
        {
            DarkTheme = !DarkTheme;

            if (DarkTheme)
            {
                button1.Text = "Light Theme";
                menuStrip1.Renderer = new ToolStripProfessionalRenderer(new DarkMenuColorTable());

                this.BackColor = ColorTranslator.FromHtml("#1E1E1E");

                richTextBox1.BackColor = ColorTranslator.FromHtml("#252526");
                richTextBox1.ForeColor = ColorTranslator.FromHtml("#D4D4D4");

                menuStrip1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                menuStrip1.ForeColor = ColorTranslator.FromHtml("#D4D4D4");


                button1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                button1.ForeColor = ColorTranslator.FromHtml("#D4D4D4");

                statusStrip1.BackColor = ColorTranslator.FromHtml("#2D2D30");
                toolStripStatusLabel1.ForeColor = Color.White;

                fileToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#2D2D30");
                fileToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#D4D4D4");

                openToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#2D2D30");
                openToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#D4D4D4");

                saveToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#2D2D30");
                saveToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#D4D4D4");

                exitToolStripMenuItem.BackColor = ColorTranslator.FromHtml("#2D2D30");
                exitToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#D4D4D4");
            }
            else
            {
                button1.Text = "Dark Theme";

                menuStrip1.Renderer = new ToolStripProfessionalRenderer(new LightMenuColorTable());

                this.BackColor = ColorTranslator.FromHtml("#F4F4F4");

                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;

                menuStrip1.BackColor = ColorTranslator.FromHtml("#F4F4F4");
                menuStrip1.ForeColor = Color.Black;

                button1.BackColor = ColorTranslator.FromHtml("#F4F4F4");
                button1.ForeColor = ColorTranslator.FromHtml("#121212");

                statusStrip1.BackColor = ColorTranslator.FromHtml("#E5E5E5");
                toolStripStatusLabel1.ForeColor = Color.Black;
            }
            LineSelection();
        }
        //
        // Event processing TextChanged
        //
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            textWasChanged = true;
            UpdateTextInfo();
        }
        //
        // Auto close the bracket
        //
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '[')
            {
                e.Handled = true;
                richTextBox1.SelectedText = "[]";
                richTextBox1.SelectionStart -= 1;
                richTextBox1.SelectionLength = 0;
            }
            if (e.KeyChar == '{')
            {
                e.Handled = true;
                richTextBox1.SelectedText = "{}";
                richTextBox1.SelectionStart -= 1;
                richTextBox1.SelectionLength = 0;
            }
            if (e.KeyChar == '(')
            {
                e.Handled = true;
                richTextBox1.SelectedText = "()";
                richTextBox1.SelectionStart -= 1;
                richTextBox1.SelectionLength = 0;
            }
            if (e.KeyChar == '"')
            {
                e.Handled = true;
                richTextBox1.SelectedText = "\"\"";
                richTextBox1.SelectionStart -= 1;
                richTextBox1.SelectionLength = 0;
            }
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
                richTextBox1.SelectedText = "\'\'";
                richTextBox1.SelectionStart -= 1;
                richTextBox1.SelectionLength = 0;
            }
        }
        //
        // Event processing Hot Keys
        //
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl + S  SAVE
            if (e.Control && e.KeyCode == Keys.S && richTextBox1.TextLength != 0)
            {
                if (string.IsNullOrEmpty(currentFilePath))
                    SaveDocument();
                else
                {
                    File.WriteAllText(currentFilePath, richTextBox1.Text);
                    textWasChanged = false;
                }

                e.SuppressKeyPress = true;
            }
            // Ctrl + Shift + S  SAVE AS
            if (e.Control && e.KeyCode == Keys.Shift && e.KeyCode == Keys.S && richTextBox1.TextLength != 0)
            {
                SaveDocument();
                e.SuppressKeyPress = true;
            }
            // Ctrl + O  OPEN
            if (e.Control && e.KeyCode == Keys.O)
            {
                if (textWasChanged)
                {
                    if (!SaveChoise())
                        return;
                }
                OpenFile();
                textWasChanged = false;
            }
            // Ctrl + N  NEW
            if (e.Control && e.KeyCode == Keys.N)
            {
                if (textWasChanged)
                {
                    if (!SaveChoise())
                        return;
                }
                richTextBox1.Clear();
                textWasChanged = false;
            }

        }
        //
        // Selection the line
        //
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            LineSelection();
        }
        //
        // Hilighting the selection line
        //
        private void LineSelection()
        {
            if (isSelecting || richTextBox1.SelectionLength > 0) return;
            int cursorPosition = richTextBox1.SelectionStart;

            isSelecting = true;

            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;

            

            int currentLine = richTextBox1.GetLineFromCharIndex(cursorPosition);
            int firstChar = richTextBox1.GetFirstCharIndexFromLine(currentLine);

            if (currentLine >= richTextBox1.Lines.Length)
            {
                isSelecting = false;
                return;
            }

            if (richTextBox1.TextLength == 0)
            {
                
                return;
            }

            string lineText = richTextBox1.Lines[currentLine];
            int lineLength = lineText.Length;

            Color currentLineColor;

            if (DarkTheme)
                currentLineColor = ColorTranslator.FromHtml("#FF2D2D30");
            else
                currentLineColor = ColorTranslator.FromHtml("#e8e8e8");

            richTextBox1.Select(firstChar, lineLength);
            richTextBox1.SelectionBackColor = currentLineColor;

            richTextBox1.Select(cursorPosition, 0);

            isSelecting = false;
        }
        //
        // Method Save Document
        //
        private bool SaveDocument()
        {
            bool saveCurrect = false;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog.FileName;
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                textWasChanged = false;
            }

            saveCurrect = true;
            return saveCurrect;
        }
        //
        // Method open another file
        //
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
        //
        // Another window with choise save file or not
        //
        private bool SaveChoise()
        {
            DialogResult result = MessageBox.Show("Do you want to save the file?", "Mini IDE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if(SaveDocument())
                    return true;
                else 
                    return false;
            }
            if(result == DialogResult.No) 
                return true;

            return false;
        }



        //
        // New colors of highlighting button File
        //
        class DarkMenuColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => ColorTranslator.FromHtml("#3E3E42");
            public override Color MenuItemBorder => ColorTranslator.FromHtml("#3E3E42");

            public override Color MenuItemSelectedGradientBegin => ColorTranslator.FromHtml("#3E3E42");
            public override Color MenuItemSelectedGradientEnd => ColorTranslator.FromHtml("#3E3E42");

            public override Color MenuItemPressedGradientBegin => ColorTranslator.FromHtml("#3E3E42");
            public override Color MenuItemPressedGradientEnd => ColorTranslator.FromHtml("#3E3E42");

            public override Color ToolStripDropDownBackground => ColorTranslator.FromHtml("#2D2D30");

            public override Color ImageMarginGradientBegin => ColorTranslator.FromHtml("#2D2D30");
            public override Color ImageMarginGradientMiddle => ColorTranslator.FromHtml("#2D2D30");
            public override Color ImageMarginGradientEnd => ColorTranslator.FromHtml("#2D2D30");

            public override Color SeparatorDark => ColorTranslator.FromHtml("#3E3E42");

            public override Color MenuBorder => ColorTranslator.FromHtml("#2D2D30");
        }
        class LightMenuColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => ColorTranslator.FromHtml("#E8E8E8");
            public override Color MenuItemBorder => ColorTranslator.FromHtml("#E8E8E8");

            public override Color MenuItemSelectedGradientBegin => ColorTranslator.FromHtml("#E8E8E8");
            public override Color MenuItemSelectedGradientEnd => ColorTranslator.FromHtml("#E8E8E8");

            public override Color MenuItemPressedGradientBegin => ColorTranslator.FromHtml("#E8E8E8");
            public override Color MenuItemPressedGradientEnd => ColorTranslator.FromHtml("#E8E8E8");

            public override Color ToolStripDropDownBackground => ColorTranslator.FromHtml("#F4F4F4");

            public override Color ImageMarginGradientBegin => ColorTranslator.FromHtml("#F4F4F4");
            public override Color ImageMarginGradientMiddle => ColorTranslator.FromHtml("#F4F4F4");
            public override Color ImageMarginGradientEnd => ColorTranslator.FromHtml("#F4F4F4");

            public override Color SeparatorDark => ColorTranslator.FromHtml("#D0D0D0");

            public override Color MenuBorder => ColorTranslator.FromHtml("#D0D0D0");
        }

        
        //
        // New colors of highlighting button File
        //
    }
}
