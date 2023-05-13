using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class moneycollector : MonoBehaviour
{
    public int moneyCount = 0;
    public TextMeshProUGUI moneyText;
    public GameObject winText;
    public GameObject resetButton;

    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + moneyCount;

        if (moneyCount >= 5) {
            winText.SetActive(true);
            resetButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Money")) {
            moneyCount++;
            Destroy(other.gameObject);
        }
    }
    public void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
