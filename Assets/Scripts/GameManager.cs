using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TMP_Text scoreText;

    void Awake() { Instance = this; }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
