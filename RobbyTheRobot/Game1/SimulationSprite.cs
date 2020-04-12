using System;
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


namespace Game1
{
    class SimulationSprite : DrawableGameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;

        private Texture2D robotImg;
        private Texture2D tileImg;
        private Texture2D canImg;
        private Texture2D lineImg;
        private SpriteFont spriteFont;

        private double time;

        private Chromosome[] allGenerations;
        private int genNumber;


        
        private Contents[,] contents;

        private int count;
        private int score;
        private int[] robPosition;



        public SimulationSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.count = 0;
            this.genNumber = 0;
            this.time = 0;
        }

        public override void Initialize()
        {
            //RobbyRobotProblem robby = new RobbyRobotProblem(4000, 200, Helpers.ScoreForAllele);

            contents = Helpers.GenerateRandomTestGrid(10);

            allGenerations = new Chromosome[6];



            String[] allFiles = Directory.GetFiles("../../../../../RobbyGeneticAlgo/GenOutputs");

            for (int i = 0; i < allFiles.Length; i++)
            {
                allGenerations[i] = storeGeneration(allFiles[i]);
            }

        
            robPosition = new int[]{ Helpers.rand.Next(0, contents.GetLength(0)), Helpers.rand.Next(0, contents.GetLength(0))};

            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.canImg = game.Content.Load<Texture2D>("can");
            this.tileImg = game.Content.Load<Texture2D>("tile");
            this.robotImg = game.Content.Load<Texture2D>("robby");
            this.lineImg = game.Content.Load<Texture2D>("pokeball");
            
            //gen = File.ReadAllLines
            //this.spriteFont = this.game.Content.Load<SpriteFont>("scoreFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (time > 1)
            {
                if (count == 200)
                {
                    count = 0;
                    genNumber++;
                    contents = Helpers.GenerateRandomTestGrid(10);
                }

                if (genNumber < allGenerations.Length)
                {
                    score += Helpers.ScoreForAllele(allGenerations[genNumber], contents, ref robPosition[0], ref robPosition[1]);
                    count++;
                }

                time = 0;
            }

            time += gameTime.ElapsedGameTime.TotalSeconds;


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Not sure if I should choose random values
            // or set them
            int x = 0; //Helpers.rand.Next(0, 10);
            int y = 0; //Helpers.rand.Next(0, 10);

            spriteBatch.Begin();

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    spriteBatch.Draw(tileImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                    x++;
                }
                y++;
                x = 0;
            }

            x = 0; y = 0;

            for (int i = 0; i < contents.GetLength(0); i++)
            {
                for (int j = 0; j < contents.GetLength(1); j++)
                {
                    Contents content = contents[i, j];

                    switch (content)
                    {
                        case Contents.Empty:

                            spriteBatch.Draw(tileImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                            break;

                        case Contents.Can:

                            spriteBatch.Draw(canImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                            break;

                    }
                }
                y++;
                x = 0;

            }

            
                spriteBatch.Draw(robotImg, new Rectangle(robPosition[0] * 32, robPosition[1] * 32, 32, 32), Color.White);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }

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
