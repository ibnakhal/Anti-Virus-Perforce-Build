using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private float damageDistance;
    private float counter;
    private GameObject spark;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private SpywareAI spyware;
    [SerializeField]
    private float focusSpeed = 5;
    [SerializeField]
    private int damage;
    private Vector3 laserPoint;
    [SerializeField]
    private Quaternion startRot;

    [SerializeField]
    private bool playerHit = false;

    [SerializeField]
    private GameObject target;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startRot = this.transform.localRotation;
        spark = this.transform.Find("Spark").gameObject;
    }
	void FixedUpdate ()
    {
        //this is where the line starts
        lineRenderer.SetPosition(0, this.transform.position);
        spark.transform.position = laserPoint;
        lineRenderer.SetPosition(1, laserPoint);
        if (spyware.isAggroed == true)
        {
            //draws line to the front
            Vector3 relativePos = spyware.frontDamageRange - transform.position;
            //Construct rotation looking at the focus of the laser
            Quaternion rotationPos = Quaternion.LookRotation(relativePos);
            //blends rotation in gradually in real time
            transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationPos, Time.deltaTime * focusSpeed);
        }
        else
        {
            //draws line to the sides
            transform.localRotation = Quaternion.Lerp(this.transform.localRotation, startRot, Time.deltaTime * focusSpeed);
        }

        HitWall();
        HitPlayer();
    }
    private void HitWall()
    {
        RaycastHit hitWall;
        Physics.Raycast(transform.position, transform.forward, out hitWall);
        if (hitWall.collider != null)
        {
            if (hitWall.collider.tag == "wall")
            {
                laserPoint = hitWall.point;
                damageDistance = hitWall.distance;
            }
        }
    }
    private void HitPlayer()
    {
        RaycastHit hitPlayer;
        if(Physics.Raycast(transform.position, transform.forward, out hitPlayer, damageDistance))
        {
            if (hitPlayer.collider != null)
            {
                if (hitPlayer.collider.tag == "Player" && !playerHit)
                {
                        hitPlayer.collider.GetComponent<PlayerController>().Ouch(damage);
                        playerHit = true;
                        StartCoroutine(waiting());
                }
            }
        }
    }


    public IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
        playerHit = false;
    }
}
