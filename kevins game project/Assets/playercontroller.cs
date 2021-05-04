using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    public GameObject ball;
    public float ballspeed;
    float timer = .5f;
    public float maxchargetime;
    public float minchargetime;
    bool reset = true;
    float angle = 45;
    public Slider chargebar; 
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        timer = minchargetime;
        chargebar.minValue = minchargetime;
        chargebar.maxValue = maxchargetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) == true && reset == true)
        {
            timer = timer + Time.deltaTime;
        }
        if ((Input.GetKeyUp(KeyCode.Space) == true && reset == true) || timer >= maxchargetime)
        {
            fire();
            reset = false;

        }
        if(Input.GetKeyUp(KeyCode.Space) == true)
        {
            reset = true;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            angle = angle + 90 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            angle = angle - 90 * Time.deltaTime;
        }
        chargebar.value = timer;

        angle = Mathf.Clamp(angle, 0, 180);
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    void fire()
    {
        GameObject go = Instantiate(ball, transform.position + transform.right, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = transform.right * ballspeed * timer;
        timer = minchargetime;
        Destroy(go, 10);
    }
}
