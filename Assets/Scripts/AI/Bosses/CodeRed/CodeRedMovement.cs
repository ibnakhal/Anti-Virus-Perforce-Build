using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeRedMovement : MonoBehaviour {

    [SerializeField]
    private List<Transform> targetLocations;

    Vector3 dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //this.transform.Translate
            	
	}

    public void newMove()
    {
        int r = Random.Range(0, targetLocations.Count);
        Transform x = targetLocations[r];
        dir = (x.position - this.transform.position);
    }
}
