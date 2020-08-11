using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Camera gameCamera;

    public GameObject gridPrefab;
    public GameObject planePrefab;
    public GameObject radarPrefab;
    public GameObject cloudPrefab;

    private Grid grid;
    private Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        grid = Instantiate(gridPrefab, transform).GetComponent<Grid>();
        plane = Instantiate(planePrefab, transform).GetComponent<Plane>();
        plane.gameCamera = gameCamera;
        plane.SetLocation(grid.GetCell(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
