using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    private float minX;
    private float maxX;
    private int score = 0;
    public GameObject pins;
    private Rigidbody ballRigidbody;
    public TextMeshProUGUI scoreText;
    private int lastScore = -1; // Store the last score displayed
    public Slider slider;
    public GameObject resetButton;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cubeObject = GameObject.FindGameObjectWithTag("Cube");
        if (cubeObject != null) {
            Renderer renderer = cubeObject.GetComponent<Renderer>();
            minX = renderer.bounds.min.x;
            maxX = renderer.bounds.max.x;
            resetButton.SetActive(false);
        }
        pins = GameObject.FindGameObjectWithTag("Pin");
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
        CountPinsDown(); // Update the score
        UpdateScoreText(); // Update the score text
    }

    void MoveBall()
    {
        Vector3 position = ball.transform.position;
        position.x = Mathf.Lerp(minX, maxX, slider.value);
        ball.transform.position = position;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ballRigidbody.AddForce(ball.transform.forward * 2000f);
            resetButton.SetActive(true);
        }
    }

    void CountPinsDown()
    {
        GameObject[] pinsArray = GameObject.FindGameObjectsWithTag("Pin");
        int newScore = score; // Store the current score
        for(int i=0;i<pinsArray.Length; i++)
        {
            if(pinsArray[i].transform.eulerAngles.z > 5 && pinsArray[i].transform.eulerAngles.z < 355 && pinsArray[i].activeSelf)
            {
                newScore++;
                pinsArray[i].SetActive(false);
            }
        }
        score = newScore; // Update the score
    }

    void UpdateScoreText()
    {
        // Only update the score text if the score has changed
        if (score != lastScore)
        {
            scoreText.text = "Score: " + score.ToString();
            lastScore = score;
        }
    }
    public void ResetGame()
    {
        // Reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
