namespace CivSharp.Core
{
    class Resource
    {
        public string Name { get; }
        public int Amount { get; }

        public Resource(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}