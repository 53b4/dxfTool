using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using netDxf;
using netDxf.Entities;

namespace DxfTool
{
    internal class CirclesCreator
    {
        public List<double[]> ReadCircleCenterDefinitions(string filePath)
        {
            var res = new List<double[]>();

            IEnumerable<string> lines = File.ReadLines(filePath);

            foreach (string line in lines)
            {
                try
                {
                    string[] reatOneLineDefinition = line.Split(Constans.INPUTSEPARATOR);

                    double[] oneVectorDefinition = Array.ConvertAll(reatOneLineDefinition,a => double.Parse(a, CultureInfo.InvariantCulture));
                    
                    res.Add(oneVectorDefinition);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
            }

            Console.WriteLine("read definitions: ");
            foreach (double[] doubles in res)
            {
                foreach (var d in doubles)
                {
                    Console.Write($"{d} ");
                }
                Console.WriteLine();
            }


            return res;
        }

        public List<Vector3> GenerateVector3s(List<double[]> vectorDefinitions)
        {
            var res = new List<Vector3>();
            foreach (double[] def in vectorDefinitions)
                res.Add(new Vector3(def));

            return res;
        }

        public List<Circle> GenerateCircle3s(List<Vector3> vector3s, double radius)
        {
            var res = new List<Circle>();

            foreach (Vector3 vector3 in vector3s)
                res.Add(new Circle(vector3, radius));

            return res;
        }

        public List<Vector2> GenerateVector2s(List<double[]> vectorDefinitions)
        {
            var res = new List<Vector2>();
            foreach (double[] def in vectorDefinitions)
                res.Add(new Vector2(def));

            return res;
        }

        public List<Circle> GenerateCircle2s(List<Vector2> vector2s, double radius)
        {
            var res = new List<Circle>();

            foreach (Vector2 vector2 in vector2s)
                res.Add(new Circle(vector2, radius));

            return res;
        }
    }
}