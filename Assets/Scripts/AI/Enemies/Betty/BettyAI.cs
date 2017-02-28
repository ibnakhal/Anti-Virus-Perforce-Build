using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BettyAI : MonoBehaviour {

    [SerializeField]
    GameObject betty;
    [SerializeField]
    GameObject moveableBetty;
    [SerializeField]
    GameObject restBetty;
    [SerializeField]
    GameObject activeBetty;
    [SerializeField]
    Transform jumpMiddle;
    [SerializeField]
    Transform jumpEnd;
    [SerializeField]
    float speed;
    [SerializeField]
    float sightRange = 3;
    [SerializeField]
    GameObject deathExplosion;

    GameObject player;
    float movementSpeed;
    bool firstJump = false;
    bool secondJump = false;
    public bool blowingUp = false;



    void Awake()
    {
        player = GameObject.Find("Player");
        this.gameObject.name = "Betty";
    }

    void Update ()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, (player.transform.position - this.transform.position), out hit, sightRange) && secondJump == false)
        {
            firstJump = true;
        }

        if (firstJump == true)
        {
            activeBetty.SetActive(true);
            Destroy(restBetty);
            movementSpeed = speed * Time.deltaTime;
            moveableBetty.transform.position = Vector3.MoveTowards(moveableBetty.transform.position, jumpMiddle.position, movementSpeed);
            if (moveableBetty.transform.position == jumpMiddle.position)
            {
                secondJump = true;
                firstJump = false;
            }
        }

        if (secondJump == true)
        {
            movementSpeed = speed * Time.deltaTime;
            moveableBetty.transform.position = Vector3.MoveTowards(moveableBetty.transform.position, jumpEnd.position, movementSpeed / 5);
        }

        if (moveableBetty.transform.position == jumpEnd.position && blowingUp == false)
        {
            Explode();
            blowingUp = true;
        }
	}

    public void Explode()
    {
        RaycastHit hit;

        Instantiate(deathExplosion, activeBetty.transform.position, Quaternion.identity);

        if (Physics.Raycast(activeBetty.transform.position, (player.transform.position - activeBetty.transform.position), out hit, sightRange))
        {
            player.GetComponent<PlayerController>().health -= 10;
        }

        GetComponentInParent<RoomController>().contents.Remove(betty);
        Destroy(betty);
    }
}
