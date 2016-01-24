using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    PlayerManager playerMgr;

    Player player;
    Rigidbody rgdBdy;
    Camera cam;

    bool canControl = true;

    float rotateSpeed = 50f;
    float camRotateSpeed = 5f;
    float moveSpeed = 10f;
    float jumpPower = 250f;
    const float maxYRotation = 20f;

    bool isGrounded;
    void Awake()
    {
        playerMgr = GameObject.FindObjectOfType<PlayerManager>();
        player = GetComponent<Player>();
        rgdBdy = GetComponent<Rigidbody>();
        cam = GameObject.FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (!canControl)
            return;
        if (playerMgr.currPlayer != player)
            return;

        Vector3 localRotation = cam.transform.localRotation.eulerAngles;


        if(Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0), Space.Self);
        }
        if(Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.Self);
        }

        if (Input.GetMouseButton(1)) // Camera Rotation
        {
            float h = Input.GetAxis("Horizontal");

            //this.transform.Rotate(new Vector3(0, h * rotateSpeed * Time.deltaTime, 0), Space.Self);
            if ((localRotation.y >= 0 && localRotation.y < 30 ||
                localRotation.y > -30 && localRotation.y <= 0)||
                localRotation.y> 330 && localRotation.y <= 360)
                cam.transform.Rotate(new Vector3(0, h * camRotateSpeed * Time.deltaTime, 0), Space.World);
        }
        else // Y Rotation 복원
        {
            if (localRotation.y > 180)
                localRotation.y = localRotation.y - 360;

            float YdecreaseAmount = localRotation.y * Time.deltaTime * 3f;
            Vector3 newRotation = new Vector3(localRotation.x, localRotation.y - YdecreaseAmount, 0);

            cam.transform.localRotation = Quaternion.Euler(newRotation);
        }
    }

    void FixedUpdate()
    {
        if (!canControl)
            return;
        if (playerMgr.currPlayer != player)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
            rgdBdy.AddForce(transform.forward * moveSpeed * Time.fixedDeltaTime * 100);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(-transform.forward * moveSpeed * Time.deltaTime, Space.World);
            rgdBdy.AddForce(-transform.forward * moveSpeed * Time.fixedDeltaTime * 100);
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
        //    rgdBdy.AddForce(transform.TransformVector(Vector3.left) * moveSpeed * Time.fixedDeltaTime * 100);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        //    rgdBdy.AddForce(transform.TransformVector(Vector3.right) * moveSpeed * Time.fixedDeltaTime * 100);
        //}

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rgdBdy.AddForce(Vector3.up * jumpPower);
            isGrounded = false;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    public void PlayerChange(bool isSelected)
    {
        canControl = isSelected;
    }


}
