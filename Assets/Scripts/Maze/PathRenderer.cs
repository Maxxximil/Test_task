using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Maze
{
    public class PathRenderer : MonoBehaviour
    {
        public MazeSpawner MazeSpawner;

        private LineRenderer componentLineRenderer;

        private void Start()
        {
            componentLineRenderer = GetComponent<LineRenderer>();
            
        }

        public void DrawPath()
        {
            Maze maze = MazeSpawner.maze;
            Vector2Int currentPosition = MazeSpawner.maze.finishPosition;
            List<Vector3> positions = new List<Vector3>(); 


            while (currentPosition != Vector2Int.zero)
            {
                int x = currentPosition.x;
                int y = currentPosition.y;
                positions.Add(new Vector3(x * MazeSpawner.CellSize.x, y * MazeSpawner.CellSize.y + 1, y * MazeSpawner.CellSize.z));

                MazeGeneratorCell currentCell = maze.cells[x, y];

                if (x > 0 && !currentCell.WallLeft &&
                    maze.cells[x - 1, y].DistanceFromStart ==
                    currentCell.DistanceFromStart - 1)
                {
                    currentPosition.x--;
                }
                else if(y > 0 && !currentCell.WallBottom &&
                    maze.cells[x, y - 1].DistanceFromStart ==
                    currentCell.DistanceFromStart - 1)
                {
                    currentPosition.y--;
                }
                else if(x < maze.cells.GetLength(0) - 1 && !maze.cells[x + 1, y].WallLeft &&
                    maze.cells[x + 1, y].DistanceFromStart ==
                    currentCell.DistanceFromStart - 1)
                {
                    currentPosition.x++;
                }
                else if (y < maze.cells.GetLength(1) - 1 && !maze.cells[x, y + 1].WallBottom &&
                    maze.cells[x, y + 1].DistanceFromStart ==
                    currentCell.DistanceFromStart - 1)
                {
                    currentPosition.y++;
                }

                
            }
            positions.Add(Vector3.zero);
            componentLineRenderer.positionCount = positions.Count;
            componentLineRenderer.SetPositions(positions.ToArray());
        }
    }
}