using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Directions direction;
    private Queue<Cell> path;
    private int delayedAppearance;

    private static Vector3 hiddenSpot = new Vector3(1.0f, 1.0f, 10f);

    public void AddDelay(int delay)
    {
        delayedAppearance = delay;
    }

    public void CalculatePath(Cell startPoint, Directions direction)
    {
        path = new Queue<Cell>();
        path.Enqueue(startPoint);
        Cell latest = startPoint;
        while (latest = latest.GetNeighbour(direction)) {
            path.Enqueue(latest);
        }
    }

    public void GoToTime(int time)
    {
        if (time < delayedAppearance) {
            transform.localPosition = hiddenSpot;
            return;
        }
        time -= delayedAppearance;
        if (time >= path.Count) {
            transform.localPosition = hiddenSpot;
        } else {
            Cell location = path.ToArray()[time];
            transform.localPosition = location.transform.localPosition + new Vector3(0f, 0f, -1f);
            if (time + 1 < path.Count) {
                Debug.Log("Setting the next cell as obscured next turn");
                path.ToArray()[time+1].SetObscuredNext(true);
            }
        }
    }
}
