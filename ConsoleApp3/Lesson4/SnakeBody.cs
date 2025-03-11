using ConsoleApp3.Lesson3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson4
{
    /// <summary>
    /// 蛇身体类型
    /// </summary>
    enum E_SnameBody_Type
    {
        /// <summary>
        /// 头
        /// </summary>
        head,
        /// <summary>
        /// 身体
        /// </summary>
        body,
    }
    internal class SnakeBody : GameObject
    {
        private E_SnameBody_Type type;

        public SnakeBody(E_SnameBody_Type type,int x,int y)
        {
            this.type = type;
            pos = new Position(x, y);
        }
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = type == E_SnameBody_Type.head ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write(type == E_SnameBody_Type.head ? "●" : "○");
        }
    }
}
