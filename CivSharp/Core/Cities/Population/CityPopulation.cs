using System;
using System.Collections.Generic;
using System.Linq;

namespace CivSharp.Core.Cities.Population
{
    class CityPopulation
    {
        //The type of worker, and then the list of people who work on it.
        public List<Population> TotalPopulation { get; }
        private City _city;


        public void AddNewPopulation()
        {
            var rand=  new Random();

            //Yes you can have a kid with yourself? Because why not
            var pop1Num = rand.Next(0, TotalPopulation.Count);
            var pop2Num = rand.Next(0, TotalPopulation.Count);
            
            var pop1 = TotalPopulation.ElementAt(pop1Num);
            var pop2 = TotalPopulation.ElementAt(pop2Num);

            var pop3 = new Population(_city.ControllingCivilization,pop1.Upkeep,
                pop1.Beliefs.GenerateChildBeliefs(pop2.Beliefs.HeldBeliefs),pop2.PopulationType);
            TotalPopulation.Add(pop3);
        }

        /// <summary>
        /// tries to put an employee into a job
        /// </summary>
        /// <param name="job">The job to employ</param>
        /// <returns>returns true if successful, false if not</returns>
        public bool TriedBecomeEmployed(Job job)
        {
            var freePeople = TotalPopulation.Where(person =>
                    person.PopulationType == PopulationType.Free || person.PopulationType == PopulationType.Other)
                .ToList();

            if (freePeople.Count <= 0)
            {
                //There are no people to work
                return false;
            }

            //Try and employ the first free person.
            //I really like how clean linq makes most of this code.
            return job.TryEmployPerson(freePeople.First());
        }
    }
}