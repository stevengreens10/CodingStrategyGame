using System;
using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System.IO;
using System.Linq;
using System.CodeDom.Compiler;
using System.Reflection;

namespace CSharpRunner
{
    public class Bot
    {

        public static CSharpCodeProvider GetCodeProvider() => new Lazy<CSharpCodeProvider>(() =>
        {
            var csc = new CSharpCodeProvider();
            var settings = csc
                .GetType()
                .GetField("_compilerSettings", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(csc);

            var path = settings
                .GetType()
                .GetField("_compilerFullPath", BindingFlags.Instance | BindingFlags.NonPublic);

            path.SetValue(settings, ((string)path.GetValue(settings)).Replace(@"bin\roslyn\", @"roslyn\"));

            return csc;
        }).Value;

        public static T FromFile<T>(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("File does not exist");



            T result = default(T);
            using (var cs = GetCodeProvider())
            {
                var assembly = typeof(T).Assembly;
                var cp = new CompilerParameters()
                {
                    GenerateInMemory = false,
                    GenerateExecutable = false,
                    IncludeDebugInformation = true,
                };

                if (Environment.OSVersion.Platform.ToString() != "Unix") cp.TempFiles = new TempFileCollection(Environment.GetEnvironmentVariable("TEMP"), true);
                else cp.TempFiles.KeepFiles = true;
                cp.ReferencedAssemblies.Add("System.dll");
                cp.ReferencedAssemblies.Add("System.Core.dll");
                cp.ReferencedAssemblies.Add(assembly.Location);
                CompilerResults cr;


                Program.Log($"Attempting to compile file \"{path}\"");

                //check if the directory exists, and if it does search for .cs files and compile them
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
                        {
                            Program.Log("-------------");
                            Program.Log($"[CompileError]: {e.ErrorText}", ConsoleColor.DarkRed);
                            Program.Log($"File: \"{e.FileName}\" Line: {e.Line}", ConsoleColor.DarkMagenta);
                            Program.Log("-------------");
                        }
                    throw new InvalidOperationException("Compliation failed, check your code.");
                }

                Program.Log("Compile finished");

                var types = cr.CompiledAssembly.GetTypes();
                var botType = types.Where(x => x.GetInterfaces().Contains(typeof(T))).FirstOrDefault();
                if (botType == null)
                    throw new TypeLoadException("Could not find an IMazeBot class");

                result = (T)cr.CompiledAssembly.CreateInstance(botType.FullName);

            }

            return result;

        }

        
    }
}
