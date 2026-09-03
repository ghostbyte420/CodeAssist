using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace AI.CodeAssist
{
    public partial class aiCodeAssistMain : Form
    {
        private codeAssistant? _codeAssistantForm;
        private interactionMenu? _interactionMenuForm;

        public aiCodeAssistMain()
        {
            InitializeComponent();

            /// Main Form Settings
            this.MinimumSize = new Size(1000, 600);

            /// Tab Control Setup
            main_tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            main_tabControl.DrawItem += Main_tabControl_DrawItem;

            main_tabControl.Alignment = TabAlignment.Right;
            main_tabControl.ItemSize = new Size(30, 80);
            main_tabControl.SizeMode = TabSizeMode.Fixed;

            /// Status Strip Setup
            main_statusStrip_button_clearCookies.DropDownButtonWidth = 0;

            /// Ensure Cookie Storage Directories Exist
            Directory.CreateDirectory(Path.GetDirectoryName(mistralCookiesFilePath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(deepAICookiesFilePath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(blackBoxCookiesFilePath)!);
            Directory.CreateDirectory(Path.GetDirectoryName(copilotCookiesFilePath)!);

            // Create and show the interactionMenu form
            _interactionMenuForm = new interactionMenu();
            _interactionMenuForm.FormClosed += (s, args) => _interactionMenuForm = null;
            _interactionMenuForm.Show();
            this.Activate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (_interactionMenuForm != null && !_interactionMenuForm.IsDisposed)
            {
                _interactionMenuForm.Close();
            }
        }

        private async void Main_Load(object sender, EventArgs e)
        {
            /// MistralAI WebView2 Initialization
            await main_tabControl_tabPage_mistralAI_webView21_browserDisplay.EnsureCoreWebView2Async();
            LoadMistralCookies();
            main_tabControl_tabPage_mistralAI_webView21_browserDisplay.Source = new Uri("https://chat.mistral.ai/chat");
            main_tabControl_tabPage_mistralAI_webView21_browserDisplay.NavigationCompleted += async (s, args) =>
            {
                if (args.IsSuccess && main_tabControl_tabPage_mistralAI_webView21_browserDisplay.Source.AbsoluteUri.Contains("chat.mistral.ai"))
                {
                    await SaveMistralCookies();
                }
            };

            /// DeepAI WebView2 Initialization
            await main_tabControl_tabPage_deepAI_webView21_browserDisplay.EnsureCoreWebView2Async();
            LoadDeepAICookies();
            main_tabControl_tabPage_deepAI_webView21_browserDisplay.Source = new Uri("https://deepai.org/chat");
            main_tabControl_tabPage_deepAI_webView21_browserDisplay.NavigationCompleted += async (s, args) =>
            {
                if (args.IsSuccess && main_tabControl_tabPage_deepAI_webView21_browserDisplay.Source.AbsoluteUri.Contains("deepai.org"))
                {
                    await SaveDeepAICookies();
                }
            };

            /// BlackBox.AI WebView2 Initialization
            await main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.EnsureCoreWebView2Async();
            LoadBlackBoxCookies();
            main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.Source = new Uri("https://app.blackbox.ai/");
            main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.NavigationCompleted += async (s, args) =>
            {
                if (args.IsSuccess && main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.Source.AbsoluteUri.Contains("blackbox.ai"))
                {
                    await SaveBlackBoxCookies();
                }
            };

            /// GitHub Copilot WebView2 Initialization
            await main_tabControl_tabPage_copilot_webView21_browserDisplay.EnsureCoreWebView2Async();
            LoadCopilotCookies();
            main_tabControl_tabPage_copilot_webView21_browserDisplay.Source = new Uri("https://github.com/login");
            main_tabControl_tabPage_copilot_webView21_browserDisplay.NavigationCompleted += async (s, args) =>
            {
                if (args.IsSuccess && main_tabControl_tabPage_copilot_webView21_browserDisplay.Source.AbsoluteUri.Contains("github.com"))
                {
                    await SaveCopilotCookies();
                }
            };
        }

        /// MistralAI Cookie Management
        private string mistralCookiesFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AICookies", "mistralai_cookies.dat");
        private void LoadMistralCookies()
        {
            try
            {
                if (File.Exists(mistralCookiesFilePath))
                {
                    var cookies = File.ReadAllText(mistralCookiesFilePath);
                    if (!string.IsNullOrEmpty(cookies))
                    {
                        var cookieManager = main_tabControl_tabPage_mistralAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                        foreach (var cookie in cookies.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var parts = cookie.Split('|');
                            if (parts.Length == 7)
                            {
                                bool isSecure = parts[3] == "TRUE";
                                bool isHttpOnly = parts[4] == "TRUE";
                                DateTime.TryParse(parts[5], out var expires);
                                var coreCookie = cookieManager.CreateCookie(parts[0], parts[1], parts[2], parts[6]);
                                coreCookie.IsSecure = isSecure;
                                coreCookie.IsHttpOnly = isHttpOnly;
                                coreCookie.Expires = expires;
                                cookieManager.AddOrUpdateCookie(coreCookie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load MistralAI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task SaveMistralCookies()
        {
            try
            {
                var cookieManager = main_tabControl_tabPage_mistralAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                var cookies = await cookieManager.GetCookiesAsync("https://chat.mistral.ai");
                var cookieList = new List<string>();
                foreach (var cookie in cookies)
                {
                    cookieList.Add($"{cookie.Name}|{cookie.Value}|{cookie.Domain}|{cookie.IsSecure}|{cookie.IsHttpOnly}|{cookie.Expires}|{cookie.Path}");
                }
                File.WriteAllText(mistralCookiesFilePath, string.Join("\n", cookieList));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save MistralAI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// DeepAI Cookie Management
        private string deepAICookiesFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AICookies", "deepai_cookies.dat");
        private void LoadDeepAICookies()
        {
            try
            {
                if (File.Exists(deepAICookiesFilePath))
                {
                    var cookies = File.ReadAllText(deepAICookiesFilePath);
                    if (!string.IsNullOrEmpty(cookies))
                    {
                        var cookieManager = main_tabControl_tabPage_deepAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                        foreach (var cookie in cookies.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var parts = cookie.Split('|');
                            if (parts.Length == 7)
                            {
                                bool isSecure = parts[3] == "TRUE";
                                bool isHttpOnly = parts[4] == "TRUE";
                                DateTime.TryParse(parts[5], out var expires);
                                var coreCookie = cookieManager.CreateCookie(parts[0], parts[1], parts[2], parts[6]);
                                coreCookie.IsSecure = isSecure;
                                coreCookie.IsHttpOnly = isHttpOnly;
                                coreCookie.Expires = expires;
                                cookieManager.AddOrUpdateCookie(coreCookie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load DeepAI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task SaveDeepAICookies()
        {
            try
            {
                var cookieManager = main_tabControl_tabPage_deepAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                var cookies = await cookieManager.GetCookiesAsync("https://deepai.org");
                var cookieList = new List<string>();
                foreach (var cookie in cookies)
                {
                    cookieList.Add($"{cookie.Name}|{cookie.Value}|{cookie.Domain}|{cookie.IsSecure}|{cookie.IsHttpOnly}|{cookie.Expires}|{cookie.Path}");
                }
                File.WriteAllText(deepAICookiesFilePath, string.Join("\n", cookieList));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save DeepAI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// BlackBox.AI Cookie Management
        private string blackBoxCookiesFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AICookies", "blackboxai_cookies.dat");

        private void LoadBlackBoxCookies()
        {
            try
            {
                if (File.Exists(blackBoxCookiesFilePath))
                {
                    var cookies = File.ReadAllText(blackBoxCookiesFilePath);
                    if (!string.IsNullOrEmpty(cookies))
                    {
                        var cookieManager = main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                        foreach (var cookie in cookies.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var parts = cookie.Split('|');
                            if (parts.Length == 7)
                            {
                                bool isSecure = parts[3] == "TRUE";
                                bool isHttpOnly = parts[4] == "TRUE";
                                DateTime.TryParse(parts[5], out var expires);
                                var coreCookie = cookieManager.CreateCookie(parts[0], parts[1], parts[2], parts[6]);
                                coreCookie.IsSecure = isSecure;
                                coreCookie.IsHttpOnly = isHttpOnly;
                                coreCookie.Expires = expires;
                                cookieManager.AddOrUpdateCookie(coreCookie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load BlackBox.AI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task SaveBlackBoxCookies()
        {
            try
            {
                var cookieManager = main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                var cookies = await cookieManager.GetCookiesAsync("https://www.blackbox.ai");
                var cookieList = new List<string>();
                foreach (var cookie in cookies)
                {
                    cookieList.Add($"{cookie.Name}|{cookie.Value}|{cookie.Domain}|{cookie.IsSecure}|{cookie.IsHttpOnly}|{cookie.Expires}|{cookie.Path}");
                }
                File.WriteAllText(blackBoxCookiesFilePath, string.Join("\n", cookieList));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save BlackBox.AI cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// GitHub Copilot Cookie Management
        private string copilotCookiesFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AICookies", "copilot_cookies.dat");

        private void LoadCopilotCookies()
        {
            try
            {
                if (File.Exists(copilotCookiesFilePath))
                {
                    var cookies = File.ReadAllText(copilotCookiesFilePath);
                    if (!string.IsNullOrEmpty(cookies))
                    {
                        var cookieManager = main_tabControl_tabPage_copilot_webView21_browserDisplay.CoreWebView2.CookieManager;
                        foreach (var cookie in cookies.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var parts = cookie.Split('|');
                            if (parts.Length == 7)
                            {
                                bool isSecure = parts[3] == "TRUE";
                                bool isHttpOnly = parts[4] == "TRUE";
                                DateTime.TryParse(parts[5], out var expires);
                                var coreCookie = cookieManager.CreateCookie(parts[0], parts[1], parts[2], parts[6]);
                                coreCookie.IsSecure = isSecure;
                                coreCookie.IsHttpOnly = isHttpOnly;
                                coreCookie.Expires = expires;
                                cookieManager.AddOrUpdateCookie(coreCookie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load GitHub Copilot cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task SaveCopilotCookies()
        {
            try
            {
                var cookieManager = main_tabControl_tabPage_copilot_webView21_browserDisplay.CoreWebView2.CookieManager;
                var cookies = await cookieManager.GetCookiesAsync("https://github.com");
                var cookieList = new List<string>();
                foreach (var cookie in cookies)
                {
                    cookieList.Add($"{cookie.Name}|{cookie.Value}|{cookie.Domain}|{cookie.IsSecure}|{cookie.IsHttpOnly}|{cookie.Expires}|{cookie.Path}");
                }
                File.WriteAllText(copilotCookiesFilePath, string.Join("\n", cookieList));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save GitHub Copilot cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// Clear Cookies Button Click Event
        private async void main_statusStrip_button_clearCookies_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Clear MistralAI cookies
                var mistralCookieManager = main_tabControl_tabPage_mistralAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                if (mistralCookieManager != null)
                {
                    var mistralCookies = await mistralCookieManager.GetCookiesAsync("https://chat.mistral.ai");
                    foreach (var cookie in mistralCookies)
                    {
                        mistralCookieManager.DeleteCookie(cookie);
                    }
                    if (File.Exists(mistralCookiesFilePath))
                    {
                        File.Delete(mistralCookiesFilePath);
                    }
                }

                // Clear DeepAI cookies
                var deepAICookieManager = main_tabControl_tabPage_deepAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                if (deepAICookieManager != null)
                {
                    var deepAICookies = await deepAICookieManager.GetCookiesAsync("https://deepai.org");
                    foreach (var cookie in deepAICookies)
                    {
                        deepAICookieManager.DeleteCookie(cookie);
                    }
                    if (File.Exists(deepAICookiesFilePath))
                    {
                        File.Delete(deepAICookiesFilePath);
                    }
                }

                // Clear BlackBox.AI cookies
                var blackBoxCookieManager = main_tabControl_tabPage_blackBoxAI_webView21_browserDisplay.CoreWebView2.CookieManager;
                if (blackBoxCookieManager != null)
                {
                    var blackBoxCookies = await blackBoxCookieManager.GetCookiesAsync("https://www.blackbox.ai");
                    foreach (var cookie in blackBoxCookies)
                    {
                        blackBoxCookieManager.DeleteCookie(cookie);
                    }
                    if (File.Exists(blackBoxCookiesFilePath))
                    {
                        File.Delete(blackBoxCookiesFilePath);
                    }
                }

                // Clear GitHub Copilot cookies
                var copilotCookieManager = main_tabControl_tabPage_copilot_webView21_browserDisplay.CoreWebView2.CookieManager;
                if (copilotCookieManager != null)
                {
                    var copilotCookies = await copilotCookieManager.GetCookiesAsync("https://github.com");
                    foreach (var cookie in copilotCookies)
                    {
                        copilotCookieManager.DeleteCookie(cookie);
                    }
                    if (File.Exists(copilotCookiesFilePath))
                    {
                        File.Delete(copilotCookiesFilePath);
                    }
                }

                MessageBox.Show("All cookies have been cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to clear cookies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// Tab Control Custom Drawing
        private void Main_tabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender!;
            Brush textBrush;
            Brush backBrush = new SolidBrush(SystemColors.Control);

            if (e.Index == tabControl.SelectedIndex)
            {
                backBrush = new SolidBrush(Color.LightSteelBlue);
                textBrush = new SolidBrush(Color.DarkBlue);
            }
            else
            {
                textBrush = new SolidBrush(Color.Navy);
            }

            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabBounds = tabControl.GetTabRect(e.Index);

            e.Graphics.FillRectangle(backBrush, tabBounds);
            e.Graphics.DrawString(tabPage.Text, tabControl.Font, textBrush, tabBounds,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            textBrush.Dispose();
            backBrush.Dispose();
        }

        /// Code Assistant Button Click Event
        private void main_menuStrip_button_codeAssistant_Click(object? sender, EventArgs e)
        {
            if (_codeAssistantForm == null || _codeAssistantForm.IsDisposed)
            {
                _codeAssistantForm = new codeAssistant();
                _codeAssistantForm.FormClosed += (s, args) => _codeAssistantForm = null;
                _codeAssistantForm.Show();
            }
            else
            {
                _codeAssistantForm.BringToFront();
            }
        }

        /// Adjust Code Assistant Button Position on Resize
        private void aiCodeAssistMain_Resize(object sender, EventArgs e)
        {
            this.Resize += (sender, e) =>
            {
                main_menuStrip_button_codeAssistant.Margin = new Padding(
                    this.ClientSize.Width - main_menuStrip_button_codeAssistant.Width - 100,
                    0,
                    0,
                    0
                );
            };
        }
    }
}
