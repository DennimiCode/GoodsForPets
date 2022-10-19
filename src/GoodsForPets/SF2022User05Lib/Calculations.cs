using System.Globalization;
using System.Text;

namespace SF2022User05Lib
{
    public class Calculations
    {
        public string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            int iterationCount = Convert.ToInt32((endWorkingTime - beginWorkingTime).TotalMinutes / consultationTime) * 2;
            string[] result = new string[iterationCount];
            TimeSpan currentInterval = beginWorkingTime;
            for (int i = 0, j = 0; i < iterationCount && currentInterval < endWorkingTime; i++)
            {
                StringBuilder stringBuilder = new StringBuilder(currentInterval.ToString("hh\\:mm", new CultureInfo("en-US")));
                if (currentInterval == startTimes[j])
                {
                    currentInterval += new TimeSpan(0, durations[j], 0);
                    if (j != startTimes.Length - 1)
                        j++;
                    continue;
                }
                else if ((currentInterval + new TimeSpan(0, consultationTime, 0)) > startTimes[j] && (endWorkingTime - currentInterval).TotalMinutes > consultationTime)
                {
                    currentInterval = startTimes[j];
                    continue;
                }
                currentInterval += new TimeSpan(0, consultationTime, 0);
                stringBuilder.Append("-");
                stringBuilder.Append(currentInterval.ToString("hh\\:mm", new CultureInfo("en-US")));
                result[i] = stringBuilder.ToString();
            }
            return result;
        }
    }
}