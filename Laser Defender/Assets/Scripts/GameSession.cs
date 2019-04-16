using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameScoreText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] int gameScore = 0;
    int health;
    [SerializeField] int scorePerKill = 50;

    // Start is called before the first frame update
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Start()
    {
        gameScoreText.text = gameScore.ToString();
        health = FindObjectOfType<Player>().health;
        healthText.text = health.ToString();
    }

    public void AddToScore()
    {
        gameScore += scorePerKill;
        gameScoreText.text = gameScore.ToString();
    }

    public void UpdateHealth()
    {
        health = FindObjectOfType<Player>().health;
        healthText.text = health.ToString();
    }

    public int GetScore()
    {
        return gameScore;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
