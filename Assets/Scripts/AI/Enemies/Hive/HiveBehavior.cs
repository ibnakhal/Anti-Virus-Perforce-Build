using UnityEngine;
using System.Collections;

public class HiveBehavior : MonoBehaviour
{
    [SerializeField]
    private float interval;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform[] holes;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int salvoCount;
    [SerializeField]
    private float salvoCooldown;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Interval());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);



    }

    public void Activate()
    {

    }

    public IEnumerator Interval()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(salvoCooldown);
            for (int i = 0; i < salvoCount; i++)
            {
                yield return new WaitForSeconds(interval);
                for (int x = 0; x < holes.Length; x++)
                {
                    GameObject clone = Instantiate(bullet, holes[x].position, holes[x].rotation);
                    clone.GetComponent<Rigidbody>().velocity = (holes[x].transform.forward * Time.deltaTime * bulletSpeed);

                }


            }

        }

    }
}
