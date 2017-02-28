using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviros : MonoBehaviour
{

    [SerializeField]
    private string[] targetTags;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int baseDamage;
    //MG - Added wallTag, to make sure the send massage does not go after walls
    [SerializeField]
    private string wallTag = "wall";
    [SerializeField]
    private string messageToSend = "Ouch";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider Other)
    {
        for (int x = 0; x < targetTags.Length; x++)
        {
            if (Other.tag == targetTags[x])
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                GameObject clone = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
                if (Other.tag != wallTag)
                {
                    Other.SendMessage(messageToSend, baseDamage);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
