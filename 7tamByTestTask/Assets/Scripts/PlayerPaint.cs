using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerPaint : MonoBehaviourPun, IPunObservable
{
    [SerializeField] GameObject hatBrim;
    [SerializeField] GameObject hatTop;
    // Start is called before the first frame update
    void Start()
    {
        PaintHat();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PaintHat();
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


        if (stream.IsWriting && photonView.IsMine)
        {
            Color hatBrimColor = hatBrim.GetComponent<Image>().color;
            stream.SendNext(hatBrimColor.r);
            stream.SendNext(hatBrimColor.g);
            stream.SendNext(hatBrimColor.b);
            stream.SendNext(hatBrimColor.a);

            Color hatTopColor = hatTop.GetComponent<Image>().color;
            stream.SendNext(hatTopColor.r);
            stream.SendNext(hatTopColor.g);
            stream.SendNext(hatTopColor.b);
            stream.SendNext(hatTopColor.a);
        }

        else if (stream.IsReading)
        {
            Color hatBrimRecivedColor;
            hatBrimRecivedColor.r = (float)stream.ReceiveNext();
            hatBrimRecivedColor.g = (float)stream.ReceiveNext();
            hatBrimRecivedColor.b = (float)stream.ReceiveNext();
            hatBrimRecivedColor.a = (float)stream.ReceiveNext();
            hatBrim.GetComponent<Image>().color = hatBrimRecivedColor;

            Color hatTopRecivedColor;
            hatTopRecivedColor.r = (float)stream.ReceiveNext();
            hatTopRecivedColor.g = (float)stream.ReceiveNext();
            hatTopRecivedColor.b = (float)stream.ReceiveNext();
            hatTopRecivedColor.a = (float)stream.ReceiveNext();
            hatTop.GetComponent<Image>().color = hatTopRecivedColor;
        }

    }
    private void PaintHat()
    {

        hatBrim.GetComponent<Image>().color = Random.ColorHSV();
        hatTop.GetComponent<Image>().color = Random.ColorHSV();
    }
}
