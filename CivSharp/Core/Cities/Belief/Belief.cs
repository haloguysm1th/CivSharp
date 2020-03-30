using System;

namespace CivSharp.Core.Cities.Belief
{
    class Belief
    {
        public BeliefTypes BeliefType { get; }
        public bool Dominate { get; }

        public Belief(BeliefTypes type, bool dominate)
        {
            BeliefType = type;
            Dominate = dominate;
        }

        public Belief(Belief p1, Belief p2)
        {
            Belief takingBelief;
            //If parent1 beliefs is dominate but not parent twos
            if (p1.Dominate && !p2.Dominate)
            {
                takingBelief = p1;
            }
            //If parent 2 is dominat but not parent 1
            else if (p2.Dominate && !p1.Dominate)
            {
                takingBelief = p2;
            }
            //If both are dominant or both receesive
            else
            {
                var rand = new Random();
                //Will pick either 0,1
                takingBelief = rand.Next(0, 2) == 1 ? p1 : p2;
            }


            BeliefType = takingBelief.BeliefType;
            Dominate = takingBelief.Dominate;
        } 
    }
}