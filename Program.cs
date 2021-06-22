using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace pj4
{
    class Program
    {
        static bool CheckPrime(int number)
        {
            int isPrime = 0;
            for (int y = 1; y < number; y++)
            {
                if (number % y == 0)
                    isPrime++;
                if (isPrime == 2) break;
            }
            if (isPrime != 2)
                return true;
            else
            {
                return false;
            }
        }

        static List<int> GetPrimeNumbers(int inputMaxNumber, int inputMinNumber)
        {
            List<int> primeList = new List<int>();
            for (int x = inputMinNumber; x < inputMaxNumber; x++)
            {
                if (CheckPrime(x) == true)
                {
                    primeList.Add(x);
                }
            }
            return primeList;
        }

        static void Main(string[] args)
        {
            
            Stopwatch stopWatch = new Stopwatch();
            Stopwatch stopWatchTask = new Stopwatch();

            stopWatchTask.Start();
            Console.Write("Please Enter Max Number: ");
            int inputMaxNumber = int.Parse(System.Console.ReadLine());
            Console.Write("Please Enter Min Number: ");
            int inputMinNumber = int.Parse(System.Console.ReadLine());
            int totalLength = inputMaxNumber - inputMinNumber;
            int subrangeLength = totalLength / 5;
            int currentStart = inputMinNumber;
            int[] minSplit = new int[5];
            int[] maxSplit = new int[5];
            for (int i = 0; i < 5; ++i)
            {
                int end = currentStart + subrangeLength;
                minSplit[i] = currentStart;
                maxSplit[i] = end;
                currentStart += subrangeLength+1;
            }
            maxSplit[4]= inputMaxNumber;
            for (int i = 0; i < 5; ++i)
            {
                Console.Write($"[ {minSplit[i]} , {maxSplit[i]} ]");
            }

            List<int> sumCountList = new List<int>();
            Task[] tasks = new Task[5];

            tasks[0] = Task.Run(() =>
            {
                int element = GetPrimeNumbers(maxSplit[0], minSplit[0]).Count();
                sumCountList.Add(element);
            });
            tasks[1] = Task.Run(() =>
            {
                int element = GetPrimeNumbers(maxSplit[1], minSplit[1]).Count();
                sumCountList.Add(element);
            });
            tasks[2] = Task.Run(() =>
            {
                int element = GetPrimeNumbers(maxSplit[2], minSplit[2]).Count();
                sumCountList.Add(element);
            }
            );
            tasks[3] = Task.Run(() =>
            {
                int element = GetPrimeNumbers(maxSplit[3], minSplit[3]).Count();
                sumCountList.Add(element);
            });
            tasks[4] = Task.Run(() =>
            {
                int element = GetPrimeNumbers(maxSplit[4], minSplit[4]).Count();
                sumCountList.Add(element);
            });

            Task.WaitAll(tasks);
            // Console.WriteLine();
            // foreach (int i in sumCountList)
            // {
            //     Console.Write($"Number prime of prime list part is : {i} ");
            // }
            // Console.WriteLine();
            int total = sumCountList.Sum(x => Convert.ToInt32(x));
            Console.WriteLine($"Number prime of prime list mutitask is:{total}");
            stopWatchTask.Stop();
            

            stopWatch.Start();
            Console.WriteLine($"Number prime of prime list is: {GetPrimeNumbers(inputMaxNumber, inputMinNumber).Count()}");
            stopWatch.Stop();

            

            TimeSpan ts = stopWatch.Elapsed;
            TimeSpan tst = stopWatchTask.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00} : Hours : {1:00} : Minutes : {2:00} : Seconds : {3:00} : Milliseconds",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            String elapsedTimeTask = String.Format("{0:00} : Hours : {1:00} : Minutes : {2:00} : Seconds : {3:00} : Milliseconds",
                tst.Hours, tst.Minutes, tst.Seconds,
                tst.Milliseconds / 10);
            
            Console.WriteLine($"RunTimeTask: {elapsedTimeTask}");
            Console.WriteLine($"RunTime: {elapsedTime}");

        }
    }
}
