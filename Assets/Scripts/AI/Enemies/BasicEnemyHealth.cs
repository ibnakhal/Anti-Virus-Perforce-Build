using UnityEngine;
using System.Collections;

public class BasicEnemyHealth : MonoBehaviour {



    [SerializeField]
    private int health;
    [SerializeField]
    private GameObject DeathParticles;
    [SerializeField]
    private GameObject toDie;
    public GameObject[] drops;
    [SerializeField]
    private int dropPercent, truepercent;
    [SerializeField]
    private int currentSwelll;
    [SerializeField]
    private int swellingLimit;
    [SerializeField]
    private float swellingRate;

    private bool hurting = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(health <=0)
        {

                Instantiate(DeathParticles, this.transform.position, this.transform.rotation);
                toDie.GetComponentInParent<RoomController>().contents.Remove(toDie);


                truepercent = Random.Range(0, 101);
                if (truepercent <= dropPercent)
                {
                    GameObject clone = Instantiate(drops[Random.Range(0, drops.Length + 1)], this.transform.position, drops[Random.Range(0, drops.Length + 1)].transform.rotation) as GameObject;

                }
                Destroy(toDie);
            }

        if (currentSwelll < swellingLimit && hurting)
        {
            this.gameObject.transform.localScale += new Vector3(swellingRate, swellingRate, swellingRate);

            currentSwelll++;
            
        }
        else
        {
            hurting = false;
            if (currentSwelll > 0)
            {
                this.gameObject.transform.localScale -= new Vector3(swellingRate, swellingRate, swellingRate);

                currentSwelll--;
            }
        }


    }


    public void Ouch(int Damage)
    {
        health -= Damage;
        hurting = true;
    }

    public void GetHurt(int Damage)
    {
        health -= Damage;
        hurting = true;
    }





}
