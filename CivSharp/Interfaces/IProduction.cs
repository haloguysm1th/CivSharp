using System.Collections.Generic;

namespace CivSharp.Interfaces
{
    interface IProduction<T>
    {
        int Amount { get; }
        Queue<T> ToProduce { get; }
    }
}