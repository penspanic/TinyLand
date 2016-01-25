using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    Camera playerCamera;
    Camera minimapCamera;

    public Player currPlayer;

    GameObject world;
    void Awake()
    {
        playerCamera = GameObject.Find("Player Camera").GetComponent<Camera>();
        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();
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

        float playerCamStartYRotation = playerCamera.transform.rotation.eulerAngles.y;
        float minimapCamStartYRotation = minimapCamera.transform.rotation.eulerAngles.y;
        float targetYRotation = target.transform.rotation.eulerAngles.y;
        if (targetYRotation - playerCamStartYRotation > 180)
        {
            targetYRotation = targetYRotation - 360;
        }

        float elapsedTime = 0f;
        while (elapsedTime < ChangeTime)
        {
            elapsedTime += Time.deltaTime;

            #region Player Camera
            Vector3 currPos = new Vector3();
            currPos.x = EasingUtil.easeInQuad(startPos.x, targetPos.x, elapsedTime / ChangeTime);
            currPos.y = EasingUtil.easeInQuad(startPos.y, targetPos.y, elapsedTime / ChangeTime);
            currPos.z = EasingUtil.easeInQuad(startPos.z, targetPos.z, elapsedTime / ChangeTime);

            playerCamera.transform.position = currPos;

            Quaternion currRotation = Quaternion.Euler(new Vector3(20, EasingUtil.easeInQuad(playerCamStartYRotation, targetYRotation, elapsedTime / ChangeTime), 0));

            playerCamera.transform.rotation = currRotation;
            #endregion

            #region Minimap Camera

            currPos.x = EasingUtil.easeInQuad(minimapCamera.transform.position.x, target.transform.position.x, elapsedTime / ChangeTime);
            currPos.y = 15;
            currPos.z = EasingUtil.easeInQuad(minimapCamera.transform.position.z, target.transform.position.z, elapsedTime / ChangeTime);

            minimapCamera.transform.position = currPos;

            currRotation = Quaternion.Euler(new Vector3(90, EasingUtil.easeInQuad(minimapCamStartYRotation, targetYRotation, elapsedTime / ChangeTime), 0));

            minimapCamera.transform.rotation = currRotation;
            #endregion
            yield return null;
        }
        playerCamera.transform.SetParent(target.transform);
        playerCamera.transform.localPosition = new Vector3(0f, 1.9f, -1.2f);
        playerCamera.transform.localRotation = Quaternion.Euler(20, 0, 0);

        minimapCamera.transform.SetParent(target.transform);

        currPlayer = target;
        currPlayer.GetComponent<PlayerControl>().PlayerChange(true);

        isChanging = false;
    }
}
