using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgo;

namespace RobbyGeneticAlgo
{
    class Generation
    {

        Chromosome[] population;

        public Generation(int populationSize, int numGenes)
        {
            population = new Chromosome[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new Chromosome(numGenes);
            }
            
        }

        public Generation(Chromosome[] members)
        {
            population = new Chromosome[members.Length];
            Allele[] temp;

            for (int i = 0; i < members.Length; i++)
            {
                temp = new Allele[members[i].Length];

                for (int j = 0; j < members[i].Length; j++)
                {
                    temp[j] = members[i][j];   
                }

                population[i] = new Chromosome(temp);
            }
        }

        public void EvalFitness(Fitness f)
        {

        }
    }
}
