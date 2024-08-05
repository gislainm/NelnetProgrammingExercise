using System;
using System.Collections.Generic;
using System.Text;

namespace NelnetProgrammingExercise.Models
{
    public class PersonModel
    {
        public string Name { get; set; }
        public PetClassification PreferredClassification { get; set; }
        public PetType PreferredType { get; set; }
        //adding preferred pet weight to the person's Model
        public PetWeightClass PreferredWeight{get;set;} 
        //adding a list of overrides, each item of the override list will be recorded in a tuple to preserve the type safety and clarity.
        public List<(PetClassification?,PetType?,PetWeightClass?)> PreferredOverrides{get;set;}
    }
}
