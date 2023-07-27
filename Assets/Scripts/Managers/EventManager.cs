using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

#region Action Events

    public event Action<float> OnFuelChange;

#endregion

#region Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

#endregion

#region Notify Event

    public void FuelAmountChange(float amount=0) => OnFuelChange?.Invoke(amount);

#endregion

}
