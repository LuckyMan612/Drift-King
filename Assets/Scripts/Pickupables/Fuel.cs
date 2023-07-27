using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] float fuelAmount = 25;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //notify the event manager that the fuel was collected
            EventManager.Instance.FuelAmountChange(fuelAmount);
            AudioManager.Instance.PlaySound(AudioManager.Instance.collectSound);
            Destroy(gameObject);
        }
    }
}
