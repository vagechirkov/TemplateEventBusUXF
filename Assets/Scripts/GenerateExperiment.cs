using System;
using System.Linq;
using UnityEngine;
using UXF;
using Random = UnityEngine.Random;

public class GenerateExperiment : MonoBehaviour
{
    public void GenerateSession(Session uxfSession)
    {
        var block = uxfSession.CreateBlock(5);
    }
    
    // Randomize order of elements in array
    public static void Randomize<T>(T[] items)
    {
        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (var i = 0; i < items.Length - 1; i++)
        {
            var j = Random.Range(i, items.Length);
            (items[i], items[j]) = (items[j], items[i]);
        }
    }
}