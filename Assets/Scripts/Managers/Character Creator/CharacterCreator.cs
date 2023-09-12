using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public int skinColor;
    public int hairColor;
    public int top;
    public int bottom;
    public Image skinColorI;
    public Image hairColorI;
    public Image topI;
    public Image bottomI;
    public Sprite[] skinSprites;
    public Sprite[] hairSprites;
    public Sprite[] topSprites;
    public Sprite[] bottomSprites;
    void Start()
    {
        skinColor = 0;
        hairColor = 0;
        top = 0; bottom = 0;
        Debug.Log(GameManager.instance.bodyParts.Length);
    }

    public void ChangeSkinColour(int value)
    {
        switch (value)
        {
            case -1:
                if(skinColor == 0)
                {
                    skinColor = skinSprites.Length - 1;
                }
                else
                {
                    skinColor -= 1;
                }
                break;
            case 1:
                if(skinColor == skinSprites.Length - 1)
                {
                    skinColor = 0;
                }
                else
                {
                    skinColor += 1;
                }
                break;
        }
        skinColorI.sprite = skinSprites[skinColor];
    }

    public void ChangeHairColour(int value)
    {
        switch (value)
        {
            case -1:
                if (hairColor == 0)
                {
                    hairColor = hairSprites.Length - 1;
                }
                else
                {
                    hairColor -= 1;
                }
                break;
            case 1:
                if (hairColor == hairSprites.Length - 1)
                {
                    hairColor = 0;
                }
                else
                {       
                    hairColor += 1;
                }
                break;
        }
        hairColorI.sprite = hairSprites[hairColor];
    }
    public void ChangeTop(int value)
    {
        switch (value)
        {
            case -1:
                if (top == 0)
                {
                    top = topSprites.Length - 1;
                }
                else
                {
                   top -= 1;
                }
                break;
            case 1:
                if (top == topSprites.Length - 1)
                {
                    top = 0;
                }
                else
                {
                    top += 1;
                }
                break;
        }
        topI.sprite = topSprites[top];
    }
    public void ChangeBottom(int value)
    {
        switch (value)
        {
            case -1:
                if (bottom == 0)
                {
                    bottom = bottomSprites.Length - 1;
                }
                else
                {
                    bottom -= 1;
                }
                break;
            case 1:
                if (bottom == bottomSprites.Length - 1)
                {
                    bottom = 0;
                }
                else
                {
                    bottom += 1;
                }
                break;
        }
        bottomI.sprite = bottomSprites[bottom];
    }

    public void ConfirmChar()
    {
        GameManager.instance.bodyParts[0] = hairColorI.sprite;
        GameManager.instance.bodyParts[1] = skinColorI.sprite;
        GameManager.instance.bodyParts[2] = topI.sprite;
        GameManager.instance.bodyParts[3] = bottomI.sprite;
        GameManager.instance.playerName = playerName.text;
        GameManager.instance.load = false;
        GameManager.instance.GameStart();
        SceneManager.UnloadSceneAsync(4);
    }
}
