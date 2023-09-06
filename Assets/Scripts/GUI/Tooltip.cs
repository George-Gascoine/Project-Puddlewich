using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Canvas parentCanvas;
    public Image tooltip;
    public Item.ItemData item;
    public int itemID;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    // Update is called once per frame
    void Update()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            new Vector3(Input.mousePosition.x + 40, Input.mousePosition.y -40, Input.mousePosition.z),
            parentCanvas.worldCamera,
            out movePos);
        transform.position = parentCanvas.transform.TransformPoint(movePos);
    }
}
