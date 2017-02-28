using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethBallBehavior : MonoBehaviour {

    [SerializeField]
    private List<Transform> shotLocations;


    [SerializeField]
    private float salvoCooldown;
    [SerializeField]
    private int salvoCount;
    [SerializeField]
    private float interval;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;
    // Use this for initialization
    void Start() {

        Transform[] tempList = this.gameObject.GetComponentsInChildren<Transform>();

        for (int x = 0; x < tempList.Length; x++)
        {
            if (tempList[x] != this.gameObject.transform)
            {
                shotLocations.Add(tempList[x]);
            }
        }
        
        
        StartCoroutine(Interval());
	}



    public IEnumerator Interval()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(salvoCooldown);
            for (int i = 0; i < salvoCount; i++)
            {
                yield return new WaitForSeconds(interval);
                for (int x = 0; x < shotLocations.Count; x++)
                {
                    Debug.Log("I'm getting the location, boss.");
                    GameObject clone = Instantiate(bullet, shotLocations[x].position, shotLocations[x].rotation);
                    clone.GetComponent<Rigidbody>().velocity = (shotLocations[x].transform.forward * Time.deltaTime * bulletSpeed);

                }


            }

        }

    }
}
