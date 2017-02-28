using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmHealth : MonoBehaviour {
    [Header("Enemy Variables")]
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private int health;
    [SerializeField]
    private int sizeModifier;

    [Header("Death Variables")]
    [SerializeField]
    private GameObject toDie;
    public GameObject[] drops;
    [SerializeField]
    private int dropPercent, truepercent;

    private float constyay;

	// Use this for initialization
	void Start () {
        particles = GetComponentInChildren<ParticleSystem>();
        //StartCoroutine(Stuph());

    }

	
	// Update is called once per frame
	void Update () {
        var em = particles.emission;
        em.rateOverTime = (float)health;
        var sha = particles.shape;
        sha.radius = ((float)(health)/sizeModifier);




        if (health <= 0)
        {
            toDie.GetComponentInParent<RoomController>().contents.Remove(toDie);
            particles.gameObject.transform.SetParent(null);

            truepercent = Random.Range(0, 101);
            if (truepercent <= dropPercent)
            {
                GameObject clone = Instantiate(drops[Random.Range(0, drops.Length + 1)], this.transform.position, drops[Random.Range(0, drops.Length + 1)].transform.rotation) as GameObject;

            }
            Destroy(toDie);

        }



    }


    public void GetHurt(int Damage)
    {
        health -= Damage;

    }

    public IEnumerator Stuph()
    {
        while(isActiveAndEnabled)
        {
            yield return new WaitForSeconds(1);
            health -= 10;
        }
    }

}
