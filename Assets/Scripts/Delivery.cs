using UnityEngine;

/*
Bu script oyundaki "kurye" gibi çalışır.
Oyuncu paketi alır, üstünde taşır ve müşteriye götürüp teslim eder.
Oyuncunun şu an paket taşıyıp taşımadığını hatırlamak için bir değişken kullanır.
*/

public class Delivery : MonoBehaviour
{
    /*
    hasPackage:
    Oyuncunun şu anda paketi var mı yok mu onu söyler.
    true  = paketi var
    false = paketi yok
    */
    bool hasPackage;

    /*
    delay:
    Paketi aldıktan sonra kaç saniye sonra yok olacağını ayarlar.
    Mesela 1 saniye sonra paket sahneden silinir.
    */
    [SerializeField] float delay = 1f;

    /*
    OnTriggerEnter2D:
    Oyuncu başka bir objeye değdiği anda bu fonksiyon çalışır.
    Mesela pakete ya da müşteriye çarptığında burası devreye girer.
    */
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Eğer çarpılan obje "Package" etiketliyse
        VE oyuncunun elinde paket YOKSA:
        - Paket alınır
        - Oyuncu artık paketi taşıyor olur
        - Parçacık efekti (ışık, duman gibi) oynar
        - Paket 1 saniye sonra yok olur
        */
        if (collision.CompareTag("Package") && !hasPackage)
        {
            Debug.Log("Picked up package");
            hasPackage = true;
            GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject, delay);
        }

        /*
        Eğer çarpılan obje "Customer" etiketliyse
        VE oyuncunun elinde paket VARSA:
        - Paket müşteriye teslim edilir
        - Oyuncunun artık paketi kalmaz
        - Parçacık efekti durur
        - Müşteri sahneden silinir
        */
        if (collision.CompareTag("Customer") && hasPackage)
        {
            Debug.Log("Delivered Package");
            hasPackage = false;
            GetComponent<ParticleSystem>().Stop();
            Destroy(collision.gameObject);
        }
    }
}
