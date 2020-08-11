using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    North,
    East,
    South,
    West
}

public class Cell : MonoBehaviour
{
    public Cell[] neighbours;
    // Start is called before the first frame update
    void Awake()
    {
        neighbours = new Cell[4];
    }

    public void SetNeighbour(Directions dir, Cell cell) {
        neighbours[(int)dir] = cell;
    }

    public bool IsNeighbour(Cell cell)
    {
        foreach (Cell c in neighbours) {
            if (c == cell) {
                return true;
            }
        }
        return false;
    }
}
