using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_sc : MonoBehaviour
{
    public DNA_sc dna; // DNA sınıfından türetilmiş bir nesne
    public float timeAlive; // Hayatta kalma süresi
    public float distanceTravelled; // Kat edilen mesafe
    public bool alive = true;

    private Vector3 startPosition;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Başlangıç konumunu ve Rigidbody bileşenini al
        startPosition = this.transform.position;
        rb = this.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody bileşeni bulunamadı! Lütfen prefabda Rigidbody bileşeni ekleyin.");
        }

        // DNA’yı başlat
        dna = new DNA_sc(2, 200); // DNA'nın uzunluğu ve max değeri

        // Hareket fonksiyonunu başlat
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        // Botun hareket etmesini DNA'ya göre yönlendir
        while (alive)
        {
            int h = dna.genes[0]; // DNA’nın ilk genini kullanarak sağa/sola hareket et
            int v = dna.genes[1]; // DNA’nın ikinci genini kullanarak ileri/geri hareket et
            rb.AddForce(new Vector3(h, 0, v) * Time.deltaTime);

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Hayatta kalma süresini artır ve kat edilen mesafeyi hesapla
        if (alive)
        {
            timeAlive += Time.deltaTime;
            distanceTravelled = Vector3.Distance(this.transform.position, startPosition);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("dead"))
        {
            // Bot öldü, bu yüzden hareketi durdurun
            alive = false;
        }
    }

    void FixedUpdate()
    {
        if (alive)
        {
            // Kat edilen mesafeyi güncelle
            distanceTravelled = Vector3.Distance(this.transform.position, startPosition);
        }
    }
}
