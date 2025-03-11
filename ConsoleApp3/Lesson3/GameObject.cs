using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson3
{
    abstract class GameObject : IDraw
    {
        //游戏对象位置
        public Position pos;

        public abstract void Draw();
    }
}
