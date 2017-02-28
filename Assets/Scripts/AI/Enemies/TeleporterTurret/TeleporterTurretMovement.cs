using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTurretMovement : MonoBehaviour {

    [SerializeField]
    private List<Transform> locations;
    [SerializeField]
    private float minTime;
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private GameObject TeleportEffect;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Movement());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Movement()
    {
        while (isActiveAndEnabled)
        {
            float rando = Random.Range(minTime, maxTime);
            int randLoc = Random.Range(0, locations.Count);
            if (this.transform.position != locations[randLoc].position)
            {
                yield return new WaitForSeconds(rando);
                GameObject clone = Instantiate(TeleportEffect, this.transform.position, this.transform.rotation) as GameObject;
                this.gameObject.transform.position = locations[randLoc].position;
                GameObject clone2 = Instantiate(TeleportEffect, this.transform.position, this.transform.rotation) as GameObject;

            }
        }
    }
}
