using System;
using System.Reflection;
using System.Collections.Generic;

namespace BenfordsLaw.Processor
{
    public class Logic
    {
        private static readonly Random GetRandom = new Random();

        /// <summary>
        /// Generates a random set of numbers and determines Benford's Law information 
        /// given a number of elements, min value, and max value.
        /// </summary>
        /// <param name="numberOfElements">Number of random integer values to generate</param>
        /// <param name="min">Lower bound (minimum) value to generate</param>
        /// <param name="max">Upper bound (maximum) value to generate</param>
        /// <returns></returns>
        public RandomizedBenfordsLawResult CalcRandomBenfordsLawResult(int numberOfElements, int min, int max)
        {
            var rv = new RandomizedBenfordsLawResult();
            rv.Result = new BenfordsLawResult();
            rv.Result.TotalElements = numberOfElements;
            rv.Elements = new List<int>();

            for(int i = 0; i< numberOfElements; i++)
            {
                // First generate random number between min and max
                var element = GetRandomNumber(min, max);

                // Take first char of result and use to generate correct property name on BenfordsLawResult object
                var propName = string.Format("StartsWith{0}", element.ToString()[0]);

                // Use reflection to get current property value
                var curValue = GetIntPropValue(rv.Result, propName);

                // Use reflection to increment value
                SetIntPropValue(rv.Result, ++curValue, propName);

                // We return a complete list of the generated numbers to the caller too
                rv.Elements.Add(element);
            }

            return rv;
        }

        public BenfordsLawResult CalcBenfordsLawResult(List<int> elements)
        {
            var rv = new BenfordsLawResult();

            foreach(int element in elements)
            {
                // Take first char of result and use to generate correct property name on BenfordsLawResult object
                var propName = string.Format("StartsWith{0}", element.ToString()[0]);

                // Use reflection to get current property value
                var curValue = GetIntPropValue(rv, propName);

                // Use reflection to increment value
                SetIntPropValue(rv, ++curValue, propName);
            }

            return rv;
        }

        private static int GetRandomNumber(int min, int max)
        {
            lock (GetRandom) // synchronize
            {
                return GetRandom.Next(min, max);
            }
        }

        private static int GetIntPropValue(object src, string propName)
        {
            return (int)(src.GetType().GetRuntimeProperty(propName)?.GetValue(src));
        }

        private static void SetIntPropValue(object obj, int value, string propName)
        {
            obj.GetType().GetRuntimeProperty(propName)?.SetValue(obj, value, null);
        }
    }
}
