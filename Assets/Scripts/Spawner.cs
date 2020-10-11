using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Das ist unser GameObject das wir spawnen möchten, also unsere Frucht
    public GameObject[] fruitToSpawnPrefab;
    public int chanceToSpawnBomb = 10;
    public GameObject bombPrefab;
    // Das sind die Orte an denen unsere Früchte auftauchen sollen
    public Transform[] spawnPlaces;
    // Minimale dauer zwischen den Erzeugungen der Früchte
    public float minWait = 0.3f;
    // Maximale dauer zwischen den Erzeugungen der Früchte
    public float maxWait = 1f;
    // Minimale Kraft mit denen die Früchte geschossen werden sollen
    public float minForce = 12;
    // Maximale Kraft mit denen die Früchte geschossen werden sollen
    public float maxForce = 17;


    // Start is called before the first frame update
    void Start()
    {
        // Damit starten wir eine CoRoutine, also eine Routine die parallel abläuft.
        // In unserem Fall das Erzeugen von Früchten.
        StartCoroutine(SpawnFruits());
    }

    // Mit einem IEnumerator können wir etwas parallel alle X Sekunden ausführen, bzw eine Methode aufrufen.
    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            // Hier brauchen wir das yield return new WaitForSeconds um zu sagen, nach wie vielen
            // Sekunden der Code ausgeführt werden soll. Wir nehmen hier einen zufälligen Wert
            // zwischen minWait und maxWait
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            // Damit holen wir uns die Position eines unserer Spawnplaces. Das zufällig unter allen die 
            // vorhanden sind. In unserem Fall erstmal nur drei, wir könnten das aber jederzeit erweitern (in Unity).
            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject go = null;
            float rnd = Random.Range(0, 100);

            if (chanceToSpawnBomb > 90)
            {
                chanceToSpawnBomb = 90;
            }
            else if (chanceToSpawnBomb < 0)
            {
                chanceToSpawnBomb = 0;
            }


            if (rnd < chanceToSpawnBomb)
            {
                go = bombPrefab;
            }
            else
            {
                go = fruitToSpawnPrefab[Random.Range(0, fruitToSpawnPrefab.Length)];
            }

            // Hier erzeugen wir nun endlich die Frucht, auf Grundlage unseres Prefabs, an der Position des Spawnplaces.
            GameObject fruit = Instantiate(go, t.transform.position, t.transform.rotation);

            // hiermit holen wir uns den Rigidbody unserer Frucht und fügen ihr etwas Kraft hinzu um sie nach oben
            // zu schleudern.
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up *
                Random.Range(minForce, maxForce), ForceMode2D.Impulse);


            Debug.Log("Frucht wurde erzeugt!");
            // Nun zerstören wir die Frucht nach 5 Sekunden, damit wir nicht irgendwann mal tausende von
            // Gameobjects im Spiel haben, welche die Performance verschlechtern.
            Destroy(fruit, 5);
        }

    }
}
