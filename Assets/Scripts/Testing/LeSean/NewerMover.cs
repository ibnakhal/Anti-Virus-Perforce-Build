using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewerMover : MonoBehaviour {

    public enum PlayerStates
    {
        walking,
        jumping,
        walling
    };
    PlayerStates state;
    

    public CharacterController my;
    public int maxJumps;
    [SerializeField]
    private int doubleJump;
    public Vector3 velocity;
    public float gravity;
    public Vector3 wallJumpTest;
    private Vector3 maxWallJumpTest;
    // Use this for initialization
    void Start()
    {
        doubleJump = maxJumps;
        my = GetComponent<CharacterController>();
        maxWallJumpTest = wallJumpTest;
        wallJumpTest = Vector3.zero;
    }


    public void Move(float x, float z)
    {
        velocity =  new Vector3(x, velocity.y, z);
    }

    public void Jump(float jumpSpeed)
    {
        RaycastHit hit;
        if (my.isGrounded)
        {
            velocity.y = jumpSpeed;
        }
        else if (doubleJump > 0)
        {
            velocity.y = jumpSpeed;
            doubleJump -= 1;
        }
      /*  else
        {
            Collider[] walls = Physics.OverlapSphere(this.transform.position, GetComponent<Collider>().bounds.extents.x * 2.1f);
            foreach (Collider wall in walls)
            {
                if (wall.tag == "wall")
                {
                    Physics.Linecast(transform.position, wall.transform.position, out hit);
                    WallJumpTest(-hit.normal);
                    Debug.DrawLine(transform.position, wall.transform.position, Color.blue, 10f);
                    Debug.DrawRay(hit.point, -hit.normal * 5, Color.red, 10f);
                    Debug.Log("Good Wall Jump");
                }
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (my.isGrounded)
        {
            wallJumpTest = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //WallJumpTest();
        }
        
    }
    public void ZeroOutVelocity()
    {
        velocity.z = 0;
        velocity.x = 0;

        if (my.isGrounded == true)
        {
            velocity.y = 0;
            doubleJump = maxJumps;
        }
    }

    public void LateUpdate()
    {
        //velocity += Physics.gravity * Time.deltaTime;
      
        
        velocity += new Vector3(velocity.x, -gravity, velocity.z) * Time.deltaTime;
        velocity = transform.TransformDirection(velocity) + new Vector3 (wallJumpTest.x, 0, wallJumpTest.z);
        my.Move(velocity * Time.deltaTime);

        if (!my.isGrounded)
        {

        }
    }

    public void WallJumpTest(Vector3 wallDir)
    {
        wallJumpTest = maxWallJumpTest;
        velocity = new Vector3(wallJumpTest.x * wallDir.x, velocity.y, wallJumpTest.z * wallDir.z);
        //velocity += new Vector3(wallJumpTest.x, 0, wallJumpTest.z);
    }

    public Vector2 WallJump()
    {
        Vector2 weapon = new Vector2(0, 0);
        Collider[] walls = Physics.OverlapSphere(this.transform.position, GetComponent<Collider>().bounds.extents.x * 2.1f);
        foreach(Collider wall in walls)
        {
            if(wall.tag == "wall")
            {
                velocity = new Vector3(0, 0, 0);
            }
        }
      //  if(){return weapon;}

        return weapon;
    }
}
