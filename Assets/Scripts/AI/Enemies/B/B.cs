using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{

    /*
     * Variables:
     *      Float fireRate 0.43
     *      Int Moves
     *      GameObject player
     *      
     * Functions:
     *      Move
     *      Boom
     *      
     */

    public enum State
    {
        idle,
        moving
    };

    public State state;

    public float fireRate;
    private float initFireRate;
    public int moves;
    //public GameObject player;
    //public Vector3 boom;
    public float moveTime;
    //public Vector3 randMove;
    public float idleTime;
    private float initIdleTime;
    private float initMoveTime;
    public float speed;
    private Rigidbody rb;
    private Vector3 direction;
	private CharacterController playerRB;
    public float maxDistFromPlayer;
    public float minDistFromPlayer;
    private float bullets;
    public GameObject bullet;


    public void Boom()
    {
        //direction = transform.InverseTransformDirection(rb.velocity);
        //locVel.z = 1;
        //rb.velocity = transform.TransformDirection(locVel);

        transform.LookAt(playerRB.transform.position);


        if (Vector3.Distance(this.transform.position, playerRB.transform.position) >= maxDistFromPlayer)
        {
            //Debug.Log("Further than 5 at" + Vector3.Distance(this.transform.position, player.transform.position) + direction);
            direction.z = 1 * speed;
        }
        else if (Vector3.Distance(this.transform.position, playerRB.transform.position) <= minDistFromPlayer)
        {

            direction.z = -1 * speed;
            //Debug.Log("Closer than 4.5 at" + Vector3.Distance(this.transform.position, player.transform.position) + direction);
        }
        else
        {
            //Debug.Log("Elsed!!!");
            direction.z = 0;
        }

        if(transform.position.y >= playerRB.transform.position.y + (minDistFromPlayer * .7))
        {
            direction.y = -1 * speed;
        }
        if (transform.position.y <= playerRB.transform.position.y + (minDistFromPlayer * .3))
        {
            direction.y = 1 * speed;
        }
        rb.velocity =  transform.TransformDirection(direction.x, direction.y, direction.z);
       // transform.position = Vector3.MoveTowards(transform.position, player.transform.position - transform.forward * 5, speed * Time.deltaTime);

        //transform.position = player.transform.position + boom;
        //A = b + c
        //therfore, C = a+B
    }
    void Start()
    {

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        fireRate = idleTime / 4;
        initMoveTime = moveTime;
        initFireRate = fireRate;
        initIdleTime = idleTime;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    // Update is called once per frame

    public void Move()
    {
     
        //rb.velocity = randMove;
        //transform.position = Vector3.MoveTowards(transform.position,  * speed * Time.deltaTime);

    }
    // Update is called once per frame

    public void Fire()
    {
        Instantiate(bullet, transform.position + (transform.forward * 3), transform.rotation);
    }

    void Update()
    {

        switch (state)
        {
            case State.idle:
                fireRate -= Time.deltaTime;
                if(fireRate <= .1f)
                {
                    Fire();
                    fireRate = initFireRate;
                    bullets--;
                }

                if(bullets == 0)
                {
                    fireRate = initFireRate;
                    bullets = 3;
                }
                idleTime -= Time.deltaTime;
                if (idleTime <= 0)
                {
                    moveTime = -1;
                    moves = 3;
                    state = State.moving;
                }

                break;

            case State.moving:
                moveTime -= Time.deltaTime;
                //Boom();
                if (moves == 0)
                {
                    direction.x = 0;
                    direction.y = 0;
                    idleTime = initIdleTime;
                    //boom = player.transform.position - transform.position;
                    state = State.idle;
                }
                if (moveTime <= 0)
                {
                    direction.x = Random.Range(-1, 2) * speed;
                    if (direction.x == 0) direction.x = 1;
                    direction.y = Random.Range(-1, 2) * speed;
                    moveTime = initMoveTime;
                    moves--;
                }
                
                break;

        }
    }

    void LateUpdate()
    {
        Boom();
    }

}
