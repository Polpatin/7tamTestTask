using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Vector3 position;
    Vector3 scale;
    GameObject parrent;
    // Start is called before the first frame update
    void Start()
    {
        ReplaceCoinToCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCoinLocation(Vector3 position, GameObject parrent, Vector3 scale)
    {
        this.position = position;
        this.parrent = parrent;
        this.scale = scale;
    }
    void ReplaceCoinToCanvas()
    {
        float yRange = 330f;
        float xRange = 760f;
        transform.SetParent(GameObject.Find("Canvas").transform);
        Vector3 randomPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange),0);
        transform.localPosition = randomPosition;
        transform.localScale = new Vector3(0.7f, 0.7f, 1);
        transform.SetAsLastSibling();
    }
}
