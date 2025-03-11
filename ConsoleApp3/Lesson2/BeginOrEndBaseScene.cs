using ConsoleApp3.Lesson1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson2
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex=0;
        protected string strTitle;
        protected string strOne;

        public abstract void EnterJDoSomething();

        public void update()
        {
            //设置颜色
            Console.ForegroundColor = ConsoleColor.White;

            //显示标题
            Console.SetCursorPosition(Game.w / 2 - strTitle.Length,5);
            Console.Write(strTitle);

            //第一个选项
            Console.SetCursorPosition(Game.w / 2 - strOne.Length, 8);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(strOne);

            //第二个选项
            Console.SetCursorPosition(Game.w / 2 - 4, 10);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write("结束游戏");

            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    nowSelIndex--;
                    if (nowSelIndex < 0)
                        nowSelIndex = 0;
                    break;
                case ConsoleKey.S:
                    nowSelIndex++;
                    if (nowSelIndex >1)
                        nowSelIndex = 1;
                    break;
                case ConsoleKey.J:
                    EnterJDoSomething();
                    break;
            }
        }
    }
}
