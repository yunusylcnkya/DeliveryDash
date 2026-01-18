using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/*
Bu script oyuncunun arabasını kontrol eder.
Araba ileri–geri gider, sağa–sola döner.
Yolda "Boost" alırsa hızlanır, duvara çarparsa tekrar yavaşlar.
Ekranda "BOOST" yazısını gösterip gizler.
*/

public class Driver : MonoBehaviour
{
    /*
    currentSpeed:
    Arabanın şu anki hızı.
    */
    [SerializeField] float currentSpeed = 5f;

    /*
    steerSpeed:
    Arabanın ne kadar hızlı döneceğini belirler.
    */
    [SerializeField] float steerSpeed = 200f;

    /*
    boostSpeed:
    Boost aldığımızda araba bu hızla gider.
    */
    [SerializeField] float boostSpeed = 10f;

    /*
    regularSpeed:
    Normalde arabanın gittiği hız.
    */
    [SerializeField] float regularSpeed = 5f;

    /*
    boostText:
    Ekranda görünen "BOOST" yazısı.
    */
    [SerializeField] TMP_Text boostText;

    /*
    Start:
    Oyun başladığında sadece 1 kere çalışır.
    Başta BOOST yazısını kapatır.
    */
    void Start()
    {
        boostText.gameObject.SetActive(false);
    }

    /*
    OnTriggerEnter2D:
    Araba bir objenin içinden geçtiğinde çalışır.
    Mesela BOOST objesine değince burası çalışır.
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Eğer çarpılan obje "Boost" etiketliyse:
        - Araba hızlanır
        - BOOST yazısı ekranda görünür
        - Boost objesi yok olur
        */
        if (collision.CompareTag("Boost"))
        {
            currentSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    /*
    OnCollisionEnter2D:
    Araba bir şeye çarptığında çalışır.
    Mesela duvara çarpınca burası devreye girer.
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        Eğer çarpılan şey "WorldCollision" etiketliyse:
        - Araba tekrar normal hızına döner
        - BOOST yazısı kaybolur
        */
        if (collision.collider.CompareTag("WorldCollision"))
        {
            currentSpeed = regularSpeed;
            boostText.gameObject.SetActive(false);
        }
    }

    /*
    Update:
    Oyun çalıştığı sürece her saniye defalarca çalışır.
    Klavyeden basılan tuşları kontrol eder.
    */
    void Update()
    {
        float move = 0f;
        float steer = 0f;

        /*
        W tuşu → ileri git
        S tuşu → geri git
        */
        if (Keyboard.current.wKey.isPressed)
        {
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            move = -1f;
        }

        /*
        A tuşu → sola dön
        D tuşu → sağa dön
        */
        if (Keyboard.current.aKey.isPressed)
        {
            steer = 1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            steer = -1f;
        }

        /*
        Time.deltaTime:
        Oyunun hızlı ya da yavaş bilgisayarlarda
        aynı şekilde çalışmasını sağlar.
        */
        float moveAmount = move * currentSpeed * Time.deltaTime;
        float steerAmount = steer * steerSpeed * Time.deltaTime;

        /*
        Arabayı hareket ettirir ve döndürür.
        */
        transform.Translate(0, moveAmount, 0);
        transform.Rotate(0, 0, steerAmount);
    }
}
