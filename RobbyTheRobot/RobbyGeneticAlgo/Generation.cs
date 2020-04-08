using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgo;

namespace RobbyGeneticAlgo
{
    public class Generation
    {

        Chromosome[] population;


        /// <summary>
        /// Constructor populates an array of random Chromosomes
        /// </summary>
        /// <param name="populationSize">amount of Chromosomes</param>
        /// <param name="numGenes">Specified length to initialize Chromosomes</param>
        public Generation(int populationSize, int numGenes)
        {

            population = new Chromosome[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new Chromosome(numGenes);
            }
            
        }

        /// <summary>
        /// Cosntructor initializes chromosome array
        /// through a deep copy
        /// </summary>
        /// <param name="members">Chromosome array to be copied</param>
        public Generation(Chromosome[] members)
        {
            population = new Chromosome[members.Length];
            Allele[] temp;

            //iterating through every chromosome
            for (int i = 0; i < members.Length; i++)
            {
                //setting the length of the allele array
                temp = new Allele[members[i].Length];

                //iterating through the alleles of chromosome at index i
                for (int j = 0; j < members[i].Length; j++)
                {
                    temp[j] = members[i][j];   
                }

                population[i] = new Chromosome(temp);
            }
        }

        /// <summary>
        /// -	a readonly indexer is used to get a specific Chromosome
        /// </summary>
        /// <param name="index">index of chromosome</param>
        /// <returns>Chromodomr at index</returns>
        public Chromosome this[int index] { get { return population[index]; } }


        public void EvalFitness(Fitness f)
        {
            foreach (Chromosome member in population)
            {
                member.EvalFitness(f);
            }

            //Sorts the array based on the highest fitness
            Array.Sort(population);
            Array.Reverse(population);
        }


        /// <summary>
        /// method is used to select a parent for the next generation
        /// </summary>
        /// <returns>The desired parent chromosome</returns>
        public Chromosome SelectParent()
        {
            int[] index = new int[10];

            for (int i = 0; i < index.Length; i++)
            {
                index[i] = Helpers.rand.Next(0, population.Length);
            }

            return population[Min(index)];
        }


        /// <summary>
        /// Method finds the smallest elements index in an int array
        /// </summary>
        /// <param name="indexes">array of elements</param>
        /// <returns>smallest element</returns>
        private int Min(int[] indexes)
        {
            int index = indexes[0];

            foreach (int i in indexes)
            {
                if (index > i)
                {
                    index = i;
                }
            }

            return index;
        }

        /// <summary>
        /// Method retrieves population
        /// For testing purposes only
        /// </summary>
        /// <returns>Chromosome population</returns>
        public Chromosome[] GetPopulation()
        {
            return population;
        }


    }
}
