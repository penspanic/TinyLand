using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour
{
    public Transform target;

    float followSpeed = 10f;

    void Update()
    {
        // Position
        Vector3 moveVec = (target.transform.position - this.transform.position) / 5;
        moveVec.y = 0;
        transform.Translate(moveVec, Space.World);

        // Rotation Implement
    }
}
