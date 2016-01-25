using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour
{
    PlayerManager playerMgr;
    Camera cam;
    void Awake()
    {
        playerMgr = GameObject.FindObjectOfType<PlayerManager>();
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject != null)
                {
                    Player player = GameUtil.GetTopLevelParent(hit.collider.gameObject).GetComponent<Player>();
                    if (player != null)
                        playerMgr.PlayerClicked(player);
                }
            }
        }
    }
}
