using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string scene;

    public void Enable(bool enable)
    {
        GetComponent<SpriteRenderer>().enabled = enable;
        GetComponent<BoxCollider>().enabled = enable;
    }

    public void Center()
    {
        Camera gameCamera = FindObjectOfType<Camera>();
        transform.localPosition = gameCamera.transform.localPosition + new Vector3(0f,0f,2f);
    }

    private void OnMouseDown()
    {
        Camera gameCamera = FindObjectOfType<Camera>();
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit == transform) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
            }
        }
    }

}
