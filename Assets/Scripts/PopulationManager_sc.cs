using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager_sc : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 10;
    private List<GameObject> population = new List<GameObject>();
    private float trialTime = 10f; // Deneme süresi
    private int generation = 1;

    // Start is called before the first frame update
    private void Start()
    {
        // İlk nesil botları oluştur
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
            GameObject bot = Instantiate(botPrefab, pos, Quaternion.identity);
            population.Add(bot);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Belirlenen sürede yeni nesle geç
        trialTime -= Time.deltaTime;
        if (trialTime <= 0)
        {
            BreedNewPopulation();
            trialTime = 10f; // Deneme süresini sıfırla
        }
    }

    private void BreedNewPopulation()
    {
        // Her botun Brain_sc bileşeninin null olmadığını kontrol et
        foreach (var bot in population)
        {
            if (bot.GetComponent<Brain_sc>() == null)
            {
                Debug.LogError("Botun Brain_sc bileşeni bulunamadı: " + bot.name);
                return; // Çık ve işlem yapma
            }
        }

        // Botların mesafelerini sıralayın
        List<GameObject> sortedList = population.OrderByDescending(o => 
        {
            var brain = o.GetComponent<Brain_sc>();
            if (brain != null)
            {
                return brain.distanceTravelled; // Geçerli mesafe değerini döndür
            }
            return 0; // Null durumunda varsayılan değer döndür
        }).ToList();

        // Popülasyonu temizle ve yeni nesil oluştur
        population.Clear();
        for (int i = sortedList.Count / 2; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // Yeni jenerasyon için sayacı artır
        generation++;
    }


    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        GameObject offspring = Instantiate(botPrefab, pos, Quaternion.identity);
        Brain_sc b = offspring.GetComponent<Brain_sc>();

        // DNA'nın yarısını bir ebeveynden, diğer yarısını diğer ebeveynden al
        b.dna = new DNA_sc(2, 200);
        for (int i = 0; i < b.dna.genes.Count; i++)
        {
            b.dna.genes[i] = (i < b.dna.genes.Count / 2) ? parent1.GetComponent<Brain_sc>().dna.genes[i] : parent2.GetComponent<Brain_sc>().dna.genes[i];
        }

        return offspring;
    }

    // private void BreedNewPopulation()
    // {
    //     List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<Brain_sc>().distanceTravelled).ToList();
        
    //     population.Clear();
    //     for (int i = sortedList.Count / 2; i < sortedList.Count - 1; i++)
    //     {
    //         population.Add(Breed(sortedList[i], sortedList[i + 1]));
    //         population.Add(Breed(sortedList[i + 1], sortedList[i]));
    //     }

    //     // Yeni jenerasyon için sayacı artır
    //     generation++;
    // }
}
