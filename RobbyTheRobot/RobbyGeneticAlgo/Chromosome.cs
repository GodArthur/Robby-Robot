using System;
using GeneticAlgo;

namespace RobbyGeneticAlgo
{
    public delegate double Fitness(Chromosome c);

    public delegate int AlleleMoveAndFitness(Chromosome c, Contents[,] grid, ref int x, ref int y);

    public delegate Chromosome[] Crossover(Chromosome a, Chromosome b);



    public class Chromosome : IComparable<Chromosome>
    {
        //array of Alleles
        private Allele[] gene;

        public double Fitness
        {
            get; private set;
        }
        


        /// <summary>
        /// Chromosome constructor initializing 
        /// the array of Alleles
        /// </summary>
        /// <param name="Length">Initial length of the allele</param>
        public Chromosome(int Length)
        {
            //setting the length of the alleles
            gene = new Allele[Length];

            //itterating over, and storing
            // the enum values of Allele
            for (int i = 0; i < gene.Length; i++)
            {

                int num = Helpers.rand.Next(0, Enum.GetNames(typeof(Allele)).Length);

                gene[i] = (Allele)num;
            }
        }

        /// <summary>
        /// Constructor makes a deep copy values of gene array
        /// </summary>
        /// <param name="gene">Array of alleles</param>
        public Chromosome(Allele[] gene)
        {
            Allele[] temp = new Allele[gene.Length];

            for (int i = 0; i < gene.Length; i++)
            {
                temp[i] = gene[i];
            }

            this.gene = gene;
        }

        /// <summary>
        /// indexer on array of Alleles.
        /// returns allele on the index
        /// </summary>
        /// <param name="index">index of the allele</param>
        /// <returns></returns>
        public Allele this[int index]
        { get { return gene[index]; } }

        public Chromosome[] Reproduce(Chromosome spouse, Crossover f, double mutationRate)
        {

        }
        
        public void EvalFitness(Fitness f)
        {
            Fitness = f(this);
        }

        public override String ToString()
        {
            string content = "";

            for (int i = 0; i < gene.Length; i++)
            {
                if (i < gene.Length - 1)
                {
                    content += gene[i] + ", ";
                }
                else
                {
                    content += gene[i];
                }
            }

            return content;
        }
    }
}
