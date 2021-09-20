using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbAI : MonoBehaviour
{
    public GameObject player;
    public bool isBlue;
    public GameObject deathParticlesPefab;
    public OrbSpawner orbSpawner;

    private float speedChangeRate;
    private float heightChangeRate;

    private float speed = 5;
    private float y;

    private bool inOrbit = false;
    private float orbitRadius;

    private float lifespan = 0;

    private void Start()
    {
        transform.LookAt(player.transform.position);
        orbitRadius = Random.Range(0.8f, 1.2f);

        speedChangeRate = Random.Range(0.1f, 0.2f);
        heightChangeRate = Random.Range(0.1f, 0.3f);
    }

    private void Update()
    {
        lifespan += Time.deltaTime;
        if (lifespan < 15)
        {
            if (inOrbit)
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                y = Mathf.PingPong(Time.time * heightChangeRate, 2);

                var t = Mathf.PingPong(Time.time * speedChangeRate, 1);
                speed = Mathf.Lerp(75, 140, t);
                transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
            }
            else
            {
                Vector3 orbitEntrance = player.transform.position - (transform.right * orbitRadius);
                orbitEntrance = new Vector3(orbitEntrance.x, transform.position.y, orbitEntrance.z);
                transform.position = Vector3.MoveTowards(transform.position, orbitEntrance, 6 * Time.deltaTime);

                if (transform.position == orbitEntrance) 
                { 
                    inOrbit = true;
                    y = transform.position.y;
                }
            }
        }
        else
        {
            transform.position += transform.forward * 5 * Time.deltaTime;
            if(Vector3.Distance(transform.position, player.transform.position) > 30)
            {
                Instantiate(deathParticlesPefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ring")
        {
            if((isBlue && collision.gameObject.name == "BlueRing") || (!isBlue && collision.gameObject.name == "PinkRing"))
            {
                orbSpawner.score++; //you get a point
                orbSpawner.UpdateScore(); //update scoreboard
            }
        }

        Instantiate(deathParticlesPefab, transform.position, Quaternion.identity);
        Destroy(gameObject); //orb is destroyed
    }
}
