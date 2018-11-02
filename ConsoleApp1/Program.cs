using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConsoleApp1
{
    class Vec2
    {
        public Vec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public int x;
        public int y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            const int row = 4;
            const int col = 4;
            int[,] array = new int[row, col];
            var p1 = RandomGetPosition(array);
            array[p1.x, p1.y] = 2;
            var p2 = RandomGetPosition(array);
            array[p2.x, p2.y] = 2;
            showLayout(array);
            //监听操作
            while (true)
            {
                var key = Console.ReadKey();
                int[,] arrayClone = new int[row, col];
                for (int i = 0; i < arrayClone.GetLength(0); i++)
                {
                    for (int j = 0; j < arrayClone.GetLength(1); j++)
                    {
                        arrayClone[i, j] = array[i, j];
                    }
                }
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        for (int j = 0; j < array.GetLength(1); j++)
                        {
                            List<Vec2> res = new List<Vec2>();
                            for (int k = 0; k < array.GetLength(0); k++)
                            {
                                res.Add(new Vec2(k, j));
                            }

                            int[] t = new int[row];
                            for (int i = 0; i < res.Count; i++)
                            {
                                t[i] = array[res[i].x, res[i].y];
                            }
                            HandlerLogic(t);
                            for (int i = 0; i < res.Count; i++)
                            {
                                array[res[i].x, res[i].y] = t[i];
                            }
                        }
                    
                        break;
                    case ConsoleKey.RightArrow:
                        for (int j = 0; j < array.GetLength(0); j++)
                        {
                            List<Vec2> res = new List<Vec2>();
                            for (int k = array.GetLength(1) - 1; k >= 0; k--)
                            {
                                res.Add(new Vec2(j, k));
                            }

                            int[] t = new int[col];
                            for (int i = 0; i < res.Count; i++)
                            {
                                t[i] = array[res[i].x, res[i].y];
                            }
                            HandlerLogic(t);
                            for (int i = 0; i < res.Count; i++)
                            {
                                array[res[i].x, res[i].y] = t[i];
                            }
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        for (int j = 0; j < array.GetLength(1); j++)
                        {
                            List<Vec2> res = new List<Vec2>();
                            for (int k = array.GetLength(0) - 1; k >= 0; --k)
                            {
                                res.Add(new Vec2(k, j));
                            }

                            int[] t = new int[row];
                            for (int i = 0; i < res.Count; i++)
                            {
                                t[i] = array[res[i].x, res[i].y];
                            }
                            HandlerLogic(t);
                            for (int i = 0; i < res.Count; i++)
                            {
                                array[res[i].x, res[i].y] = t[i];
                            }
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        for (int j = 0; j < array.GetLength(0); j++)
                        {
                            List<Vec2> res = new List<Vec2>();
                            for (int k = 0; k <= array.GetLength(1) - 1; ++k)
                            {
                                res.Add(new Vec2(j, k));
                            }

                            int[] t = new int[col];
                            for (int i = 0; i < res.Count; i++)
                            {
                                t[i] = array[res[i].x, res[i].y];
                            }
                            HandlerLogic(t);
                            for (int i = 0; i < res.Count; i++)
                            {
                                array[res[i].x, res[i].y] = t[i];
                            }
                        }
                        break;
                    default:
                        break;
                }
               
                //判断是否移动
                bool f = true;
                for (int i = 0; i < arrayClone.GetLength(0); i++)
                {
                    for (int j = 0; j < arrayClone.GetLength(1); j++)
                    {
                        if (arrayClone[i,j] != array[i, j])
                        {
                            f = false;
                            break;
                        }
                    }
                }
                if (!f)
                {  //随机出现
                    var vec2 = RandomGetPosition(array);
                    array[vec2.x, vec2.y] = 2;
                }

                //渲染
                showLayout(array);
            }
        }

        public static void HandlerLogic(int[] array)
        {
            //处理前
            HandlerZero(array);
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                {
                    array[i] *= 2;
                    array[i + 1] = 0;
                    break;
                }
            }
            //处理后
            HandlerZero(array);
        }

        public static void HandlerZero(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (array[j] == 0)
                        {
                            array[i] += array[j];
                            array[j] = array[i] - array[j];
                            array[i] -= array[j];
                            break;
                        }
                    }
                }
            }
        }

        public static Vec2 RandomGetPosition(Array array)
        {
            Random random = new Random();
            List<Vec2> canSelect = new List<Vec2>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if ((int)array.GetValue(i, j) == 0)
                    {
                        canSelect.Add(new Vec2(i, j));

                    }
                }
            }

            return canSelect[random.Next(0, canSelect.Count)];
        }

        public static void showLayout(int[,] array)
        {
            Console.Clear();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                    Console.Write('\t');

                }
                Console.WriteLine("");
            }

        }

    }
}