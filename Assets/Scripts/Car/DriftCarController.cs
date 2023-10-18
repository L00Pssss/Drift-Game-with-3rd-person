using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftCarController : MonoBehaviour
{
    public WheelCollider[] wheelColliders;
    public float maxSpeed = 100f;
    public float acceleration = 20f;
    public float steerSpeed = 5f;
    public float driftFactor = 0.95f;

    private float currentSpeed = 0f;
    private float currentSteer = 0f;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private Rigidbody m_rigidbody;
    private bool isDrifting = false;

    private void Start()
    {
        if (centerOfMass != null)
        {
            m_rigidbody.centerOfMass = centerOfMass.localPosition;
        }
    }

    private void Update()
    {
        // Ввод с клавиатуры
        float throttle = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");

        // Управление скоростью
        currentSpeed = Mathf.Clamp(currentSpeed + throttle * acceleration * Time.deltaTime, -maxSpeed, maxSpeed);

        // Управление поворотом
        currentSteer = Mathf.Lerp(currentSteer, steer * steerSpeed, Time.deltaTime * 5f);

        // Применение силы движения
        foreach (var wheelCollider in wheelColliders)
        {
            wheelCollider.motorTorque = throttle * acceleration;
            wheelCollider.steerAngle = steer * steerSpeed;
            WheelFrictionCurve friction = wheelCollider.sidewaysFriction;

            // Настройте параметры сцепления бокового скольжения
            friction.stiffness = 0.2f;
            friction.extremumSlip = 1.0f;
            friction.extremumValue = 2000.0f;
            friction.asymptoteSlip = 2.0f;
            friction.asymptoteValue = 1000.0f;

            // Примените настройки к WheelCollider
            wheelCollider.sidewaysFriction = friction;
        }

        // Дрифт
        if (throttle != 0 && Mathf.Abs(steer) > 0.5f)
        {
            isDrifting = true;
        }
        else
        {
            isDrifting = false;
        }

        // Проверка на дрифт
        if (isDrifting)
        {
            // Измените центр массы для создания дрифта
            if (currentSpeed > 0)
            {
                GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -1, 0); // Настройте значения по вашим потребностям
            }

            // Примените дополнительные силы для усиления дрифта
            // Например:
            // GetComponent<Rigidbody().AddForce(transform.up * driftForce);
        }
        else
        {
            // Восстановите центр массы
        //    originalCenterOfMass = GetComponent<GameObject>().centerOfMass; // Сохраняем начальное значение центра массы
        }

        // Продолжайте обновление скорости, управления и т. д.
    }
}
