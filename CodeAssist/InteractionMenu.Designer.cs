namespace AI.CodeAssist
{
    partial class interactionMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(interactionMenu));
            interactionMenu_panel = new Panel();
            interactionMenu_panel_titleBar = new Panel();
            interactionMenu_panel_button_likeImAThreeYearOld = new Button();
            label1 = new Label();
            interactionMenu_panel.SuspendLayout();
            interactionMenu_panel_titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // interactionMenu_panel
            // 
            interactionMenu_panel.AutoScroll = true;
            interactionMenu_panel.BackColor = Color.Black;
            interactionMenu_panel.Controls.Add(interactionMenu_panel_titleBar);
            interactionMenu_panel.Controls.Add(interactionMenu_panel_button_likeImAThreeYearOld);
            interactionMenu_panel.Dock = DockStyle.Fill;
            interactionMenu_panel.Location = new Point(0, 0);
            interactionMenu_panel.Name = "interactionMenu_panel";
            interactionMenu_panel.Size = new Size(527, 146);
            interactionMenu_panel.TabIndex = 0;
            // 
            // interactionMenu_panel_titleBar
            // 
            interactionMenu_panel_titleBar.Controls.Add(label1);
            interactionMenu_panel_titleBar.Dock = DockStyle.Top;
            interactionMenu_panel_titleBar.Location = new Point(0, 0);
            interactionMenu_panel_titleBar.Name = "interactionMenu_panel_titleBar";
            interactionMenu_panel_titleBar.Size = new Size(527, 22);
            interactionMenu_panel_titleBar.TabIndex = 4;
            // 
            // interactionMenu_panel_button_likeImAThreeYearOld
            // 
            interactionMenu_panel_button_likeImAThreeYearOld.Font = new Font("Segoe UI", 10F);
            interactionMenu_panel_button_likeImAThreeYearOld.Location = new Point(12, 39);
            interactionMenu_panel_button_likeImAThreeYearOld.Name = "interactionMenu_panel_button_likeImAThreeYearOld";
            interactionMenu_panel_button_likeImAThreeYearOld.Size = new Size(500, 85);
            interactionMenu_panel_button_likeImAThreeYearOld.TabIndex = 3;
            interactionMenu_panel_button_likeImAThreeYearOld.Text = resources.GetString("interactionMenu_panel_button_likeImAThreeYearOld.Text");
            interactionMenu_panel_button_likeImAThreeYearOld.UseVisualStyleBackColor = true;
            interactionMenu_panel_button_likeImAThreeYearOld.Click += AnyButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10F);
            label1.ForeColor = SystemColors.ScrollBar;
            label1.Location = new Point(397, 4);
            label1.Name = "label1";
            label1.Size = new Size(113, 19);
            label1.TabIndex = 0;
            label1.Text = "Prompt Selection";
            // 
            // interactionMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(527, 146);
            Controls.Add(interactionMenu_panel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "interactionMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "InteractionMenu";
            interactionMenu_panel.ResumeLayout(false);
            interactionMenu_panel_titleBar.ResumeLayout(false);
            interactionMenu_panel_titleBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel interactionMenu_panel;
        private Button interactionMenu_panel_button_likeImAThreeYearOld;
        private Panel interactionMenu_panel_titleBar;
        private Label label1;
    }
}