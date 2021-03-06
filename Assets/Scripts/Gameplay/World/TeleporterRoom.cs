﻿using UnityEngine;
using System.Collections;

public class TeleporterRoom : MonoBehaviour
{
	public delegate void PlayerMoveDel (Transform loc);
	public PlayerMoveDel playerMoveEvent;

    [SerializeField]
    public Transform Deposit;
    [SerializeField]
    public bool taken;
    [SerializeField]
    public bool cleared;
    [SerializeField]
    public bool bossRoom;
    [SerializeField]
    public bool boonRoom;
    [SerializeField]
    public Transform ownDeposit;
    [SerializeField]
    private ParticleSystem sys0;
    [SerializeField]
    private ParticleSystem sys1;
    [SerializeField]
    private Material c0;
    [SerializeField]
    private Material c1;
    [SerializeField]
    private Material b0;
    [SerializeField]
    private Material b1;
    [SerializeField]
    private Material g0;
    [SerializeField]
    private Material g1;


    public void Update()
    {
		if (Deposit != null) {
			if (Deposit.GetComponentInParent<TeleporterRoom> ().cleared == true && !Deposit.GetComponentInParent<TeleporterRoom> ().boonRoom) {
				sys0.gameObject.GetComponent<Renderer> ().material = c0;
				sys1.gameObject.GetComponent<Renderer> ().material = c1;

			}
			if (Deposit.GetComponentInParent<TeleporterRoom> ().bossRoom) {
				sys0.gameObject.GetComponent<Renderer> ().material = b0;
				sys1.gameObject.GetComponent<Renderer> ().material = b1;


			}
			if (Deposit.GetComponentInParent<TeleporterRoom> ().boonRoom) {
				sys0.gameObject.GetComponent<Renderer> ().material = g0;
				sys1.gameObject.GetComponent<Renderer> ().material = g1;


			}
		}

    }



    public void OnTriggerEnter(Collider Other)

    {
		
		if(Other.tag == "Player" && Deposit != null)
        {
			if (FindObjectOfType<MinimapManager> () != null) {
				FindObjectOfType<MinimapManager> ().PlayerMove (Deposit);
			}

            Other.transform.position = Deposit.position;
            Other.transform.rotation = Deposit.rotation;
        }
    }






}
