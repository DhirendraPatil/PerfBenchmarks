using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMark
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"..\..\..\datafile.txt";
            if (args.Length > 0)
                filePath = args[0];

            Stopwatch sw = Stopwatch.StartNew();
            var report = PerfTest(filePath);
            sw.Start();
            Console.WriteLine("total time taken to process the file {0} Milliseconds, total words count {1} and Total line {2}", sw.ElapsedMilliseconds, s_totalwords, s_lines);
            WriteReport(report);
            Console.ReadLine();
        }

        static char s_delimiter = ' ';
        static Int64 s_totalwords = 0;
        static Int64 s_lines=0;
        static Dictionary<string, int> PerfTest(string filePath)
        {
            var lines = GetLines(filePath);
            var dict = new Dictionary<string, int>();
            foreach (var line in lines)
            {
                var words = line.Split(s_delimiter);
                foreach (var w in words)
                {
                    int counter;
                    if (dict.TryGetValue(w, out counter))
                    {
                        counter++;
                        dict[w] = counter;
                    }
                    else
                    {
                        dict.Add(w, 1);
                    }
                    s_totalwords++;
                }
                s_lines++;
            }
            return dict;
        }

        static IEnumerable<string> GetLines(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var reader = new StreamReader(stream);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        static void WriteReport(Dictionary<string, int> report)
        {
            if (report.Count <= 0) return;
            var max = report.Values.Max();
            Console.WriteLine("Summary of words: No of Unique words {0} Max Count '{1}'={2}", report.Count, report.FirstOrDefault(pair => pair.Value == max).Key, max);
        }
    }
}
