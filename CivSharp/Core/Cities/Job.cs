using System.Collections.Generic;
using CivSharp.Core.Cities.Population;

namespace CivSharp.Core.Cities
{
    class Job
    {
        public int TotalJobSlots { get; set; }

        public int UsedJobSlots
        {
            get { return _peopleWorkingHere.Count; }
        }

        public PopulationType JobType { get; set; }

        private List<Population.Population> _peopleWorkingHere;

        public Job(PopulationType jobType, int totalSlots)
        {
            JobType = jobType;
            TotalJobSlots = totalSlots;
        }

        public bool TryEmployPerson(Population.Population person)
        {
            if (UsedJobSlots >= TotalJobSlots) return false;

            person.PopulationType = JobType;
            _peopleWorkingHere.Add(person);

            return true;
        }
    }
}