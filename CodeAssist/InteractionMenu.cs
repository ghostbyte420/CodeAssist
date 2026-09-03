using System;
using System.Windows.Forms;
using System.Drawing;

namespace AI.CodeAssist
{
    public partial class interactionMenu : Form
    {
        private bool _isDragging = false;
        private Point _offset;

        public interactionMenu()
        {
            InitializeComponent();
            // Wire up the drag events for the title bar panel
            interactionMenu_panel_titleBar.MouseDown += interactionMenu_panel_titleBar_MouseDown;
            interactionMenu_panel_titleBar.MouseMove += interactionMenu_panel_titleBar_MouseMove;
            interactionMenu_panel_titleBar.MouseUp += interactionMenu_panel_titleBar_MouseUp;
        }

        // Drag logic for the title bar
        private void interactionMenu_panel_titleBar_MouseDown(object? sender, MouseEventArgs e)
        {
            _isDragging = true;
            _offset = e.Location;
        }

        private void interactionMenu_panel_titleBar_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - _offset.X, currentScreenPos.Y - _offset.Y);
            }
        }

        private void interactionMenu_panel_titleBar_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        // Single method to handle all button clicks
        private void AnyButton_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender!; // Get the button that was clicked
            Clipboard.SetText(btn.Text); // Copy the button's text to the clipboard
            MessageBox.Show($"Copied to clipboard: {btn.Text}", "Copied!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
