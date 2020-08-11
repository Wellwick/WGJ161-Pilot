using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int range;
    public Cell location;
    public Grid grid;

    // Start is called before the first frame update
    public void AddRadar()
    {
        grid.AddRadar(this, location, range);
    }

    public void SetLocation(Cell cell)
    {
        location = cell;
        transform.localPosition = cell.transform.localPosition + new Vector3(0f, 0f, -1f);
    }
}
