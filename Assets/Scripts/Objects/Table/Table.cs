using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class Table : MonoBehaviour
{
    public List<Item.ItemData> TableSlots = new();
    public int numSlots = 4;
    public Player player;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        for (int i = 0; i < numSlots; i++)
        {
            TableSlots.Insert(i, null);
        }   
    }

    private void Update()
    {
        if (TableSlots[0] != null)
        {
            sprite = Resources.Load<Sprite>("Sprites/Items/" + TableSlots[0].sprite);
            spriteRenderer.sprite = sprite;
        }
    }
    private void OnMouseDown()
    {
        for (int i = 0; i < numSlots; i++)
        {
            if (TableSlots[i] == null && player.equippedItem != null)
            {
                TableSlots[i] = player.equippedItem;
                Debug.Log(TableSlots);
                break;
            }
            else if (TableSlots[i] != null && player.equippedItem == null)
            {

            }
            else
            {
                Debug.Log("Full");
            }
        }
    }
}
