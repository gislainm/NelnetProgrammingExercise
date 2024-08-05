using System;
using System.Collections.Generic;
using System.Text;

namespace NelnetProgrammingExercise.Models
{
    public class PetModel
    {
        public string Name { get; set; }
        //adding the pet weight to the petModel
        public double Weight {get;set;}
        public PetClassification Classification { get; set; }
        public PetType Type { get; set; }
    }
}
