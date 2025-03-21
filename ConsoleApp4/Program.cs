using System.Text;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // 设置控制台编码为 UTF-8
            Game g = new Game();
            g.Start();
        }
    }
}
