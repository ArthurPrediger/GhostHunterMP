using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room, creating one...");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 5 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        //SceneManager.LoadScene(1);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        PhotonNetwork.Disconnect();
    }
}
