using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgo;

namespace RobbyGeneticAlgo
{

    public delegate void GenerationEventHandler(int num, Generation g);
    public class RobbyRobotProblem
    {
        //no idea if this is right. probably not
        private int numGenerations;
        private int popSize;
        private int numActions;
        private int numGenes;
        private int gridSize;
        private double eliteRate;
        private double mutationRate;
        private Generation currentG;
        public event GenerationEventHandler GenerationEvent;

        private Contents[][,] contents;

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

        public void Start()
        {
            this.currentG = new Generation(this.popSize, this.numGenes);
            for (int i = 0; i < numGenerations; i++)
            {
                this.EvalFitness(this.RobbyFitness);
                this.GenerationReplaced(i, this.currentG);
                this.generateNextGeneration();
            }
        }

        public void EvalFitness(Fitness f)
        {
            for(int i = 0; i < this.contents.Length; i++)
            {
                this.contents[i] = Helpers.GenerateRandomTestGrid(this.gridSize);
            }
            this.currentG.EvalFitness(RobbyFitness);

        }

        public void generateNextGeneration()
        {
            Chromosome[] newGen = new Chromosome[this.popSize];
            for(int i = 0; i < (this.eliteRate * this.popSize); i++)
            {
                newGen[i] = this.currentG[i];
            }

            Chromosome firstParent = this.currentG.SelectParent();
            Chromosome secondParent = this.currentG.SelectParent();

            for(int i = (int)(this.eliteRate * this.popSize); i < newGen.Length; i+=2)
            {
                Chromosome[] mutated = firstParent.Reproduce(secondParent, firstParent.DoubleCrossover, this.mutationRate);
                for (int j = 0; j < mutated.Length; j++)
                {
                    newGen[(int)(this.eliteRate * this.popSize)] = mutated[j];
                }
            }

            this.currentG = new Generation(newGen);
        }

        public double RobbyFitness(Chromosome c)
        {
            int fitnessOfChro = 0;
            for(int i = 0; i < this.contents.Length; i++)
            {
                fitnessOfChro += Helpers.RunRobbyInGrid(this.contents[i], c, this.numActions, Helpers.ScoreForAllele);
            }

            return fitnessOfChro / this.contents.Length;
        }
        
        public void GenerationReplaced(int num, Generation g)
        {
           this.GenerationEvent.Invoke(num, g);
        }
    }
}
