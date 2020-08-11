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

    public void AddRadar(Radar radar, Cell centre, int range)
    {
        Vector3 location = centre.transform.localPosition;
        // Make a circle and test which cells fall within it!
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                bool inCircle = 
                    (i - location.x) * (i - location.x) + 
                    (j - location.y) * (j - location.y) <= 
                    (range * range);
                if (inCircle) {
                    grid[i, j].AddRadar(radar);
                }
            }
        }
    }

    public void RemoveRadar(Radar radar, Cell centre, int range)
    {
        Vector3 location = centre.transform.localPosition;
        // Make a circle and test which cells fall within it!
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                bool inCircle = 
                    (i - location.x) * (i - location.x) + 
                    (j - location.y) * (j - location.y) <= 
                    (range * range);
                if (inCircle) {
                    grid[i, j].RemoveRadar(radar);
                }
            }
        }
    }

    public Vector3 CentrePoint()
    {
        return new Vector3(width * 0.5f - 0.5f, height * 0.5f - 0.5f, -10f);
    }

    public float ScreenSize()
    {
        float widthRequirement = width / 16f;
        float heightRequirement = height / 9f;
        return Mathf.Max(widthRequirement, heightRequirement) * 4.5f;
    }

    public Cell GetCell(int x, int y)
    {
        return grid[x, y];
    }

    public void ClearObscurations()
    {
        foreach (Cell cell in grid) {
            cell.SetObscuredNext(false);
        }
    }

    public void Recolour(int radarCount)
    {
        foreach (Cell cell in grid) {
            cell.Recolour(radarCount);
        }
    }
}
