using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAI : MonoBehaviour {

    [SerializeField]
    GameObject deathExplosion;
    [SerializeField]
    float baseSpeed = 250;
    [SerializeField]
    float highSpeed = 2000;
    [SerializeField]
    float explosionRadius = 3;
    [SerializeField]
    GameObject top;

    GameObject player;
    float finalSpeed;
    float topHealth = 10;



	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        finalSpeed = baseSpeed;
        StartCoroutine(Explode());
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Rotate(Vector3.up * finalSpeed * Time.deltaTime);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));

        while (finalSpeed < highSpeed)
        {
            finalSpeed += 50;
            yield return new WaitForSeconds(0.1f);
        }

        Instantiate(deathExplosion, this.transform.position, Quaternion.identity);

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, (player.transform.position - this.transform.position), out hit, explosionRadius))
        {
            player.GetComponent<PlayerController>().health -= 5;
        }

        while (finalSpeed > baseSpeed)
        {
            finalSpeed -= 50;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(Explode());
    }
}
