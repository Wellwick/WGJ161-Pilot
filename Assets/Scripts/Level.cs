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

    protected Grid grid;
    protected List<Plane> planes;
    protected List<Radar> radars;
    protected List<Cloud> clouds;

    protected void PrepareLevel()
    {
        planePrefab.GetComponent<Plane>().gameCamera = gameCamera;
        planes = new List<Plane>();
        radars = new List<Radar>();
        clouds = new List<Cloud>();
    }

    protected void SetupGrid(int width, int height)
    {
        gridPrefab.GetComponent<Grid>().width = 18;
        gridPrefab.GetComponent<Grid>().height = 12;
        grid = Instantiate(gridPrefab, transform).GetComponent<Grid>();
    }

    protected void AddPlane(int x, int y, Color c)
    {
        Plane plane = Instantiate(planePrefab, transform).GetComponent<Plane>();
        plane.SetLocation(grid.GetCell(x, y));
        plane.color = c;
        plane.level = this;
        planes.Add(plane);
    }

    protected void AddRadar(int x, int y, int range)
    {
        Radar radar = Instantiate(radarPrefab, transform).GetComponent<Radar>();
        radar.SetLocation(grid.GetCell(x, y));
        radar.range = range;
        radar.grid = grid;
        radars.Add(radar);
    }

    protected void AddCloud(int x, int y, Directions direction, int delayTime)
    {
        Cloud cloud = Instantiate(cloudPrefab, transform).GetComponent<Cloud>();
        // Probably better practice to add the delay before the path calculation
        cloud.AddDelay(delayTime);
        cloud.CalculatePath(grid.GetCell(x, y), direction);
        clouds.Add(cloud);
    }

    protected void AddAirport(int x, int y)
    {
        grid.GetCell(x, y).isAirport = true;
    }

    protected void BeginLevel()
    {
        gameCamera.transform.localPosition = grid.CentrePoint();
        gameCamera.orthographicSize = grid.ScreenSize();

        foreach (Radar radar in radars) {
            radar.AddRadar();
        }

        GoToTime(0);
    }

    public void GoToTime(int time)
    {
        grid.Recolour(radars.Count);
        grid.ClearObscurations();

        foreach (Cloud cloud in clouds) {
            cloud.GoToTime(time);
        }

        if (LevelComplete()) {
            Debug.Log("The level has been completed!");
        }
    }

    public bool LevelComplete()
    {
        foreach (Plane plane in planes) {
            if (!plane.IsSafe()) {
                return false;
            }
        }

        return true;
    }
    
}
