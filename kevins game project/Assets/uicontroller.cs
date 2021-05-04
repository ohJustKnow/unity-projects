using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uicontroller : MonoBehaviour
{
    float timer;
    public GameObject endscreen;
    public Text finalscore;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer>20)
        {
            endscreen.SetActive(true);
            finalscore.text = "Your Score is: " + spawntargets.score;
            Time.timeScale = 0;
        }
    }
    public void restartgame()
    {
        
        spawntargets.score = 0;
        SceneManager.LoadScene("canongame");
    }
}
