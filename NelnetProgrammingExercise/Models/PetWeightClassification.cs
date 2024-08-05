using System;
using System.Collections.Generic;
using System.Text;

namespace NelnetProgrammingExercise.Models
{
    public enum PetWeightClass
    {
        ExtraSmall,
        Small,
        Medium,
        Large,
        ExtraLarge,

    }

//Mapping each pet weight class to its minimum and maximum weight.
    public static class PetWeightClassRanges
    {
        //dictionary of each pet weight class with a tuple of its minimum and maximum weight. The dictionary will facilitate a quick lookup of pets weight class.
        public static Dictionary<PetWeightClass, (double Min, double Max)> WeightRanges = new Dictionary<PetWeightClass,(double Min, double Max)>
        {
            {PetWeightClass.ExtraSmall,(0,1.0)},
            {PetWeightClass.Small,(1.0,5.0)},
            {PetWeightClass.Medium,(5.0,15.0)},
            {PetWeightClass.Large,(15.0,30.0)}, 
            {PetWeightClass.ExtraLarge,(30.0,double.MaxValue)}
        };

        /* This method should receive a number value of the weight of a pet and return the weight class of that specific pet. 
            If an invalid argument is given (a weight less than 0 or greater than doubleMaxValue), the method throws an exception */
        public static PetWeightClass GetPetWeightClass(double value)
        {
            foreach(var weightRange in WeightRanges)
            {
                if(value > weightRange.Value.Min && value <= weightRange.Value.Max)
                {
                    return weightRange.Key;
                };
            };
            throw new ArgumentException("Pet Weight is out of defined ranges");
        }
    }
}