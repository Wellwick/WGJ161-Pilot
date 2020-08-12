using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell[] neighbours;
    
    public bool obscuredNext;
    private bool trail;

    private List<Radar> inRange;

    // Start is called before the first frame update
    void Awake()
    {
        neighbours = new Cell[4];
        obscuredNext = false;
        trail = false;
        inRange = new List<Radar>();
    }

    public void Recolour(int radarCount)
    {
        if (radarCount == 0) {
            GetComponent<MeshRenderer>().materials[0].color = Color.white;
        } else {
            GetComponent<MeshRenderer>().materials[0].color = Color.Lerp(Color.white, Color.red, (float)inRange.Count/radarCount*0.8f);
        }
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

    public Cell GetNeighbour(Directions direction)
    {
        return neighbours[(int)direction];
    }

    public Directions GetDirection(Cell cell)
    {
        foreach (Directions direction in System.Enum.GetValues(typeof(Directions))) {
            if (neighbours[(int)direction] == cell) {
                return direction;
            }
        }
        // This should never happen!
        return 0;
    }

    public void AddRadar(Radar radar)
    {
        inRange.Add(radar);
    }

    public void RemoveRadar(Radar radar)
    {
        inRange.Remove(radar);
    }

    public void SetObscuredNext(bool o)
    {
        obscuredNext = o;
    }

    public void SetTrail(bool t)
    {
        trail = t;
    }

    public bool IsSafeNextStep()
    {
        return ((inRange.Count == 0) || obscuredNext) && !trail;
    }
}
