using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Color color;
    public Camera gameCamera;

    private Cell location;
    private List<Cell> trail;

    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().materials[0].color = color;
        selected = false;
        trail = new List<Cell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLocation(Cell cell)
    {
        location = cell;
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit == transform) {
                selected = true;
                Debug.Log("Plane has been clicked");
            }
        }
    }

    private void OnMouseDrag()
    {
        if (!selected) {
            return;
        }
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Cell cell = objectHit.GetComponent<Cell>();
            // No point doing anything if we aren't dragging over a cell
            if (cell) {
                // Check the last of the trail first, since we can backtrack
                int index = trail.IndexOf(cell);
                if (index > -1 && index == trail.Count - 1) {
                    location = cell;
                    location.GetComponent<MeshRenderer>().materials[0].color = Color.white;
                    transform.localPosition = objectHit.localPosition + new Vector3(0f, 0f, -1f);
                    trail.Remove(cell);

                } else if (index != -1) {
                    // If it's not the most recent, can't go there
                    return;
                }
                if (location.IsNeighbour(cell)) {
                    // This is the only case where it's safe to move
                    trail.Add(location);
                    location.GetComponent<MeshRenderer>().materials[0].color = 
                        Color.Lerp(Color.white, color, 0.5f);
                    location = cell;
                    transform.localPosition = objectHit.localPosition + new Vector3(0f,0f,-1f);
                }
            }
        }
    }

    private void OnMouseUp()
    {
        selected = false;
    }
}
