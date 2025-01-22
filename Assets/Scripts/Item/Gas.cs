using UnityEngine;

public class Gas : MonoBehaviour, IItem
{
    private float _speed = 7f;

    private float _lifeTime = 6f;
    private float _time = 0f;

    Rigidbody _rigidbody;

    private void Awake()
    {
        TryGetComponent(out _rigidbody);
    }

    public void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if(GameManager.Instance.IsDead || _time >= _lifeTime)
        {
            Destroy(this.gameObject);
        }

        _rigidbody.linearVelocity = Vector3.forward * -_speed;
    }

    public void Use(GameObject target)
    {
        if(target.TryGetComponent(out PlayerController playerController))
        {
            playerController.ChargeGas();
            Destroy(this.gameObject);
        }
    }
}
