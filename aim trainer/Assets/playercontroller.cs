using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    public float turnspeed = .7f;
    float yrotation;
    public GameObject cube;
    float timer = 30;
    int hits;
    int score;
    int misses;
    int shots;
    public Text leaderboardsdisplay;
    public Text scoredisplay;
    public Text timerdisplay;
    public GameObject endpanel;
    public Text hitsdisplay;
    public Text missesdisplay;
    public Text shotsfireddisplay;
    public Text accuracydisplay;
    bool gamerunning = true;
    public GameObject inputbox;
    public GameObject escapepanel;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cubespawn(); cubespawn(); cubespawn();
        
    }

    void cubespawn()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        GameObject go = Instantiate(cube, new Vector3(x, y, 4), Quaternion.identity);
    }

    void cameramovement()
    {
        float mousey = -1 * Input.GetAxis("Mouse Y");
        yrotation += mousey;
        yrotation = Mathf.Clamp(yrotation, -90, 90);

        transform.localRotation = Quaternion.Euler(yrotation * turnspeed, 0, 0);
        transform.parent.rotation = transform.parent.rotation * Quaternion.Euler(0, Input.GetAxis("Mouse X") * turnspeed, 0);
    }
    void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            shots = shots + 1;


            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                hits = hits + 1;
                
                Destroy(hit.collider.gameObject);
                cubespawn();
            }

        }
    }

    void updatehighscores(int newscore)
    {
        string name = "";
        bool highscorecheck = false;
        int[] arrscores = new int[10];
        string[] names = new string[10];
        for (int i = 0; i < 10; i++)
        {
            
            arrscores[i] = PlayerPrefs.GetInt("Score" + i + 1, 0);
            names[i] = PlayerPrefs.GetString("name" + i + 1, "Player");
        }
        for (int i = 0; i < 10; i++)
        {
            if (newscore >= arrscores[i])
            {
                highscorecheck = true;
                PlayerPrefs.SetInt("Score" + i + 1, newscore);
                PlayerPrefs.SetString("name" + i + 1, name);
                newscore = arrscores[i];
                name = names[i];
            }
        }
        if (highscorecheck == true)
        {
            inputbox.SetActive(true);
        }
    }

    public void restartgame()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        for (int i = 0; i < cubes.Length; i++)
        {
            Destroy(cubes[i]);
            cubespawn();
        }
        gamerunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        endpanel.SetActive(false);
        timer = 30;
        hits = 0;
        score = 0;
        misses = 0;
        shots = 0;
       // PlayerPrefs.SetString("name" + i + 1, "")
    }

    public void updatename()
    {
        
        for (int i = 0; i < 10; i++)
        {
            
            if (score == PlayerPrefs.GetInt("Score" + i + 1, 0))
            {
                PlayerPrefs.SetString("name" + i + 1, inputbox.GetComponent<InputField>().text);
                break;
            }
        }
        inputbox.GetComponent<InputField>().text = "";
        inputbox.SetActive(false);
        updatehighscoredisplay();
        
    }

    void clearscores()
    {
        for(int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("Score" + i + 1, 0);
            PlayerPrefs.SetString("name" + i + 1, "");
        }
    }

    void updatehighscoredisplay()
    {
        string s = "";
        
        for (int i = 0; i < 10; i++)
        {
            s = s + $"#{i + 1}   " + PlayerPrefs.GetInt("Score" + i + 1, 0).ToString() + " - " + PlayerPrefs.GetString("name" + i + 1, "") + "\n";
            
        }
        leaderboardsdisplay.text = s;

        for (int i = 0; i < 10; i++)
        {
            
            if (score == PlayerPrefs.GetInt("Score" + i + 1, 0))
            {

                inputbox.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, -10 - (i * 23));
                break;
            }
        }

    }

    void endgame()
    {

        gamerunning = false;
        Cursor.lockState = CursorLockMode.None;
        misses = shots - hits;
        score = hits - misses;
        endpanel.SetActive(true);
        hitsdisplay.text = "Hits: " + hits;
        scoredisplay.text = "Score: " + score;
        accuracydisplay.text = "Acc: " +  Mathf.Round((float)hits / (hits + misses) * 1000) / 10 + "%";
        missesdisplay.text = "Misses: " + misses;
        shotsfireddisplay.text = "Shots: " + shots;


        updatehighscores(score);
        updatehighscoredisplay();
        
    }

    void pausegame()
    {
        gamerunning = false;
        escapepanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }
    public void resumegame()
    {
        gamerunning = true;
        escapepanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void exitgame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gamerunning == true)
        {
            restartgame();
        }

        if (timer <= 0 && gamerunning)
        {
            endgame();
            timerdisplay.text = "Timer: 0.0";
        }

        if (gamerunning)
        {
            timer = timer - Time.deltaTime;
            timerdisplay.text = "Timer: " + string.Format("{0:F1}", Mathf.Round(timer * 10) / 10);
            cameramovement();
            fire();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gamerunning == true)
        {
            pausegame();
        }
       

    }

}
