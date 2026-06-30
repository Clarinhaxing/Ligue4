using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            UnityEngine.Debug.Log("Sou o jogador local!");
        }
    }
}