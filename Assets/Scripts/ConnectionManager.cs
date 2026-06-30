using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public void StartHost()
    {
        if (NetworkManager.Singleton.IsListening)
        {
            Debug.Log("Já existe uma conexão ativa.");
            return;
        }

        NetworkManager.Singleton.StartHost();
        Debug.Log("Host iniciado!");
    }

    public void StartClient()
    {
        if (NetworkManager.Singleton.IsListening)
        {
            Debug.Log("Já existe uma conexão ativa.");
            return;
        }

        NetworkManager.Singleton.StartClient();
        Debug.Log("Client iniciado!");
    }
}