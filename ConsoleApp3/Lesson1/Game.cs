using ConsoleApp3.Lesson2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Lesson1
{
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum E_Scene_Type
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
    }

    internal class Game
    {
        //窗口宽高
        public const int w = 80;
        public const int h = 20;

        //当前选中的场景
        public static ISceneUpdate nowScene;

        public Game()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);

            ChangeScene(E_Scene_Type.Begin);
        }

        //游戏开始的方法
        public void Start()
        {
            while (true)
            {
                if (nowScene != null)
                {
                    nowScene.update();
                }
            }
        }

        public static void ChangeScene(E_Scene_Type type)
        {
            //切场景前先擦除上一个场景的东西
            Console.Clear();
            switch (type)
            {
                case E_Scene_Type.Begin:
                    nowScene = new BeginScene();
                    break;
                case E_Scene_Type.Game:
                    nowScene = new GameScene();
                    break;
                case E_Scene_Type.End:
                    nowScene = new EndScene();
                    break;
            }
        }

    }
}
