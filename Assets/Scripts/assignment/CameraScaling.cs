using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    [SerializeField] KeyCode zoomIn = KeyCode.W;
    [SerializeField] KeyCode zoomOut = KeyCode.S;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(zoomIn)) {
            Camera.main.orthographicSize -= 1f;
        }
        if (Input.GetKeyDown(zoomOut)) {
            Camera.main.orthographicSize += 1f;
        }
        
    }
}
