using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSnap : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(1f, 0.5f);
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Snap(transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Snap(transform.localPosition);
    }

    Vector3 Snap(Vector3 localPosition)
    {
        // Calculate ratios for simple grid snap
        float xx = Mathf.Round(localPosition.y / gridSize.y - localPosition.x / gridSize.x);// Y grid coordinate
        float yy = Mathf.Round(localPosition.y / gridSize.y + localPosition.x / gridSize.x);// X grid coordinate

        // Calculate grid aligned position from current position
        Vector3 position = transform.localPosition;
        float x = (yy - xx) * 0.5f * gridSize.x;
        float y = (yy + xx) * 0.5f * gridSize.y;
        float z = 2.0f;

        return new Vector3(x, y, z);
    }
}
