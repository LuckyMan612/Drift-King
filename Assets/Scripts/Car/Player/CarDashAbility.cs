using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(CarFuel))]
public class CarDashAbility : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource dashAudioSource;
    [SerializeField] ParticleSystem dashParticle;
    CarController carController;
    CarFuel carFuel;

    [Header("Input")]
    [SerializeField] KeyCode dashKey;

    [Header("Dash Settings")]
    [SerializeField] float fuelConsumptionPerDash = -10;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashOnCooldownTime;
    private float defaultMoveSpeed;
    private bool dashOnCooldown = false;

    void Start()
    {
        carController = GetComponent<CarController>();
        carFuel = GetComponent<CarFuel>();

        defaultMoveSpeed = carController.MoveSpeed;
    }

    void Update()
    {   
        bool dashKeyDown = Input.GetKeyDown(dashKey);
        bool hasEnoughFuel = carFuel.CurrentFuel + fuelConsumptionPerDash >= 0;

        if(!dashKeyDown || !hasEnoughFuel || dashOnCooldown)
            return;

        dashOnCooldown = true;
        dashAudioSource.Play();
        //notify the event manager that the dash ability was used
        EventManager.Instance.FuelAmountChange(fuelConsumptionPerDash);

        StartCoroutine(Dash());
        Invoke("ResetdashOnCooldown", dashOnCooldownTime);
    }
        
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime) 
        {
            carController.MoveSpeed = dashSpeed;
            //transform.Translate(Vector3.forward * dashSpeed);

            dashParticle.Play();
            yield return null;
        }
        carController.MoveSpeed = defaultMoveSpeed;
    }

    void ResetdashOnCooldown() => dashOnCooldown = false;
}
