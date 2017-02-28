using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour {
    public bool oneDead = false;
    public bool twoDead = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(oneDead && twoDead)
        {
            this.gameObject.GetComponentInParent<RoomController>().contents.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
	}
}
