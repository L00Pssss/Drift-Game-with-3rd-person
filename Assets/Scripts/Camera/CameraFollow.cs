using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    
    [Header("Offset")] 
    [SerializeField] private float _viewHight;
    [SerializeField] private float _hight;
    [SerializeField] private float _distance;
    
    [Header("Damping")]
    [SerializeField] private float _rotationDamping;
    [SerializeField] private float _heightDamping;
    [SerializeField] private float _speedThreshold;

    [SerializeField] private Transform _target; // for debug
    [SerializeField] private Rigidbody _rigidbody;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void FixedUpdate()
    {
        if (_target == null || _rigidbody == null) return;
        Vector3 velocity = _rigidbody.velocity;
        Vector3 targetRotation = _target.eulerAngles;

        if (velocity.magnitude > _speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }
        
        float currentAngel = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y,
            _rotationDamping * Time.fixedDeltaTime);
        
        float currentHeight = Mathf.Lerp(transform.position.y, _target.position.y + _hight,
            _heightDamping * Time.fixedDeltaTime);
        
        Vector3
            positionOffset =
                Quaternion.Euler(0, currentAngel, 0) * Vector3.forward *
                _distance; 
        transform.position = _target.position + positionOffset;
        
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Rotation
        transform.LookAt(_target.position + new Vector3(0, _viewHight, 0));
    }
    
    
    public void SetTarget(Transform newTarget, Rigidbody newRigidbody)
    {
        _target = newTarget;
        _rigidbody = newRigidbody;
    }
}
