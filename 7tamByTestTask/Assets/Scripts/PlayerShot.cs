using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerShot : MonoBehaviourPun
{
    float timer = 0f;
    float rechargTime = 0.3f;
    GameObject barrel;
    [SerializeField] GameObject bulletPrefab;
    GameObject FireButton;
    void Start()
    {
        FireButton = GameObject.Find("FireButton");
        FireButton.GetComponent<Button>().onClick.AddListener(Shot);

        FireButton.GetComponentInChildren<Text>().text = "Recharge";
        barrel = GameObject.Find("GunBarrel");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
        if (timer > rechargTime)
        {
            FireButton.GetComponentInChildren<Text>().text = "Redy to fire";

        }
        if (timer < rechargTime)
        {
            FireButton.GetComponentInChildren<Text>().text = "Recharge";

        }
    }
    public void Shot()
    {
        if (timer > rechargTime)
        {
            bulletPrefab.GetComponent<Bullet>().barrel = barrel;
            PhotonNetwork.Instantiate(bulletPrefab.name, barrel.transform.localPosition, barrel.transform.localRotation);
            timer = 0;
        }
    }
}

