using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : Level
{

    // Start is called before the first frame update
    void Start()
    {
        PrepareLevel();

        SetupGrid(12, 18);

        AddPlane(0, 0, Color.green);
        AddPlane(8, 8, Color.blue);

        AddRadar(5, 5, 4);
        AddRadar(6, 7, 2);

        AddCloud(0, 3, Directions.East, 3);

        AddAirport(2, 3);
        AddAirport(12, 4);

        BeginLevel();
    }
}
