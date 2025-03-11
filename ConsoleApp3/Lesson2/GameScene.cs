using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Lesson1;
using ConsoleApp3.Lesson4;
using ConsoleApp3.Lesson5;
using ConsoleApp3.Lesson6;

namespace ConsoleApp3.Lesson2
{
    internal class GameScene:ISceneUpdate
    {
        Map Map;
        Snake snake;
        Food food;

        int updateIndex=0;

        public GameScene()
        {
            Map = new Map();
            snake = new Snake(40,10);
            food = new Food(snake);
        }

        public void update()
        {
            if (updateIndex % 4444==0)
            {
                Map.Draw();
                snake.Move();
                snake.Draw();
                food.Draw();

                //检测是否撞墙
                if(snake.CheckEnd(Map))
                {
                    //结束逻辑
                    Game.ChangeScene(E_Scene_Type.End);
                }

                snake.CheckEatFood(food);

                updateIndex = 0;
            }
            ++updateIndex;

            //检测键盘是否是空闲的，如果是，则返回false，让程序不被卡住
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_Move_Dir.Up);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_Move_Dir.Down);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_Move_Dir.Left);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_Move_Dir.Right);
                        break;
                }
            }
            
        }
    }
}
