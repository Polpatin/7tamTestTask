using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Bars : MonoBehaviourPun, IPunObservable
{
    [SerializeField] GameObject Cowboy;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text coinsText;
    GameObject PoopUpPanel;
    bool isMathcStarted = false;
    bool isPlayerDie = false;
    // Start is called before the first frame update
    void Start()
    {
        PoopUpPanel = GameObject.Find("EndGamePanel");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FolllowTheCowboy();
        //  UpdateBars();
        print("There is " + GetAlivePlayersAmount() + " players on the arena. Match is started " +isMathcStarted);

       // isMathcStarted = Cowboy.GetComponent<PlayerController>().IsMathcStarted;
        if (GetAlivePlayersAmount() > 1 & isMathcStarted == false)
        {
            isMathcStarted = true;
        }

        if (Cowboy.GetComponent<PlayerController>().Health <= 0)
        {
            isPlayerDie = true;

            PoopUpMathchIsEndPanel();
        }
        if (GetAlivePlayersAmount() == 1 && isMathcStarted == true)
        {
            PoopUpMathchIsEndPanel();
        }
    }
    void FolllowTheCowboy()
    {
        transform.position = new Vector3(Cowboy.transform.position.x, Cowboy.transform.position.y + 2, 70);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void SetHealthValueSlider(int value)
    {
        healthSlider.value = value;
    }
    public void SetCoinsTextValue(int value)
    {
        coinsText.text = "Coins " + value;
    }





    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


        if (stream.IsWriting && photonView.IsMine)
        {
            stream.SendNext((int)healthSlider.value);
            stream.SendNext((string)coinsText.text);
        }

        else if (stream.IsReading)
        {
            healthSlider.value = (int)stream.ReceiveNext();
            coinsText.text = (string)stream.ReceiveNext();
        }

    }
    void PoopUpMathchIsEndPanel()
    {
        if (photonView.IsMine == true)
        {

            if (isPlayerDie == true)
            {
                PoopUpPanel.transform.SetAsLastSibling();
                GameObject.Find("WinOrLooseText").GetComponent<Text>().text = "You die!";
                GameObject.Find("CoinsText").GetComponent<Text>().text = "Coins " + GetComponentInParent<PlayerController>().Coins;
            }

            if(isPlayerDie==false & GetAlivePlayersAmount()==1)
            {
                PoopUpPanel.transform.SetAsLastSibling();
                GameObject.Find("WinOrLooseText").GetComponent<Text>().text = "You win!";
                GameObject.Find("CoinsText").GetComponent<Text>().text = "Coins " + GetComponentInParent<PlayerController>().Coins ;
            }
            print("Is player die " + isPlayerDie);
        }


        
    }
    int GetAlivePlayersAmount()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int countOfAlivePlayers = 0;

        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().Health > 0)
            {
                countOfAlivePlayers++;
            }
        }
        return countOfAlivePlayers;
    }





}
