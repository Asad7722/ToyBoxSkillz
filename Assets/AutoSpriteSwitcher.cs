using UnityEngine;
using UnityEngine.UI;

public class AutoSpriteSwitcher : MonoBehaviour
{
    [Header("Sprites to Switch Between")]
    public Sprite spriteA;               
    public Sprite spriteB;              
    public float switchInterval = 1f;    

    private Image spriteRenderer;
    private bool usingSpriteA = true;

    void Start()
    {
        spriteRenderer = GetComponent<Image>();
        spriteRenderer.sprite = spriteA;
        StartCoroutine(SwitchSprites());
    }

    System.Collections.IEnumerator SwitchSprites()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            usingSpriteA = !usingSpriteA;
            spriteRenderer.sprite = usingSpriteA ? spriteA : spriteB;
        }
    }
}
