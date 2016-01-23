using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{

    PlayerManager playerMgr;

    Player player;
    bool canControl = true;

    float rotateSpeed = 5f;
    float moveSpeed = 5f;
    const float maxYRotation = 20f;
    void Awake()
    {
        playerMgr = GameObject.FindObjectOfType<PlayerManager>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (!canControl)
            return;
        if (playerMgr.currPlayer != player)
            return;

        Vector3 prevRotation;
        float h = Input.GetAxis("Horizontal");

        prevRotation = this.transform.rotation.eulerAngles;
        this.transform.Rotate(new Vector3(0, h * rotateSpeed * Time.deltaTime, 0), Space.Self);


        //float v = Input.GetAxis("Vertical");

        //prevRotation = this.transform.rotation.eulerAngles;
        //this.transform.Rotate(new Vector3(v * rotateSpeed * Time.deltaTime, 0, 0),Space.Self);
        //if (!(this.transform.rotation.eulerAngles.x < 30 && this.transform.rotation.eulerAngles.x > -30))
        //    this.transform.rotation = Quaternion.Euler(prevRotation);


        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime,Space.World);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void PlayerChange(bool isSelected)
    {
        canControl = isSelected;
    }
    
 
}
