using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Maze
{
    //????? ???????????????? ?????????
    public class MazeSpawner : MonoBehaviour
    {
        public Cell CellPrefab;
        public SpawnPoint SpawnPoint;
        public FinishPosition FinishPoint;
        public GameObject DeadPoint;

        public Vector3 CellSize = new Vector3(1, 1, 0);

        public Maze maze;
        public PathRenderer PathRenderer;

        private void Start()
        {
            MazeGenerator generator = new MazeGenerator();
            maze = generator.GenerateMaze();//???????? ?????????
            GenerateMaze(generator, maze);

            PathRenderer.DrawPath();
                
        }
        //???????
        private void GenerateMaze(MazeGenerator generator, Maze maze)
        {
            for (int x = 0; x < maze.cells.GetLength(0); x++)
            {
                for (int y = 0; y < maze.cells.GetLength(1); y++)
                {
                    Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity).GetComponent<Cell>();

                    //?????????? ????? ? ??????????? ?? ?????????
                    c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                    c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
                    c.Floor.SetActive(maze.cells[x, y].Floor);

                    //??????? ????? ?????
                    if (x == 0 && y == 0)
                    {

                        Instantiate(SpawnPoint, c.transform);
                        GameController.Instance.SpawnPoint = c.transform;
                    }

                    //??????? ?????
                    if (x == generator.Width - 2 && y == generator.Height - 2)
                    {
                        Instantiate(FinishPoint, c.transform);
                        GameController.Instance.FinishPoint = c.transform;
                    }

                    //??????? ???? ??????
                    if (x != 0 && y != 0 && x != generator.Width - 2 && y != generator.Height - 2)
                    {
                        if (y != generator.Height - 1 && x != generator.Width - 1)
                        {
                            int rand = Random.Range(0, GameController.Instance.Difficulty + 1);
                            if (rand == 0) Instantiate(DeadPoint, c.transform);
                        }      
                    }
                }
            }
        }
    }
}