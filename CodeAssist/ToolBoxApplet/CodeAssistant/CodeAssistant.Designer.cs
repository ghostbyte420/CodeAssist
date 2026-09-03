using static ScintillaNET.Style;

namespace AI.CodeAssist
{
    partial class codeAssistant
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(codeAssistant));
            codeAssistant_button_projectPathSearch = new Button();
            codeAssistant_button_exportCodeBase = new Button();
            codeAssistant_textBox_projectPathDirectory = new TextBox();
            codeAssistant_richTextBox_exportCodeBase_preview = new RichTextBox();
            codeAssistant_progressBar_exportCodeBase = new ProgressBar();
            codeAssistant_openFileDirectory = new Button();
            codeAssistant__folderBrowserDialog = new FolderBrowserDialog();
            panel1 = new Panel();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            panel3 = new Panel();
            pictureBox2 = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // codeAssistant_button_projectPathSearch
            // 
            codeAssistant_button_projectPathSearch.Font = new Font("Segoe UI", 10.5F);
            codeAssistant_button_projectPathSearch.Location = new Point(3, 50);
            codeAssistant_button_projectPathSearch.Name = "codeAssistant_button_projectPathSearch";
            codeAssistant_button_projectPathSearch.Size = new Size(124, 91);
            codeAssistant_button_projectPathSearch.TabIndex = 0;
            codeAssistant_button_projectPathSearch.Text = "Search for Project";
            codeAssistant_button_projectPathSearch.UseVisualStyleBackColor = true;
            codeAssistant_button_projectPathSearch.Click += codeAssistant_button_projectPathSearch_Click;
            // 
            // codeAssistant_button_exportCodeBase
            // 
            codeAssistant_button_exportCodeBase.Font = new Font("Segoe UI", 10.5F);
            codeAssistant_button_exportCodeBase.Location = new Point(4, 147);
            codeAssistant_button_exportCodeBase.Name = "codeAssistant_button_exportCodeBase";
            codeAssistant_button_exportCodeBase.Size = new Size(124, 91);
            codeAssistant_button_exportCodeBase.TabIndex = 1;
            codeAssistant_button_exportCodeBase.Text = "Export Codebase";
            codeAssistant_button_exportCodeBase.UseVisualStyleBackColor = true;
            codeAssistant_button_exportCodeBase.Click += codeAssistant_button_exportCodeBase_Click;
            // 
            // codeAssistant_textBox_projectPathDirectory
            // 
            codeAssistant_textBox_projectPathDirectory.Dock = DockStyle.Fill;
            codeAssistant_textBox_projectPathDirectory.Location = new Point(0, 0);
            codeAssistant_textBox_projectPathDirectory.Name = "codeAssistant_textBox_projectPathDirectory";
            codeAssistant_textBox_projectPathDirectory.Size = new Size(956, 23);
            codeAssistant_textBox_projectPathDirectory.TabIndex = 2;
            codeAssistant_textBox_projectPathDirectory.TextAlign = HorizontalAlignment.Right;
            // 
            // codeAssistant_richTextBox_exportCodeBase_preview
            // 
            codeAssistant_richTextBox_exportCodeBase_preview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            codeAssistant_richTextBox_exportCodeBase_preview.Font = new Font("Segoe UI", 10.5F);
            codeAssistant_richTextBox_exportCodeBase_preview.Location = new Point(0, 0);
            codeAssistant_richTextBox_exportCodeBase_preview.Name = "codeAssistant_richTextBox_exportCodeBase_preview";
            codeAssistant_richTextBox_exportCodeBase_preview.ShowSelectionMargin = true;
            codeAssistant_richTextBox_exportCodeBase_preview.Size = new Size(956, 488);
            codeAssistant_richTextBox_exportCodeBase_preview.TabIndex = 3;
            codeAssistant_richTextBox_exportCodeBase_preview.Text = "";
            // 
            // codeAssistant_progressBar_exportCodeBase
            // 
            codeAssistant_progressBar_exportCodeBase.Dock = DockStyle.Bottom;
            codeAssistant_progressBar_exportCodeBase.Location = new Point(0, 538);
            codeAssistant_progressBar_exportCodeBase.Name = "codeAssistant_progressBar_exportCodeBase";
            codeAssistant_progressBar_exportCodeBase.Size = new Size(1085, 23);
            codeAssistant_progressBar_exportCodeBase.TabIndex = 4;
            // 
            // codeAssistant_openFileDirectory
            // 
            codeAssistant_openFileDirectory.Font = new Font("Segoe UI", 10.5F);
            codeAssistant_openFileDirectory.Location = new Point(3, 244);
            codeAssistant_openFileDirectory.Name = "codeAssistant_openFileDirectory";
            codeAssistant_openFileDirectory.Size = new Size(124, 91);
            codeAssistant_openFileDirectory.TabIndex = 5;
            codeAssistant_openFileDirectory.Text = "Open Directory";
            codeAssistant_openFileDirectory.UseVisualStyleBackColor = true;
            codeAssistant_openFileDirectory.Click += codeAssistant_openFileDirectory_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(codeAssistant_richTextBox_exportCodeBase_preview);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(129, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(956, 488);
            panel1.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(codeAssistant_textBox_projectPathDirectory);
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(129, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(956, 50);
            panel2.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = CodeAssist.Properties.Resources.divi_001;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(956, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox2);
            panel3.Controls.Add(codeAssistant_button_projectPathSearch);
            panel3.Controls.Add(codeAssistant_button_exportCodeBase);
            panel3.Controls.Add(codeAssistant_openFileDirectory);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(129, 538);
            panel3.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Bottom;
            pictureBox2.Image = CodeAssist.Properties.Resources.hedr_001;
            pictureBox2.Location = new Point(0, 330);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(129, 208);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // codeAssistant
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1085, 561);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(codeAssistant_progressBar_exportCodeBase);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "codeAssistant";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CodeAssistant";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button codeAssistant_button_projectPathSearch;
        private System.Windows.Forms.Button codeAssistant_button_exportCodeBase;
        private System.Windows.Forms.TextBox codeAssistant_textBox_projectPathDirectory;
        private System.Windows.Forms.RichTextBox codeAssistant_richTextBox_exportCodeBase_preview;
        private System.Windows.Forms.ProgressBar codeAssistant_progressBar_exportCodeBase;
        private System.Windows.Forms.Button codeAssistant_openFileDirectory;
        private System.Windows.Forms.FolderBrowserDialog codeAssistant__folderBrowserDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}
