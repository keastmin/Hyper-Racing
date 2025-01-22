using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _player;

    [Header("UI")]
    [SerializeField] private Slider _gasSlider;
    [SerializeField] private TextMeshProUGUI _gasText;

    void Start()
    {
        _gasSlider.value = _player.CurrentGas / _player.MaxGas;
        _gasText.text = $"{(int)_player.CurrentGas}/{(int)_player.MaxGas}";
    }

    void Update()
    {
        _gasSlider.value = _player.CurrentGas / _player.MaxGas;
        _gasText.text = $"{(int)_player.CurrentGas}/{(int)_player.MaxGas}";
    }
}
