using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;
    public int currentHealth;
    public enum StateOfAgression { low, med, high};
    public StateOfAgression AgroControl;
    public enum StateOfOrb { Spawnning, Orbiting, Seeking};
    public StateOfOrb OrbitalStatus;
    [SerializeField]
    private GameObject mainBoss;
    private Transform center;
    private Vector3 axis = Vector3.up;
    private Vector3 orbitalPosition;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float ejectSpeedMin;
    [SerializeField]
    private float ejectSpeedMax;
    [SerializeField]
    private float radiusSpd;
    [SerializeField]
    private float rotSpd;
    [SerializeField]
    private float ejectSpeed;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float SeekingSpd;
    public bool seekingPlayer = false;


	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        //the main object these things orbit
        mainBoss = GameObject.FindGameObjectWithTag("PoisonIvyMain");
        player = GameObject.FindGameObjectWithTag("Player");

        //the speed it spawns from the boss
        ejectSpeed = Random.Range(ejectSpeedMin, ejectSpeedMax);
        OrbitalStatus = StateOfOrb.Spawnning;
        center = mainBoss.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //health counter
        if(currentHealth <= 0)
        {
            Death();
        }

        //recieves signal from the boss
        seekingPlayer = mainBoss.GetComponent<BossMain>().sendToPlayer;

        if(seekingPlayer)
        {
            OrbitalStatus = StateOfOrb.Seeking;
        }

        if(OrbitalStatus == StateOfOrb.Spawnning)
        {
            StartingOrbit();
        }
        if(OrbitalStatus == StateOfOrb.Orbiting)
        {
            OrbitingStuff();
        }
        if(OrbitalStatus == StateOfOrb.Seeking)
        {
            SeekingPlayer();
        }
	}

    //it gets spawned and locates the boss before spawning
    void StartingOrbit()
    {
        
        this.transform.Translate(Vector3.forward * ejectSpeed * Time.deltaTime);
        ejectSpeed -= 2.5f * Time.deltaTime;
        if(ejectSpeed <= 0)
        {
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, Vector3.back);
            if(Physics.Raycast(ray, out hit))
            {
                radius = hit.distance;
            }
            OrbitalStatus = StateOfOrb.Orbiting;
        }
    }

    //actrually orbiting the boss
    void OrbitingStuff()
    {
        transform.RotateAround(center.position, axis, rotSpd * Time.deltaTime);
        orbitalPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, orbitalPosition, radiusSpd * Time.deltaTime);
    }

    //looking for the player
    void SeekingPlayer()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * SeekingSpd * Time.deltaTime);
    }

    public void GetHurt(int Damage)
    {
        currentHealth -= Damage;

    }

    void Death()
    {
        //mainBoss.GetComponent<BossMain>().orbiterCounter--;
        Destroy(this.gameObject);
    }
}
