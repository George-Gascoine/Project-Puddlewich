using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]

public class Stir : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector2 origPos;

    private void Start()
    {
        origPos = transform.position;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
    void OnMouseUp()
    {
        transform.position = origPos;
    }
}