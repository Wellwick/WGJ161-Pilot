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

class Direction
{
    public static Quaternion Rotation(Directions direction)
    {
        return Quaternion.Euler(0f, 0f, 90f * (int)direction);
    }
}
