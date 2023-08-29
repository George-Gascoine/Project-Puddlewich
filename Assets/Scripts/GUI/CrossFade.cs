using UnityEngine;
using UnityEngine.UI;

public class CrossFade : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public Image image;

    private void Start()
    {
        image.canvasRenderer.SetAlpha(0f);
    }
    private void Update()
    {
    }
    public void ScreenFadeOut() 
    {
        image.CrossFadeAlpha(1, 0.5f, false);
    }

    public void ScreenFadeIn()
    {
        image.CrossFadeAlpha(0, 0.5f, false);
    }
}