using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using API;

namespace UnitTestProject
{
    [TestClass]
    public class TestAPIsLists
    {
        // TODO TestAPIsLists
        [TestMethod]
        public void Adding()
        {
            var list = new TestingAPIs.ListOfDoubles(0);

            foreach (var item in TestingAPIs.ListOfDoubles.Numbers(10))
                list.Add(item);

            var field = (List<double>)new PrivateObject(list).GetField("list");

            Assert.IsNotNull(field);
        }

        [TestMethod]
        public void Count()
        {
            var list = new TestingAPIs.ListOfDoubles(10);
            if (list.Count != 10)
                throw new AssertFailedException("Count does not return correct number of items in list");
        }

        [TestMethod]
        public void Find()
        {
            double[] numbers;
            var list = new TestingAPIs.ListOfDoubles(10, out numbers);

            if (!list.Find(numbers[5]))
                throw new AssertFailedException("Can't find number in list");

            var field = (List<double>)new PrivateObject(list).GetField("list");
            Assert.AreEqual(numbers[5], field[5]);
        }

        [TestMethod]
        public void Removing()
        {
            double[] numbers;
            var list = new TestingAPIs.ListOfDoubles(10, out numbers);

            list.Remove(numbers[5]);
            var field = (List<double>)new PrivateObject(list).GetField("list");

            Assert.AreEqual(9, field.Count);
            
            int index = 0;
            for (int i = 0; i < field.Count; i++)
            {
                if (i != 5)
                {
                    if (field[i] != numbers[index])
                    {
                        throw new AssertFailedException("Error when removing");
                    }

                    index++;
                }
                else
                {
                    index += 2;
                }
            }
        }

        [TestMethod]
        public void RemoveAt()
        {
            double[] numbers;
            TestingAPIs.ListOfDoubles list;

            bool ok;
            do
            {
                list = new TestingAPIs.ListOfDoubles(10, out numbers);

                ok = true;
                for (int i = 0; i < list.Count; i++)
                    if (i != 5)
                        if (numbers[i] == numbers[5])
                            ok = false;
            } while (!ok);

            list.RemoveAt(5);

            var field = (List<double>)new PrivateObject(list).GetField("list");
            Assert.AreEqual(9, field.Count);

            for (int i = 0; i < field.Count; i++)
                Assert.AreNotEqual(numbers[5], field[i]);
        }

        [TestMethod]
        public void Clear()
        {
            var list = new TestingAPIs.ListOfDoubles(10);
            list.Clear();

            var field = (List<double>)new PrivateObject(list).GetField("list");
            if (field.Count > 0)
                throw new AssertFailedException("The list is not clear");
        }

        [TestMethod]
        public void Update()
        {
            var list = new TestingAPIs.ListOfDoubles(10);
            bool ok = list.Update(489.56145D, 5);
            if (!ok)
                throw new AssertFailedException("List has not been updated");

            var field = (List<double>)new PrivateObject(list).GetField("list");
            Assert.AreEqual(489.56145D, field[5]);
        }

        [TestMethod]
        public void GetAt()
        {
            double[] numbers;
            var list = new TestingAPIs.ListOfDoubles(10, out numbers);

            Assert.AreEqual(numbers[5], list.GetAt(5));
        }
    }

    #region only for testing
    public class TestingAPIs
    {
        public class ListOfDoubles : API.Lists<double>
        {
            public ListOfDoubles(int size) 
                : base()
            {
                foreach (var item in Numbers(size))
                    this.Add(item);
            }

            public ListOfDoubles(int size, out double[] numbers) 
                : base()
            {
                numbers = Numbers(size);

                foreach (var item in numbers)
                    this.list.Add(item);
            }

            public static double[] Numbers(int size)
            {
                var rd = new Random(DateTime.Now.Second);
                double[] temp = new double[size];

                for (int i = 0; i < size; i++)
                    temp[i] = rd.NextDouble() * rd.Next(1, 1000);

                return temp;
            }
        }
    }
    #endregion
}
