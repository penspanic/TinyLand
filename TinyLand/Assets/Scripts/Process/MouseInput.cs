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
                    GameObject obj = GameUtil.GetTopLevelParent(hit.collider.gameObject);

                    if (obj.GetComponent<Player>() != null)
                        playerMgr.PlayerClicked(obj.GetComponent<Player>());
                    else if (obj.GetComponent<Car>() != null)
                        playerMgr.CarClicked(obj.GetComponent<Car>());
                }
            }
        }
    }
}
