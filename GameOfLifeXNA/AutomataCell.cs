using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGrid;

namespace GameOfLifeXNA
{
    class AutomataCell:GridCell
    {
        public bool State{ get; set;}
        public Color OnColor { get; set; }
        public Color OffColor { get; set; }

        private Texture2D pixel;


        public AutomataCell(Game game, SpriteBatch spriteBatch, Point gridPosition, Point screenCoordinates, int width, int height, bool state, Color onColor, Color offColor) : base(game,spriteBatch, gridPosition, screenCoordinates, width, height)
        {
            OnColor = onColor;
            OffColor = offColor;
            State = state;
            pixel = new Texture2D(game.GraphicsDevice,1,1,1,TextureUsage.None,SurfaceFormat.Color);
            pixel.SetData(new[] {Color.White});
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(pixel, new Rectangle(ScreenCoordinates.X, ScreenCoordinates.Y,Width,Height), State ? OnColor : OffColor);
            base.Draw(gameTime);
        }
    }
}
