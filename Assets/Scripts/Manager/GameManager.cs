using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsDead = true;

    public PlayerController Player;
    public GasSpawner Gas;
    public RoadPullingManager Road;

    public GameObject GameReadyPanel;
    public GameObject GameOverPanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        GameReadyPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void GameStart()
    {
        GameReadyPanel.SetActive(false);
        GameOverPanel.SetActive(false);

        IsDead = false;
        Gas.StartCoroutine("SpawnGas");
        Player.CurrentGas = Player.MaxGas;
        Player.transform.position = Vector3.zero;
        Player.transform.rotation = Quaternion.identity;
    }

    public void GameOver()
    {
        IsDead = true;

        GameOverPanel.SetActive(true);
    }
}
