using System.IO;

namespace ConsoleCalculator
{
    class Log
    {
        static FileStream fs;
        static StreamWriter sw;

        static Log()
        {
            fs = new FileStream("log.txt", FileMode.Append);
            sw = new StreamWriter(fs, System.Text.Encoding.Default)
            {
                AutoFlush = true
            };
        }
        public static void Write(string text)
        {
            sw.Write(text);
        }
        public static void Write(string format, params object[] arg)
        {
            sw.Write(format, arg);
        }
        public static void WriteLine(string text)
        {
            sw.WriteLine(text);
        }
        public static void WriteLine()
        {
            sw.WriteLine();
        }
        public static void Dispose()
        {
            sw.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}
