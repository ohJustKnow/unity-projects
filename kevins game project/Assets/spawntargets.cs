using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawntargets : MonoBehaviour
{
    public float timebetweenspawn = 10;
    public GameObject target;
    float timer = 9;
    public static int score = 0;
    public Text scoredisplay;
    void Start()
    {
        timer = timebetweenspawn - 1;
    }

    public void spawntarget()
    {
        Vector3 spawnpoint = new Vector3(0,0,0);
        spawnpoint.x = Random.Range(-39,39);
        spawnpoint.y = Random.Range(-16, 16);

        while ((transform.position - spawnpoint).magnitude<10)
        {
            spawnpoint.x = Random.Range(-39, 39);
            spawnpoint.y = Random.Range(-16, 16);
        }
        GameObject go = Instantiate(target, spawnpoint, Quaternion.identity);
        Destroy(go, timebetweenspawn);
        if (timer < timebetweenspawn)
        {
            score++;
            scoredisplay.text = "Score: " + score;
        }
        timer = 0;
    }
    private void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > timebetweenspawn)
        {
            spawntarget();
        }
    }
}
