using NelnetProgrammingExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NelnetProgrammingExercise
{
    class Program
    {
        private static PersonModel[] People;
        private static PetModel[] Pets;

        #region Initialization
 
        private static void SetupObjects()
        {
            People = new PersonModel[]
            {
                new PersonModel()
                {
                    Name = "Dalinar",
                    PreferredClassification = PetClassification.Mammal,
                    PreferredType = PetType.Snake,
                    PreferredWeight = PetWeightClass.Medium,
                    //adding pet overrides for Dalinar for testing
                    PreferredOverrides = new List<(PetClassification?, PetType?, PetWeightClass?)>{
                        (PetClassification.Bird,null,null),
                        (PetClassification.Arachnid,null,null),
                        (null,PetType.Dog,null),
                        (null,null,PetWeightClass.Large),
                        (null,null,PetWeightClass.ExtraLarge),
                    }
                },
                new PersonModel()
                {
                    Name = "Kaladin",
                    PreferredClassification = PetClassification.Bird,
                    PreferredType = PetType.Goldfish,
                    PreferredWeight = PetWeightClass.ExtraSmall,
                    //adding pet overrides for Kaladin for testing
                    PreferredOverrides = new List<(PetClassification?, PetType?, PetWeightClass?)>{
                        (PetClassification.Mammal,null,null),
                        (PetClassification.Reptile,null,null),
                        (null,PetType.Dog,null),
                        (null,PetType.Cat,null),
                        (null,PetType.Betta,null),
                        (null,null,PetWeightClass.ExtraLarge),
                    }
                }
            };

            Pets = new PetModel[]
            {
                new PetModel()
                {
                    Name = "Garfield",
                    Classification = PetClassification.Mammal,
                    Type = PetType.Cat,
                    Weight = 20.0
                },
                new PetModel()
                {
                    Name = "Odie",
                    Classification = PetClassification.Mammal,
                    Type = PetType.Dog,
                    Weight = 15.0
                },
                new PetModel()
                {
                    Name = "Peter Parker",
                    Classification = PetClassification.Arachnid,
                    Type = PetType.Spider,
                    Weight = 0.5
                },
                new PetModel()
                {
                    Name = "Kaa",
                    Classification = PetClassification.Reptile,
                    Type = PetType.Snake,
                    Weight = 25.0
                },
                new PetModel()
                {
                    Name = "Nemo",
                    Classification = PetClassification.Fish,
                    Type = PetType.Goldfish,
                    Weight = 0.5
                },
                new PetModel()
                {
                    Name = "Alpha",
                    Classification = PetClassification.Fish,
                    Type = PetType.Betta,
                    Weight = 0.1
                },
                new PetModel()
                {
                    Name = "Splinter",
                    Classification = PetClassification.Mammal,
                    Type = PetType.Rat,
                    Weight = 0.5
                },
                new PetModel()
                {
                    Name = "Coco",
                    Classification = PetClassification.Bird,
                    Type = PetType.Parrot,
                    Weight = 6.0
                },
                new PetModel()
                {
                    Name = "Tweety",
                    Classification = PetClassification.Bird,
                    Type = PetType.Canary,
                    Weight = 0.05
                }
            };
        }

        #endregion

        /**
            This method returns an ordered IEnumerable of pets, string indicating if the pet is good match, 
            and the ordering priority of the pet as requested in requirement 5. This method is called for 
            each person and return a pet-list matched to the preferrence of the person
        **/
        private static IEnumerable<(PetModel Pet, string IsGoodMatch, int Priority)> MatchPets(PersonModel person, PetModel[] pets)
        {
            var matches = new List<(PetModel Pet, string IsGoodMatch, int Priority)>();
            bool petAdded = false;
            foreach(PetModel pet in pets)
            {
                foreach((PetClassification?,PetType?,PetWeightClass?) preferredOverride in person.PreferredOverrides)
                {
                    /** check person's preferred overrides, if any of them matches the pet, the pet is automatically a bad match otherwise, 
                    we move on to compare the pet with the person's preferrences in a pet and if any of the person's preference match the pet, then the pet 
                    is a good match **/
                    if(
                        (preferredOverride.Item1.HasValue && preferredOverride.Item1.Value == pet.Classification) ||
                        (preferredOverride.Item2.HasValue && preferredOverride.Item2.Value == pet.Type) ||
                        (preferredOverride.Item3.HasValue && preferredOverride.Item3.Value == PetWeightClassRanges.GetPetWeightClass(pet.Weight))
                    )
                    {
                        matches.Add((pet,"bad",4));
                        petAdded = true;
                        break;
                    }
                    else
                    {
                        //this flag is to help skip to the next iteration if the pet is determined to be a bad match by considerring the person's preferred overrides
                        petAdded = false;
                    }
                } 
                if (petAdded)
                {
                    //if the current pet has been already added to the matches list, we then continue to the next iteration i.e to the next pet in the list.
                    continue;
                }
                //if no decision was made by considering the overrides, we then determine if the pet is a good match by looking at the person's preferrences
                else if (person.PreferredType == pet.Type)
                {
                    matches.Add((pet,"good",1));
                }
                else if (person.PreferredClassification == pet.Classification)
                {
                    matches.Add((pet,"good",2));
                }
                else if (person.PreferredWeight == PetWeightClassRanges.GetPetWeightClass(pet.Weight))
                {
                    matches.Add((pet,"good",3));
                }
                else
                {
                   matches.Add((pet,"bad",4)); 
                }
            }
            // after the matched list has been created, we return it ordered by priority.
            return matches.OrderBy(match=>match.Priority);
        }

        static void Main(string[] args)
        {
            SetupObjects();

            foreach(PersonModel person in People) {
                //print the current person's name
                Console.WriteLine($"Pets for {person.Name}");

                //get the ordered pets matched list for the person
                var matchingResult = MatchPets(person,Pets);
                //iterate through the ordered pets list, and print out the pet's name, and if it's a good match or bad match
                foreach(var match in matchingResult)
                {
                    Console.WriteLine(string.Format($"{match.Pet.Name} would be a {match.IsGoodMatch} pet."));   
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
