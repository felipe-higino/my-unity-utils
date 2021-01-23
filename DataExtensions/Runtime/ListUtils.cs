using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace FelipeUtils.Lambda
{
    public static class ListUtils
    {
        static System.Random rng = new System.Random();
        /// <summary>
        /// Randomizes the collection items order
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            // Fisher - Yates shuffle method
            var list = collection.ToList();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            // var randomOrderedList = collection.OrderBy(item => randomizer.Next());
            return list;
        }

        /// <summary>
        /// Returns a pretty string of collection
        /// </summary>
        public static string ToString<T>(this IEnumerable<T> collection)
        {
            var stringToPrint = "\n---- list ---- \n";
            var index = 0;
            foreach (var item in collection)
            {
                stringToPrint += $"[{index}] - {item}\n";
                index++;
            }
            return stringToPrint;
        }

        /// <summary>
        /// Calls all actions in collection
        /// </summary>
        public static void CallAll(this IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                action?.Invoke();
            }
        }

        public static void Evaluate<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection) { }
        }
    }
}
