using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMain : MonoBehaviour
{
    private enum BossStates { Spawning, Seek, Rest};
    [SerializeField]
    private BossStates bossControl;
    public GameObject orbiters;
    public GameObject Spawnner;
    public bool sendToPlayer = false;
    public bool canSeek = false;
    public int orbiterCounter;
    [SerializeField]
    private int orbiterMax;
    
    public float spawnTimer;
    private float cSpawnTimer;
    public float restTimer;
    private float cRestTimer;


	// Use this for initialization
	void Start ()
    {
        bossControl = BossStates.Rest;
        cRestTimer = restTimer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(bossControl == BossStates.Rest)
        {
            sendToPlayer = false;
            cRestTimer -= Time.deltaTime;
            if(cRestTimer <= 0)
            {
                if(canSeek)
                {
                    bossControl = BossStates.Seek;
                }
                else if (!canSeek)
                {
                    bossControl = BossStates.Spawning;
                }
            }
        }
        if (bossControl == BossStates.Spawning)
        {
            if (orbiterCounter < orbiterMax)
            {
                cSpawnTimer -= Time.deltaTime;
                if (cSpawnTimer <= 0)
                {

                    Spawning();
                    orbiterCounter++;
                    cSpawnTimer = spawnTimer;
                }

            }
            else
            {
                bossControl = BossStates.Rest;
                canSeek = true;
                cRestTimer = restTimer;
            }

        }

        if(bossControl == BossStates.Seek)
        {
            SendThemToDie();
        }
 
	}
    
    void Spawning()
    {

        Instantiate(orbiters, Spawnner.transform.position, Spawnner.transform.rotation);
    }

    void SendThemToDie()
    {
        sendToPlayer = true;
        orbiterCounter = 0;
        cRestTimer = restTimer;
        canSeek = false;
        bossControl = BossStates.Rest;
    }

}
