using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] GameObject CoinPrefab;
    private float yRange = 330f;
    private float xRange = 760f;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            InvokeRepeating("SpawnCoinInRandomPlace", 1f, 4f);
        }

    }
    void SpawnCoinInRandomPlace()
    {

        Vector3 randomPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);
        CoinPrefab.GetComponent<Coin>().SetCoinLocation(randomPosition, GameObject.Find("Canvas"), new Vector3(0.7f, 0.7f, 1));
        PhotonNetwork.Instantiate(CoinPrefab.name, new Vector2(0, 0), Quaternion.identity);


    }


}
