using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using JSpank.Test.Helpers.Extensions;
using System.Linq;
using JSpank.Test.Helpers.Data;
using JSpank.Test.Helpers.Generics;
using System;
using JSpank.Test.Helpers.Validate;
using System.Text.RegularExpressions;

namespace JSpank.Test
{
    [TestClass]
    public class UtilTest : BaseTest
    {
        [TestMethod]
        public void CollectionExtensions()
        {
            var total = 10;
            var result = Db.Ints(total);
            Assert.AreEqual(result.Count(), total);

            var result_rdn = result.GetToRandon<int>(total);
            Assert.AreEqual(result_rdn.Count(), total);

            var result_rdn_one = result.GetToRandonOne<int>();
            Assert.IsTrue(result.Contains(result_rdn_one));
            Assert.IsTrue(result_rdn.Contains(result_rdn_one));
        }

        [TestMethod]
        public void DateNow()
        {
            var dt = DateAndTime.ToOrNow("01/01/2016");
            var dt_ = new DateTime(2016, 01, 01, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Assert.AreEqual(dt.Date, dt_.Date);
            Assert.AreEqual(dt.Hour, dt_.Hour);
            Assert.AreEqual(dt.Minute, dt_.Minute);

            dt = DateAndTime.ToOrNow("01/01/2016 01:02:03");
            dt_ = new DateTime(2016, 01, 01, 01, 02, 03);
            Assert.AreEqual(dt.Date, dt_.Date);
            Assert.AreEqual(dt.Hour, dt_.Hour);
            Assert.AreEqual(dt.Minute, dt_.Minute);
            Assert.AreEqual(dt.Second, dt_.Second);
        }

        [TestMethod]
        public void Age()
        {
            double days;
            var birth = DateTime.Now;
            var to = DateTime.Now.AddDays(1);
            Assert.AreEqual(DateAndTime.Age(birth, to), 0);
            Assert.AreEqual(DateAndTime.Age(birth, to, out days), 0);
            Assert.AreEqual(days, 1);

            to = DateTime.Now.AddYears(1);
            Assert.AreEqual(DateAndTime.Age(birth, to), 1);
            Assert.AreEqual(DateAndTime.Age(birth, to, out days), 1);
            Assert.AreEqual(days, 0);
        }

        [TestMethod]
        public void CNPJIsValid()
        {
            Assert.IsFalse(CNPJValidate.IsValid("123"));
            Assert.IsTrue(CNPJValidate.IsValid("68.013.186/0001-57"));
        }

        [TestMethod]
        public void CPFIsValid()
        {
            Assert.IsFalse(CPFValidate.IsValid("123"));
            Assert.IsTrue(CPFValidate.IsValid("021.480.440-28"));
        }

        [TestMethod]
        public void TryCast()
        {
            object result = null;

            result = TryCast<int?>("1");
            Assert.IsNotNull(result);

            result = TryCast<int>("1");
            Assert.IsNotNull(result);

            result = TryCast<int?>(null);
            Assert.IsNull(result);

            result = TryCast<int>(null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetNumbers()
        {
            var result = ValueValidate.GetNumbers("123456-6");
            Assert.IsTrue(int.Parse(result) > 0);
        }

        [TestMethod]
        public void GetNumbersT_Decimal()
        {
            var result = ValueValidate.GetNumbers<decimal>("2");
            Assert.AreEqual(result, 2);

            result = ValueValidate.GetNumbers<decimal>("-2");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void CPFGenerate_Get()
        {
            var result = CPFGenerate.Get;
            Assert.IsTrue(result > 0);

            var result2 = CPFValidate.IsValid(result.ToString());
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void CNPJGenerate_Get()
        {
            var result = CNPJGenerate.Get;
            Assert.IsTrue(result > 0);

            var result2 = CNPJValidate.IsValid(result.ToString());
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Regex_EqualEqual()
        {
            const string value = "true";

            Assert.IsTrue(Regex.IsMatch(value , "true", RegexOptions.IgnoreCase));
            Assert.IsFalse(Regex.IsMatch(value, "Trues", RegexOptions.IgnoreCase));
            //Assert.IsTrue(Regex.IsMatch(value, "true", RegexOptions.IgnoreCase));           

        }

   
        T TryCast<T>(object obj)
        {
            if (obj is T)
                return (T)obj;
            else if (obj != null)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter.CanConvertFrom(obj.GetType()))
                    return (T)converter.ConvertFrom(obj);
            }

            return default(T);
        }
    }
}
