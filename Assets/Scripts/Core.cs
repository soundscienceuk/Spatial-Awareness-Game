using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Core : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject deathText;
    [SerializeField] GameObject mainMenu;



    // Start is called before the first frame update
    void Start()
    {
        deathText.SetActive(false);
        scoreText.text = "0";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreIn)
    {
        score += scoreIn;
        scoreText.text = score.ToString();
    }

    public void SetWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void SetDead()
    {
        deathText.gameObject.SetActive(true);
    }

    void ShowMenu()
    {
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        print("Exiting application");
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
