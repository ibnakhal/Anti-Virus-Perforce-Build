using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Adware : MonoBehaviour {

    public bool triggered;
    private PlayerController pC;

    [Header("RNG")]
    [SerializeField]
    int x;
    int x2;
    [SerializeField]
    int y;
    int y2;
    [SerializeField]
    int z;

    [Header ("Status Traits")]
    [SerializeField]
    private Image[] ads;
    [SerializeField]
    private string alert;
    [SerializeField]
    private int timeLimit = 8;
    [SerializeField]
    private int lowerTLimit = 2;
    [SerializeField]
    private int lowerImageX = 90;
    [SerializeField]
    private int upperImageX = 850;
    [SerializeField]
    private int lowerImageY = 90;
    [SerializeField]
    private int upperImageY = 90;
    [SerializeField]
    private int delayLimit = 2;

    private int imageX;
    private int imageY;
    public Canvas adSpace;



    // Use this for initialization
    void Start () {
        
        pC = this.GetComponentInParent<PlayerController>();
        triggered = false;

        //disables all ads in the start
        for(int i = 0; i < ads.Length; i++)
        {
            ads[i].enabled = false;
        }        
    }
	
	// Update is called once per frame
	void Update () {
        //for testing, remember to remove when everything is done
	    if(Input.GetKeyDown(KeyCode.T))
        {
            Infect();
        }
        //if(Input.GetKeyUp(KeyCode.T))
        //{
        //    triggered = false;
        //}
        //clears screen when the debuff is cleared
        if(triggered == false)
        {
            for (int i = 0; i < ads.Length; i++)
            {
                if (ads[i].enabled == true)
                {
                    ads[i].enabled = false;
                }
            }
        }
	}
    public void Infect()
    {
        triggered = true;
        StartCoroutine(Adwared());
        pC.Warner(alert);
    }

    public IEnumerator Adwared()
    {
        while(triggered)
        {
            //randomizes which ad to spawn
            x = Random.Range(0, ads.Length);
            imageX = Random.Range(lowerImageX, upperImageX);
            imageY = Random.Range(lowerImageY, upperImageY);
            ads[x].enabled = true;
            ads[x].transform.position = new Vector3(imageX, imageY, 0);
            y2 = Random.Range(1, 4);
            yield return new WaitForSeconds(y2);
            x2 = Random.Range(0, ads.Length);
            imageX = Random.Range(lowerImageX, upperImageX);
            imageY = Random.Range(lowerImageY, upperImageY);
            ads[x2].enabled = true;
            ads[x2].transform.position = new Vector3(imageX, imageY, 0);
            //randomizes the time the ad stays on screen
            y = Random.Range(lowerTLimit, timeLimit);
            yield return new WaitForSeconds(y);
            ads[x].enabled = false;
            ads[x2].enabled = false;
            //what does this do?
            z = Random.Range(0, delayLimit);
            Debug.Log("Z = " + z);
            yield return new WaitForSeconds(z);

        }
        //failsave 1 incase something doesnt finish
        ads[x].enabled = false; ;
    }



}
