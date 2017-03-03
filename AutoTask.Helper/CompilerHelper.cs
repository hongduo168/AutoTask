using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper
{
    public class CompilerHelper
    {
        private Assembly assembly = null;
        public void BuildDLL(string code)
        {
            // 1.CSharpCodePrivoder
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.ICodeComplier
            //ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            // 3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            //Trace.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            objCompilerParameters.ReferencedAssemblies.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quartz.dll"));
            objCompilerParameters.ReferencedAssemblies.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoTask.Helper.dll"));
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = false;
            objCompilerParameters.OutputAssembly = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoTask.Job.dll");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using AutoTask.Helper;");
            sb.AppendLine("using Quartz;");
            sb.AppendLine("namespace AutoTask.Service.Job");
            sb.AppendLine("{");
            sb.AppendLine(code);
            sb.AppendLine("}");
            // 4.CompilerResults
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, sb.ToString());
            //CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, code);

            if (cr.Errors.HasErrors)
            {
                Console.WriteLine("编译错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    Trace.WriteLine(err.ErrorText);
                }
                Trace.WriteLine("==========================================");
                //return null;
            }
            else
            {
                assembly = cr.CompiledAssembly;
            }
            //var type = cr.CompiledAssembly.GetType("AutoTask.Service.Job." + className);
            //return type;
        }

        public Type GetType(string name)
        {
            var type = assembly.GetType("AutoTask.Service.Job." + name);
            return type;
        }
    }
}
