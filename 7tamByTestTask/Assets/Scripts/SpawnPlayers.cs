using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
   
    

    // Start is called before the first frame update
    void Start()
    {
       
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
       
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
