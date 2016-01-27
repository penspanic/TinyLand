using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    UnityStandardAssets.Vehicles.Car.CarController carController;

    PlayerManager playerMgr;

    void Awake()
    {
        carController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        playerMgr = GameObject.FindObjectOfType<PlayerManager>();
    }

    void FixedUpdate()
    {
        if(playerMgr.currPlayer.currCar == this)
            InputProcess();
    }

    void InputProcess()
    {
        float steering = 0;
        float accel = 0;
        float brake = 0;

        if (Input.GetKey(KeyCode.W))
        {
            accel = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            brake = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            steering = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            steering += 1;
        }

        carController.Move(steering, accel, brake, 0);
    }
}
