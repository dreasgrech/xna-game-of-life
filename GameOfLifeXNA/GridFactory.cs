using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLifeXNA
{
    class GridFactory
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private readonly Random random;

        public int CellSize { get; set; }
        public Rectangle Position { get; set; }
        public Color LineColor { get; set; }
        public Color CellOnColor { get; set; }
        public Color CellOffColor { get; set; }

        public GridFactory(Game game, SpriteBatch spriteBatch, int cellSize, Rectangle position, Color lineColor, Color onColor, Color offColor)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            CellSize = cellSize;
            Position = position;
            LineColor = lineColor;
            CellOnColor = onColor;
            CellOffColor = offColor;
            random = new Random();
        }

        public PlaygroundGrid NewEmptyGrid()
        {
            return new PlaygroundGrid(game,spriteBatch,CellSize,Position,LineColor,(gridPosition,screenPosition, size) => new AutomataCell(game,spriteBatch, gridPosition, screenPosition, size, size, false, CellOnColor,CellOffColor));
        }

        public PlaygroundGrid NewRandomizedGrid(double lifeDistribution)
        {
            return new PlaygroundGrid(game,spriteBatch,CellSize,Position,LineColor, (gridPosition, screenPosition, size) => new AutomataCell(game, spriteBatch, gridPosition, screenPosition, size, size, random.NextDouble() < lifeDistribution ? true : false, CellOnColor, CellOffColor));
        }
    }
}
