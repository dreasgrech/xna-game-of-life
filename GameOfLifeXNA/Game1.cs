using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNAGameConsole;
using XNAGrid;
using XNAMouse;
using XNASingleStroke;
using GameOfLifeXNA.ConsoleCommands;

namespace GameOfLifeXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private PlaygroundGrid grid;
        private MouseCursor cursor;
        private SingleKeyStroke singleStrokeHandler;
        private GameConsole console;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var gridFactory = new GridFactory(this, spriteBatch, 50, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White, Color.DarkGreen, Color.Black);
            grid = gridFactory.NewEmptyGrid();
            grid.DrawLines = true;
            cursor = new MouseCursor(this, spriteBatch, Content.Load<Texture2D>("cursor"));
            cursor.LeftClick += cursor_LeftClick;
            cursor.LeftDrag += cursor_LeftClick;
            cursor.RightClick += cursor_RightClick;
            cursor.RightDrag += cursor_RightClick;
            singleStrokeHandler = new SingleKeyStroke(this);
            singleStrokeHandler.KeyDown += singleStrokeHandler_KeyDown;
            Components.Add(grid);
            Components.Add(cursor);
            console = new GameConsole(this, spriteBatch, new IConsoleCommand[] { new ClearGridCommand(grid), new StepGenerationCommand(grid), new FullScreenCommand(graphics), new CellSizeCommand(grid) }, new GameConsoleOptions(){FontColor=Color.Crimson});

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Components.Add(singleStrokeHandler);
            // TODO: use this.Content to load your game content here
        }

        void singleStrokeHandler_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (console.Opened)
            {
                return;
            }
            if (e.Key == Keys.Enter)
            {
                if (grid.IsActive)
                {
                    grid.Stop();
                } else
                {
                    grid.Start();
                }
            }
            if (e.Key == Keys.Space || e.Key == Keys.Right)
            {
                if (!grid.IsActive)
                {
                    grid.Step();
                }
            }
            if (e.Key == Keys.Tab)
            {
                grid.DrawLines = !grid.DrawLines;
            }
        }

        void cursor_RightClick(object sender, MouseClickEventArgs e)
        {
            var cell = grid.CellAtCoordinate(e.Position.X, e.Position.Y);
            if (cell == null)
            {
                return;
            }
            cell.State = false;
        }
        void cursor_LeftClick(object sender, MouseClickEventArgs e)
        {
            var cell = grid.CellAtCoordinate(e.Position.X, e.Position.Y);
            if (cell == null)
            {
                return;
            }
            cell.State = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
