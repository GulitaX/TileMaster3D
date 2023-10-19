using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToTexture : MonoBehaviour
{
    public Sprite inputSprite;
    public Texture2D texture;
    [SerializeField]
    private Material material;

    private void Start()
    {
        texture = textureFromSprite(inputSprite);
        material.SetTexture("_MainTex", texture);
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
