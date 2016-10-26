using DatGen.BL;
using DatGen.Dat;
using System;
using System.Diagnostics;

namespace DatGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            repository.Init();

            ScriptGen generator = new ScriptGen(repository);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            ScriptWriter writer = new ScriptWriter(generator);
            writer.Write(@"D:\testScript.sql", 1000);

            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds);

            repository.Init();

            watch.Reset();
            watch.Start();

            writer.WriteAsync(@"D:\testScriptAsync.sql", 1000);

            watch.Stop();

            Console.WriteLine("async " + watch.ElapsedMilliseconds);

            repository.Init();

            watch.Reset();
            watch.Start();

            writer.WriteScript(@"D:\testScriptMerged.sql", 1000);

            watch.Stop();

            Console.WriteLine("Sript " + watch.ElapsedMilliseconds);

            Console.ReadLine();

        }
    }
}
