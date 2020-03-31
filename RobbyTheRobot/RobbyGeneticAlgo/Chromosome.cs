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
        
        //Length of Allele array
        public int Length
        { get; }


        /// <summary>
        /// Chromosome constructor initializing 
        /// the array of Alleles
        /// </summary>
        /// <param name="Length">Initial length of the allele</param>
        public Chromosome(int Length)
        {
            this.Length = Length;

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
            this.Length = gene.Length;
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
            //value determines whether or not to mutate
            double mutate;
            Chromosome[] children = f(this, spouse);

            //temporary array to store old and mutated values
            Allele[] temp = new Allele[this.Length];
            
            //iterrating through every child
            for (int i = 0; i < children.Length; i++)
            {
                for (int j = 0; j < children[i].Length; j++)
                {
                    //Assigning the value for whether or not we should mutate
                    mutate = Helpers.rand.NextDouble();

                    //Checking if mutation should happen
                    if (mutate < mutationRate)
                    {
                        temp[j] = (Allele)Helpers.rand.Next(0, this.Length);
                    }
                    else
                    {
                        temp[j] = children[i][j]; 
                    }
                }

                children[i] = new Chromosome(temp);
            }

            return children;
        }
        
        public void EvalFitness(Fitness f)
        {
            Fitness = f(this);
        }

        /// <summary>
        /// Method converts array of allele to string.
        /// Overrides ToString method in object class
        /// </summary>
        /// <returns>newly converted string of allele</returns>
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


        public int CompareTo(Chromosome c)
        {
            return (this.Fitness.CompareTo(c.Fitness));
        }

        public Chromosome[] SingleCrossover(Chromosome a, Chromosome b)
        {
            //declaring the new subset of offspring allele
            Allele[] firstSet = new Allele[a.Length];
            Allele[] secondSet = new Allele[a.Length];

            //number at which splits the two chromosomes
            int split = Helpers.rand.Next(0, a.Length);

            //setting first set of allele
            for (int i = 0; i < firstSet.Length; i++)
            {
                if (i <= split)
                {
                    firstSet[i] = a[i];
                }
                else
                {
                    firstSet[i] = b[i];
                }
            }

            //setting the second set of allele
            for (int i = 0; i < secondSet.Length; i++)
            {
                if (i <= split)
                {
                    secondSet[i] = b[i];
                }
                else
                {
                    secondSet[i] = a[i];
                }
            }

            Chromosome[] children = new Chromosome[2];

            //storing the newly created chromosomes
            children[0] = new Chromosome(firstSet);
            children[1] = new Chromosome(secondSet);

            return children;
        }

        public Chromosome[] DoubleCrossover(Chromosome a, Chromosome b)
        {
            //declaring the new subset of offspring allele
            Allele[] firstSet = new Allele[a.Length];
            Allele[] secondSet = new Allele[a.Length];

            //choosing the first and second halfway random points
            int half1 = Helpers.rand.Next(0, (a.Length / 2));
            int half2 = Helpers.rand.Next(a.Length / 2, a.Length);


            //storing the first set of allele
            for (int i = 0; i < firstSet.Length; i++)
            {
                if (i <= half1)
                {
                    firstSet[i] = a[i];
                }
                else if (i > half1 && i <= half2)
                {
                    firstSet[i] = b[i];
                }

                else 
                {
                    firstSet[i] = a[i];
                }
            }


            //storing the second set of allele
            for (int i = 0; i < secondSet.Length; i++)
            {
                if (i <= half1)
                {
                    secondSet[i] = b[i];
                }
                else if (i > half1 && i <= half2)
                {
                    secondSet[i] = a[i];
                }

                else
                {
                    secondSet[i] = b[i];
                }
            }

            Chromosome[] children = new Chromosome[2];

            children[0] = new Chromosome(firstSet);
            children[1] = new Chromosome(secondSet);

            return children;
        }

       
        /// <summary>
        /// Method to have a consistent single crossover point
        /// Only used for testing purposes
        /// </summary>
        public Chromosome[] ConstantCrossover(Chromosome a, Chromosome b)
        {
            //declaring the new subset of offspring allele
            Allele[] firstSet = new Allele[a.Length];
            Allele[] secondSet = new Allele[a.Length];

            //Controlled Chrossover point
            int split = 4;

            //setting first set of allele
            for (int i = 0; i < firstSet.Length; i++)
            {
                if (i <= split)
                {
                    firstSet[i] = a[i];
                }
                else
                {
                    firstSet[i] = b[i];
                }
            }

            //setting the second set of allele
            for (int i = 0; i < secondSet.Length; i++)
            {
                if (i <= split)
                {
                    secondSet[i] = b[i];
                }
                else
                {
                    secondSet[i] = a[i];
                }
            }

            Chromosome[] children = new Chromosome[2];

            //storing the newly created chromosomes
            children[0] = new Chromosome(firstSet);
            children[1] = new Chromosome(secondSet);

            return children;
        }
    }
}
