using ConsoleApp3.Lesson3;
using ConsoleApp3.Lesson4;
using ConsoleApp3.Lesson5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson6
{
    /// <summary>
    /// 蛇的移动方向
    /// </summary>
    enum E_Move_Dir
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class Snake : IDraw
    {
        SnakeBody[] bodies;
        int nowNum;
        E_Move_Dir dir;

        public Snake(int x,int y)
        {
            bodies = new SnakeBody[200];

            bodies[0] = new SnakeBody(E_SnameBody_Type.head, x, y);
            nowNum = 1;

            dir = E_Move_Dir.Right;
        }

        public void Draw()
        {
            for (int i = 0; i < nowNum; i++)
            {
                bodies[i].Draw();
            }
        }

        public void Move()
        {
            //擦除最后一个位置
            SnakeBody lastBody = bodies[nowNum - 1];
            Console.SetCursorPosition(lastBody.pos.x, lastBody.pos.y);
            Console.Write("  ");

            for(int i = nowNum - 1; i > 0; i--)
            {
                bodies[i].pos = bodies[i - 1].pos;
            }

            switch (dir)
            {
                case E_Move_Dir.Up:
                    --bodies[0].pos.y;
                    break;
                case E_Move_Dir.Down:
                    ++bodies[0].pos.y;
                    break;
                case E_Move_Dir.Left:
                    bodies[0].pos.x -= 2;
                    break;
                case E_Move_Dir.Right:
                    bodies[0].pos.x += 2;
                    break;
            }
        }

        public void ChangeDir(E_Move_Dir dir)
        {
            if(dir==this.dir ||
                nowNum >1 &&
                (this.dir==E_Move_Dir.Left && dir==E_Move_Dir.Right ||
                 this.dir==E_Move_Dir.Right && dir==E_Move_Dir.Left ||
                 this.dir==E_Move_Dir.Down && dir==E_Move_Dir.Down ||
                 this.dir==E_Move_Dir.Down && dir==E_Move_Dir.Up
                ))
            {
                return;
            }

            this.dir = dir;
        }

        public bool CheckEnd(Map map)
        {
            //判断是否和墙体重合
            for (int i = 0; i < map.walls.Length; i++)
            {
                if (bodies[0].pos == map.walls[i].pos)
                {
                    return true;
                }
            }

            for (int i = 1; i < nowNum; i++)
            {
                if (bodies[i].pos == bodies[0].pos)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckSamePos(Position p)
        {
            for (int i = 0; i < nowNum; i++)
            {
                if (bodies[i].pos == p)
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckEatFood(Food food)
        {
            if (bodies[0].pos == food.pos)
            {
                //让食物随机
                food.RandomPos(this);

                //长身体
                AddBody();
            }
        }

        private void AddBody()
        {
            SnakeBody frontBody = bodies[nowNum - 1];
            bodies[nowNum] = new SnakeBody(E_SnameBody_Type.body, frontBody.pos.x, frontBody.pos.y);
            //加长度
            ++nowNum;
        }
    }
}
