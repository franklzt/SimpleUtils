using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomElement
{
    public static class RandomElement
    {
        public static int Choose(this float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        public static void Shuffle(this int[] deck)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                int temp = deck[i];
                int randomIndex = Random.Range(0, deck.Length);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }


    }
}


