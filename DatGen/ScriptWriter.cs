using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using DatGen.BL;
using DatGen.Dat;

namespace DatGen
{
    public class ScriptWriter
    {
        private readonly IScriptGen _generator;

        public ScriptWriter(IScriptGen generator)
        {
            _generator = generator;
        }

        public void Write(string filePath, int entityCount)
        {
            using (TextWriter writer = new StreamWriter(filePath))
            {

                string insertLine = _generator.GetInsertLine();
                writer.WriteLine(insertLine);

                for (int i = 1; i <= entityCount; i++)
                {
                    if (i > 1) writer.WriteLine(",");

                    UserEntity user = _generator.GenerateUser();
                    string valueLine = _generator.GetValueLine(user);
                    writer.Write(valueLine);
                }
            }
        }

        public void WriteScript(string filePath, int entityCount)
        {
            using (TextWriter writer = new StreamWriter(filePath))
            {
                string mergedResult = _generator.CreateScript(entityCount);
                writer.Write(mergedResult);
            }
        }

        public void WriteAsync(string filePath, int entityCount)
        {
            ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
            bool isComplete = false;

            Task task = new Task(() =>
            {
                using (TextWriter writer = new StreamWriter(filePath))
                {
                    string insertLine = _generator.GetInsertLine();
                    writer.WriteLine(insertLine);
                    string valueLine;

                    int i = 1;
                    while (!isComplete)
                    {
                        bool isNew = queue.TryDequeue(out valueLine);
                        if (isNew)
                        {
                            if (i > 1) writer.WriteLine(",");
                            writer.Write(valueLine);

                            i++;
                        }
                    }
                }
            });

            task.Start();

            Parallel.For(1, entityCount, i =>
            {
                UserEntity user = _generator.GenerateUser();
                string valueLine = _generator.GetValueLine(user);
                queue.Enqueue(valueLine);
            });

            isComplete = true;

            task.Wait();
        }
    }
}