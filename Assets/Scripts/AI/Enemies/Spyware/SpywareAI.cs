using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpywareAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private float turnSpeed = 0.01f;
    public int currentHealth;
    [SerializeField]
    private int maxHealth = 10;
    private float damageDistance;
    public Vector3 frontDamageRange;
    public bool isAggroed;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
    }
    private void FixedUpdate()
    {
        Vector3 relativePos = player.transform.position - transform.position;
        //Construct rotation looking at the Player
        Quaternion rotationPos = Quaternion.LookRotation(relativePos);
        //blends rotation in gradually in real time
        transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationPos, Time.deltaTime * turnSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        EyeTargeting();

        if (isAggroed == false)
        {
            StartCoroutine(AggroTrue());
        }
        else
        {
            StartCoroutine(AggroFalse());
        }

    }
    private void EyeTargeting()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "wall")
            {
                //spark.transform.position = hitWall.point;
                //detects how far away the wall is, so damage doesn't go through walls
                damageDistance = hit.distance;
                //detectys where this raycast is hitting the wall
                frontDamageRange = hit.point;
            }
        }
    }
    private IEnumerator AggroTrue()
    {
        yield return new WaitForSeconds(3);
        isAggroed = true;
    }
    private IEnumerator AggroFalse()
    {
        yield return new WaitForSeconds(3);
        isAggroed = false;
    }
}
