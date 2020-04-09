using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyGeneticAlgo;

namespace RobbyGeneticAlgoUnitTests
{
    [TestClass]
    public class GenerationTest
    {
        [TestMethod]
        public void TestIndex()
        {
            Chromosome[] c = new Chromosome[10];

            bool sameChromosome = true;

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Chromosome(10);
            }

            Generation g = new Generation(c);

            for (int i = 0; i < g.GetPopulation().Length; i++)
            {
                //using index instead of GetPopulation()
                for (int j = 0; j < g[i].Length; j++)
                {
                    if (g[i][j] != c[i][j])
                    {
                        sameChromosome = false;
                    }
                }
            }

            Assert.IsTrue(sameChromosome);
        }

        [TestMethod]
        public void TestConstructorCopy()
        {
            Chromosome[] c = new Chromosome[10];
            int genesSize = 9;

            bool createdPop = true;

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Chromosome(genesSize);
            }

            Generation g = new Generation(c);

            if (g.GetPopulation().Length != c.Length)
            {
                createdPop = false;
            }
            else
            {
                for (int i = 0; i < g.GetPopulation().Length; i++)
                {
                    for (int j = 0; j < g.GetPopulation()[i].Length; j++)
                    {
                        if (g.GetPopulation()[i][j] != c[i][j])
                        {
                            createdPop = false;
                        }
                    }
                }
            }

            Assert.IsTrue(createdPop);
        }

        [TestMethod]
        public void TestConstructor()
        {
            int popSize = 5;
            int numGenes = 7;

            bool createsPop = true;

            Generation g = new Generation(popSize, numGenes);

            if (g.GetPopulation().Length != popSize)
            {
                createsPop = false;
            }
            else
            {
                for (int i = 0; i < g.GetPopulation().Length; i++)
                {
                    if (g.GetPopulation()[i].Length != numGenes)
                    {
                        createsPop = false;
                    }
                }
            }

            Assert.IsTrue(createsPop);
        }

        [TestMethod]
        public void TestEvalFitness()
        {
            Chromosome[] c = new Chromosome[10];

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Chromosome(10);
            }
            Generation g = new Generation(c);

            g.EvalFitness(g.RandomChromosomeFitness);

            bool sortedDescending = true;

            for (int i = 0; i < g.GetPopulation().Length; i++)
            {
                if (i + 1 != g.GetPopulation().Length)
                {
                    if (g[i].Fitness < g[i + 1].Fitness)
                    {
                        sortedDescending = false;
                        break;
                    }
                }
            }

            Assert.IsTrue(sortedDescending);
        }

        [TestMethod]
        public void TestSelectParent()
        {
            //Choosing a population of size 10 to know which 10
            //chromosomes the index is choosing from
           
            Generation g = new Generation(10, 7);

            g.EvalFitness(g.RandomChromosomeFitness);

            Chromosome test = g.SelectParent();

            bool highestFitness;

            //finding elem with largest fitness
            //to compare the value returned by SelectParent()

           // int index = 0;

           /* for (int i = 0; i < g.GetPopulation().Length; i++)
            {
                if (g[index].Fitness < g[i].Fitness)
                {
                    index = i;
                }
            }*/

            if (test.Fitness == g[0].Fitness)
            {
                highestFitness = true;
            }
            else
            {
                highestFitness = false;
            }

            Assert.IsTrue(highestFitness);
        }
    }
}
