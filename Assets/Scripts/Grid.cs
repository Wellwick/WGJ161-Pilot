using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width, height;
    public GameObject cell;
    private Cell[,] grid;

    // Start is called before the first frame update
    void Awake()
    {
        grid = new Cell[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                grid[i, j] = Instantiate(cell, transform).GetComponent<Cell>();
                grid[i, j].transform.localPosition = new Vector3(i, j);
            }
        }
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (i > 0) {
                    grid[i, j].SetNeighbour(Directions.West, grid[i - 1, j]);
                }
                if (i < width - 1) {
                    grid[i, j].SetNeighbour(Directions.East, grid[i + 1, j]);
                }
                if (j > 0) {
                    grid[i, j].SetNeighbour(Directions.South, grid[i, j - 1]);
                }
                if (j < height - 1) {
                    grid[i, j].SetNeighbour(Directions.North, grid[i, j + 1]);
                }
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        return grid[x, y];
    }
}
