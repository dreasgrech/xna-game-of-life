using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNAGameConsole;

namespace GameOfLifeXNA.ConsoleCommands
{
    class StepGenerationCommand:IConsoleCommand
    {
        private PlaygroundGrid grid;
        public StepGenerationCommand(PlaygroundGrid grid)
        {
            this.grid = grid;
        }

        public string Execute(string[] arguments)
        {
            grid.Step();
            return "Stepped generation";
        }

        public string Name
        {
            get { return "step"; }
        }

        public string Description
        {
            get { return "Steps to the next generation"; }
        }
    }
}
