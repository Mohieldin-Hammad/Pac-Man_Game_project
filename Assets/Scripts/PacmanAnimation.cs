using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAnimation : MonoBehaviour
{

    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites = new Sprite[3];
    private float animationTime = 0.2f;
    private int animationIndex = 0;


    private void Awake()
    {
        // Returns the Component of type in the GameObject or any of its children using depth first search.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // InvokeRepeating is repeating a function each specific time
        InvokeRepeating(nameof(animationProcess), animationTime, animationTime);
    }

    
    // switching between pacman sprites
    private void animationProcess()
    {
        animationIndex++;
        // make sure that the animation idnex don't get out of range
        if (animationIndex >= sprites.Length) animationIndex = 0;
        
        // chose the sprite from the sprites array
        if (animationIndex < sprites.Length && animationIndex >= 0)
        {
            spriteRenderer.sprite = sprites[animationIndex];
        }
    }
}
