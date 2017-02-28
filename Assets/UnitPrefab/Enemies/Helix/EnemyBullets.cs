using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour {
    public int damage;
    public float selfTimer;
    private float counter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //failsave so the bullets dont go forever and clogg up the screen
        counter += Time.deltaTime;
        if(counter >= selfTimer)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Collider>().tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Ouch(damage);
        }
        if(other.gameObject.GetComponent<Collider>().tag == "wall")
        {
            Destroy(this.gameObject);
        }
    }
}
