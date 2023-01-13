using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Maze
{
    public class MazeGenerator
    {
        public int Width = 15;
        public int Height = 15;

        //√енераци€ всех €чеек лабиринта
        public MazeGeneratorCell[,] GenerateMaze()
        {
            MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
                }
            }

            for (int x = 0; x < maze.GetLength(0); x++)
            {
                maze[x, Height - 1].WallLeft = false;
                maze[x, Height - 1].Floor = false;
            }

            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[Width - 1, y].WallBottom = false;
                maze[Width - 1, y].Floor = false;
            }

                RemoveWallsWithBacktrecker(maze);
            

            return maze;
        }

        //ѕосле генерации всех €чеек убираем стены по алгоритму
        private void RemoveWallsWithBacktrecker(MazeGeneratorCell[,] maze)
        {
            MazeGeneratorCell current = maze[0, 0];
            current.Visited = true;

            Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
            do
            {
                List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();


                int x = current.X;
                int y = current.Y;


                if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
                if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
                if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
                if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

                if (unvisitedNeighbours.Count > 0)
                {
                    MazeGeneratorCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                    RemoveWall(current, chosen);
                    chosen.Visited = true;
                    stack.Push(chosen);
                    current = chosen;
                }
                else
                {
                    current = stack.Pop();
                }
            } while (stack.Count > 0);
        }
        //—крипт убирани€ стены
        private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
        {
            if(a.X == b.X)
            {
                if (a.Y > b.Y)
                {
                    a.WallBottom = false;
                }
                else b.WallBottom = false;

            }
            else
            {
                if (a.X > b.X) a.WallLeft = false;
                else b.WallLeft = false;
            }
        }
    }
}