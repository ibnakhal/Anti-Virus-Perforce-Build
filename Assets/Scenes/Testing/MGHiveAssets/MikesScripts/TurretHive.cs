using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHive : MonoBehaviour
{
    [SerializeField]
    private GameObject TurretToSpawn;
    [SerializeField]
    private GameObject TurretSpawnPoint;
    [SerializeField]
    private GameObject[] ArmJoints;

    [SerializeField]
    private Animator SpawnAnimation;

    [SerializeField]
    private bool SpawnATurret;


    [SerializeField]
    private float ArmOpenDelay;
    [SerializeField]
    private float OpenArmSpawnDelay;
    [SerializeField]
    private float ArmCloseDelay;
    [SerializeField]
    private float spawnTimer;

    // Update is called once per frame
    void Update ()
    {
		if(SpawnATurret == true)
        {
            SpawnATurret = false;
            SpawnAnimation.SetBool("Spawning", true);
            StartCoroutine(SpawnTurret());
        }
	}

    IEnumerator SpawnTurret()
    {
        yield return new WaitForSeconds(ArmOpenDelay);
        print("Arms Open, Deploy turret");
        yield return new WaitForSeconds(OpenArmSpawnDelay);
        print("Turret Delpoyed, closing arms");
        GameObject clone = Instantiate(TurretToSpawn, TurretSpawnPoint.transform.position, TurretSpawnPoint.transform.rotation);
        yield return new WaitForSeconds(ArmCloseDelay);
        print("Arms closed, resuming movement");
        SpawnAnimation.SetBool("Spawning", false);
        yield return new WaitForSeconds(spawnTimer);
        SpawnATurret = true;
    }
}
