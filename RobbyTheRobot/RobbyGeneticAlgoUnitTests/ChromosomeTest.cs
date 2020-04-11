using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyGeneticAlgo;

namespace RobbyGeneticAlgoUnitTests
{
    [TestClass]
    public class ChromosomeTest
    {
        //Not fully sure how to test this
        [TestMethod]
        public void TestConstructorLength()
        {

            Chromosome c = new Chromosome(3);
            //bool isAllele = false;

            for(int i = 0; i < c.Length; i++)
            {
                //if (c[i] != null)
                {
                   // isAllele
                }
            }
            
        }


        [TestMethod]
        public void TestConstructorArray()
        {
            Allele[] temp = new Allele[] { Allele.North, Allele.South, Allele.East, Allele.West };
            Chromosome c = new Chromosome(temp);
            bool notEqual = false;

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != temp[i])
                {
                    notEqual = true;
                    break;
                }
            }

            Assert.IsFalse(notEqual);
        }
        
        [TestMethod]
        public void TestIndex()
        {
            Allele[] temp = new Allele[] { Allele.East, Allele.North, Allele.Random };
            Chromosome c = new Chromosome(temp);

            Assert.AreEqual(Allele.North, c[1]);
        }

        [TestMethod]
        public void TestEvalFitness()
        {
            Chromosome c = new Chromosome(5);

            c.EvalFitness(c.ConstantFitness);

            Assert.AreEqual(5.0, c.Fitness);
        }



        [TestMethod]
        public void TestSingleCrossover()
        {
            Chromosome a = new Chromosome(10);
            Chromosome b = new Chromosome(10);

            Chromosome test = new Chromosome(10);
            Chromosome[] arr = test.SingleCrossover(a, b);

            Chromosome child1 = arr[0];
            Chromosome child2 = arr[1];

            //number at which the split is supposed to happen
            //initialized at random values
            int splitNum1 = 1;
            int splitNum2 = 2;

            //check if crossover does happen
            bool isChild1 = true;
            bool isChild2 = true;

            //check if crossover happens at the same split point and
            //if it does happen correctly
            bool areSiblings = false;

            //loops to find the split value, and to see if the values of the second half matches
            for (int i = 0; i < child1.Length; i++)
            {
                bool stopLoop = false;

                //finding the split point
                if (child1[i] != a[i])
                {
                    splitNum1 = i - 1;

                    stopLoop = true;

                    //checking if the second half matches
                    for (int j = i; j < child1.Length; j++)
                    {
                        if (child1[j] != b[i])
                        {
                            isChild1 = false;
                            break;
                        }
                    }
                }

                if (stopLoop)
                {
                    break;
                }
            }

            //checking first half of child 1
            for (int i = 0; i <= splitNum1; i++)
            {
                if (child1[i] != a[i])
                {
                    isChild1 = false;
                }
            }

            //repeating same process for second child
            for (int i = 0; i < child2.Length; i++)
            {
                bool stopLoop = false;

                //finding the split point
                if (child2[i] != b[i])
                {
                    splitNum2 = i - 1;

                    stopLoop = true;

                    //checking if second half matches
                    for (int j = i; j < child2.Length; j++)
                    {
                        if (child1[j] != a[i])
                        {
                            isChild2 = false;
                            break;
                        }
                    }
                }

                if (stopLoop)
                {
                    break;
                }
            }

            //checking first half of child 1
            for (int i = 0; i <= splitNum2; i++)
            {
                if (child2[i] != a[i])
                {
                    isChild2 = false;
                }
            }

            if (splitNum1 == splitNum2 && isChild1 == isChild2)
            {
                areSiblings = true;
            }

            Assert.IsTrue(areSiblings);


        }

        [TestMethod]
        public void TestDoubleCrossover()
        {
            Chromosome a = new Chromosome(10);
            Chromosome b = new Chromosome(10);

            Chromosome test = new Chromosome(10);
            Chromosome[] arr = test.DoubleCrossover(a, b);

            Chromosome child1 = arr[0];
            Chromosome child2 = arr[1];

            //two numbers where the genes split in first child
            //initialized at random values
            int splitNum1 = 1;
            int splitNum2 = 2;

            //two numbers where the genes split in the second child
            //initialized at random values
            int splitNum3 = 3;
            int splitNum4 = 4;

            //check if crossover does happen
            bool isChild1 = true;
            bool isChild2 = true;

            //check if crossover happens at the same split point and
            //if it does happen correctly
            bool areSiblings = false;


            //loops to find the split values of child1
            for (int i = 0; i < child1.Length; i++)
            {
                bool stopLoop = false;

                //finding the split point
                if (child1[i] != a[i])
                {
                    splitNum1 = i - 1;

                    //checking if the second half matches
                    for (int j = i; j < child1.Length; j++)
                    {
                        if (child1[j] != b[i])
                        {
                            splitNum2 = i - 1;
                            stopLoop = true;
                            break;
                        }
                    }
                }

                if (stopLoop)
                {
                    break;
                }
            }

            //checking if the values match in child 1
            for (int i = 0; i < child1.Length; i++)
            {
                if (i <= splitNum1 || i > splitNum2)
                {
                    if (child1[i] != a[i])
                    {
                        isChild1 = false;
                    }
                }
                else //if (i > splitNum1 && i <= splitNum2)
                {
                    if (child1[i] != b[i])
                    {
                        isChild1 = false;
                    }
                }
            }



            //repeating same process for second child

            //loops to find the split values in child2
            for (int i = 0; i < child2.Length; i++)
            {
                bool stopLoop = false;

                //finding the split point
                if (child2[i] != b[i])
                {
                    splitNum3 = i - 1;

                    //finding second split value
                    for (int j = i; j < child2.Length; j++)
                    {
                        if (child2[j] != a[i])
                        {
                            splitNum4 = i - 1;
                            stopLoop = true;
                            break;
                        }
                    }
                }

                if (stopLoop)
                {
                    break;
                }
            }

            //checking if the values match in child 2
            for (int i = 0; i < child2.Length; i++)
            {
                if (i <= splitNum3 || i > splitNum4)
                {
                    if (child2[i] != b[i])
                    {
                        isChild2 = false;
                    }
                }
                else //if (i > splitNum1 && i <= splitNum2)
                {
                    if (child2[i] != a[i])
                    {
                        isChild2 = false;
                    }
                }
            }

            if (splitNum1 == splitNum3 && splitNum2 == splitNum4 && isChild1 == isChild2)
            {
                areSiblings = true;
            }

            Assert.IsTrue(areSiblings);
        }

        [TestMethod]
        public void TestReproduce()
        {
            Chromosome a = new Chromosome(10);
            Chromosome b = new Chromosome(10);

            Chromosome[] c = a.Reproduce(b, a.ConstantCrossover, 1);

            bool isMutatedChild1 = false;
            bool isMutatedChild2 = false;

            //a bool to check if both children have been mutated
            bool areMutated = false;

            Chromosome child1 = c[0];
            Chromosome child2 = c[1];

            //Checking if child1 is mutated
            for (int i = 0; i < child1.Length; i++)
            {
                //In the Crossover method used to test, the crossover
                //point is 4
                if (i <= 4)
                {
                    if (child1[i] != a[i])
                    {
                        isMutatedChild1 = true;
                    }
                }
                else
                {
                    if (child1[i] != b[i])
                    {
                        isMutatedChild1 = true;
                    }
                }    
            }


            //Checking if child2 is mutated
            for (int i = 0; i < child2.Length; i++)
            {
                //In the Crossover method used to test, the crossover
                //point is 4
                if (i <= 4)
                {
                    if (child2[i] != b[i])
                    {
                        isMutatedChild2 = true;
                    }
                }
                else
                {
                    if (child2[i] != a[i])
                    {
                        isMutatedChild2 = true;
                    }
                }
            }

            if (isMutatedChild1 == true && isMutatedChild2 == true)
            {
                areMutated = true;
            }

            Assert.IsTrue(areMutated);

        }

        [TestMethod]
        public void TestToString()
        {
            Chromosome c = new Chromosome(new Allele[]{Allele.North, Allele.South, Allele.East, Allele.West});

            string geneString = "North, South, East, West";

            Assert.IsTrue(c.ToString().Equals(geneString));
        }

        [TestMethod]
        public void TestCompareTo()
        {
            Chromosome[] c = new Chromosome[10];

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Chromosome(10);
                c[i].EvalFitness(c[i].RandomFitness);
            }

            bool sortedDescending = true;

            Array.Sort(c);

            for (int i = 0; i < c.Length; i++)
            {
                if (i + 1 != c.Length)
                {
                    if (c[i].Fitness < c[i + 1].Fitness)
                    {
                        sortedDescending = false;
                        break;
                    }
                }
            }

            Assert.IsTrue(sortedDescending);
        }

    }
}
