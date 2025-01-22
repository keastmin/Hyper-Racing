using RockingProjects;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _verticalSpeed = 10f;
    [SerializeField] private float _horizontalSpeed = 20f;
    [SerializeField] private float _moveChangeSpeed = 7f;
    [SerializeField] private float _rotateAngle = 30f;
    [SerializeField] private float _rotateSpeed = 5f;
    [SerializeField] private Transform _rotatePivot;

    [Header("Util")]
    [SerializeField] private float _maxGas = 100f;
    [SerializeField] private float _gasRecovery = 30f;
    [SerializeField] private float _gasConsumption = 10f;
    [SerializeField] private float _gasConsumptionRate = 1f;

    [Header("Constraints")]
    [SerializeField] private float _maxZPos = 17f;
    [SerializeField] private float _minZPos = -6f;
    [SerializeField] private float _maxXPos = 10f;
    [SerializeField] private float _minXPos = -10f;

    // Velocity
    private float _horizontalTargetVelocity = 0f;
    private float _verticalTargetVelocity = 0f;
    private float _horizontalCurrentVelocity = 0f;
    private float _verticalCurrentVelocity = 0f;
    private Vector3 _lastNonZeroDirection = Vector3.zero;

    // Health
    private float _currentGas;

    // Properties
    public float CurrentGas { get => _currentGas; set => _currentGas = value; }
    public float MaxGas => _maxGas;

    // Input
    private Vector2 _inputVector = Vector2.zero;

    PlayerInput _playerInput;
    Rigidbody _rigidbody;

    private void Awake()
    {
        TryGetComponent(out _playerInput);
        TryGetComponent(out _rigidbody);
        _currentGas = _maxGas;
    }
    
    void Update()
    {
        if (GameManager.Instance.IsDead) return;

        ConsumeGas();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsDead) return;

        this._inputVector = _playerInput.InputVector;
        Move();
        Rotate();
        ConstraintPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        if(item != null)
        {
            item.Use(this.gameObject);
        }
    }

    #region Gas Methods

    private void ConsumeGas()
    {
        _currentGas -= _gasConsumption * Time.deltaTime * _gasConsumptionRate;

        if (_currentGas <= 0)
        {
            GameManager.Instance.GameOver();
            _rigidbody.linearVelocity = Vector3.zero;
        }
        _currentGas = Mathf.Clamp(_currentGas, 0, _maxGas);
    }

    #endregion


    #region Movement Methods

    private void Move()
    {
        // speed
        SetSpeed(_inputVector);
        Vector3 velocity = Vector3.right * _horizontalCurrentVelocity + Vector3.forward * _verticalCurrentVelocity;

        // Movement
        _rigidbody.linearVelocity = velocity;
    }

    private void SetSpeed(Vector2 inputVector)
    {
        _horizontalTargetVelocity = inputVector.x * _horizontalSpeed;
        _verticalTargetVelocity = inputVector.y * _verticalSpeed;

        _horizontalCurrentVelocity = Mathf.Lerp(_horizontalCurrentVelocity, 
                                                _horizontalTargetVelocity, 
                                                Time.fixedDeltaTime * _moveChangeSpeed);
        _verticalCurrentVelocity = Mathf.Lerp(_verticalCurrentVelocity, 
                                              _verticalTargetVelocity, 
                                              Time.fixedDeltaTime * _moveChangeSpeed);
    }

    private void Rotate()
    {
        float targetAngle = 0f;

        if(_inputVector.y >= 0)
        {
            if (_inputVector.x < 0) targetAngle -= _rotateAngle;
            else if (_inputVector.x > 0) targetAngle += _rotateAngle;
        }
        else
        {
            if (_inputVector.x > 0) targetAngle -= _rotateAngle;
            else if (_inputVector.x < 0) targetAngle += _rotateAngle;
        }

        float newAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.fixedDeltaTime * _rotateSpeed);

        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }

    private void ConstraintPosition()
    {
        if (transform.position.z > _maxZPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _maxZPos);
        }
        else if (transform.position.z < _minZPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _minZPos);
        }

        if (transform.position.x > _maxXPos)
        {
            transform.position = new Vector3(_maxXPos, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _minXPos)
        {
            transform.position = new Vector3(_minXPos, transform.position.y, transform.position.z);
        }
    }
    #endregion


    #region Item Methods

    public void ChargeGas()
    {
        _currentGas += _gasRecovery;
        if (_currentGas > _maxGas) _currentGas = _maxGas;
    }

    #endregion
}
