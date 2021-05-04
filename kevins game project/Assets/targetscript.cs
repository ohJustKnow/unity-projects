using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetscript : MonoBehaviour
{
    public GameObject explosion;
    spawntargets targetspawner;
    void Start()
    {
        targetspawner = GameObject.Find("targetspawner").GetComponent<spawntargets>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(go, 1);
        Destroy(collision.gameObject);
        targetspawner.spawntarget();
        Destroy(gameObject);
    }

}
