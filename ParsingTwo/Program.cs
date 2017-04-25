using System;
using System.Data;

namespace ConsoleApplication53
{
    class BinTreeNode
    {
        public BinTreeNode()
        { }
        public BinTreeNode(string rac)
        {
            Info = rac;
            Left = null;
            Right = null;
        }
        public string Info { get; set; }
        public BinTreeNode Left { get; set; }
        public BinTreeNode Right { get; set; }
    }

    class Program
    {
        static char scanSimbol;
        static int sn;
        static string ExprStr;
        static bool error = false;

        static BinTreeNode Expression()
        {
            BinTreeNode tree = new BinTreeNode();
            tree.Left = Addend();
            while (scanSimbol == '+' || scanSimbol == '-')
            {
                BinTreeNode poow = new BinTreeNode();
                poow.Info = scanSimbol.ToString();
                poow.Left = tree;
                NextSimbol();
                poow.Right = Addend();
                tree = poow;
            }
            return tree;
        }

        static BinTreeNode Addend()
        {
            BinTreeNode tree = new BinTreeNode();
            tree.Left = Factor();
            while (scanSimbol == '*' || scanSimbol == '/')
            {
                BinTreeNode poow = new BinTreeNode();
                poow.Info = scanSimbol.ToString();
                poow.Left = tree;
                NextSimbol();
                poow.Right = Factor();
                tree = poow;
            }
            return tree;
        }

        static BinTreeNode Factor()
        {
            BinTreeNode tree = new BinTreeNode();
            if (scanSimbol == '-')
            {
                tree.Info = "-";
                NextSimbol();
            }
            if (Letter())
            {
                tree.Info += scanSimbol.ToString();
                NextSimbol();
            }
            else if (Digit()) Number(tree);
            else if (scanSimbol == '(')
            {
                NextSimbol();
                tree.Left = Expression();

                if (scanSimbol == ')') NextSimbol();
                else Error("Не хаватает закрывающей скобочки");
            }
            else Error("Ошибка после арифметического знака");
            return tree;
        }


        static bool Letter()
        {
            return char.IsLetter(scanSimbol);
        }

        static void Number(BinTreeNode tree)
        {
            while (Digit())
            {
                tree.Info += scanSimbol.ToString();
                NextSimbol();
            }
        }

        static bool Digit()
        {
            return char.IsDigit(scanSimbol);
        }

        static void NextSimbol()
        {
            if (sn < ExprStr.Length - 1)
            {
                sn++;
                scanSimbol = ExprStr[sn];
            }
            else
            {
                scanSimbol = '#';
            }
        }

        static void Error(string msg)
        {
            error = true;
            Console.WriteLine(msg);
        }

        static BinTreeNode Clean(BinTreeNode dl)
        {
            while (string.IsNullOrEmpty(dl.Info) && dl.Right == null)
            {
                dl = dl.Left;
            }
            if (dl.Left != null)
            {
                dl.Left = Clean(dl.Left);
            }


            while (string.IsNullOrEmpty(dl.Info) && dl.Left == null)
            {
                dl = dl.Right;
            }
            if (dl.Right != null)
            {
                dl.Right = Clean(dl.Right);
            }
            return dl;

        }

        static int cat(BinTreeNode der)
        {
            if (der?.Info == null) return 0;
            if (der.Info == "-" && der.Right == null) return -cat(der.Right);
            if ("+-*/".IndexOf(der.Info) >= 0)
            {
                switch (der.Info)
                {
                    case "+":
                        {
                            return cat(der.Left) + cat(der.Right);
                        }
                    case "-":
                        {
                            return cat(der.Left) - cat(der.Right);
                        }
                    case "*":
                        {
                            return cat(der.Left) * cat(der.Right);
                        }
                    case "/":
                        {
                            return cat(der.Left) / cat(der.Right);
                        }
                }
            }
            return int.Parse(der.Info);

        }



        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку");
            ExprStr = Console.ReadLine();
            while (ExprStr.IndexOf("  ") >= 0)
            {
                ExprStr = ExprStr.Replace("  ", " ");
            }
            ExprStr += "#";
            sn = 0;
            scanSimbol = ExprStr[sn];
            BinTreeNode tree = Expression();
            if (sn < ExprStr.Length - 1 || scanSimbol != '#')
            {
                Error("Множитель не распознается");
            }
            else
            {
                if (error)
                {
                    Console.ReadKey(); return;
                }
                Console.WriteLine("Хорошая строка");
                for (int i = 0; i < ExprStr.Length; i++)
                {
                    if (char.IsLetter(ExprStr[i]))
                    {
                        int rr;
                        bool ok;
                        do
                        {
                            Console.Write("Введите значение переменной {0} =", ExprStr[i]);
                            ok = int.TryParse(Console.ReadLine(), out rr);
                            if (!ok) Console.WriteLine("Неверный ввод");
                        } while (!ok);
                        ExprStr = ExprStr.Replace(ExprStr[i].ToString(), rr.ToString());
                    }
                }
                sn = 0;
                scanSimbol = ExprStr[sn];
                tree = Expression();
                for (int i = 0; i < 10; i++)
                    tree = Clean(tree);
                try
                {
                    Console.WriteLine("Результат " + cat(tree));
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Невозможно вычислить");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Слишком большое число");
                }
            }
            Console.ReadKey();
        }
    }
}
