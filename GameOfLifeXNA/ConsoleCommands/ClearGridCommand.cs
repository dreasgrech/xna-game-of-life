using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameConsole;

namespace GameOfLifeXNA.ConsoleCommands
{
    class ClearGridCommand:IConsoleCommand
    {
        private PlaygroundGrid grid;
        public ClearGridCommand(PlaygroundGrid grid)
        {
            this.grid = grid;
        }

        public string Execute(string[] arguments)
        {
            grid.Clear();
            return "Grid cleared";
        }

        public string Name
        {
            get { return "reset"; }
        }

        public string Description
        {
            get { return "Clears the grid (kills all the cells)"; }
        }
    }
}
