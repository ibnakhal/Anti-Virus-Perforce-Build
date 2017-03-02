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
    [SerializeField]
    GameObject deathImplosion;

    ILYBGFXBasicBehavior[] bossSpinner;
    BasicEnemyHealth[] childHealth;
    B42BasicBehaviorV2[] bossSpawner;

    float spinSpeed = 1000;

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

        yield return new WaitForSeconds(3);
        
        while(phaseFour.transform.localScale.x >= 0.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
            
        yield return new WaitForSeconds(0.5f);
        while (phaseThree.transform.localScale.x >= 1 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        } 
        yield return new WaitForSeconds(0.5f);
        while (phaseFour.transform.localScale.x >= 0.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseTwo.transform.localScale.x <= 3 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.5f);
        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseThree.transform.localScale.x >= 0.1f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.5f);
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseTwo.transform.localScale.x >= 1 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.5f);
        while (phaseThree.transform.localScale.x >= 0.5f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseFour.transform.localScale.x >= 0.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }
        while (phaseTwo.transform.localScale.x <= 3 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.0001f);
        }

        spinSpeed = 3000;

        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.1f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseTwo.transform.localScale.x >= 1 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.5f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.5f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x <= 2 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseFour.transform.localScale.x >= 0.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseTwo.transform.localScale.x <= 3 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.1f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseTwo.transform.localScale.x >= 1 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseTwo.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.5f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x <= 3 && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseThree.transform.localScale.x >= 0.5f && phaseThree.activeSelf == true)
        {
            phaseThree.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            phaseThree.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        yield return new WaitForSeconds(0.5f);
        while (phaseFour.transform.localScale.x <= 1.5f && phaseFour.activeSelf == true)
        {
            phaseFour.transform.localScale += Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }
        while (phaseTwo.transform.localScale.x >= 0.1 && phaseTwo.activeSelf == true)
        {
            phaseTwo.transform.localScale -= Vector3.one * Time.deltaTime * 10;
            yield return new WaitForSeconds(0.00001f);
        }

        yield return new WaitForSeconds(1);

        Instantiate(deathImplosion, this.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(4.5f);

        Instantiate(deathExplosion, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
