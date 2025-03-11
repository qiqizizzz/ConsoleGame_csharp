using ConsoleApp3.Lesson1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson2
{
    internal class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            strTitle = "游戏结束";
            strOne = "返回主菜单";
        }
        public override void EnterJDoSomething()
        {
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_Scene_Type.Begin);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
