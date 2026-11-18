using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float speed;
    public float groundDist;
    
    private bool canMove = true;

    public LayerMask terrainLayer;
    private Rigidbody rb;
    public SpriteRenderer sr;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!canMove)
            return;
        /*RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
        */
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //checks if left shift being held
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        //choose between walk and sprint
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Debug.Log("horizontal" + x + "vertical" + z);
        Vector3 moveDir = new Vector3(x, 0, z);
        //applied movement
        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);

        //flip sprite depending on direction
        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    }

    public void FreezePlayer()
    {
        canMove = false;
        rb.linearVelocity = Vector3.zero;
    }

    public void UnfreezePlayer()
    {
        canMove = true;
    }
}