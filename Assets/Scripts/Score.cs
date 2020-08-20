using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreUi;
    public TextMeshProUGUI highScoreUi;
    

    // Start is called before the first frame update
    void Start()
    {
        
        scoreUi.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.DeleteAll();
        highScoreUi.text = highScore.ToString();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore",highScore);
           
        }
        
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("score"))
        {
           
            score++;
            scoreUi.text = score.ToString();

        }


      


    }

}
