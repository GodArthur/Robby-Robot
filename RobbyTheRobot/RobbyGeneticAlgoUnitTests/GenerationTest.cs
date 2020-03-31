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
        public void testCon()
        {

        }
    }
}
