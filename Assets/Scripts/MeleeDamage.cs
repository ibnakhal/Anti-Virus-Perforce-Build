using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField]
    private string[] TagsToHit;
    [SerializeField]
    private int damageDealt;
    [SerializeField]
    private string MessageToSend;

    public void OnTriggerEnter(Collider other)
    {
        print("shread the target");
        print(other.tag);
        for(int i = 0; i < TagsToHit.Length; i++)
        {
            if(other.tag == TagsToHit[i])
            {
                print("sending a message");
                other.SendMessage(MessageToSend, damageDealt);
            }
        }
    }
}
