using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour/*SimulationBehaviour*/, IPlayerJoined
{
    [SerializeField] public NetworkPrefabRef playerPrefab;
    public GameLogic gamelogic;

    public bool isExecute = false;
    public NetworkObject playerObject;
    public GameManager gameManager;
    public PlayerRef player;

    public void StartSpawn()
    {
        Debug.Log("PlayerSpawner StartSpawn>>" + gameManager.name);
        playerPrefab = GameManager.instance.SelectedCharacter;
        Debug.Log("NetworkRunner PlayerSpawner Started" + playerPrefab);

        Debug.Log("NetworkRunner PlayerSpawner PlayerJoined LocalPlayer>>" + playerPrefab);
        if (player == Runner.LocalPlayer)
        {
            playerObject = Runner.Spawn(playerPrefab, new Vector3(0, 6, 0), Quaternion.identity, player);

            gamelogic.PlayerAdd(player, playerObject);
        }
    }
    public void SetData(GameManager gameManager_)
    {
        gameManager = gameManager_;
    }
    void Update()
    {
        gamelogic = FindObjectOfType<GameLogic>();
    }
    public void PlayerJoined(PlayerRef player_)
    {
        Debug.Log("NetworkRunner PlayerSpawner PlayerJoined >>");
        //if (player_ == Runner.LocalPlayer)
        //{
        player = player_;
        // }
    }
}
