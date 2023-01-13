using Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Maze
{
    //Спавн сгенерированного лабиринта
    public class MazeSpawner : MonoBehaviour
    {
        public Cell CellPrefab;
        public SpawnPoint SpawnPoint;
        public FinishPosition FinishPoint;
        public GameObject DeadPoint;

        public Vector3 CellSize = new Vector3(1, 1, 0);

        private void Start()
        {
            MazeGenerator generator = new MazeGenerator();
            MazeGeneratorCell[,] maze = generator.GenerateMaze();//Вызываем генератор
            GenerateMaze(generator, maze);

           

                
        }
        //Создаем
        private void GenerateMaze(MazeGenerator generator, MazeGeneratorCell[,] maze)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity).GetComponent<Cell>();

                    //Активируем стены в зависимости от генерации
                    c.WallLeft.SetActive(maze[x, y].WallLeft);
                    c.WallBottom.SetActive(maze[x, y].WallBottom);
                    c.Floor.SetActive(maze[x, y].Floor);

                    //Спавним спавн поинт
                    if (x == 0 && y == 0)
                    {

                        Instantiate(SpawnPoint, c.transform);
                        GameController.Instance.SpawnPoint = c.transform;
                    }

                    //Спавним Финиш
                    if (x == generator.Width - 2 && y == generator.Height - 2)
                    {
                        Instantiate(FinishPoint, c.transform);
                        GameController.Instance.FinishPoint = c.transform;
                    }

                    //Спавним зоны смерти
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