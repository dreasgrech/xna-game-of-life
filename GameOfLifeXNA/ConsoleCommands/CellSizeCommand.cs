using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameConsole;

namespace GameOfLifeXNA.ConsoleCommands
{
    class CellSizeCommand:IConsoleCommand
    {
        private readonly PlaygroundGrid grid;
        public CellSizeCommand(PlaygroundGrid grid)
        {
            this.grid = grid;
        }
        public string Execute(string[] arguments)
        {
            grid.ChangeGridSize(int.Parse(arguments[0]));
            return "Cell size changed";
        }

        public string Name
        {
            get { return "cellsize"; }
        }

        public string Description
        {
            get { return "Changes the size of the cells of the grid"; }
        }
    }
}
