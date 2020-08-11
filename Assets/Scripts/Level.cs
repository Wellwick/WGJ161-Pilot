﻿using System.Collections;
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
    private List<Plane> planes;
    private List<Radar> radars;
    private List<Cloud> clouds;

    private int time;

    // Start is called before the first frame update
    void Start()
    {
        grid = Instantiate(gridPrefab, transform).GetComponent<Grid>();
        gameCamera.transform.localPosition = grid.CentrePoint();
        gameCamera.orthographicSize = grid.ScreenSize();

        planes = new List<Plane>();
        planePrefab.GetComponent<Plane>().gameCamera = gameCamera;
        Plane plane = Instantiate(planePrefab, transform).GetComponent<Plane>();
        plane.color = Color.green;
        plane.SetLocation(grid.GetCell(0, 0));
        plane.level = this;
        planes.Add(plane);
        Plane plane2 = Instantiate(planePrefab, transform).GetComponent<Plane>();
        plane2.color = Color.blue;
        plane2.SetLocation(grid.GetCell(8, 8));
        plane2.level = this;
        planes.Add(plane2);

        radars = new List<Radar>();
        Radar radar1 = Instantiate(radarPrefab, transform).GetComponent<Radar>();
        radar1.SetLocation(grid.GetCell(5, 5));
        radar1.range = 4;
        radar1.grid = grid;
        radars.Add(radar1);
        Radar radar2 = Instantiate(radarPrefab, transform).GetComponent<Radar>();
        radar2.SetLocation(grid.GetCell(6, 7));
        radar2.range = 2;
        radar2.grid = grid;
        radars.Add(radar2);

        foreach (Radar radar in radars) {
            radar.AddRadar();
        }
    }

    public void GoToTime(int time)
    {
        grid.Recolour(radars.Count);

        foreach (Cloud cloud in clouds) {
            cloud.GoToTime(0);
        }
    }


    
}
