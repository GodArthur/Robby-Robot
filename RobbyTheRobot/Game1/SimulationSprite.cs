﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobbyGeneticAlgo;
using GeneticAlgo;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Game1
{
    /// <summary>
    /// Authors: Andrzej Fedorowicz and Korjon Chang-Jones
    /// Version: 2020-04-13
    /// This class takes care of the updates and drawing for the game
    /// </summary>
    class SimulationSprite : DrawableGameComponent
    {
        //Instance variables needed throughout the game
        private Game1 game;
        private SpriteBatch spriteBatch;

        private Texture2D robotImg;
        private Texture2D tileImg;
        private Texture2D canImg;
        private Texture2D lineImg;
        private SpriteFont spriteFont;

        //Time of each instruction(each time update is called)
        private double time;

        private Chromosome[] allGenerations;
        private int genNumber;

        //This is for outputting the generation #
        String[] stringGens;

        
        private Contents[,] contents;

        private int count;
        private int score;
        private int numCans;
        private int[] robPosition;

        private Boolean isFinished = false;


        /// <summary>
        /// Constructor sets relevant instance variables
        /// </summary>
        /// <param name="game">The current game</param>
        public SimulationSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.count = 0;
            this.genNumber = 0;
            this.time = 0;
            //random number to store can
            numCans = 1;
        }

        /// <summary>
        /// Initializing non-graphical components
        /// </summary>
        public override void Initialize()
        {
            //Creating the grid and loading in the generations
            this.contents = Helpers.GenerateRandomTestGrid(10);
            this.allGenerations = new Chromosome[6];
            this.stringGens = new String[6];

            //this is just to order the files
            String[] allFiles = Directory.GetFiles("../../../../../RobbyGeneticAlgo/GenOutputs");
            String temp = allFiles[1];
            allFiles[1] = allFiles[3];
            allFiles[3] = temp;
            temp = allFiles[2];
            allFiles[2] = allFiles[3];
            allFiles[3] = temp;
            allFiles[3] = allFiles[4];
            allFiles[4] = temp;
            allFiles[4] = allFiles[5];
            allFiles[5] = temp;

            //Loading the generations
            for (int i = 0; i < allFiles.Length; i++)
            {
                allGenerations[i] = storeGeneration(allFiles[i]);
                int startIndex = allFiles[i].IndexOf('n', 40) + 1;
                int endIndex = allFiles[i].IndexOf('.', 20) - startIndex;
                this.stringGens[i] = allFiles[i].Substring(startIndex, endIndex);
            }

            //Setting robbys position
            robPosition = new int[]{ Helpers.rand.Next(0, contents.GetLength(0)), Helpers.rand.Next(0, contents.GetLength(0))};

            base.Initialize();

        }

        /// <summary>
        /// Load Content loads graphical components
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.canImg = game.Content.Load<Texture2D>("can");
            this.tileImg = game.Content.Load<Texture2D>("tile");
            this.robotImg = game.Content.Load<Texture2D>("robby");
            this.lineImg = game.Content.Load<Texture2D>("pokeball");
            
            this.spriteFont = this.game.Content.Load<SpriteFont>("scoreFont");

            base.LoadContent();
        }

        /// <summary>
        /// Update updates the game state each clock cycle
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //If the game is finished, it will wait and exit
            if(this.isFinished)
            {
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

            //Just so it's easier to look at robby we don't update is as fast
            if (time > 0.075)
            {
                //Pretty much these ifs handle whether or not the chromosome is finished and whether to calculate the fitness
                if (genNumber < allGenerations.Length)
                {
                    if(count == 200 && genNumber == allGenerations.Length - 1)
                    {
                        this.isFinished = true;
                    }
                    else if (count > 200)
                    {
                        count = 0;
                        genNumber++;
                        this.contents = Helpers.GenerateRandomTestGrid(10);
                        this.score = 0;
                        this.robPosition = new int[] { Helpers.rand.Next(0, contents.GetLength(0)), Helpers.rand.Next(0, contents.GetLength(0)) };

                    }
                    else
                    {
                        score += Helpers.ScoreForAllele(allGenerations[genNumber], contents, ref robPosition[0], ref robPosition[1]);
                        count++;
                    }
                    time = 0;
                }
                
            }

            time += gameTime.ElapsedGameTime.TotalSeconds;


            base.Update(gameTime);
        }

        /// <summary>
        /// Draw draws the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            
            int x = 0; 
            int y = 0;
            

            spriteBatch.Begin();

            //If the game is still going on continue drawing
            if (!this.isFinished)
            {
                numCans = 0;
                //So pretty much we draw the grid full of tiles first
                for (int i = 0; i < contents.GetLength(0); i++)
                {
                    for (int j = 0; j < contents.GetLength(1); j++)
                    {
                        spriteBatch.Draw(tileImg, new Rectangle(x * 50, y * 50, 50, 50), Color.White);
                        x++;
                    }
                    y++;
                    x = 0;
                }

                x = 0; y = 0;
                //Then we draw the tiles and cans(so the cans look on top tiles)
                for (int i = 0; i < contents.GetLength(0); i++)
                {
                    for (int j = 0; j < contents.GetLength(1); j++)
                    {
                        Contents content = contents[j, i];

                        switch (content)
                        {
                            case Contents.Empty:

                                x++;
                                break;

                            case Contents.Can:

                                spriteBatch.Draw(canImg, new Rectangle(x * 50, y * 50, 50, 50), Color.White);
                                numCans++;
                                x++;
                                break;

                        }
                    }
                    y++;
                    x = 0;

                }


                spriteBatch.Draw(robotImg, new Rectangle(robPosition[0] * 50, robPosition[1] * 50, 50, 50), Color.White);

                //This is for information output
                if (count <= 200)
                {
                    this.spriteBatch.DrawString(this.spriteFont, "Current generation: " + this.stringGens[this.genNumber], new Vector2(50, 540), Color.AliceBlue);
                    this.spriteBatch.DrawString(this.spriteFont, "Move: " + this.count + "/200", new Vector2(50, 560), Color.AliceBlue);
                    this.spriteBatch.DrawString(this.spriteFont, "Points: " + this.score + "/500", new Vector2(50, 580), Color.AliceBlue);
                }

                //If the generation is done, it stops for the user to see the final result
                if (this.count > 200 || (this.count > 200 && numCans == 0))
                {
                    Thread.Sleep(2000);
                }

            }
            else
            {
                //If the game is done, an end screen is drawn
                Thread.Sleep(2000);
                GraphicsDevice.Clear(Color.Black);
                this.spriteBatch.DrawString(this.spriteFont, "The End!" , new Vector2(50, 300), Color.AliceBlue);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// This method is used to get the generation as a chromosome from the file
        /// </summary>
        /// <param name="path">File to get from</param>
        /// <returns>The created chromosome</returns>
        public Chromosome storeGeneration(string path)
        {
            String[] gen = File.ReadAllText(path).Split(',');
            Allele[] pattern = new Allele[gen.Length];

            for (int i = 0; i < gen.Length; i++)
            {
                pattern[i] = (Allele)Enum.Parse(typeof(Allele), gen[i]);
            }

            return new Chromosome(pattern);
        }
    }


}
