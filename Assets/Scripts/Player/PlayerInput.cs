using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _inputVector = Vector2.zero;
    public Vector2 InputVector => _inputVector;

    void Update()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        _inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _inputVector.x += 1;
        }
    }
}
