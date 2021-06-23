using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MoneyToString
{
    class Program
    {
        public static List<string> MainMethodArgs = new List<string>();
        static void Main(string[] args)
        {
            MainMethodArgs = args.ToList();
            Run(MainMethodArgs.ToArray());
        }

        public static void Run(string[] args)
        {
            decimal number;

            Console.WriteLine("Введите число от 0 до 2 147 483 647, разделитель целой и дробной части - запятая:");

            string value = Console.ReadLine();


            if (decimal.TryParse(value, out number))
            {
                try
                {
                    Console.WriteLine(NumberConverter.NumberToWords(number));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
                Console.WriteLine("Ошибка ввода, попробуйте еще раз...");

            Restarter();
        }

        public static void Restarter()
        {
            Console.WriteLine("Нажмите Enter для выхода, или R чтобы попробовать еще раз...");
            string restart = Console.ReadLine();
            if (restart.ToUpper() == "R")
            {
                Run(MainMethodArgs.ToArray());
            }
        }
    }

}
