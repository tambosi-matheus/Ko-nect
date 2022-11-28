using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private TrailRenderer trail;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            trail.enabled = false;
            trail.enabled = true;
        }

        if (Input.touchCount == 0 || Input.GetTouch(0).deltaPosition.magnitude < 1f) return;
        var touch = Input.GetTouch(0);
        
        Vector3 pos = touch.position;
        pos.z = 50;
        Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(pos);        
        transform.position = realWorldPos;
    }
}
