using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletController : MonoBehaviour
{
    // has pallet will be used to identify if the current node has pallet
    public bool hasPallet;
    public SpriteRenderer pallet;

    public GameManager gameManager;
    private int score;


    private void Awake()
    {
        // At first all the nodes have a pallet
        pallet = GetComponentInChildren<SpriteRenderer>();
        hasPallet = true;

    }

    // checking if the collision is on the pacman (Player child)
    private void OnTriggerEnter2D(Collider2D col)
    {
        // if the pallet touched by the pacman rigidbody it will be enabled
        if (col.gameObject.transform.GetChild(0).tag == "Pacman" && hasPallet)
        {
            // disable the render of the pallet
            pallet.enabled = false;
            hasPallet=false;

            // increasing the score by th amount of score
            score = 1;
            gameManager.setScore(score);
        }
    }



}
