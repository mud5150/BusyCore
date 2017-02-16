using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BusyCore
{
	class Program
	{
		static void Main(string[] args)
		{

			if (args.Length == 1)
			{
				var threadCount = new Int32();
				if (Int32.TryParse(args[0], out threadCount))
				{
					var tasks = new List<Task>();

					var totalStopwatch = new Stopwatch();
					totalStopwatch.Start();

					for (int i = 0; i < threadCount; i++)
					{
						tasks.Add(Task.Factory.StartNew(() =>
						{
							Work.DoWork();
						}));
					}
					Task.WaitAll(tasks.ToArray());
					totalStopwatch.Stop();

					Console.WriteLine(
						String.Format(
							"Total elapsed time is {0} seconds", totalStopwatch.Elapsed.TotalSeconds	
							)
						);
				}
				else
				{
					Console.WriteLine("Bad argument");
				}
			}

			else
			{
				Work.DoWork();
			}
			


			Console.ReadLine();
		}
	}

	class Work
	{

		public static void DoWork()
		{
			Console.WriteLine(
				String.Format("Doing work on thread {0}", Thread.CurrentThread.ManagedThreadId)
				);

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			long nthPrime = FindPrimeNumber(400000); //400000 takes about 10 seconds on my laptop

			stopWatch.Stop();
			var elapsed = stopWatch.Elapsed;

			Console.WriteLine(
				String.Format(
					"Thread {0} completed in {1} seconds", Thread.CurrentThread.ManagedThreadId, elapsed.TotalSeconds
					)
				);
		}

		private static long FindPrimeNumber(int n)
		{
			int count = 0;
			long a = 2;
			while (count < n)
			{
				long b = 2;
				int prime = 1;// to check if found a prime
				while (b * b <= a)
				{
					if (a % b == 0)
					{
						prime = 0;
						break;
					}
					b++;
				}
				if (prime > 0)
				{
					count++;
				}
				a++;
			}
			return (--a);
		}
	}
}
