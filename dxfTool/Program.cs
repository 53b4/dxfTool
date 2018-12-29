using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using netDxf;
using netDxf.Entities;
using netDxf.Header;

namespace DxfTool
{
    internal static class Constans
    {
        internal const string DXFEXT = ".dxf";
        internal const char INPUTSEPARATOR = ',';
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var messages = ValidateArguments(args);

            if (messages.Count > 0)
                foreach (string s in messages)
                {
                    Console.WriteLine(s);
                    Console.WriteLine("press any key to exit...");
                    Console.ReadKey();
                    return;
                }

            var arguments = PrepareArguments(args);

            var cc = new CirclesCreator();
            var circlesDefinitions = cc.ReadCircleCenterDefinitions(args[0]);

            var vector3s = cc.GenerateVector3s(circlesDefinitions);
            var circle3s = cc.GenerateCircle3s(vector3s, (double)arguments[1]);

            var dxf = new DxfDocument(DxfVersion.AutoCad2007);
            dxf.Viewport.ShowGrid = false;

            dxf.AddEntity(circle3s);

            dxf.Save((string)arguments[2]);
            Console.WriteLine($"generate file: {arguments[2]}");
        }

        private static List<string> ValidateArguments(string[] args)
        {
            var messages = new List<string>();
            double res;
            if (args.Length < 2)
            {
                messages.Add(@"Enter at least two arguments.
                               - path to  source file 
                               - radius of circle with double precision 
                               - optionally name of output file. input name with extension dxf as default will be used 
                ");
            }
            else if (!File.Exists(args[0]))
            {
                messages.Add(@"Incorrect path or file name. Should be file filename or path to existing directory");
            }
            else if (!double.TryParse(args[1], out res))
            {
                messages.Add(@"Incorrect format of radius -  enter double value, use coma as separator");
            }
            else if (args.Length > 2 && !IsValidPath(args[2]))
            {
                messages.Add(@"Incorrect path or file name of output file. Should be file correct filename or path to existing directory");
            }

            return messages;
        }
        private static bool IsValidPath(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            bool nameIsOK = true;
            bool pathIsOK = true;

            if (!string.IsNullOrEmpty(dirPath))
            {
                try
                {
                    Path.GetFullPath(dirPath);
                }
                catch (Exception e)
                {
                    pathIsOK = false;
                }
            }

            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
                nameIsOK = false;

            return pathIsOK && nameIsOK;
        }

        private static ArrayList PrepareArguments(string[] args)
        {
            var arguments = new ArrayList(3);
            arguments.Add(args[0]);
            arguments.Add(Double.Parse(args[1]));

            if (args.Length > 2)
            {
                if (!string.IsNullOrEmpty(Path.GetDirectoryName(args[2])))
                    Directory.CreateDirectory(Path.GetDirectoryName(args[2]));

                if (!Path.GetFileName(args[2]).EndsWith(Constans.DXFEXT))
                    arguments.Add($"{args[2]}{Constans.DXFEXT}");
                else
                    arguments.Add(args[2]);
            }
            else
                arguments.Add(Path.ChangeExtension(args[0], Constans.DXFEXT));

            Console.WriteLine("arguments: " + Environment.NewLine);

            foreach (var argument in arguments)
                Console.WriteLine(argument + Environment.NewLine);

            return arguments;
        }
    }
}
