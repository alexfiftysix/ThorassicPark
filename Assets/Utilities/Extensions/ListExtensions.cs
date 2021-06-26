using System;
using System.Collections.Generic;
using Random = System.Random;

public static class ListExtensions
{
    private static Random _random = new Random();
    
    public static T RandomChoice<T>(this List<T> list)
    {
        var index = _random.Next(list.Count);
        return list[index];        
    }
}
