using System.Collections;
using System.Collections.Generic;
using Photon.Pun; 
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{    
    public GameObject playerPrefab;

    public List<Transform> spawnPoints;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = false
        };
        PhotonNetwork.JoinOrCreateRoom("Test", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Присоединился к комнате с {PhotonNetwork.CurrentRoom.PlayerCount}, Игроки и ID: {id}");

        if (id > spawnPoints.Count)
        {
            Debug.LogError("Нет точек спавна");
            return;
        }

        if (PhotonNetwork.LocalPlayer.TagObject == null)
        {
            var playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[id - 1].position, Quaternion.identity);
            playerInstance.name = $"Player_{id}";
            PhotonNetwork.LocalPlayer.TagObject = playerInstance;
            StartCoroutine(InitializePlayer(playerInstance, $"Player_{id}"));
        }
        else
        {
            Debug.LogWarning("Игрок уже существует");
        }
    }

    private IEnumerator InitializePlayer(GameObject playerInstance, string namePerson)
    {
        yield return new WaitForSeconds(0.5f);

        CameraController.Instance.FindJoystick();
        CameraController.Instance.FindPlayer(namePerson);

        HealthSystem.Instance.UpdateUI();
    }
}
