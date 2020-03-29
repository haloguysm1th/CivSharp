using System;
using System.Collections.Generic;
using System.Linq;

namespace CivSharp.Core.Cities.Belief
{
    class Beliefs
    {
        public List<Belief> HeldBeliefs { get; }

        public Beliefs()
        {
            HeldBeliefs = new List<Belief>();
        }

        public Beliefs(List<Belief> beliefs)
        {
            HeldBeliefs = beliefs;
        }

        public void AddBelief(Belief belief)
        {
            HeldBeliefs.Add(belief);
        }

        public Beliefs GenerateChildBeliefs(List<Belief> otherBeliefs)
        {
            var beliefs = new Beliefs();
            //TODO fix this so that people with different count of beliefs can have children
            if (HeldBeliefs.Count != otherBeliefs.Count)
            {
                throw new ArgumentException("The beliefs need to be the same length!");
            }

            for (int i = 0; i < HeldBeliefs.Count; i++)
            {
                var p1 = HeldBeliefs.ElementAt(i);
                var p2 = otherBeliefs.ElementAt(i);
                var belief = new Belief(p1,p2);
                beliefs.AddBelief(belief);
            }

            return beliefs;
        }
    }
}