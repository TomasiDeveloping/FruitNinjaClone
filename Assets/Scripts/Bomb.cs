using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade b = collision.GetComponent<Blade>();

        // Wenn es nämlich kein Script hat, ist das Objekt "b" leer.
        // In dem fall "returnen" wir, also wir springen aus diesem Methodenaufruf raus.
        if (!b)
        {
            return;
        }

        // Damit rufen wir die OnBombHit Methode des GameManagers auf
        // Beachte dabei, dass FindObjectOfType relativ resourcenshungrig ist. Also nur verwenden
        // wenn du diese Methode nicht super häufig nutzt.
        FindObjectOfType<GameManager>().OnBombHit();
    }
}
