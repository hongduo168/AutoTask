using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper
{
    public static class CodeHelper
    {
        public static string CodeString(string className, string requestHost, string requestPath)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("using System;");
            //sb.AppendLine("using AutoTask.Helper;");
            //sb.AppendLine("using Quartz;");
            //sb.AppendLine("namespace AutoTask.Service.Job");
            //sb.AppendLine("{");
            sb.AppendLine("    public class " + className + " : IJob");
            sb.AppendLine("    {");
            sb.AppendLine("        public void Execute(IJobExecutionContext context)");
            sb.AppendLine("        {");
            sb.AppendLine("             TraceHelper.WriteLine(\"" + className + "\");");
            sb.AppendLine("             string resonse = RequestHelper.POSTAsync(\"" + requestHost + "\", \"" + requestPath + "\");");
            //sb.AppendLine("             TraceHelper.WriteLine(\"HTTP返回值\");");
            //sb.AppendLine("             TraceHelper.WriteLine(resonse);");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            //sb.AppendLine("}");


            string code = sb.ToString();
            Trace.WriteLine(code);

            return code;
        }

    }
}
