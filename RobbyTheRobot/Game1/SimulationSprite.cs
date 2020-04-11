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

        //private SpriteFont spriteFont;
        private Contents[,] contents;

        private int count;



        public SimulationSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.count = 0;

        }

        public override void Initialize()
        {
            //RobbyRobotProblem robby = new RobbyRobotProblem(4000, 200, Helpers.ScoreForAllele);

            contents = Helpers.GenerateRandomTestGrid(10);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.canImg = game.Content.Load<Texture2D>("can");
            this.tileImg = game.Content.Load<Texture2D>("tile");
            this.robotImg = game.Content.Load<Texture2D>("robby");
            this.lineImg = game.Content.Load<Texture2D>("pokeball");
            //this.spriteFont = this.game.Content.Load<SpriteFont>("scoreFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            

            count++;



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
                    Contents content = contents[i,j];

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

                        case Contents.Wall:

                            spriteBatch.Draw(lineImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                            break;

                    }
                }
                y++;
                x = 0;
            }
         
 
            
            //spriteBatch.Draw(canImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }


}
