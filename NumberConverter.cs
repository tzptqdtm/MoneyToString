using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyToString
{
    public static class NumberConverter
    {
        /// <summary>
        /// Метод возвращает дробное число в виде строки с указанием валюты
        /// </summary>
        /// <param name="number">Положительное число от 0 до 2,147,483,647 с дробной частью</param>
        /// <returns>Представление числа в виде строки с указанием валюты (Dollar)</returns>
        public static string NumberToWords(decimal number)
        {

            if (number < 0 || number > Int32.MaxValue)
            {
                throw new ArgumentOutOfRangeException("number", "Число не попадает в диапазон 0 - " + Int32.MaxValue);
            }

            int dollars = Decimal.ToInt32(number);
            int cents = Decimal.ToInt32((number - dollars) * 100);

            var d = dollars != 1 ? " DOLLARS AND " : " DOLLAR AND ";

            var c = cents != 1 ? " CENTS" : " CENT";

            return NumberToWords(dollars) + d + NumberToWords(cents) + c;
        }
        /// <summary>
        /// Метод возвращает целое число в виде строки
        /// </summary>
        /// <param name="num">Положительное число от 0 до 2,147,483,647</param>
        /// <returns>Представление целого числа в виде строки</returns>
        public static string NumberToWords(int num)
        {
            if (num == 0)
            {
                return "Zero";
            }
            string result = "";
            int toProcess = num;
            int magnitude = 0;
            while (toProcess > 0)
            {
                string chunkWord = ChunkToWord(toProcess % 1000);
                string magWord = AmountToWord(magnitude);
                if (!string.IsNullOrWhiteSpace(chunkWord))
                {
                    result = chunkWord + magWord + result;
                }

                toProcess /= 1000;
                magnitude++;
            }
            return result.Trim();
        }

        private static string ChunkToWord(int chunk)
        {
            StringBuilder sb = new StringBuilder();
            int unprocessed = chunk;
            if (unprocessed > 99)
            {
                sb.Append(DigitToWord(unprocessed / 100));
                sb.Append(" Hundred");
                unprocessed %= 100;
                if (unprocessed > 0)
                {
                    sb.Append(" and");
                }
            }
            if (unprocessed is >= 10 and < 20)
            {
                sb.Append(TeensToWord(unprocessed));
            }
            else
            {
                sb.Append(TensToWord(unprocessed / 10));
                sb.Append(DigitToWord(unprocessed % 10));
            }
            return sb.ToString();
        }

        private static string DigitToWord(int digit)
        {
            return digit switch
            {
                9 => " Nine",
                8 => " Eight",
                7 => " Seven",
                6 => " Six",
                5 => " Five",
                4 => " Four",
                3 => " Three",
                2 => " Two",
                1 => " One",
                _ => ""
            };
        }

        private static string TensToWord(int tens)
        {
            return tens switch
            {
                9 => " Ninety",
                8 => " Eighty",
                7 => " Seventy",
                6 => " Sixty",
                5 => " Fifty",
                4 => " Forty",
                3 => " Thirty",
                2 => " Twenty",
                _ => ""
            };
        }

        private static string TeensToWord(int teens)
        {
            return teens switch
            {
                19 => " Nineteen",
                18 => " Eighteen",
                17 => " Seventeen",
                16 => " Sixteen",
                15 => " Fifteen",
                14 => " Fourteen",
                13 => " Thirteen",
                12 => " Twelve",
                11 => " Eleven",
                10 => " Ten",
                _ => ""
            };
        }

        private static string AmountToWord(int am)
        {
            return am switch
            {
                1 => " Thousand,",
                2 => " Million,",
                3 => " Billion,",
                4 => " Trillion,",
                5 => " Quadrillion,",
                0 => "",
                _ => ""
            };
        }
    }
}
