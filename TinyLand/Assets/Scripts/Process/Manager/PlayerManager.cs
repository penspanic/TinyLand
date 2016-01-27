using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    Camera playerCamera;
    Camera minimapCamera;

    public Player currPlayer;

    GameObject world;

    CameraManager cameraMgr;

    public bool isChanging;

    readonly float ChangeTime = 2f;
    void Awake()
    {
        playerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();
        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();
        currPlayer = playerCamera.transform.parent.gameObject.GetComponent<Player>();
        world = GameObject.Find("World");

        cameraMgr = GameObject.FindObjectOfType<CameraManager>();

    }

    public void PlayerClicked(Player player)
    {
        if (isChanging)
            return;
        cameraMgr.PlayerChange(player);
    }

    public void CarClicked(Car car)
    {
        if (isChanging)
            return;
        cameraMgr.GetInCar(car);
    }

}
