using System.Diagnostics;

namespace ConsoleApp2
{
    enum E_SceneType
    {
        Begin,
        Game,
        End
    }



    #region 格子结构体和格子枚举
    enum E_Grid_Type
    {
        /// <summary>
        /// 普通格子
        /// </summary>
        Normal,
        /// <summary>
        /// 炸弹
        /// </summary>
        Boom,
        /// <summary>
        /// 暂停
        /// </summary>
        pause,
        /// <summary>
        /// 时空隧道，随机倒退，暂停，换位置
        /// </summary>
        Tunnel
    }

    /// <summary>
    /// 位置信息结构体，包含xy位置
    /// </summary>
    struct Vector2
    {
        public int x;
        public int y;
        public Vector2(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    struct Grid
    {
        //格子的类型
        public E_Grid_Type type;
        //格子的位置
        public Vector2 pos;
        public Grid(int x,int y,E_Grid_Type type)
        {
            pos.x = x;
            pos.y = y;
            this.type = type;
        }

        //打印
        public void Draw()
        {
            switch (type)
            {
                case E_Grid_Type.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(pos.x,pos.y);
                    Console.Write("□");
                    break;
                case E_Grid_Type.Boom:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(pos.x,pos.y);
                    Console.Write("●");
                    break;
                case E_Grid_Type.pause:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(pos.x,pos.y);
                    Console.Write("‖");
                    break;
                case E_Grid_Type.Tunnel:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(pos.x,pos.y);
                    Console.Write("↻");
                    break;
            }
        }
    }
    #endregion

    #region 地图结构体
    struct Map
    {
        public Grid[] grid;
        public Map(int x,int y,int num)
        {
            //设置x和y变化的次数
            int indexX = 0;
            int indexY = 0;

            //每个x的长度
            int stepNum = 2;

            grid = new Grid[num];
            Random r = new Random();
            int randomNum;

            for(int i = 0; i < num; i++)
            {
                randomNum = r.Next(0, 101);

                //设置类型
                if(randomNum<85 || randomNum==0 || randomNum == num-1)
                {
                    grid[i].type = E_Grid_Type.Normal;
                }
                else if(randomNum>=85 && randomNum < 90)
                {
                    grid[i].type = E_Grid_Type.Boom;
                }
                else if(randomNum>=90 && randomNum < 95)
                {
                    grid[i].type = E_Grid_Type.pause;
                }
                else
                {
                    grid[i].type = E_Grid_Type.Tunnel;
                }

                grid[i].pos = new Vector2(x, y);

                if (indexX == 10)
                {
                    y += 1;
                    //计数
                    indexY++;
                    if (indexY == 2)
                    {
                        indexX = 0;
                        indexY = 0;
                        stepNum = -stepNum;
                    }
                }
                else
                {
                    x += stepNum;
                    indexX++;
                }

            }
        }

        public void Draw()
        {
            for(int i = 0; i < grid.Length; i++)
            {
                grid[i].Draw();
            }
        }
    }
    #endregion

    #region 玩家枚举和玩家结构体
    enum E_PlayerType 
    {
        /// <summary>
        /// 玩家
        /// </summary>
        Player,
        /// <summary>
        /// 电脑
        /// </summary>
        Computer
    }
    struct Player
    {
        public E_PlayerType type;
        public int nowIndex;
        public bool isPause;//是否暂停

        public Player(int nowIndex,E_PlayerType type )
        {
            this.type = type;
            this.nowIndex = nowIndex;
            isPause = false;
        }

        public void Draw(Map mapInfo)
        {
            Grid grid = mapInfo.grid[nowIndex];
            //设置位置
            Console.SetCursorPosition(grid.pos.x, grid.pos.y);
            switch (type)
            {
                case E_PlayerType.Player:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("★");
                    break;
                case E_PlayerType.Computer:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("▲");
                    break;
            }

        }

    }

    #endregion

    internal class Program
    {
        static int w = 50;
        static int h = 30;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            E_SceneType nowSceneType = E_SceneType.Begin;
            ConsoleInit(w,h);
            while (true)
            {
                switch (nowSceneType)
                {
                    case E_SceneType.Begin:
                        //开始场景逻辑
                        Console.Clear();
                        BeginOrEndScene(w,h,ref nowSceneType);
                        break;
                    case E_SceneType.Game:
                        //游戏场景逻辑
                        Console.Clear();
                        GameScene(w, h,ref nowSceneType);
                        break;
                    case E_SceneType.End:
                        //结束场景逻辑
                        Console.Clear();
                        BeginOrEndScene(w, h, ref nowSceneType);
                        break;

                }
            }
        }

        #region 窗口初始化函数
        static void ConsoleInit(int w,int h)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }
        #endregion

