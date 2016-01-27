using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera minimapCamera;

    public Transform worldTransform;

    PlayerManager playerMgr;
    Minimap minimap;
    void Awake()
    {
        playerMgr = GameObject.FindObjectOfType<PlayerManager>();
        minimap = GameObject.FindObjectOfType<Minimap>();
    }

    public void PlayerChange(Player player)
    {
        StartCoroutine(PlayerChangeProcess(player));
    }

    public void GetInCar(Car car)
    {
        StartCoroutine(GetInCarProcess(car));
    }

    const float ChangeTime = 2f;
    IEnumerator PlayerChangeProcess(Player player)
    {
        playerMgr.isChanging = true;

        playerMgr.currPlayer.GetComponent<PlayerControl>().PlayerChange(false);

        playerCamera.transform.SetParent(worldTransform);

        Vector3 startPos = playerCamera.transform.position;
        Vector3 targetPos = player.transform.position;

        targetPos += player.transform.TransformVector(new Vector3(0f, 1.9f, -1.2f));

        float playerCamStartYRotation = playerCamera.transform.rotation.eulerAngles.y;
        float minimapCamStartYRotation = minimapCamera.transform.rotation.eulerAngles.y;
        float targetYRotation = player.transform.rotation.eulerAngles.y;
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

            currPos = GameUtil.EasingVector3(EasingUtil.easeInQuad, startPos, targetPos, elapsedTime / ChangeTime);

            playerCamera.transform.position = currPos;

            Quaternion currRotation = Quaternion.Euler(new Vector3(20, EasingUtil.easeInQuad(playerCamStartYRotation, targetYRotation, elapsedTime / ChangeTime), 0));

            playerCamera.transform.rotation = currRotation;
            #endregion

            //#region Minimap Camera

            //currPos.x = EasingUtil.easeInQuad(minimapCamera.transform.position.x, player.transform.position.x, elapsedTime / ChangeTime);
            //currPos.y = 15;
            //currPos.z = EasingUtil.easeInQuad(minimapCamera.transform.position.z, player.transform.position.z, elapsedTime / ChangeTime);

            //minimapCamera.transform.position = currPos;

            //currRotation = Quaternion.Euler(new Vector3(90, EasingUtil.easeInQuad(minimapCamStartYRotation, targetYRotation, elapsedTime / ChangeTime), 0));

            //minimapCamera.transform.rotation = currRotation;
            //#endregion
            yield return null;
        }
        playerCamera.transform.SetParent(player.transform);
        playerCamera.transform.localPosition = new Vector3(0f, 1.9f, -1.2f);
        playerCamera.transform.localRotation = Quaternion.Euler(20, 0, 0);

        minimapCamera.transform.SetParent(player.transform);

        playerMgr.currPlayer = player;
        playerMgr.currPlayer.GetComponent<PlayerControl>().PlayerChange(true);

        playerMgr.isChanging = false;
    }
    
    IEnumerator GetInCarProcess(Car car)
    {

        playerMgr.isChanging = true;

        playerCamera.transform.SetParent(worldTransform);
        playerMgr.currPlayer.transform.SetParent(car.transform, false);
        playerMgr.currPlayer.transform.localPosition = Vector3.zero;
        playerMgr.currPlayer.RideCar(car);

        Vector3 startPos = playerCamera.transform.position;
        Vector3 targetPos = car.transform.position;

        targetPos += car.transform.TransformVector(new Vector3(0, 2.5f, -3f));

        float playerCamStartYRotation = playerCamera.transform.rotation.eulerAngles.y;
        float minimapCamStartYRotation = minimapCamera.transform.rotation.eulerAngles.y;
        float targetYRotation = car.transform.rotation.eulerAngles.y;
        if (targetYRotation - playerCamStartYRotation > 180)
        {
            targetYRotation = targetYRotation - 360;
        }

        minimap.target = car.transform;
        float elapsedTime = 0f;
        while(elapsedTime < ChangeTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 currPos = new Vector3();

            currPos = GameUtil.EasingVector3(EasingUtil.easeInQuad, startPos, targetPos, elapsedTime / ChangeTime);

            playerCamera.transform.position = currPos;

            Quaternion currRotation = Quaternion.Euler(new Vector3(20, EasingUtil.easeInQuad(playerCamStartYRotation, targetYRotation, elapsedTime / ChangeTime), 0));

            playerCamera.transform.rotation = currRotation;

            yield return null;
        }
        playerCamera.transform.SetParent(car.transform);

        playerCamera.transform.localPosition = new Vector3(0, 2.5f, -3f);
        playerCamera.transform.localRotation = Quaternion.Euler(20, 0, 0);

        playerMgr.isChanging = false;
    }
}
