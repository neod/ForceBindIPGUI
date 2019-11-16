using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ForceBindIPGUI.Models
{
    public static class ForceBindIPConnector
    {
        public static void Launch(Profiles profile)
        {

            PeHeaderReader phr = new PeHeaderReader(profile.Path);

            ProcessStartInfo startInfo = new ProcessStartInfo((phr.Is32BitHeader) ? "ForceBindIP.exe" : "ForceBindIP64.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.ArgumentList.Add(profile.IP);
            startInfo.ArgumentList.Add(convertProgFilePath(profile.Path));

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

        }

        private static string convertProgFilePath(string path)
        {
            if (path.Contains("Program Files (x86)")) return path.Replace("Program Files (x86)", "PROGRA~2");
            if (path.Contains("Program Files")) return path.Replace("Program Files", "PROGRA~1");
            return path;
        }

    }
}
