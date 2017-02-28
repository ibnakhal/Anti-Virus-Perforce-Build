using UnityEngine;
using System.Collections;

public class TurretMove : MonoBehaviour
{

    [SerializeField]
    private GameObject body = null;
    [Header ("Movement")]
    [SerializeField]
    public Vector3 vector = new Vector3(0, 0, 0);
    [SerializeField]
    private Transform direction = null;
    [SerializeField]
    public float force = 1f;


    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip moveClip;

    private Vector3 temp;

    [SerializeField]
    private Vector3 normal;


    // Use this for initialization
    void Start()
    {
        source.clip = moveClip;
        source.Play();
        vector = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        temp = transform.rotation.eulerAngles;
        temp.x = 0.0f;
        temp.z = 0.0f;
        temp.y = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I HIT");
        Debug.Log("Collision points: " + collision.contacts.Length);

        normal = collision.contacts[0].normal;

        Collider Other = collision.collider;

        if (Other.tag != "Player")
        {
            Debug.Log("WAll hit");
            //           Vector3 kiddy = Other.transform.FindChild("DIRECT").forward;
            vector = Vector3.Reflect(vector, normal);
            //this.transform.LookAt(this.transform.position + vector);
            Move();
        }
    }

    public void Move()
    {
        this. transform.rotation = Quaternion.Euler(temp);
        this.transform.Translate(vector * Time.deltaTime * force);
    }

}
