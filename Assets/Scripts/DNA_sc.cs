using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_sc
{
    public List<int> genes = new List<int>();
    public int dnaLength = 10;
    public int maxValue = 5;

    public DNA_sc(int length, int max)
    {
        dnaLength = length;
        maxValue = max;
        SetRandom();
    }

    // DNA'yi rastgele değerlere ayarla
    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }

    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < genes.Count; i++)
        {
            if (Random.Range(0f, 1f) < mutationRate)
            {
                genes[i] = Random.Range(0, maxValue);
            }
        }
    }

    public DNA_sc Crossover(DNA_sc partner)
    {
        DNA_sc child = new DNA_sc(dnaLength, maxValue);
        int midpoint = Random.Range(0, dnaLength);
        
        for (int i = 0; i < dnaLength; i++)
        {
            child.genes[i] = (i > midpoint) ? genes[i] : partner.genes[i];
        }
        
        return child;
    }
}