        #region 开始场景逻辑+结束场景逻辑
        static void BeginOrEndScene(int w,int h,ref E_SceneType nowSceneType)
        {
            Console.ForegroundColor = ConsoleColor.White;
            int Flag = 0;
            bool isQuitBegin = false;
            Console.SetCursorPosition(nowSceneType==E_SceneType.Begin?w/2-3:w/2-4, 8);
            Console.Write(nowSceneType == E_SceneType.Begin?"飞行棋":"游戏结束");
            Console.ForegroundColor = ConsoleColor.Red;
            while (true)
            {
                isQuitBegin = false;
                Console.ForegroundColor = (Flag == 0) ? ConsoleColor.Red : ConsoleColor.White;
                Console.SetCursorPosition(nowSceneType == E_SceneType.Begin ? w / 2 - 4 : w / 2 - 5, 12);
                Console.Write(nowSceneType == E_SceneType.Begin ? "开始游戏":"回到主菜单");
                Console.ForegroundColor = (Flag == 1) ? ConsoleColor.Red : ConsoleColor.White;
                Console.SetCursorPosition(w / 2 - 4, 14);
                Console.Write("退出游戏");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        Flag--;
                        if (Flag < 0)
                        {
                            Flag = 0;
                        }
                        break;
                    case ConsoleKey.S:
                        Flag++;
                        if (Flag > 1)
                        {
                            Flag = 1;
                        }
                        break;
                    case ConsoleKey.J:
                        if (Flag == 0)
                        {
                            nowSceneType = nowSceneType == E_SceneType.Begin ? E_SceneType.Game:E_SceneType.Begin;
                            isQuitBegin = true;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;
                 }
                if (isQuitBegin == true)
                {
                    break;//跳出循环
                }
        }
        }
        #endregion

        #region 游戏场景逻辑
        static void GameScene(int w,int h,ref E_SceneType nowSceneType)
        {
            DrawWall(w, h);
            Map map = new Map(14, 3, 80);
            map.Draw();
            //绘制玩家
            Player player = new Player(0,E_PlayerType.Player);
            Player computer = new Player(0, E_PlayerType.Computer);
            DrawPlayer(player, computer, map);
            bool isGameOver = false;
            while (true)
            {
                //游戏逻辑
                //玩家扔色子
                Console.ReadKey(true);
                isGameOver=RandomMove(w, h, ref player, ref computer, map);
                map.Draw();
                DrawPlayer(player, computer, map);
                if (isGameOver)
                {
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }

                //电脑扔色子
                //玩家扔色子
                Console.ReadKey(true);
                isGameOver = RandomMove(w, h, ref computer, ref player, map);
                map.Draw();
                DrawPlayer(player, computer, map);
                if (isGameOver)
                {
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.End;
                    break;
                }

            }
            
        }
        #endregion

        #region 不变的场景
        static void DrawWall(int w,int h)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for(int i = 0; i < w; i+=2)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("⬛");
                Console.SetCursorPosition(i, h-1);
                Console.Write("⬛");
                Console.SetCursorPosition(i, h-6);
                Console.Write("⬛");
                Console.SetCursorPosition(i, h-11);
                Console.Write("⬛");
            }
            for(int i = 0; i < h; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("⬛");
                Console.SetCursorPosition(w-2, i);
                Console.Write("⬛");
            }

