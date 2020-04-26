using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgo;

namespace RobbyGeneticAlgo
{

    /// <summary>
    /// This delegate is the event type that gets fired after each generation is completed
    /// </summary>
    /// <param name="num">Number of the gen</param>
    /// <param name="g">The actual generation</param>
    public delegate void GenerationEventHandler(int num, Generation g);

    /// <summary>
    /// Author: Andrzej Fedorowicz 1842644
    /// Version: 2020-04-13
    /// This class takes care of combining the Generation and Helpers classes to run a full genetic algorithm run of robby over a specified generation size
    /// </summary>
    public class RobbyRobotProblem
    {
        //A bunch of instance variables that will be used in the class
        private int numGenerations;
        private int popSize;
        private int numActions;
        private int numGenes;
        private int gridSize;
        private double eliteRate;
        private double mutationRate;
        private Generation currentG;
        public event GenerationEventHandler GenerationEvent;

        //This one is for the test grids
        private Contents[][,] contents;

        /// <summary>
        /// The constructor sets the needed instance variables
        /// </summary>
        /// <param name="numGenerations"></param>
        /// <param name="popSize"></param>
        /// <param name="f">This delegate is to calculate the allele score</param>
        /// <param name="numActions">Number of actions for the robot</param>
        /// <param name="numTestGrids"></param>
        /// <param name="gridSize"></param>
        /// <param name="numGenes"></param>
        /// <param name="eliteRate">How many elites should be taken from previous gen</param>
        /// <param name="mutationRate"></param>
        public RobbyRobotProblem(int numGenerations, int popSize, AlleleMoveAndFitness f, int numActions = 200, int numTestGrids = 100, int gridSize = 10, int numGenes = 243, double eliteRate = 0.05, double mutationRate = 0.05)
        {
            this.numGenerations = numGenerations;
            this.popSize = popSize;
            this.numActions = numActions;
            this.eliteRate = eliteRate;
            this.mutationRate = mutationRate;
            this.gridSize = gridSize;
            this.numGenes = numGenes;

            this.contents = new Contents[numTestGrids][,];

        }

        /// <summary>
        /// Start takes care of looping through the desired number of generations to be created and
        /// calls other methods to create them
        /// </summary>
        public void Start()
        {
            //Create the first generation and then evalutes all generations fitness, calls the event for a new gen, and creates a new gen
            this.currentG = new Generation(this.popSize, this.numGenes);
            for (int i = 0; i < numGenerations; i++)
            {
                this.EvalFitness(this.RobbyFitness);
                this.GenerationReplaced(i + 1, this.currentG);
                this.generateNextGeneration(Chromosome.DoubleCrossover);
            }
        }

        /// <summary>
        /// This takes care of creating the test grids for each generation and calling the method of evaluating the fitness
        /// </summary>
        /// <param name="f">The method to use to calculate the fitness</param>
        public void EvalFitness(Fitness f)
        {
            //Creating test grids
            for(int i = 0; i < this.contents.Length; i++)
            {
                this.contents[i] = Helpers.GenerateRandomTestGrid(this.gridSize);
            }
            //Calculating fitness of gen
            this.currentG.EvalFitness(f);

        }

        /// <summary>
        /// Method takes care of generating a new generation
        /// </summary>
        /// <param name="c">Method to calculate how to mutate</param>
        public void generateNextGeneration(Crossover c)
        {
            //Setting the elite from the previous generation
            Chromosome[] newGen = new Chromosome[this.popSize];
            for(int i = 0; i < (this.eliteRate * this.popSize); i++)
            {
                newGen[i] = this.currentG[i];
            }

            
            int indexerForGen = (int)(this.eliteRate * this.popSize);
            //Loops through the new gen and sets the chromosomes to offspring from the parents
            while(indexerForGen < this.popSize)
            {
                Chromosome firstParent = this.currentG.SelectParent();
                Chromosome secondParent = this.currentG.SelectParent();
                Chromosome[] mutated = firstParent.Reproduce(secondParent, c, this.mutationRate);
                for (int j = 0; j < mutated.Length; j++)
                {
                    newGen[indexerForGen] = mutated[j];
                    indexerForGen++;  
                }
            }

            //Setting the currentgeneration to the new gen
            this.currentG = new Generation(newGen);
        }

        /// <summary>
        /// This method takes care of calculating the fitness of each chromosome
        /// </summary>
        /// <param name="c">The chromosome to calculate the fitness for</param>
        /// <returns></returns>
        public double RobbyFitness(Chromosome c)
        {
            //Loops through each test grid and calculates the fitness
            int fitnessOfChro = 0;
            for(int i = 0; i < this.contents.Length; i++)
            {
                fitnessOfChro += Helpers.RunRobbyInGrid(this.contents[i], c, this.numActions, Helpers.ScoreForAllele);
            }

            //Return the average fitness
            return fitnessOfChro / (double)this.contents.Length;
        }
        
        /// <summary>
        /// This method invokes the event after each generation is created
        /// </summary>
        /// <param name="num">The generation number</param>
        /// <param name="g">The current generation</param>
        public void GenerationReplaced(int num, Generation g)
        {
           this.GenerationEvent.Invoke(num, g);
        }
    }
}
