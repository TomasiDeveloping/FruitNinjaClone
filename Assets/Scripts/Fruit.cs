using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // Das Prefab welches wir als unsere geschnittene Frucht verwenden wollen.
    public GameObject slicedFruitPrefab;
    public float explosionforce = 5;

    // mit dieser Methode können wir unsere aktuelle Frucht löschen und eine geschnittene
    // Frucht generieren
    public void CreateSlicedFruit()
    {
        // Damit legen wir ein neues GameObject an, welches unsere Geschnittene Frucht ist
        GameObject inst = Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        // hiermit holen wir uns die Rigidbodys aller Elemente die sich in unserer 
        // geschnittenen Frucht befinden. In diesem Fall 2, also die linke und die rechte Seite
        Rigidbody[] rbOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();

        // Hier spielen wir den Sound ab.
        FindObjectOfType<GameManager>().PlayRandomSliceSound();


        // mit der Foreach Schleife, schleifen wir nun durch alle Rigibodys durch, die sich in 
        // diesem Array rbOnSliced befinden. In unserem Fall 2 Stück, links und rechts.
        foreach (Rigidbody r in rbOnSliced)
        {
            // Damit geben wir dem Rigidbody eine zufällige Rotation. Unsere Fruchtstücke
            // haben also unterschiedliche Drehungen
            r.transform.rotation = Random.rotation;
            // Damit fügen wir eine Explosionskraft zum Rigidbody hinzu, schleudern 
            // die Stücke also ordentlich weg.
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, explosionforce);
        }

        FindObjectOfType<GameManager>().IncreaseScore(3);

        // Hiermit löschen wir die alte Frucht, also die nicht geschnittene, ganze frucht.
        Destroy(inst.gameObject, 5);
        Destroy(gameObject);
    }

    // Wird von Unity automatisch ausgeführt, wenn eine Kollision auftritt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hiermit erstellen wir eine Blade objekt, wenn das Objekt mit dem wir kollidieren das 
        // Blade Script hat.
        Blade b = collision.GetComponent<Blade>();

        // Wenn es nämlich kein Script hat, ist das Objekt "b" leer.
        // In dem fall "returnen" wir, also wir springen aus diesem Methodenaufruf raus.
        if (!b)
        {
            return;
        }
        // Ansonsten schneiden wir die Frucht.
        CreateSlicedFruit();
    }

}
