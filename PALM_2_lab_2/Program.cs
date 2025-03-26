using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PALM_2_lab_2
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            int[][] jagged = null;
            int choice;
            do
            {
                Console.WriteLine("Для створення (нової) матриці введіть 1");
                Console.WriteLine("Для виконання блоку 1 (Варіант 11) введіть 2");
                Console.WriteLine("Для виконання блоку 2 (Варіант 11) введіть 3");
                Console.WriteLine("Для виконання блоку 3 (варіант 8) введіть 4");
                Console.WriteLine("Для виконання блоку 4 (варіант 9) введіть 5");
                Console.WriteLine("Для виходу з програми введіть 0");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        jagged = Matrix();
                        break;
                    case 2:
                        Console.WriteLine("Блок 1 (Підрахувати кількість від’ємних елементів матриці.)");
                        Block_1(jagged);
                        break;
                    case 3:
                        Console.WriteLine("Блок 2 (Обміняти місцями відповідні елементи першого (технічно 0-го) рядка і головної діагоналі.)");
                        Block_2(jagged);
                        break;
                    case 4:
                        Console.WriteLine("Блок 3 (Упорядкувати за неспаданням рядок з максимальним елементом матриці.)");
                        Block_3(jagged);
                        break;
                    case 5:
                        Console.WriteLine("Блок 4 (Упорядкувати стовпчики матриці за неспаданням сум елементів у цих стовпчиках.)");
                        Block_4(jagged);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Команда ``{0}'' не розпізнана. Зробіть, будь ласка, вибір із 1, 2, 3, 4, 5, 0.", choice);
                        break;
                }
            } while (choice != 0);
        }
        public static void Block_1(int[][] jagged)
        {
            int cnt = 0;

            foreach (int[] arr in jagged)
            {
                foreach (int i in arr)
                {
                    if (i < 0)
                        cnt++;
                }
            }

            Console.WriteLine($"Кількість від'ємних елементів в матриці: {cnt}");
        }
        public static void Block_2(int[][] jagged)
        {
            bool first = true;
            int i = 0;
            foreach (int[] arr in jagged)
            {
                // пропущення першого рядка, так як елемент вже на своєму місці
                if (first)
                {
                    first = false;
                    continue;
                }
                // заміна перших елементів з головною діагоналлю
                int temp = arr[0];
                arr[0] = arr[i + 1];
                arr[i + 1] = temp;
                i++;
            }

            foreach (int[] arr in jagged)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
        }
        public static void Block_3(int[][] jagged)
        {
            int biggest = int.MinValue;
            // Знаходження найбільшого елемента в матриці
            foreach (int[] arr in jagged)
            {
                foreach (int i in arr)
                {
                    if (biggest < i)
                        biggest = i;
                }
            }

            List<int> rowsWithBiggest = new List<int>();
            // Якщо масив і в зубчатому масиві містить найбільший елемент, масив додається в лист
            for (int i = 0; i < jagged.Length; i++)
                if (jagged[i].Contains(biggest))
                    rowsWithBiggest.Add(i);

            foreach (int i in rowsWithBiggest)
            {
                // Сортування кожного масиву з найбільшим елементом методом вставками
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    int key = jagged[i][j];
                    int k = j - 1;
                    while (k >= 0 && jagged[i][k] > key)
                    {
                        jagged[i][k + 1] = jagged[i][k];
                        k = k - 1;
                    }
                    jagged[i][k + 1] = key;
                }
            }

            foreach (int[] arr in jagged)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
        }
        public static void Block_4(int[][] jagged)
        {
            int[] coloumnSum = new int[jagged[0].Length];

            // знаходження сум всіх ствопчиків
            for (int i = 0; i < coloumnSum.Length; i++)
            {
                for (int j = 0; j < jagged.Length; j++)
                {
                    coloumnSum[i] += jagged[j][i];
                }
            }
            Console.WriteLine(string.Join(" ", coloumnSum));

            // сортування стовпчиків методом вставками
            for (int i = 1; i < coloumnSum.Length; i++)
            {
                int key = coloumnSum[i];
                int j = i - 1;
                while (j >= 0 && coloumnSum[j] > key)
                {
                    for (int k = 0; k < jagged.Length; k++)
                    {
                        int temp = jagged[k][j];
                        jagged[k][j] = jagged[k][j + 1];
                        jagged[k][j + 1] = temp;
                    }
                    coloumnSum[j + 1] = key;
                    j = j - 1;
                }
            }

            foreach (int[] arr in jagged)
            {
                Console.WriteLine(string.Join(" ", arr));
            }
        }
        static int[][] Matrix()
        {
            Console.Write("Введіть кількість рядків в матриці: ");
            int n = int.Parse(Console.ReadLine());

            int[][] jagged = new int[n][];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Введіть масив елементів на рядку {i + 1}: ");
                jagged[i] = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            }

            Console.WriteLine("Ваша матриця: ");
            foreach (int[] arr in jagged)
            {
                Console.WriteLine(string.Join(" ", arr));
            }

            return jagged;
        }
    }
}
