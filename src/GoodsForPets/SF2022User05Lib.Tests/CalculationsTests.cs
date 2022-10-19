namespace SF2022User05Lib.Tests
{
    [TestClass]
    public class CalculationsTests
    {
        [TestMethod]
        public void method1()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0), new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60, 30, 10, 10, 40};
            TimeSpan beginWorkingTime = new TimeSpan(8,0,0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] expected =
{
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00"
            };
            // act
            string[] result = calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // assert            
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], result[i]);

        }

        [TestMethod]
        public void method2()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0), new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method3()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = {new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60,50 };
            TimeSpan beginWorkingTime = new TimeSpan(-8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(-18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<OverflowException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method4()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = {  };
            int[] durations = { 60, 50 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method5()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = {  };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method6()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60,};
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = -30;
            // act
            // assert            
            Assert.ThrowsException<OverflowException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method7()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { };
            int[] durations = { };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method8()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(15, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { -5, };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method9()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 1,2 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod]
        public void method10()
        {
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 3 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<IndexOutOfRangeException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
    }
}