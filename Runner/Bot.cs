using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace CSharpRunner
{
    public class Bot
    {
        public static IMazeBot FromFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("File does not exist");

            IMazeBot result = null;
            using (var cs = new CSharpCodeProvider())
            {
                var assembly = typeof(IMazeBot).Assembly;
                var cp = new CompilerParameters()
                {
                    GenerateInMemory = false,
                    GenerateExecutable = false,
                    IncludeDebugInformation = true
                };
                if (Environment.OSVersion.Platform.ToString() != "Unix") cp.TempFiles = new TempFileCollection(Environment.GetEnvironmentVariable("TEMP"), true);
                else cp.TempFiles.KeepFiles = true;
                cp.ReferencedAssemblies.Add("System.dll");
                cp.ReferencedAssemblies.Add("System.Core.dll");
                cp.ReferencedAssemblies.Add(assembly.Location);
                CompilerResults cr;

                //check if the directory exists, and if it does search for .cs files
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
                    cr = cs.CompileAssemblyFromFile(cp, files);
                }
                else cr = cs.CompileAssemblyFromFile(cp, new string[] { path });

                //check for compiler errors
                if (cr.Errors.HasErrors)
                {
                    foreach (CompilerError e in cr.Errors)
                        if (!e.IsWarning)
                            Console.Error.WriteLine(e.ToString());
                    throw new InvalidOperationException("Compliation failed, check your code.");
                }

                var types = cr.CompiledAssembly.GetTypes();
                var botType = types.Where(x => x is IMazeBot).FirstOrDefault();
                if (botType == null)
                    throw new TypeLoadException("Could not find an IMazeBot class");

                result = (IMazeBot)cr.CompiledAssembly.CreateInstance(botType.FullName);

            }

            return result;

        }

        
    }
}
