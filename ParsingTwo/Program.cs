using System;
using System.Data;

namespace ConsoleApplication53
{
    // описание дерева
    class BinTreeNode
    {
        public BinTreeNode() { }                    // бесполезный конструктор
        public BinTreeNode(string rac)
        {
            Info = rac;
            Left = null;
            Right = null;
        }           // конструктор из строки
        public string Info { get; set; }            // операцмя в корне либо значение в крайней ветке 
        public BinTreeNode Left { get; set; }       // левая ветвь
        public BinTreeNode Right { get; set; }      // правая ветвь
    }

    class Program
    {
        static char scanSimbol;                        // символ в обработке
        static int sn;
        static string ExprStr;                         // строка выражения
        static bool error = false;                     // обнаружена ли ошибка во вводе

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
        }             // обработка выражения
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
        }                 // обр-ка слагаемого
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
        }                 // множителя
        static bool Letter()
        {
            return char.IsLetter(scanSimbol);
        }                        // если буква
        static void Number(BinTreeNode tree)
        {
            while (Digit())
            {
                tree.Info += scanSimbol.ToString();
                NextSimbol();
            }
        }        // чтение числа
        static bool Digit()
        {
            return char.IsDigit(scanSimbol);
        }                         // проверка, является ли чисом
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
        }                    // переход к обработке следующего символа
        static void Error(string msg)
        {
            error = true;
            Console.WriteLine(msg);
        }               // ошибка
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

        }    // чистка лишних веток

        static void Main(string[] args)
        {
            string ans = "";

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
            } else {
                if (error) { Console.ReadKey(); return; }
                Console.WriteLine("Хорошая строка");
                sn = 0;
                scanSimbol = ExprStr[sn];
                tree = Expression();
                for (int i = 0; i < 10; i++)
                    tree = Clean(tree);
                try
                {
                    Console.WriteLine("Результат " + ans);
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
