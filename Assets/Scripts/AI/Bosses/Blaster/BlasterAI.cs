using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterAI : MonoBehaviour
{
    public bool laserNetActive = false;
    [SerializeField]
    private float laserNetDuration = 3.0f;

    [SerializeField]
    private float interval;
    [SerializeField]
    private float speed;

    //where bullets are spawned from
    [SerializeField]
    private Transform[] topHoles;
    [SerializeField]
    private Transform[] botHoles;
    //the barrel itself
    [SerializeField]
    private GameObject[] topBarrels;
    [SerializeField]
    private GameObject[] botBarrels;
    //barrel position on the turret
    [SerializeField]
    private Transform[] botTurretTransforms;
    [SerializeField]
    private Transform[] topTurretTransforms;
    //barrel position on the wall
    [SerializeField]
    private Transform[] botWallTransforms;
    [SerializeField]
    private Transform[] topWallTransforms;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int salvoCount;
    [SerializeField]
    private float salvoCooldown;

    private bool onWall = false;

    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if(onWall)
        {
            StartCoroutine(MorphToTurret());
            //StartCoroutine(TopSimpleShoot());
        }
        else
        {
            StartCoroutine(MorphToWall());
        }
    }

    private IEnumerator TopSimpleShoot()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(salvoCooldown);
            for (int i = 0; i < salvoCount; i++)
            {
                yield return new WaitForSeconds(interval);
                for (int x = 0; x < topHoles.Length; x++)
                {
                    GameObject clone = Instantiate(bullet, topHoles[x].position, topHoles[x].rotation);
                    clone.GetComponent<Rigidbody>().velocity = (topHoles[x].transform.forward * Time.deltaTime * bulletSpeed);
                }
            }
        }
    }
    private IEnumerator BottomSimpleShoot()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(salvoCooldown);
            for (int i = 0; i < salvoCount; i++)
            {
                yield return new WaitForSeconds(interval);
                for (int x = 0; x < botHoles.Length; x++)
                {
                    GameObject clone = Instantiate(bullet, botHoles[x].position, botHoles[x].rotation);
                    clone.GetComponent<Rigidbody>().velocity = (botHoles[x].transform.forward * Time.deltaTime * bulletSpeed);
                }
            }
        }
    }
    private IEnumerator MorphToWall()
    {

        yield return new WaitForSeconds(5);
        for (int i = 0; i < topBarrels.Length; i++)
        {
            topBarrels[i].transform.position = topWallTransforms[i].position;
            topBarrels[i].transform.rotation = topWallTransforms[i].rotation;

            botBarrels[i].transform.position = botWallTransforms[i].position;
            botBarrels[i].transform.rotation = botWallTransforms[i].rotation;
        }

        onWall = true;
    }
    private IEnumerator MorphToTurret()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < topBarrels.Length; i++)
        {
            topBarrels[i].transform.position = topTurretTransforms[i].position;
            topBarrels[i].transform.rotation = topTurretTransforms[i].rotation;

            botBarrels[i].transform.position = botTurretTransforms[i].position;
            botBarrels[i].transform.rotation = botTurretTransforms[i].rotation;
        }

        onWall = false;
    }
    /*private void MorphTop()
    {
        for (int i = 0; i < topBarrels.Length; i++)
        {
            //TODO make this a Lerp
            //topBarrels[i].transform.position = topTurretTransforms[i].position;
            //topBarrels[i].transform.rotation = topTurretTransforms[i].rotation;

            topBarrels[i].transform.position = topWallTransforms[i].position;
            topBarrels[i].transform.rotation = topWallTransforms[i].rotation;
        }
    }
    private void MorphBot()
    {
        for (int i = 0; i < botBarrels.Length; i++)
        {
            //TODO make this a Lerp and have different positions
            //botBarrels[i].transform.position = botTurretTransforms[i].position;
            //botBarrels[i].transform.rotation = botTurretTransforms[i].rotation;

            botBarrels[i].transform.position = botWallTransforms[i].position;
            botBarrels[i].transform.rotation = botWallTransforms[i].rotation;
        }
    }
    private void MovePlatforms()
    {

    }
    private IEnumerator LaserNet()
    {
        laserNetActive = false;
        yield return new WaitForSeconds(laserNetDuration);
        laserNetActive = true;
    }*/
}


