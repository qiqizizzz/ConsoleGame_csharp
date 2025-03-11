using ConsoleApp3.Lesson1;
using System.Text;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // 设置控制台编码为 UTF-8
            Game game = new Game();
            game.Start();
        }
    }
}
