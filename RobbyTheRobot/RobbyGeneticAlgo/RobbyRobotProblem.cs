using System;
using System.Collections.Generic;
using System.Text;

namespace RobbyGeneticAlgo
{
    public class RobbyRobotProblem
    {
        //no idea if this is right. probably not
        private int numGenerations;
        private int popSize;
        private int numActions;
        private int numGenes;
        //private int numTestGrids;
        private int gridSize;
        private double eliteRate;
        private double mutationRate;

        private Contents[,] contents;

        public RobbyRobotProblem(int numGenerations, int popSize, AlleleMoveAndFitness f, int numActions, int numTestGrids, int gridSize, int numGenes, double eliteRate, double mutationRate)
        {
            this.numGenerations = numGenerations;
            this.popSize = popSize;
            this.numActions = numActions;
            this.gridSize = gridSize;
            this.eliteRate = eliteRate;
            this.mutationRate = mutationRate;

            contents = new Contents[gridSize, gridSize];

        }

        public void Start()
        {
            Generation g = new Generation(popSize, numGenes);

            for (int i = 0; i < numGenerations; i++)
            {
                //g.EvalFitness
            }
        }
        
    }
}
