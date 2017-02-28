using UnityEngine;
using System.Collections;

public class NyxBulletBehavior : MonoBehaviour {


    [SerializeField]
    private GameObject explosion;


public void OnTriggerEnter(Collider Other)
    {
        if(Other.tag != "Boss" && Other.tag != "Nyxem Shot Point" && Other.tag != "Untagged" && Other.tag != "Bullet" && Other.tag != "Gun")
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            GameObject clone = Instantiate(explosion, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(this.gameObject);
        }
    }
}
