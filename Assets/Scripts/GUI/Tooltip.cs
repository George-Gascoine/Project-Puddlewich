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
    public Slot slot;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    // Update is called once per frame
    void Update()
    {
        if (!slot.toolbarSlot)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                new Vector3(Input.mousePosition.x + 120, Input.mousePosition.y - 120, Input.mousePosition.z),
                parentCanvas.worldCamera,
                out movePos);
            transform.position = parentCanvas.transform.TransformPoint(movePos);
        }
        else
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                new Vector3(Input.mousePosition.x + 120, Input.mousePosition.y + 120, Input.mousePosition.z),
                parentCanvas.worldCamera,
                out movePos);
            transform.position = parentCanvas.transform.TransformPoint(movePos);
        }
    }
    public void SetTooltip()
    {
        itemName.text = slot.slotItem.name;
        itemDescription.text = slot.slotItem.description;
    }
}
