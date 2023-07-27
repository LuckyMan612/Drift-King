using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CarController))]
public class CarFuel : MonoBehaviour
{
    Color red = new Color(1, 0.2f, 0.2f, 1);
    Color green = new Color(0.2f, 1, 0.2f, 1);
    
    [Header("References")]
    [SerializeField] Light backLight;

    [Header("Fuel Stats")]
    [SerializeField] float currentFuel;
    [SerializeField] float maxFuel = 100;
    [SerializeField] float fuelDepleteSpeed = 0.1f;
    [SerializeField] private Slider fuelBar;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject hud;
    public bool outOfGas;

    public static CarFuel Instance;

    public float CurrentFuel
    {
        get => currentFuel;
        set
        {
            currentFuel = Mathf.Clamp(value, 0, maxFuel);
            backLight.gameObject.SetActive(currentFuel > 0);
            if(currentFuel < maxFuel/2)
            {
                backLight.color = red;
            }
            else
            {
                backLight.color = green;
            }
        }
    }

    private void OnEnable()
    {
        //subscribe to the event from EventManager
        EventManager.Instance.OnFuelChange += HandleFuel;
    }

    private void OnDisable()
    {
        //unsubscribe to the event from EventManager
        EventManager.Instance.OnFuelChange -= HandleFuel;
    }

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CurrentFuel = maxFuel;
        StartCoroutine(BackLightFlash());


        fuelBar.maxValue = maxFuel;
        fuelBar.value = currentFuel;

        StartCoroutine(DepleteFuel(fuelDepleteSpeed));
    }

    private void HandleFuel(float amount) => CurrentFuel += amount;

    private IEnumerator BackLightFlash()
    {
        while(true)
        {
            //toggle the red light on and off
            backLight.enabled = !backLight.enabled;
            yield return new WaitForSeconds(currentFuel / 100);
        }
    }

    IEnumerator DepleteFuel(float fuelDepleteSpeed)
    {
        while (true)
        {
            if (currentFuel > 0)
            {
                currentFuel -= fuelDepleteSpeed;
                fuelBar.value = currentFuel; // Fuel Bar
            }
            else
            {
                // Game Over
                outOfGas = true;
                gameOverScreen.SetActive(true);
                hud.SetActive(false);


                Timer.Instance.EndTimer();
            }

            yield return new WaitForSeconds(0.1f); // Every Milisecond
        }
    }
}
