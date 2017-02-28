using UnityEngine;
using System.Collections;


public class Stage1 : MonoBehaviour
{
    [Header("General")]
    public int stage;

    [Header("UI")]
    [SerializeField]
    private AudioSource aSource;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color endColor;
    [SerializeField]
    private Renderer rend;

    [Header("Targeting")]
    [SerializeField]
    private Vector3 hiddenvector;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform[] spPoints;
    [SerializeField]
    private bool hitPlayer = false;




    [Header("Fire")]



    [Header("Pulse")]
    [SerializeField]
    public int pulseDamage;
    [SerializeField]
    private GameObject pulseField;
    private bool pulsing = false;
    [SerializeField]
    private float radialLimit;


    [Header("Spawn")]
    [SerializeField]
    private GameObject spawnable;
    [SerializeField]
    private bool spawning = false;

    [SerializeField]
    private bool Done = true, charging = false;


    [SerializeField]
    private float pTime, fTime, radius, radialLock, force, lerpValue, timer, duration;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private AudioClip chargeClip, dischargeClip;

    [SerializeField]
    public enum StateType
    {
        Fire,
        Pulse,
        Spawn,
        size,
        
    };

    public StateType currentStateType;
    private NyxemBasicBehavior nyx;
    [SerializeField]
    private int shotLimit, totalCount;
    void Start()
    {
        StartCoroutine("Started");
        //boolNum = new bool[(int)boolType.size];
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nyx = GameObject.FindGameObjectWithTag("Boss").GetComponent<NyxemBasicBehavior>();
        radialLock = radius;
        hiddenvector = pulseField.gameObject.transform.localScale;


    }

    void Update()
    {
        if(charging)
        {
            timer += Time.deltaTime;

            lerpValue = (timer / duration);
            rend.material.color = Color.Lerp(startColor, endColor, lerpValue);
        }
        if(pulsing)
        {
            timer += Time.deltaTime;

            lerpValue = (timer / duration);
            rend.material.color = Color.Lerp(endColor,startColor, lerpValue);
        }
        else if (!charging && !pulsing)
        {
            lerpValue = 0;
        }
    }


    public IEnumerator Fire()
    {
        Done = false;
        while (totalCount < shotLimit)// && boolNum[(int)boolType.Firing])
        {
            yield return new WaitForSeconds(fTime);
            int r = Random.Range(0, nyx.spPoints.Length);
            Vector3 VectorD = (player.position - nyx.spPoints[r].position);
            //Raycast to find out if it goes through Nyxem, if so re-roll DONE!
            Ray cast = new Ray(nyx.spPoints[r].position, VectorD);
            RaycastHit hit;
            bool infoReturn = Physics.Raycast(cast, out hit, 100);
            if (infoReturn)
            {
                if (hit.transform.tag == "Boss")
                {
                    //print("I HIT MYSELF");
                    r = Random.Range(0, nyx.spPoints.Length);
                }
                else if (hit.transform.tag == "Player")
                {

                    GameObject clone = Instantiate(nyx.bullet, nyx.spPoints[r].position, nyx.spPoints[r].rotation) as GameObject;
                    clone.GetComponent<Rigidbody>().velocity = (VectorD * Time.deltaTime * 50);
                    totalCount++;
                }
            }

            //print(VectorD);
            //print("BLARGHARGHARGHAREH");

        }
        totalCount = 0;
        Done = true;
    }

    public IEnumerator Started()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(1);
            if (Done)
            {
                int s = Random.Range(0, ((int)StateType.size-stage));
                StateType state = (StateType)s;
                string name = state.ToString();
                print(name);
                StartCoroutine(name);
            }

        }

    }

    public IEnumerator Spawn()
    {
        Done = false;
        spawning = true;
        while (spawning)
        {
            Debug.Log("Pickles and Eggs!");
            for (int x = 0; x < nyx.spPoints.Length; x++)
            {
                GameObject clone = Instantiate(spawnable, nyx.spPoints[x].position, nyx.spPoints[x].rotation) as GameObject;
                yield return new WaitForSeconds(0.2f);
            }
            spawning = false;
        }
        Done = true;
    }

    public IEnumerator Pulse()
    {
        Done = false;

        aSource.clip = chargeClip;
        duration = chargeClip.length;
        charging = true;
        aSource.Play();

        
        
        yield return new WaitForSeconds(aSource.clip.length);
        aSource.clip = dischargeClip;
        duration = dischargeClip.length;
        aSource.Play();
        charging = false;
        lerpValue = 0;

        pulsing = true;
        while (pulsing && radius < radialLimit)
        {

            Vector3 targetDir = (player.position - this.gameObject.transform.position);
            Ray ray = new Ray(this.gameObject.transform.position, targetDir);
            RaycastHit hit;
            bool returned = Physics.Raycast(ray, out hit, radius);
            //Debug.Log(returned);
            if( returned)
            {
                if (hit.transform.tag == "Player" && !hitPlayer)
                {
                    //Debug.Log("HIT");
                    player.gameObject.GetComponent<PlayerController>().Ouch(pulseDamage);
                    hitPlayer = true;
                }
            }
            yield return new WaitForSeconds(0.0001f);
            radius += 0.5f;
           // print(radius);
            pulseField.gameObject.transform.localScale += new Vector3(0.0021F, 0.0021f, 0.0021f);
            //print (pulseField.gameObject.transform.localScale);
        }
        pulsing = false;
        lerpValue = 0;
        timer = 0;
        Done = true;
        radius = radialLock;
        hitPlayer = false;
        pulseField.gameObject.transform.localScale = hiddenvector;
       
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.position, radius);
    }


    public void StageChanger()
    {
        stage += -1;
    }
}

