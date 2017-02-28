using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B42DeathManager : MonoBehaviour {

    [SerializeField]
    GameObject phaseOne;
    [SerializeField]
    GameObject phaseTwo;
    [SerializeField]
    GameObject phaseThree;
    [SerializeField]
    GameObject phaseFour;
    [SerializeField]
    GameObject deathExplosion;

    ILYBGFXBasicBehavior[] bossSpinner;
    BasicEnemyHealth[] childHealth;
    B42BasicBehaviorV2[] bossSpawner;

	// Use this for initialization
	void Start ()
    {
        bossSpinner = GetComponentsInChildren<ILYBGFXBasicBehavior>();

    }

    public void BossDeath()
    {
        for(int i = 0; i < bossSpinner.Length; i++)
        {
            Destroy(bossSpinner[i]);
        }

        bossSpawner = GetComponentsInChildren<B42BasicBehaviorV2>();

        for (int i = 0; i < bossSpawner.Length; i++)
        {
            Destroy(bossSpawner[i]);
        }

        StartCoroutine(KillChildren());
    }

    IEnumerator KillChildren()
    {
        childHealth = GetComponentsInChildren<BasicEnemyHealth>();

        for (int i = 0; i < childHealth.Length; i++)
        {
            childHealth[i].Ouch(500);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(2);

        if (phaseFour.activeSelf == true)
        {
            yield return new WaitForSeconds(1);
            phaseFour.SetActive(false);
        }

        if (phaseThree.activeSelf == true)
        {
            yield return new WaitForSeconds(1);
            phaseThree.SetActive(false);
        }

        if (phaseTwo.activeSelf == true)
        {
            yield return new WaitForSeconds(1);
            phaseTwo.SetActive(false);
        }

        yield return new WaitForSeconds(3);

        Instantiate(deathExplosion, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
