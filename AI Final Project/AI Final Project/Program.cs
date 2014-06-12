using System;

namespace AI_Final_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (project_driver game = new project_driver())
            {
                game.Run();
            }
        }
    }
}

