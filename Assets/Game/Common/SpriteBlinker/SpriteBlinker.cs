using System.Collections;
using UnityEngine;

public class SpriteBlinker
{
    public bool isBlinking { get; private set; }

    private SpriteRenderer[] sprites;
    private MonoBehaviour objectToBlink;

    private bool spritesAreVisible = true;

    private IEnumerator currentBlink;

    public SpriteBlinker(SpriteRenderer[] sprites, MonoBehaviour objectToBlink)
    {
        this.sprites = sprites;
        this.objectToBlink = objectToBlink;
    }

    public void StopBlink()
    {
        objectToBlink.StopCoroutine(currentBlink);
        isBlinking = false;
    }

    public void StartBlinkWith(float seconds, float blinkFrequency)
    {
        if (isBlinking)
            throw new System.Exception($"Is not possible to start a blink if a GameObject is already blinking. ");
        currentBlink = BlinkWith(seconds, blinkFrequency);
        objectToBlink.StartCoroutine(currentBlink);
    }
    private IEnumerator BlinkWith(float seconds, float blinkFrequency)
    {
        isBlinking = true;
        spritesAreVisible = true;

        float timeSinceStart = 0;
        do
        {
            InvertSpritesVisibility();
            yield return new WaitForSeconds(blinkFrequency);
            timeSinceStart += blinkFrequency;
        }
        while (timeSinceStart < seconds || !spritesAreVisible);

        isBlinking = false;
    }

    private void InvertSpritesVisibility()
    {
        foreach (SpriteRenderer r in sprites)
        {
            float alpha = spritesAreVisible ? 0 : 1;
            r.color = new Color(r.color.r, r.color.g, r.color.b, alpha);
        }

        spritesAreVisible = !spritesAreVisible;
    }
}