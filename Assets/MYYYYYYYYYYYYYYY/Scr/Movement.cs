using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 10f;  
    public float turnSpeed = 80f;  
    public float jumpForce = 5f;  
    private Rigidbody rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    
    private int jumpCount = 0; 
    private float cooldownTime = 5f;  
    private float timeSinceLastJump = 0f;  

    
    public float customGravity = -9.81f;  
    private Vector3 gravityForce;  


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        rb.freezeRotation = true;

        
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        
        timeSinceLastJump += Time.deltaTime;

        
        float move = Input.GetAxis("Vertical") * moveSpeed;   
            
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime; 

        
        Vector3 moveDirection = transform.forward * move;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z); 

        
        float turnAmount = turn * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f)); 

        
        gravityForce = new Vector3(0, customGravity, 0);  
        rb.AddForce(gravityForce, ForceMode.Acceleration);  

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPositionAndRotation();
        }
        
       
    }

    void Jump()
    {
        if (timeSinceLastJump >= cooldownTime)
        {
            jumpCount = 0;  
        }

        if (jumpCount < 2 && IsGrounded())  
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            
            jumpCount++;

            
            timeSinceLastJump = 0f;
        }
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    
    void ResetPositionAndRotation()
    {
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
    