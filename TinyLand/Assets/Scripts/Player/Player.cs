using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public bool isDriving
    {
        get;
        private set;
    }

    public Car currCar
    {
        get;
        private set;
    }

    Renderer renderer;
    Collider collider;
    Rigidbody rgdBdy;

    void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();
        collider = GetComponentInChildren<Collider>();
        rgdBdy = GetComponent<Rigidbody>();
    }

    public void RideCar(Car car)
    {
        isDriving = true;
        currCar = car;

        renderer.enabled = false;
        collider.enabled = false;
        rgdBdy.isKinematic = true;
    }

    public void ExitCar()
    {
        isDriving = false;
        currCar = null;

        renderer.enabled = true;
        collider.enabled = true;
        rgdBdy.isKinematic = false;
    }
}