using System;
using System.IO;
using AI.CodeAssist;

namespace AI.CodeAssist
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Tell the app: "Put the toy box in this room!"
            string toyBox = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CodeAssist",
                "WebView2");
            Directory.CreateDirectory(toyBox);
            Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", toyBox);

            ApplicationConfiguration.Initialize();
            Application.Run(new aiCodeAssistMain());
        }
    }
}