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


namespace Game1
{
    class SimulationSprite : DrawableGameComponent
    {
        private Game1 game;
        private SpriteBatch spriteBatch;

        private Texture2D robotImg;
        private Texture2D tileImg;
        private Texture2D canImg;

        private SpriteFont spriteFont;

        private int count;



        public SimulationSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.count = 0;

        }

        public override void Initialize()
        {
            RobbyRobotProblem robby = new RobbyRobotProblem(4000, 200, Helpers.ScoreForAllele);


            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.canImg = game.Content.Load<Texture2D>("can");
            this.tileImg = game.Content.Load<Texture2D>("tile");
            this.robotImg = game.Content.Load<Texture2D>("robby");
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
            int x = Helpers.rand.Next(0, 10);
            int y = Helpers.rand.Next(0, 10);

            spriteBatch.Begin();
            spriteBatch.Draw(canImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }


}
