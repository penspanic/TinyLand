using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    Camera playerCamera;
    public Player currPlayer;

    GameObject world;
    void Awake()
    {
        playerCamera = GameObject.FindObjectOfType<Camera>();
        currPlayer = playerCamera.transform.parent.gameObject.GetComponent<Player>();
        world = GameObject.Find("World");

    }

    public void PlayerClicked(Player player)
    {
        if (isChanging)
            return;
        StartCoroutine(ChangePlayer(player));
    }


    bool isChanging = false;
    readonly float ChangeTime = 2f;
    IEnumerator ChangePlayer(Player target)
    {
        isChanging = true;

        currPlayer.GetComponent<PlayerControl>().PlayerChange(false);

        playerCamera.transform.SetParent(world.transform);

        Vector3 startPos = playerCamera.transform.position;
        Vector3 targetPos = target.transform.position;

        targetPos += target.transform.TransformVector(new Vector3(0f, 1.9f, -1.2f));

        float startYRotation = playerCamera.transform.rotation.eulerAngles.y;
        float targetYRotation = target.transform.rotation.eulerAngles.y;
        if (targetYRotation - startYRotation > 180)
        {
            targetYRotation = targetYRotation - 360;
        }

        float elapsedTime = 0f;
        while (elapsedTime < ChangeTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 currPos = new Vector3();
            currPos.x = EasingUtil.easeInQuad(startPos.x, targetPos.x, elapsedTime / ChangeTime);
            currPos.y = EasingUtil.easeInQuad(startPos.y, targetPos.y, elapsedTime / ChangeTime);
            currPos.z = EasingUtil.easeInQuad(startPos.z, targetPos.z, elapsedTime / ChangeTime);

            playerCamera.transform.position = currPos;


            Quaternion currRotation = Quaternion.Euler(new Vector3(20, EasingUtil.easeInQuad(startYRotation, targetYRotation, elapsedTime / ChangeTime)));

            playerCamera.transform.rotation = currRotation;

            yield return null;
        }
        playerCamera.transform.SetParent(target.transform);
        playerCamera.transform.localPosition = new Vector3(0f, 1.9f, -1.2f);
        playerCamera.transform.localRotation = Quaternion.Euler(20, 0, 0);

        currPlayer = target;
        currPlayer.GetComponent<PlayerControl>().PlayerChange(true);

        isChanging = false;
    }
}
