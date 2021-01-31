using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThesisApp.Models;

namespace ThesisApp.BenchmarkClasses
{
    /// <summary>
    /// This class is used to implement the logic of the Prime Numbers test
    /// </summary>
    public static class PrimeNumbersTest
    {
        //Execute Prime Numbers test in synchronous mode
        public static void StartSync(IProgress<PNumTestReportModel> progress, int range, int nthPos)
        {
            //Create an instance of the report model object which will be later sent in progress report
            PNumTestReportModel report = new PNumTestReportModel();

            long RangeResult = PrimeNumbersRange(range);
            long NthPosResult = NthPrimeNumber(nthPos);

            //Report the changes to the UI
            report.NthPNumberResult = NthPosResult;
            report.PNumberRangeResult = RangeResult;
            report.ProgressArcAngle = 360;
            progress.Report(report);
        }

        //Execute Prime Numbers test in synchronous mode
        public static async Task StartAsync(IProgress<PNumTestReportModel> progress, int range, int nthPos, CancellationToken cancellationToken)
        {
            //Create an instance of the report model object which will be later sent in progress report
            PNumTestReportModel report = new PNumTestReportModel();

            Task<long> rangeTask = Task.Run(() => {return PrimeNumbersRangeAsync(range, ref progress, ref report, ref cancellationToken); });
            long RangeResult = await rangeTask;
            cancellationToken.ThrowIfCancellationRequested();
            report.PNumberRangeResult = RangeResult;
            report.ProgressArcAngle = 180;
            progress.Report(report);

            Task<long> nthTask = Task.Run(() => { return NthPrimeNumberAsync(nthPos, ref progress, ref report, ref cancellationToken); });
            long NthPosResult = await nthTask;
            cancellationToken.ThrowIfCancellationRequested();
            report.ProgressArcAngle = 360;
            report.NthPNumberResult = NthPosResult;
            progress.Report(report);
        }

        //This method is used to find how many prime numbers there are in the given range
        private static long PrimeNumbersRange(int range)
        {
            long result = 0;
            for (int i = 2; i < range; i++)
            {
                bool isPrime = true;
                long a = 2;
                while (a * a <= i)
                {
                    if (i % a == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    a++;
                }
                if (isPrime)
                    result++;
            }
            return result;
        }

        /// <summary>
        /// This method does the same as <see cref="PrimeNumbersRange(int)"/> but also reports progress
        /// </summary>
        private static long PrimeNumbersRangeAsync(int range, ref IProgress<PNumTestReportModel> progress, ref PNumTestReportModel report, ref CancellationToken cancellationToken)
        {
            long result = 0;
            for (int i = 2; i < range; i++)
            {
                bool isPrime = true;
                long a = 2;
                while (a * a <= i)
                {
                    if (i % a == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    a++;
                }
                if (isPrime)
                    result++;

                //report progress to the UI
                if (i % (range / 3) == 0 && i > 0 || i == range - 1)
                {
                    report.ProgressArcAngle += 45;
                    progress.Report(report);
                }

                if (cancellationToken.IsCancellationRequested)
                    break;
            }
            return result;
        }

        //This method is used to find the Nth prime number
        private static long NthPrimeNumber(int NthPos)
        {
            int count = 0;
            int result = 2;
            while (count < NthPos)
            {
                long a = 2;
                bool isPrime = true;

                while (a * a <= result)
                {
                    if (result % a == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    a++;
                }
                if (isPrime)
                    count++;
                result++;
            }
            return --result;
        }

        /// <summary>
        /// This method does the same as <see cref="NthPrimeNumber(int)"/> but also reports progress
        /// </summary>
        private static long NthPrimeNumberAsync(int NthPos, ref IProgress<PNumTestReportModel> progress, ref PNumTestReportModel report, ref CancellationToken cancellationToken)
        {
            int count = 0;
            int result = 2;
            int curReportIndex = 0;
            while (count < NthPos)
            {
                long a = 2;
                bool isPrime = true;

                while (a * a <= result)
                {
                    if (result % a == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    a++;
                }
                if (isPrime)
                    count++;

                result++;

                //report progress to the UI
                if (count % (NthPos / 5) == 0 && count > 0 && count != curReportIndex)
                {
                    curReportIndex = count;
                    report.ProgressArcAngle += 30;
                    progress.Report(report);
                }

                if (cancellationToken.IsCancellationRequested)
                    break;
            }
            return --result;
        }
    }
}
