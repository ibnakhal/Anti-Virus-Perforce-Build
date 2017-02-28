using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTurretAttack : MonoBehaviour {
    [Header("Targeting")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int bulletSpeed;

    [Header("Shooting")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Fire());
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}
    
    public IEnumerator Fire()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(fireRate);
            GameObject clone = Instantiate(bullet, this.transform.position, this.transform.rotation);
            Vector3 vD = (player.transform.position - this.transform.position);
            clone.GetComponent<Rigidbody>().velocity = (vD * Time.deltaTime * bulletSpeed);

        }
    }
}
