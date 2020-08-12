using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Color color;
    public Camera gameCamera;
    public Level level;

    private Cell location;
    private Stack<Cell> trail;

    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().materials[0].color = color;
        selected = false;
        trail = new Stack<Cell>();
    }

    private void FaceDirection(Directions direction)
    {
        transform.localRotation = Direction.Rotation(direction);
    }

    public void SetLocation(Cell cell)
    {
        location = cell;
        transform.localPosition = cell.transform.localPosition + new Vector3(0f, 0f, -2f);
        cell.SetTrail(true);
    }

    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit == transform) {
                selected = true;
                level.GoToTime(trail.Count);
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
                if (trail.Count > 0 && trail.Peek() == cell) {
                    location.SetTrail(false);
                    location = cell;
                    location.GetComponent<MeshRenderer>().materials[0].color = Color.white;
                    transform.localPosition = objectHit.localPosition + new Vector3(0f, 0f, -1f);
                    trail.Pop();
                    // Figure out based on the latest cell in the trail which direction to face
                    if (trail.Count == 0) {
                        FaceDirection(Directions.North);
                    } else {
                        Cell newLast = trail.Peek();
                        FaceDirection(newLast.GetDirection(location));
                    }
                    level.GoToTime(trail.Count);
                    return;
                } else if (trail.Contains(cell)) {
                    // If it's in the trail already, we can't go there
                    return;
                }
                if (location.IsNeighbour(cell) && cell.IsSafeNextStep()) {
                    // This is the only case where it's safe to move
                    trail.Push(location);
                    location.GetComponent<MeshRenderer>().materials[0].color = 
                        Color.Lerp(Color.white, color, 0.5f);
                    SetLocation(cell);
                    level.GoToTime(trail.Count);
                }
            }
        }
    }

    private void OnMouseUp()
    {
        selected = false;
    }
}
