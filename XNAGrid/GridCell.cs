using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAGrid
{
    public class GridCell
    {
        public Point GridPostion { get; set; }
        public Point ScreenCoordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        protected Game Game{ get; set;}
        protected SpriteBatch SpriteBatch{ get; set;}

        public GridCell(Game game, SpriteBatch spriteBatch, Point gridPosition, Point screenCoordinates, int width, int height)
        {
            Game = game;
            SpriteBatch = spriteBatch;
            GridPostion = gridPosition;
            ScreenCoordinates = screenCoordinates;
            Width = width;
            Height = height;
        }

        public bool IsOrthagonalWith(GridCell otherCell)
        {
            return GridPostion.X == otherCell.GridPostion.X || GridPostion.Y == otherCell.GridPostion.Y;
        }

        public virtual void Draw(GameTime gameTime) {}
        public virtual void Update(GameTime gameTime) {}
    }
}
