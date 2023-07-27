using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] float moveSpeed = 25;
    [SerializeField] float maxSpeed = 35;
    [SerializeField] float drag = 0.98f;
    [SerializeField] float steerAngle = 20;
    [SerializeField] float traction = 1;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    Vector3 MoveForce;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Don't Drive if out of gas
        if (CarFuel.Instance.outOfGas)
        {
            return;
        }

        // Move
        MoveForce += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
        rb.velocity = MoveForce * 50 * Time.fixedDeltaTime;
    }

    void Update()
    {
        // Don't Drive if out of gas
        if (CarFuel.Instance.outOfGas)
        {
            return;
        }

        // Steer
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * steerAngle * Time.deltaTime);

        // Drag
        MoveForce *= drag;

        // Max speed limit
        MoveForce = Vector3.ClampMagnitude(MoveForce, maxSpeed);

        // Traction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, traction * Time.deltaTime) * MoveForce.magnitude;
    }
}