            //文字信息
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 10);
            Console.Write("□:普通格子");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, h - 9);
            Console.Write("‖:暂停，一回合不动");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(26, h - 9);
            Console.Write("●:炸弹，倒退5格");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(2, h - 8);
            Console.Write("↻:时空隧道，随机倒退，暂停，换位置");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(2, h - 7);
            Console.Write("★:玩家");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(12, h - 7);
            Console.Write("▲:电脑");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(22, h - 7);
            Console.Write("○:玩家与电脑重合");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, h - 5);
            Console.Write("按任意键开始扔色子");
        }
        #endregion

        #region 绘制玩家
        static void DrawPlayer(Player player,Player computer,Map map)
        {
            //重合时
            if (player.nowIndex == computer.nowIndex)
            {
                Grid grid = map.grid[player.nowIndex];
                Console.SetCursorPosition(grid.pos.x, grid.pos.y);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("○");
            }
            else
            {
                player.Draw(map);
                computer.Draw(map);
            }
        }
        #endregion

        #region 擦除的函数
        static void clearInfo()
        {
            Console.SetCursorPosition(2, h-5);
            Console.Write("                                     ");
            Console.SetCursorPosition(2, h - 4);
            Console.Write("                                     ");
            Console.SetCursorPosition(2, h - 3);
            Console.Write("                                     ");
            Console.SetCursorPosition(2, h - 2);
            Console.Write("                                     ");
        }
        #endregion


        #region 扔色子 函数
        /// <summary>
        /// 扔色子函数
        /// </summary>
        /// <param name="w">窗口的宽</param>
        /// <param name="h">窗口的高</param>
        /// <param name="p">扔色子的对象</param>
        /// <param name="map">地图信息</param>
        /// <returns>默认返回false 代表没有结束</returns>
        static bool RandomMove(int w,int h,ref Player p, ref Player otherP, Map map)
        {
            clearInfo();

            Console.ForegroundColor = p.type == E_PlayerType.Player ? ConsoleColor.Cyan : ConsoleColor.Magenta;


            if (p.isPause == true)
            {
                Console.SetCursorPosition(2, h - 5);
                Console.Write("处于暂停点，{0}需要暂停一回合", p.type==E_PlayerType.Player?"你":"电脑");
                Console.SetCursorPosition(2, h - 4);
                Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayerType.Player ? "电脑" : "你");
                p.isPause = false;
                return false;
            }

            Random r = new Random();
            int randomNum = r.Next(1, 7);
            p.nowIndex += randomNum;

            //打印扔的点数
            Console.SetCursorPosition(2, h - 5);
            Console.Write("{0}扔出点数为：" + randomNum,p.type==E_PlayerType.Player?"你":"电脑");

            if (p.nowIndex >= map.grid.Length - 1)
            {
                p.nowIndex = map.grid.Length-1;
                Console.SetCursorPosition(2, h - 4);
                if (p.type == E_PlayerType.Player)
                {
                    Console.Write("恭喜你，率先到达了终点");
                }
                else
                {
                    Console.Write("很遗憾，电脑率先到达了终点");
                }
                Console.SetCursorPosition(2, h - 3);
                Console.Write("请按任意键结束游戏");
                return true;
            }
            else
            {
                //获取格子的类型
                Grid grid = map.grid[p.nowIndex];


                //判断当前对象到了什么格子
                switch (grid.type) 
                {
                    case E_Grid_Type.Normal:
                        //不作处理，打印信息
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}到达一个安全位置" , p.type == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayerType.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.Boom:
                        //炸弹,后退五格
                        p.nowIndex -= 5;
                        if (p.nowIndex < 0)
                        {
                            p.nowIndex = 0;
                        }
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}踩到了炸弹，退后5格", p.type == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayerType.Player ? "电脑" : "你");
                        break;
                    case E_Grid_Type.pause:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}到达了暂停点，{1}需要暂停一回合", p.type == E_PlayerType.Player ? "你" : "电脑", p.type == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayerType.Player ? "电脑" : "你");
                        p.isPause = true;
                        break;
                    case E_Grid_Type.Tunnel:
                        Console.SetCursorPosition(2, h - 4);
                        Console.Write("{0}踩到了时空隧道", p.type == E_PlayerType.Player ? "你" : "电脑");

                        //随机
                        int randomNum2 = r.Next(1, 91);
                        if (randomNum2 <= 30)
                        {
                            //触发倒退
                            p.nowIndex -= 5;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发倒退5格");
                        }else if (randomNum2 <= 60)
                        {
                            //触发暂停
                            p.isPause = true;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("触发暂停一回合");
                        }
                        else
                        {
                            //换位置
                            int t = p.nowIndex;
                            p.nowIndex = otherP.nowIndex;
                            otherP.nowIndex = t;
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("惊喜，惊喜，双方交换位置");
                        }
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键，让{0}开始扔色子", p.type == E_PlayerType.Player ? "电脑" : "你");
                        break;
                }

            }

            


            //默认没有结束
            return false;
        }
        #endregion
    }
}
