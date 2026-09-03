using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace AI.CodeAssist
{
    public partial class codeAssistant : Form
    {
        private Scintilla scintilla = null!;

        public codeAssistant()
        {
            InitializeComponent();
            ReplaceRichTextBoxWithScintilla();
        }

        private void ReplaceRichTextBoxWithScintilla()
        {
            scintilla = new Scintilla
            {
                Dock = DockStyle.Fill,
                Location = codeAssistant_richTextBox_exportCodeBase_preview.Location,
                Size = codeAssistant_richTextBox_exportCodeBase_preview.Size,
                Anchor = codeAssistant_richTextBox_exportCodeBase_preview.Anchor
            };

            // Configure Scintilla for C# syntax highlighting
            scintilla.LexerName = "cpp";
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Cpp.Default].ForeColor = Color.Black;
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
            scintilla.Styles[Style.Cpp.CommentDoc].ForeColor = Color.Green;
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.Purple;
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.Red;
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.Red;
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Brown;

            // Add C# keywords
            scintilla.SetKeywords(0, "abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else enum event explicit extern false finally fixed float for foreach goto if implicit in int interface internal is lock long namespace new null object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static string struct switch this throw true try typeof uint ulong unchecked unsafe ushort using virtual void volatile while");
            scintilla.SetKeywords(1, "add alias ascending descending dynamic from get global group into join let orderby partial remove select set value var where yield");

            // Remove the RichTextBox and add Scintilla to panel1
            panel1.Controls.Remove(codeAssistant_richTextBox_exportCodeBase_preview);
            panel1.Controls.Add(scintilla);
        }

        private void codeAssistant_button_projectPathSearch_Click(object sender, EventArgs e)
        {
            if (codeAssistant__folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                codeAssistant_textBox_projectPathDirectory.Text = codeAssistant__folderBrowserDialog.SelectedPath;
            }
        }

        private void codeAssistant_openFileDirectory_Click(object sender, EventArgs e)
        {
            string projectFolder = codeAssistant_textBox_projectPathDirectory.Text;
            if (string.IsNullOrEmpty(projectFolder))
            {
                MessageBox.Show("Please select the project folder first.");
                return;
            }

            string outputDirectory = Path.Combine(projectFolder, "AI.CodeAssist");
            if (Directory.Exists(outputDirectory))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = outputDirectory,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("The output directory does not exist. Please export the code base first.");
            }
        }

        private void codeAssistant_button_exportCodeBase_Click(object sender, EventArgs e)
        {
            string projectFolder = codeAssistant_textBox_projectPathDirectory.Text;
            string outputFileBase = "CodeBase_";
            if (string.IsNullOrEmpty(projectFolder))
            {
                MessageBox.Show("Please select the project folder.");
                return;
            }

            try
            {
                string[] files = Directory.EnumerateFiles(projectFolder, "*.cs", SearchOption.AllDirectories).ToArray();
                if (files.Length == 0)
                {
                    MessageBox.Show("No .cs files found in the selected project folder.");
                    return;
                }

                codeAssistant_progressBar_exportCodeBase.Visible = true;
                codeAssistant_progressBar_exportCodeBase.Minimum = 0;
                codeAssistant_progressBar_exportCodeBase.Maximum = files.Length;
                codeAssistant_progressBar_exportCodeBase.Value = 0;

                MessageBox.Show($"Found {files.Length} .cs files to process.");

                int maxCharsPerFile = 140000;
                int currentFileIndex = 1;
                int currentCharCount = 0;
                StreamWriter? outfile = null;
                string outputDirectory = Path.Combine(projectFolder, "AI.CodeAssist");
                Directory.CreateDirectory(outputDirectory);
                StringBuilder previewContent = new StringBuilder();
                string currentOutputFile = Path.Combine(outputDirectory, $"{outputFileBase}{currentFileIndex}.txt");
                outfile = new StreamWriter(currentOutputFile);
                int totalChars = 0;
                string endMessage = "\n=== Please Do Not Do OR Say Anything, Until I Prompt You By Typing the word \"Toronto\" ===";

                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    string fileContent = File.ReadAllText(file);
                    string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    codeAssistant_progressBar_exportCodeBase.Value = i + 1;
                    Application.DoEvents();

                    // Add file marker and content to preview
                    previewContent.AppendLine();
                    previewContent.AppendLine($"=== File: {file} ===");
                    previewContent.AppendLine();

                    foreach (string line in lines)
                    {
                        previewContent.AppendLine(line);
                    }
                    previewContent.AppendLine();

                    // Write to output file
                    if (currentCharCount + fileContent.Length + endMessage.Length + 100 > maxCharsPerFile)
                    {
                        outfile.WriteLine(endMessage); // Append message before closing the file
                        outfile.Close();
                        currentFileIndex++;
                        currentOutputFile = Path.Combine(outputDirectory, $"{outputFileBase}{currentFileIndex}.txt");
                        outfile = new StreamWriter(currentOutputFile);
                        currentCharCount = 0;
                    }
                    outfile.WriteLine($"=== File: {file} ===");
                    outfile.WriteLine();
                    outfile.WriteLine(fileContent);
                    outfile.WriteLine();
                    currentCharCount += fileContent.Length + 100;
                    totalChars += fileContent.Length + 100;
                }

                // Append the message to the last file
                outfile.WriteLine(endMessage);
                outfile.Close();
                previewContent.AppendLine(endMessage); // Append message to preview

                MessageBox.Show($"Combined code has been written to {currentFileIndex} files in the Output folder. Total characters: {totalChars}");

                // Update the Scintilla control with the preview content
                scintilla.Text = previewContent.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                codeAssistant_progressBar_exportCodeBase.Visible = false;
            }
        }
    }
}
