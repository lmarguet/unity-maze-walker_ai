using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    private readonly List<int> genes;
    private readonly int dnaLength;
    private readonly int maxGeneValue;

    public DNA(int length, int maxGeneValue)
    {
        genes = new List<int>();
        dnaLength = length;
        this.maxGeneValue = maxGeneValue;
        
        GenerateRandomGenes();
    }

    void GenerateRandomGenes()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxGeneValue));
        }
    }

    public void SetGeneValue(int pos, int value)
    {
        genes[pos] = value;
    }
    
    public int GetGeneValue(int pos)
    {
        return genes[pos];
    }

    public void Combine(DNA dna1, DNA dna2)
    {
        for (var i = 0; i < dnaLength; i++)
        {
            genes[i] = i < dnaLength / 2 ? dna1.genes[i] : dna2.genes[i];
        }
    }
    
    
    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxGeneValue);
    }

}