using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixMain : MonoBehaviour {
    
    private enum states { port, stern};
    public GameObject Manager;
    [Header ("Please note to change while making it symmetrical the starting distance must equal speedLimit")]
    public int currentHealth;
    private float baseSpeed;
    [SerializeField]
    private float shootDelay;
    private Vector3 selfPos;
    public float beforeShoot;
    private float cBeforeShoot;
    [SerializeField]
    private int MaxHealth = 50;
    public int CurrentHealth;
    public float bSpeed;
    [Header ("Directly Changes the max distance it will vibrate")]
    public float speedLimit;
    private float innerTuning;
    public float shTimer;
    public float speed;
    public enum whichSide { top, bottom};
    [SerializeField]
    private states currentState;
    public whichSide startingSide;
    [Header ("Directly changes the Frequency")]
    public float smoothingValue;
    public GameObject bullets;
    private float shooting;
    private Vector3 locVel;

    // Use this for initialization
    void Start ()
    {
        currentHealth = MaxHealth;
        selfPos = this.transform.position;
		if(startingSide == whichSide.top)
        {
            currentState = states.port;
            
        }
        if(startingSide == whichSide.bottom)
        {
            currentState = states.stern;
            
        }
	}
	
	// Update is called once per frame
	public void Update ()
    {
        //testing for death
        
        
        if(currentHealth <= 0)
        {
            Death();
        }
        cBeforeShoot += Time.deltaTime;
        if(cBeforeShoot >= beforeShoot)
        {
            Shooting();
        }
        
        if(startingSide == whichSide.top)
        {
            if(currentState == states.port)
            {
                if(speed <0.5 && speed >= 0)
                {
                    this.transform.position = selfPos;
                    speed = 0;
                }
            }
        }
        if(startingSide == whichSide.bottom)
        {
            if(currentState == states.stern)
            {
                if(speed > -0.5f && speed <= 0)
                {
                    this.transform.position = selfPos;
                    speed = 0;
                }
            }           
        }
        if(currentState == states.port)
        {
            PortSideMove();
            //if (startingSide == whichSide.bottom)
            //{
                if (speed <= -speedLimit)
                {
                    currentState = states.stern;
                }
            //}
            //else
            //{
                //if (speed <= -speedLimit - innerTuning)
                //{
                //    currentState = states.stern;
                //}
            //}
        }
        if(currentState == states.stern)
        {
            SternSideMove();
            //if (startingSide == whichSide.top)
            //{ 
                if (speed >= speedLimit)
                {
                    currentState = states.port;
                }
            //}
            //else
            //{
                //if (speed >= speedLimit + innerTuning)
                //{
                //    currentState = states.port;
                //}
            //}
        }

		
	}
    void PortSideMove()
    {
        this.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (startingSide == whichSide.top)
        {
            speed -= (smoothingValue + innerTuning) * Time.deltaTime;
        }
        else
        {
            speed -= smoothingValue * Time.deltaTime;
        }
    }
    void SternSideMove()
    {
        this.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (startingSide == whichSide.bottom)
        {
            speed += (smoothingValue + innerTuning) * Time.deltaTime;
        }
        else
        {
            speed += smoothingValue * Time.deltaTime;
        }
    }

    void Shooting()
    {
        shooting++;
        if (shooting % shootDelay == 0)
        {
            Debug.Log("ShootyShoot");
            
            GameObject Clone = Instantiate(bullets, this.transform.position, transform.rotation) as GameObject;

            //Clone.GetComponent<Rigidbody>().AddForce(this.transform.forward * bSpeed * Time.deltaTime);
            locVel = Clone.transform.InverseTransformDirection(Clone.GetComponent<Rigidbody>().velocity);

            Clone.GetComponent<Rigidbody>().velocity = transform.forward * bSpeed;
        }
        

    }

    public void GetHurt(int Damage)
    {
        currentHealth -= Damage;

    }

    void Death()
    {
        
        if(startingSide == whichSide.top)
        {
            Manager.GetComponent<HelixManager>().oneDead = true;
            Destroy(this.gameObject);
        }
        else
        {
            Manager.GetComponent<HelixManager>().twoDead = true;
            Destroy(this.gameObject);
        }  
        
        
    }
    

}
