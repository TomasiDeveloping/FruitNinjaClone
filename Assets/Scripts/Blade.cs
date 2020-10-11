using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minVelo = 0.1f;

    // Wir brauchen den Rigidbody der Blade
    private Rigidbody2D rb;
    private Vector3 lastMousePos;
    private Vector3 mouseVelo;
    private Collider2D col;

    // In der Awake Methode setzen wir den Rigidbody Component unserer Blade. 
    // Awake wird sehr früh im Lebenszyklus aufgrufen, noch früher als "Start"
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Hier holen wir uns einfach den Collider der Blade
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // Wir aktivieren unseren Collider der Blade nur wenn die Mouse/der Finger sich bewegt.
        col.enabled = IsMouseMoving();

        SetBladeToMouse();
    }

    // Mit dieser Methode setzen wir die Position unserer Blade and die Position unserer Maus.
    private void SetBladeToMouse()
    {
        var mousePos = Input.mousePosition;
        // Wir setzen die Z Position unserer Mouse auf 10, damit wir auf der gleichen
        // Z Ebene sind, wie unsere Früchte, nämlich auf z= 0. Denn unsere Kamera ist auf z=-10
        // und wir wollen ja mit unserer Blade die Früchte zerstören.
        mousePos.z = 10;
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Diese Methode ermittelt, ob sich die Maus bewegt oder nicht und gibt dies zurück (bool)
    private bool IsMouseMoving()
    {
        // Wir holen uns die aktuelle 3D Position der Maus
        Vector3 curMousePos = transform.position;
        // Wir ermitteln wie weit die Maus sich im vergleich zum vorherigen Aufruf bewegt hat.
        float traveled = (lastMousePos - curMousePos).magnitude;
        // Wir setzen die aktuelle Position als die "letzte" Position für den nächsten Aufruf.
        lastMousePos = curMousePos;
        // Hier überprüfen wir, ob die Bewegung über unsere minVelo hinaus geht, wenn ja
        // dann geben wir true zurück, sonst false.
        if (traveled > minVelo)
            return true;
        else
            return false;
    }

}
