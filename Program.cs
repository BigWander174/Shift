using System;
using System.Collections.Generic;
using System.Linq;

namespace Shift
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();

            var numbers = input.Split(new string[] {">>", "<<"}, StringSplitOptions.None);

            var rule = GetRule(GetShift(input, numbers));

            var binaryNumber = AddZerosToStartAndEnd(ConvertToBinary(int.Parse(numbers[0])));

            var result = UseCellMachine(binaryNumber, int.Parse(numbers[1]), rule).Select(element => element.Substring(1, element.Length - 2));

            foreach (var line in result)
            {
                Console.WriteLine(line);
            }
        }

        private static List<string> UseCellMachine(string binaryNumber, int numberOfIterations, string rule)
        {
            List<string> output = new List<string>() 
            {
                binaryNumber
            };
            for (int i = 0; i < numberOfIterations; i++)
            {
                output.Add(CalculateNewLine(output[i], rule));
            }

            return output;
        }

        private static string CalculateNewLine(string binaryNumber, string rule)
        {
            string result = "";
            for (int i = 1; i < binaryNumber.Length - 1; i++)
            {
                var a = int.Parse(binaryNumber[i - 1].ToString());
                var b = int.Parse(binaryNumber[i].ToString());
                var c = int.Parse(binaryNumber[i + 1].ToString());

                int index = a * 4 + b * 2 + c;
                result += rule[index];
            }

            return AddZerosToStartAndEnd(result);
        }

        private static string AddZerosToStartAndEnd(string v)
        {
            return "0" + v + "0";
        }

        private static string ConvertToBinary(int number)
        {
            if (number < 2)
            {
                return number.ToString();
            }

            return (number % 2).ToString() + ConvertToBinary(number / 2);
        }

        private static string GetRule(string input)
        {
            return (input == ">>") ? "00001111" : "01010101";
        }

        private static string GetShift(string input, string[] numbers)
        {
            foreach (var number in numbers)
            {
                input = input.Replace(number, String.Empty);
            }

            return input;
        }
    }
}
