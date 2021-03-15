using NUnit.Framework;
using System;
using TaxCalculatorInterviewTests;

namespace TaxCalculatorNUnitTest
{
    public class Tests
    {
        TaxCalculator _taxCalculator;

        [SetUp]
        public void Setup()
        {
            _taxCalculator = new TaxCalculator();
        }

        [Test]
        public void UpdateDublicateItemIsSuccess()
        {
            _taxCalculator.CustomRates.Clear();
            _taxCalculator.SetCustomTaxRate(Commodity.Default, 0.25);
            _taxCalculator.SetCustomTaxRate(Commodity.Default, 0.50);
            Assert.IsTrue(_taxCalculator.CustomRates.Count == 1);
        }
        [Test]
        public void GetTaxRateForDateTimeOrReturnStandardIsSuccess()
        {
            _taxCalculator.CustomRates.Clear();
            _taxCalculator.SetCustomTaxRate(Commodity.Default, 0.75);
            _taxCalculator.SetCustomTaxRate(Commodity.Food, 0.50);

            var tr = _taxCalculator.CustomRates[0];

            var expected = _taxCalculator.GetTaxRateForDateTime(Commodity.Default, tr.Item1);

            Assert.AreEqual(0.75, expected);

        }
    }
}