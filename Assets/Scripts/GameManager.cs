using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // UI - User Interface - Benutzeroberfläche
    [Header("Score Elements")]
    public int score;
    public Text scoreText;
    public int highscore;
    public Text highscoreText;


    [Header("GameOver Elements")]
    public GameObject gameOverPanel;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;

    public AudioClip bombSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Damit deaktivieren wir unseren GameOverpanel, sobald das Spiel startet.
        gameOverPanel.SetActive(false);
        // Highscore auf 0 setzen. Für Testzwecke.
        // PlayerPrefs.SetInt("Highscore", 0);
        GetHighscore();
    }

    // Damit holen wir uns den Highscore
    private void GetHighscore()
    {
        // Playerprefs ermöglicht es uns simple Daten wie Spielereinstellungen, Scores etc. permanent zu speichern.
        highscore = PlayerPrefs.GetInt("Highscore");
        // Hier setzen wir den Text unseres Highscores
        highscoreText.text = "Best: " + highscore;
    }

    // Damit erhöhen wir die Punktzahl um die Zahl "num" die uns beim Aufruf übergeben wird.
    public void IncreaseScore(int num)
    {
        // Damit erhöhen wir den "score" um "num"
        score += num;
        // und setzen den Text des Scores entsprechend auf den Wert
        scoreText.text = score.ToString();

        // Wir überprüfen, ist der Score höher als der highscore, wenn ja, dann müssen wir denn Highscore 
        // durch den aktuellen Score ersetzen.
        if (score > highscore)
        {
            // hierbei setzen wir den Score an der Position "Highscore" in den Playerprefs als neuen highscore.
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }

    }

    // Diese Methode rufen wir auf, wenn die Bombe getroffen wurde.
    public void OnBombHit()
    {
        PlayBombSound();
        // Damit pausieren wir das Spiel.
        Time.timeScale = 0;
        // Damit aktivieren wir den gameOverpanel, sobald wir eine Bombe berühren.
        gameOverPanel.SetActive(true);

        Debug.Log("Bomb hit");
    }

    // Damit starten wir das Spiel neu.
    public void RestartGame()
    {
        // Wir setzen die Punktzahl auf 0
        score = 0;
        // Setzen den angezeigten Wert auf 0
        scoreText.text = "0";

        // Deaktivieren unseren GameOverPanel
        gameOverPanel.SetActive(false);

        // Hiermit gehen wir durch alle GameObjects im Spiel, die ein bestimmtes Tag haben und löschen sie.
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("interactable"))
        {
            Destroy(g);
        }

        GetHighscore();

        // Und nun setzen wir die Spielgeschwindigkeit wieder auf 1, also auf die normale Spielgeschwindigkeit.
        Time.timeScale = 1;
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }

    public void PlayBombSound()
    {
        audioSource.PlayOneShot(bombSound);
    }
}
