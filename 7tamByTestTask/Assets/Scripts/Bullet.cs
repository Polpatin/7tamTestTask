using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public GameObject barrel;
    float force = 100;
    private void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        transform.position = barrel.transform.position;
        transform.rotation = barrel.transform.rotation;
        transform.SetAsLastSibling();
        GetComponent<Rigidbody2D>().AddForce(transform.up * force);
    }
    void Update()
    {
        BulletOutOfBoundsDestroy();
    }




    void BulletOutOfBoundsDestroy()
    {
        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20 || transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetPhotonView().ViewID > gameObject.GetPhotonView().ViewID)
            {
                PhotonNetwork.Destroy(collision.gameObject);

            }
        }
    }
}
