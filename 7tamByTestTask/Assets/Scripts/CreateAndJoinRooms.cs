using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField CreateInput;
    [SerializeField] InputField JoinInput;

   public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(CreateInput.text,roomOptions);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinInput.text);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
   
}
