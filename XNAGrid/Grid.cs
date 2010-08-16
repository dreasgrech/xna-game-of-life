using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAGrid
{
    public class Grid<T> : DrawableGameComponent, IEnumerable<T> where T:GridCell
    {
        /// <summary>
        /// Size of each grid cell, in pixels
        /// </summary>
        public int CellSize { get; protected set; }

        /// <summary>
        /// The position of where the grid is drawn on screen and its size
        /// </summary>
        public Rectangle Position { get; set; }

        /// <summary>
        /// If true, the grid lines are drawn
        /// </summary>
        public bool DrawLines{ get; set;}

        /// <summary>
        /// Returns the number of cells that are in the grid, horizontally and vertically
        /// </summary>
        public Point Size
        {
            get
            {
                return new Point(Position.Width / CellSize, Position.Height / CellSize);
            }
        }

        public Color LineColor { get; set; }

        protected SpriteBatch SpriteBatch{ get; set;}
        private readonly Texture2D pixel;
        protected T[,] cells;
        protected Func<Point, Point, int, T> InitCells{ get; set;}

        public Grid(Game game, SpriteBatch spriteBatch, int cellSize, Rectangle position, Color lineColor, Func<Point,Point,int,T> initCells) : base(game)
        {
            SpriteBatch = spriteBatch;
            CellSize = cellSize;
            Position = position;
            LineColor = lineColor;
            pixel = new Texture2D(game.GraphicsDevice, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            pixel.SetData(new[] { LineColor });
            InitCells = initCells;
            Init(initCells);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    yield return cells[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            DrawCells(gameTime);
            if (DrawLines)
            {
                DrawGridLines();
            }
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        protected void Init(Func<Point,Point,int,T> cellInit)
        {
            cells = new T[Size.X,Size.Y];
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    cells[i, j] = cellInit(new Point(i, j), new Point(Position.X + i*CellSize, Position.Y + j*CellSize), CellSize);
                }
            }
        }

        public IEnumerable<T> GetAdjacentCells(T cell)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var adjacentCell = CellAtPosition(cell.GridPostion.X + i, cell.GridPostion.Y + j);
                    if (adjacentCell != null && adjacentCell != cell)
                    {
                        yield return adjacentCell;
                    }
                }
            }
        }

        /// <summary>
        /// Finds the cell at the specified screen coordinates
        /// </summary>
        /// <param name="x">X screen coordinate</param>
        /// <param name="y">Y screen coordinate</param>
        /// <returns>Returns null if the grid is not in the screen coordinate</returns>
        public T CellAtCoordinate(int x, int y)
        {
            if(!IsCoordinateValid(x, y))
            {
                return null;
            }
            var position = PositionAtCoordinate(x, y);
            return CellAtPosition(position.X, position.Y);
        }

        /// <summary>
        /// Finds the cell at the specified grid position
        /// </summary>
        /// <param name="x">X ordinal (0 based)</param>
        /// <param name="y">Y ordinal (0 based)</param>
        /// <returns>Returns null if the position is not part of the grid</returns>
        public T CellAtPosition(int x, int y)
        {
            return IsPositionValid(new Point(x, y)) ? cells[x, y] : null;
        }

        /// <summary>
        /// Finds the grid position at the given screen coordinate
        /// </summary>
        /// <param name="x">X screen coordinate</param>
        /// <param name="y">Y screen coordinate</param>
        /// <returns>Returns the grid position at the specified screen coordinate</returns>
        Point PositionAtCoordinate(float x, float y)
        {
            x -= Position.X;
            y -= Position.Y;
            return new Point((int)Math.Ceiling(Convert.ToDouble(Size.X * x / Position.Width)) - 1, (int)Math.Ceiling(Convert.ToDouble(Size.Y * y / Position.Height)) - 1);
        }

        /// <summary>
        /// Returns true if the inputted position is part of the grid
        /// </summary>
        /// <param name="position">X, Y coordinates of the grid</param>
        /// <returns></returns>
        bool IsPositionValid(Point position)
        {
            return position.X >= 0 && position.X < Size.X && position.Y >= 0 && position.Y < Size.Y;
        }

        /// <summary>
        /// Returns true if the grid is in the inputted screen coordinates 
        /// </summary>
        /// <param name="x">X screen coordinate</param>
        /// <param name="y">Y screen coordinate</param>
        /// <returns></returns>
        bool IsCoordinateValid(int x, int y)
        {
            return x >= Position.X && x <= Position.Width && y >= Position.Y && y <= Position.Height;
        }

        void DrawGridLines()
        {
            for (int i = 0; i <= Size.X; i++) //Vertical lines
            {
                SpriteBatch.Draw(pixel, new Rectangle(Position.X + i * CellSize, Position.Y, 1, Position.Height), LineColor);
            }
            for (int i = 0; i <= Size.Y; i++) //Horizontal lines
            {
                SpriteBatch.Draw(pixel,new Rectangle(Position.X,Position.Y + i *CellSize,Position.Width,1),LineColor);
            }
        }
        
        void DrawCells(GameTime gameTime)
        {
            foreach (var cell in this)
            {
                cell.Draw(gameTime);
            }
        }
    }
}
