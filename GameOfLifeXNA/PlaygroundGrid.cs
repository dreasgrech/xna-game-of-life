using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGrid;

namespace GameOfLifeXNA
{
    class PlaygroundGrid:Grid<AutomataCell>
    {
        public int CurrentGeneration { get; private set; }
        /// <summary>
        /// Returns true if the grid is currently active (stepping through generations)
        /// </summary>
        public bool IsActive
        {
            get
            {
                return Enabled;
            }
        }
        public int Interval{ get; set;}
        private double lastUpdate;
        //private LinkedList<List<Point>> history;

        public PlaygroundGrid(Game game, SpriteBatch spriteBatch, int cellSize, Rectangle position, Color lineColor, Func<Point,Point,int,AutomataCell> initCells) : base(game, spriteBatch, cellSize, position, lineColor, initCells)
        {
            Interval = 100;
            Stop();
            //history = new LinkedList<List<Point>>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var now = gameTime.TotalGameTime.TotalMilliseconds;
            if (now - lastUpdate < Interval)
            {
                return;
            }
            lastUpdate = now;
            Step();
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        /// <summary>
        /// Advance to the next generation
        /// </summary>
        public void Step()
        {
            CurrentGeneration++;
            bool[,] newGeneration = GetNewGenerationStates();
            SetNewStates(newGeneration);
        }

        /// <summary>
        /// Kills all the cells
        /// </summary>
        public void Clear()
        {
            foreach (var cell in this)
            {
                cell.State = false;
            }
        }

        bool[,] GetNewGenerationStates()
        {
            //LinkedList<Point> newG = new LinkedList<Point>();
            var newGeneration = new bool[Size.X,Size.Y];
            foreach (var cell in this)
            {
                var liveNeighbourCount = GetAdjacentCells(cell).Where(c => c.State).Count();
                if (liveNeighbourCount == 2 || liveNeighbourCount == 3)
                {
                    var newState = liveNeighbourCount == 3 || cell.State;
                    newGeneration[cell.GridPostion.X, cell.GridPostion.Y] = newState;
                    //if (newState)
                    //{
                    //    newG.AddLast(cell.GridPostion);
                    //}
                }
            }
            return newGeneration;
        }

        void SetNewStates(bool[,] newGeneration)
        {
            for (var i = 0; i < Size.X; i++)
            {
                for (var j = 0; j < Size.Y; j++)
                {
                    cells[i, j].State = newGeneration[i, j];
                }
            }
        }

        public void ChangeGridSize(int newSize)
        {
            CellSize = newSize;
            Init(InitCells);
        }
    }
}
