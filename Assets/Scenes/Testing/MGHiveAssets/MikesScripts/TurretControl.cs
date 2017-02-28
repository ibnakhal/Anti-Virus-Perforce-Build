using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField]
    private bool IsShooting;
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private GameObject[] barrels;
    [SerializeField]
    private bool IsCooledDown;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private AudioSource shotSound;
    [SerializeField]
    private float shotCool;
    [SerializeField]
    private float reloadCooldown;
    private bool fireMe;

    private GameObject player;
	void Start ()
    {
        player = GameObject.Find("Player");
        IsCooledDown = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.LookAt(player.transform);
		if(fireMe == false)
        {
            fireMe = true;
            IsCooledDown = true;
            print("I am firing");
            StartCoroutine("SingleShoot");
        }
	}

    private IEnumerator SingleShoot()
    {
        print("I am firing");

        if (IsCooledDown)
        {
            IsCooledDown = false;
            for (int i = 0; i < barrels.Length; i++)
            {
                Rigidbody clone = Instantiate(bullet,
                                               barrels[i].transform.position,
                                               barrels[i].transform.rotation) as Rigidbody;
                clone.velocity = barrels[i].transform.forward * shotSpeed;
                shotSound.Play();
                print("Fireing one bullet from barrel" + i);
                yield return new WaitForSeconds(shotCool);
                if (i == barrels.Length - 1)
                {
                    print("I have fired all my bullets");
                    fireMe = false;
                    yield return new WaitForSeconds(reloadCooldown);
                    IsCooledDown = true;
                }
            }
          
        }
    }
}
