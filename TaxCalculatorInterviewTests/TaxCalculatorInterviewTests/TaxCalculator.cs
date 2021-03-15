using System;
using System.Collections.Generic;
using System.Linq;

//The focus should be on clean, simple and easy to read code 
//Everything but the public interface may be changed
namespace TaxCalculatorInterviewTests
{

    /// <summary>
    /// Implements a tax calculator for our client.
    /// The calculator has a set of standard tax rates that are hard-coded in the class.
    /// It also allows our client to remotely set new, custom tax rates.
    /// Finally, it allows the fetching of tax rate information for a specific commodity and point in time.
    /// TODO: We know there are a few bugs in the code below, since the calculations look messed up every now and then.
    ///       There are also a number of things that have to be implemented.
    /// </summary>
    public class TaxCalculator : ITaxCalculator
    {
        public Dictionary<Commodity, Tuple<DateTime, double>> CustomRates;

        public TaxCalculator()
        {
            CustomRates = new Dictionary<Commodity, Tuple<DateTime, double>>();
        }
        /// <summary>
        /// Get the standard tax rate for a specific commodity.
        /// </summary>
        public double GetStandardTaxRate(Commodity commodity)
        {
            if (commodity == Commodity.Default)
                return 0.25;
            if (commodity == Commodity.Alcohol)
                return 0.25;
            if (commodity == Commodity.Food)
                return 0.12;
            if (commodity == Commodity.FoodServices)
                return 0.12;
            if (commodity == Commodity.Literature)
                return 0.6;
            if (commodity == Commodity.Transport)
                return 0.6;
            if (commodity == Commodity.CulturalServices)
                return 0.6;

            return 0.25;
        }


        /// <summary>
        /// This method allows the client to remotely set new custom tax rates.
        /// When they do, we save the commodity/rate information as well as the UTC timestamp of when it was done.
        /// NOTE: Each instance of this object supports a different set of custom rates, since we run one thread per customer.
        /// </summary>
        public void SetCustomTaxRate(Commodity commodity, double rate)
        {
            if (CustomRates.ContainsKey(commodity))
            {
                CustomRates[commodity] = Tuple.Create(DateTime.Now, rate);
            }
            else
            {
                CustomRates.Add(commodity, Tuple.Create(DateTime.Now, rate));
            }

        }


        /// <summary>
        /// Gets the tax rate that is active for a specific point in time (in UTC).
        /// A custom tax rate is seen as the currently active rate for a period from its starting timestamp until a new custom rate is set.
        /// If there is no custom tax rate for the specified date, use the standard tax rate.
        /// </summary>
        public double GetTaxRateForDateTime(Commodity commodity, DateTime date)
        {
            foreach (var item in CustomRates)
            {
                if (item.Key == commodity && item.Value.Item1 == date)
                {
                    return item.Value.Item2;
                }
            }
            return GetCurrentTaxRate(commodity);
        }

        /// <summary>
        /// Gets the tax rate that is active for the current point in time.
        /// A custom tax rate is seen as the currently active rate for a period from its starting timestamp until a new custom rate is set.
        /// If there is no custom tax currently active, use the standard tax rate.
        /// </summary>
        public double GetCurrentTaxRate(Commodity commodity)
        {
            return GetTaxRateForDateTime(commodity, DateTime.Now);
        }

    }

    public enum Commodity
    {
        //PLEASE NOTE: THESE ARE THE ACTUAL TAX RATES THAT SHOULD APPLY, WE JUST GOT THEM FROM THE CLIENT!
        Default,            //25%
        Alcohol,            //25%
        Food,               //12%
        FoodServices,       //12%
        Literature,         //6%
        Transport,          //6%
        CulturalServices    //6%
    }
}
