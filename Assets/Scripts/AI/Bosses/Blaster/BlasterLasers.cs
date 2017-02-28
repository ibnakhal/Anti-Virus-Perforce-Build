using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires Line Renderer on empty game object
public class BlasterLasers : MonoBehaviour
{
    private float damageDistance;
    [SerializeField]
    private GameObject spark;

    private LineRenderer lineRenderer;
    [SerializeField]
    private int damage;

    private Vector3 laserPoint;
    private Quaternion startRot;

    [SerializeField]
    private BlasterAI blaster;
    private bool playerHit = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startRot = this.transform.localRotation;
        spark = this.transform.Find("Spark").gameObject;
    }
    void FixedUpdate()
    {
        //this is where the line starts
        lineRenderer.SetPosition(0, this.transform.position);
        //this is where the line ends
        lineRenderer.SetPosition(1, laserPoint);
        spark.transform.position = laserPoint;

        if (blaster.laserNetActive)
        {
            HitWall();
            HitPlayer();
        }
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
        if (Physics.Raycast(transform.position, transform.forward, out hitPlayer, damageDistance))
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
