using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class Program1
{
    public static bool PasswordComplexityPolicy()
    {
        var tempFile = Path.GetTempFileName();

        Process p = new Process();
        p.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\secedit.exe");
        p.StartInfo.Arguments = String.Format(@"/export /cfg ""{0}"" /quiet", tempFile);
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.UseShellExecute = false;
        p.Start();
        p.WaitForExit();

        var file = IniFile.Load(tempFile);

        IniSection systemAccess = null;
        var passwordComplexityString = "";
        var passwordComplexity = 0;

        return file.Sections.TryGetValue("System Access", out systemAccess)
            && systemAccess.TryGetValue("PasswordComplexity", out passwordComplexityString)
            && Int32.TryParse(passwordComplexityString, out passwordComplexity)
            && passwordComplexity == 1;
    }

    public class IniFile
    {
        public static IniFile Load(string filename)
        {
            var result = new IniFile();
            result.Sections = new Dictionary<string, IniSection>();
            var section = new IniSection(String.Empty);
            result.Sections.Add(section.Name, section);

            foreach (var line in File.ReadAllLines(filename))
            {
                var trimedLine = line.Trim();
                switch (line[0])
                {
                    case ';':
                        continue;
                    case '[':
                        section = new IniSection(trimedLine.Substring(1, trimedLine.Length - 2));
                        result.Sections.Add(section.Name, section);
                        break;
                    default:
                        var parts = trimedLine.Split('=');
                        if (parts.Length > 1)
                        {
                            section.Add(parts[0].Trim(), parts[1].Trim());
                        }
                        break;
                }
            }

            return result;
        }

        public IDictionary<string, IniSection> Sections { get; private set; }
    }

    public class IniSection : Dictionary<string, string>
    {
        public IniSection(string name)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
