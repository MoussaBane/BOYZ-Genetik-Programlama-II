using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_sc
{
    public List<int> genes = new List<int>();
    public int dnaLength;
    public int maxValue;

    public DNA_sc(int length, int max)
    {
        dnaLength = length;
        maxValue = max;
        SetRandom();
    }

    // Set random values for DNA genes
    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }

    // Set a specific gene at position
    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    // Retrieve a specific gene value
    public int GetInt(int pos)
    {
        return genes[pos];
    }

    // Combine two DNA sets from two parents
    public void Combine(DNA_sc d1, DNA_sc d2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            // Combine half genes from each parent
            genes[i] = i < dnaLength / 2 ? d1.genes[i] : d2.genes[i];
        }
    }

    // Mutation method (note: add mutation rate as parameter)
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

    // Cross over DNA with a partner
    public DNA_sc Crossover(DNA_sc partner)
    {
        DNA_sc child = new DNA_sc(dnaLength, maxValue);
        int midpoint = Random.Range(0, dnaLength);
        
        // Genes from both parents up to midpoint
        for (int i = 0; i < dnaLength; i++)
        {
            child.genes[i] = (i > midpoint) ? genes[i] : partner.genes[i];
        }
        
        return child;
    }
}
