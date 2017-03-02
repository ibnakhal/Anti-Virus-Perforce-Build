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
            while(phaseFour.transform.localScale.x >= 0.1f)
            {
                phaseFour.transform.localScale -= Vector3.one * Time.deltaTime * 10;
                yield return new WaitForSeconds(0.0001f);
            }
        }

        if (phaseThree.activeSelf == true)
        {
            yield return new WaitForSeconds(0.5f);
            while (phaseThree.transform.localScale.x >= 0.1f)
            {
                phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
                yield return new WaitForSeconds(0.0001f);
            }
        }

        if (phaseTwo.activeSelf == true)
        {
            yield return new WaitForSeconds(0.5f);
            while (phaseTwo.transform.localScale.x >= 0.1f)
            {
                phaseTwo.transform.localScale -= Vector3.one * Time.deltaTime * 10;
                yield return new WaitForSeconds(0.0001f);
            }
        }

        yield return new WaitForSeconds(3);

        Instantiate(deathExplosion, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
