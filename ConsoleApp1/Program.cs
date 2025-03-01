using System;



namespace MyApp
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int nowSceneID = 1;
            bool isQuitwhile = false;
            bool isQuitEndwhile = false;
            int w = 50;
            int h = 30;
            bool isFight = false;
            bool isOver = false;
            Console.CursorVisible = false;
            char keyboard;
            string gameInfo = "";
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
            while (true)
            {
                switch (nowSceneID)
                {
                    case 1:
                        isQuitwhile = false;
                        Console.Clear();
                        #region 开始场景逻辑
                        Console.SetCursorPosition(w / 2 - 7, 8);
                        Console.Write("白马王子营救公主");
                        int selID = 0;
                        while (true)
                        {
                            Console.SetCursorPosition(w / 2 - 4, 13);
                            Console.ForegroundColor = selID == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("开始游戏");
                            Console.SetCursorPosition(w / 2 - 4, 15);
                            Console.ForegroundColor = selID == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("结束游戏");
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input)
                            {
                                case 'w':
                                case 'W':
                                    selID--;
                                    if (selID < 0)
                                    {
                                        selID = 0;
                                    }
                                    break;
                                case 's':
                                case 'S':
                                    selID++;
                                    if (selID > 1)
                                    {
                                        selID = 1;
                                    }
                                    break;
                                case 'j':
                                case 'J':
                                    if (selID == 0)
                                    {
                                        nowSceneID = 2;
                                        isQuitwhile = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (isQuitwhile == true)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;
                    case 2:
                        isOver = false; // 确保每次进入场景2都重置

                        #region 红墙
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        for (int i = 0; i < w; i += 2)
                        {
                            Console.SetCursorPosition(i, 0);
                            Console.Write("⬛");
                            Console.SetCursorPosition(i, h - 1);
                            Console.Write("⬛");
                            Console.SetCursorPosition(i, h - 6);
                            Console.Write("⬛");
                        }
                        for (int i = 0; i < h; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write("⬛");
                            Console.SetCursorPosition(w - 2, i);
                            Console.Write("⬛");
                        }
                        #endregion
                        #region BOSS属性
                        int bossX = 24;
                        int bossY = 15;
                        int bossAtkMin = 7;
                        int bossAtkMax = 12;
                        int bossHp = 100;
                        string bossIcon = "⬛";
                        ConsoleColor bossColor = ConsoleColor.Green;
                        #endregion
                        #region 玩家属性
                        int playerX = 8;
                        int playerY = 3;
                        int playerAtkMin = 8;
                        int playerAtkMax = 13;
                        int playerHp = 100;
                        string playerIcon = "⬤";
                        ConsoleColor playerColor = ConsoleColor.Yellow;
                        #endregion
                        #region 公主属性
                        int princessX = 24;
                        int princessY = 5;
                        string princessIcon = "★";
                        ConsoleColor princessColor = ConsoleColor.Blue;
                        #endregion
                        while (true)
                        {
                            if (bossHp > 0)
                            {
                                Console.ForegroundColor = bossColor;
                                Console.SetCursorPosition(bossX, bossY);
                                Console.Write(bossIcon);
                            }
                            else
                            {
                                #region 公主显示相关
                                Console.SetCursorPosition(princessX, princessY);
                                Console.ForegroundColor = princessColor;
                                Console.Write(princessIcon);
                                #endregion
                            }

                            //画出玩家
                            Console.ForegroundColor = playerColor;
                            Console.SetCursorPosition(playerX, playerY);
                            Console.Write(playerIcon);
                            keyboard = Console.ReadKey(true).KeyChar;
                            Console.SetCursorPosition(playerX, playerY);
                            Console.Write("  ");

                            if (isFight == false)
                            {
                                #region 玩家移动相关
                                switch (keyboard)
                                {
                                    case 'w':
                                    case 'W':
                                        playerY--;
                                        if (playerY < 1)
                                        {
                                            playerY = 1;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHp > 0)
                                        {
                                            playerY++;
                                        } else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerY++;
                                        }
                                        break;
                                    case 'a':
                                    case 'A':
                                        playerX -= 2;
                                        if (playerX < 2)
                                        {
                                            playerX = 2;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHp > 0)
                                        {
                                            playerX += 2;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerX+=2;
                                        }
                                        break;
                                    case 's':
                                    case 'S':
                                        playerY++;
                                        if (playerY > h - 7)
                                        {
                                            playerY = h - 7;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHp > 0)
                                        {
                                            playerY--;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerY--;
                                        }
                                        break;
                                    case 'd':
                                    case 'D':
                                        playerX += 2;
                                        if (playerX > w - 4)
                                        {
                                            playerX = w - 4;
                                        }
                                        else if (playerX == bossX && playerY == bossY && bossHp > 0)
                                        {
                                            playerX -= 2;
                                        }
                                        else if (playerX == princessX && playerY == princessY && bossHp <= 0)
                                        {
                                            playerX -= 2;
                                        }
                                        break;
                                    case 'j':
                                    case 'J':
                                        if ((playerX == bossX - 2 && playerY == bossY ||
                                            playerX == bossX + 2 && playerY == bossY ||
                                            playerX == bossX && playerY == bossY + 1 ||
                                            playerX == bossX && playerY == bossY - 1) && bossHp > 0)
                                        {
                                            isFight = true;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("开始和Boss战斗了,按J键继续");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("玩家当前血量为{0}", playerHp);
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write($"Boss当前血量为{bossHp}");
                                        }
                                        else if((playerX == princessX - 2 && playerY == princessY ||
                                            playerX == princessX + 2 && playerY == princessY ||
                                            playerX == princessX && playerY == princessY + 1 ||
                                            playerX == princessX && playerY == princessY - 1) && bossHp<= 0)
                                        {
                                            nowSceneID = 3;
                                            isOver = true;
                                            break;
                                        }
                                       
                                        break;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 战斗相关
                                //开始战斗了
                                if (keyboard == 'j' || keyboard == 'J')
                                {
                                    if (bossHp < 0)
                                    {
                                        isFight = false;
                                    }
                                    else if (playerHp < 0) {
                                        nowSceneID = 3;
                                        break;
                                    }
                                    else
                                    {
                                        //玩家打boss
                                        Random r = new Random();
                                        int atk = r.Next(playerAtkMin, playerAtkMax);
                                        bossHp -= atk;
                                        //玩家打boss
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("                                              ");
                                        Console.SetCursorPosition(2, h - 4);
                                        Console.Write("你对boss造成了{0}点伤害,boss剩余血量为{1}", atk, bossHp);
                                        if (bossHp > 0)
                                        {
                                            //boss打玩家
                                            atk = r.Next(bossAtkMin, bossAtkMax);
                                            playerHp -= atk;
                                            if (playerHp <= 0)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("                                           ");
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("很遗憾，你未能将boss消灭，战败了");
                                                gameInfo = "游戏失败";
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("                                           ");
                                                Console.SetCursorPosition(2, h - 3);
                                                Console.Write("boss对你造成了{0}点伤害,你的剩余血量为{1}", atk, playerHp);
                                            }
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("                                           ");
                                            Console.SetCursorPosition(2, h - 5);
                                            Console.Write("你战胜了boss，快去营救公主");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("                                           ");
                                            Console.SetCursorPosition(2, h - 4);
                                            Console.Write("前往公主身边按J键继续");
                                            Console.SetCursorPosition(2, h - 3);
                                            Console.Write("                                           ");
                                            Console.ForegroundColor = bossColor;
                                            Console.SetCursorPosition(bossX, bossY);
                                            Console.Write("  ");
                                            gameInfo="游戏成功";
                                        }
                                    }
                                }

                                #endregion
                            }
                            if (isOver)
                            {
                                break;
                            }
                        }
                break; 
                    case 3:
                        isQuitEndwhile = false;
                        Console.Clear();
                        Console.SetCursorPosition(w / 2 - 4, 5);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("GameOver");

                        Console.SetCursorPosition(w / 2 - 4, 7);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(gameInfo);

                        int selID2 = 0;
                        
                        while (true)
                        {
                            Console.SetCursorPosition(w / 2 - 6, 9);
                            Console.ForegroundColor = selID2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("回到开始界面");
                            Console.SetCursorPosition(w / 2 - 4, 11);
                            Console.ForegroundColor = selID2 == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("结束游戏");
                            char input = Console.ReadKey(true).KeyChar;
                            switch (input)
                            {
                                case 'w':
                                case 'W':
                                    selID2--;
                                    if (selID2 < 0)
                                    {
                                        selID2 = 0;
                                    }
                                    break;
                                case 's':
                                case 'S':
                                    selID2++;
                                    if (selID2 > 1)
                                    {
                                        selID2 = 1;
                                    }
                                    break;
                                case 'j':
                                case 'J':
                                    if (selID2 == 0)
                                    {
                                        nowSceneID = 1;
                                        isQuitEndwhile = true;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (isQuitEndwhile == true)
                            {
                                break;
                            }
                        }
                        break;
                }
            }
        }
    }
}