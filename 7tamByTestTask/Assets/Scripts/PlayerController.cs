using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class PlayerController : MonoBehaviourPun
{
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] private bl_Joystick Joystick;
    [SerializeField] GameObject FireButton;
    [SerializeField] GameObject BarsPrefab;
    //PhotonView view;
    int health = 10;
    int coins = 0;
    bool isMathcStarted = false;
    public int Health { get { return health; } }
    public int Coins { get { return coins; } }
    public bool IsMathcStarted { get { return isMathcStarted; } }


    // Start is called before the first frame update
    void Start()
    {
        PlaceToCanvas();
        Joystick = GameObject.Find("Joystick").GetComponent<bl_Joystick>();
        //view = GetComponent<PhotonView>();
        StartCoroutine("StartFight");
        StartCoroutine("DieIfHealthZero");
    }
   
    // Update is called once per frame
    void Update()
    {
       
        if (photonView.IsMine)
        {
            //For Keyboard
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0);
            movementDirection.Normalize();

            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.back);
                transform.rotation = new Quaternion(0, 0, toRotation.z, toRotation.w);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }


            //ForJoystick
            float v = Joystick.Vertical;
            float h = Joystick.Horizontal;

            Vector3 translate = (new Vector3(h, v, 0));
            translate.Normalize();
            transform.Translate(translate * speed * Time.deltaTime, Space.World);

            if (translate != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(translate, Vector3.back);
                transform.rotation = new Quaternion(0, 0, toRotation.z, toRotation.w);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            ReturnToBoubdaries();
        }

    }
    private void ReturnToBoubdaries()
    {
        float xBorder = 760f;
        float yBorder = 340;

        if (transform.localPosition.x > xBorder)
        {
            transform.localPosition = new Vector2(xBorder, transform.localPosition.y);
        }
        if (transform.localPosition.x < -xBorder)
        {
            transform.localPosition = new Vector2(-xBorder, transform.localPosition.y);
        }


        if (transform.localPosition.y > yBorder)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, yBorder);
        }
        if (transform.localPosition.y < -yBorder)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, -yBorder);
        }


    }
    private void PlaceToCanvas()
    {
        float yRange = 330f;
        float xRange = 760f;
        transform.SetParent(GameObject.Find("Canvas").transform);
        Vector2 randomPosition = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);
        transform.localPosition = randomPosition;
        transform.localScale = new Vector3(100f, 100f, 1);
        transform.SetAsLastSibling();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Bullet")
        {

                         
                health--;

                GetComponentInChildren<Bars>().SetHealthValueSlider(health);

                print("Hited by bullet, health " + health);
          
            PhotonNetwork.Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Coin")
        {

           
                coins++;
                GetComponentInChildren<Bars>().SetCoinsTextValue(coins);
            

            PhotonNetwork.Destroy(collision.gameObject);


        }

    }

    IEnumerator StartFight()
    {
        yield return new WaitUntil(() => PhotonNetwork.CountOfPlayers >= 2);
        GameObject.Find("CoinsSpawner").GetComponent<CoinsSpawner>().enabled = true;
        isMathcStarted = true;
    }
    IEnumerator DieIfHealthZero()
    {
        yield return new WaitUntil(() =>photonView.transform.gameObject.GetComponent<PlayerController>().health <= 0);
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
   
    //перебор всех подключенных и проверка на свойство isDed

}
