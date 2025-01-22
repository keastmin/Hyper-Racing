using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float _speed = 30f;

    void Update()
    {
        if (GameManager.Instance.IsDead) return;

        transform.position -= Vector3.forward * _speed * Time.deltaTime;
    }
}
