namespace SF2022User05Lib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void method1()
        {
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0), new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60, 30, 10, 10, 40};
            TimeSpan beginWorkingTime = new TimeSpan(8,0,0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            string[] result = calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // assert            
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
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected[2], result[2]);
            Assert.AreEqual(expected[3], result[3]);
            Assert.AreEqual(expected[4], result[4]);
            Assert.AreEqual(expected[5], result[5]);
            Assert.AreEqual(expected[6], result[6]);
            Assert.AreEqual(expected[7], result[7]);
            Assert.AreEqual(expected[8], result[8]);
            Assert.AreEqual(expected[9], result[9]);
            Assert.AreEqual(expected[10], result[10]);
            Assert.AreEqual(expected[11], result[11]);
            Assert.AreEqual(expected[12], result[12]);
            Assert.AreEqual(expected[13], result[13]);

        }
        [TestMethod]
        public void method2()
        {
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = {new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60,50 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<InvalidOperationException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
        [TestMethod]
        public void method4()
        {
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60,};
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            // assert            
            Assert.ThrowsException<InvalidOperationException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
        [TestMethod]
        public void method7()
        {
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
            // arrange 
            var calc = new Calculations();
            TimeSpan[] startTimes = { new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), new TimeSpan(16, 50, 0), };
            int[] durations = { 60,};
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            // act
            string[] result = calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            // assert            
            Assert.ThrowsException<InvalidOperationException>(() => calc.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
        [TestMethod]
        public void method8()
        {
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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
            //TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime
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