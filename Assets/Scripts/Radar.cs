using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int range;
    public float spinSpeed;
    public Cell location;
    public Grid grid;

    private static Vector3 rotateAngle = new Vector3(0f, 0f, 1f);

    // Start is called before the first frame update
    public void AddRadar()
    {
        grid.AddRadar(this, location, range);
        foreach (Transform child in transform) {
            if (child.name == "SpinScan") {
                foreach (Transform beneath in child) {
                    beneath.localScale = new Vector3(0.1f, range, 0.5f);
                    beneath.localPosition = Vector3.up * 0.5f * range;
                }
                // Give it a random scan offset
                child.Rotate(Radar.rotateAngle * Random.Range(-180f, 180f));
            }
        }
    }

    public void SetLocation(Cell cell)
    {
        location = cell;
        transform.localPosition = cell.transform.localPosition + new Vector3(0f, 0f, -1f);
    }

    private void Update()
    {
        foreach (Transform child in transform) {
            if (child.name == "SpinScan") {
                child.Rotate(Radar.rotateAngle * spinSpeed * Time.deltaTime);
            }
        }
    }
}
