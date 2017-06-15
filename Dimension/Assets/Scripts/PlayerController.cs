using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

//The value of Horizontal input
    private float inputDirection;   

//The value of Vertical Velocity
    private float verticalVelocity;    

//Movement speed
    private float speed = 5.0f;

//Gravity effecting the player
	public float gravity = 25.0f;

//The force of a jump
	public float jumpForce = 10.0f;

//IF we can double jump or not
    private bool canDoubleJump = false;

// 3 vector holding x, y and z values for movement ( x for left and right and y for up down, z is depth and not used in 2d platformer
    private Vector3 moveVector;


    private Vector3 lastMotion;
    private CharacterController controller; // ref to our controller



	void Start ()
    {

        controller = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        IsControllerGrounded();
        moveVector = Vector3.zero;
        inputDirection = Input.GetAxis("Horizontal") * speed;

    
        if (IsControllerGrounded())
        {
            verticalVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Mkae player jump;
                verticalVelocity = jumpForce;
                canDoubleJump = true;
            }
            moveVector.x = inputDirection;

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDoubleJump)
                {
                    //Mkae player jump;
                    verticalVelocity = jumpForce * 1.2f;
                    canDoubleJump = false;

                }
                
            }

            verticalVelocity -= gravity * Time.deltaTime;
            moveVector.x = lastMotion.x;
        }
       
        moveVector.y = verticalVelocity;
       
        //Debug.Log(moveVector);
        controller.Move(moveVector * Time.deltaTime);
        lastMotion = moveVector;

		if(HitRoof()){
			verticalVelocity = 0;
		}
	}


	private bool HitRoof(){

		Vector3 leftRayTopStart;
		Vector3 rightRayTopStart;

		leftRayTopStart = controller.bounds.center;
		rightRayTopStart = controller.bounds.center;

		leftRayTopStart.x -= controller.bounds.extents.x;
		rightRayTopStart.x += controller.bounds.extents.x;


		Debug.DrawRay(leftRayTopStart, Vector3.up, Color.blue);
		Debug.DrawRay(rightRayTopStart, Vector3.up, Color.yellow);

		if (Physics.Raycast (leftRayTopStart, Vector3.up, (controller.height / 2) + 0.1f)){
			return true;
		}



		if (Physics.Raycast(rightRayTopStart, Vector3.up, (controller.height / 2) + 0.1f)){
			return true;
		}

		return false;

	}



    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;
		Vector3 leftRayTopStart;
		Vector3 rightRayTopStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

		leftRayTopStart = controller.bounds.center;
		rightRayTopStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        rightRayStart.x += controller.bounds.extents.x;

		leftRayTopStart.x -= controller.bounds.extents.x;
		rightRayTopStart.x += controller.bounds.extents.x;



        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);
		Debug.DrawRay(leftRayTopStart, Vector3.up, Color.blue);
		Debug.DrawRay(rightRayTopStart, Vector3.up, Color.yellow);

        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height / 2) + 0.1f))
            return true;

        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height / 2) + 0.1f))
            return true;

		if (Physics.Raycast (leftRayTopStart, Vector3.up, (controller.height / 2) + 0.1f))
			controller.SimpleMove (Vector3.zero);
			return false;

		if (Physics.Raycast(rightRayTopStart, Vector3.up, (controller.height / 2) + 0.1f))
			controller.SimpleMove (Vector3.zero);
			return false;

        return false;
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Hit " + hit.gameObject.name + "." );

        if (controller.collisionFlags == CollisionFlags.Sides)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2.0f);
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
                canDoubleJump = true;
            }
        }

        //Collectibles
        switch (hit.gameObject.tag)
        {
            case "Coin":
                Destroy(hit.gameObject);
                LevelManager.Instance.CollectCoin();
                break;
            case "JumpPad":
                //JumpPad Code
                verticalVelocity = jumpForce * 2;
                break;
            case "Teleporter":
                //Teleporter Code
                transform.position = hit.transform.GetChild(0).position;       
                break;
            case "WinBox":
                //Win Code
                LevelManager.Instance.Win();
                break;

            default:
                break;
        }
    }
}
