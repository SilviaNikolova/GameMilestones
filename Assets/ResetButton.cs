using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetButton : MonoBehaviour
{
    public void ResetGame()
    {
        // Reset the game
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.ResetGame();
    }
}
