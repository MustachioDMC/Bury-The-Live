using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugadorA : MonoBehaviour
{   
    public int vidaMax;
    Vector2 knockBack;
    public float velocidad;
    public float fuerzaSalto;
    bool saltando;
    public float distanciaCaja;
    public Vector2 tamannoCaja;
    public LayerMask capaSuelo;
    bool atacando;
    bool muerto = false;
    int DobleCount;
    Animator anim;
    Vector3 movimiento;
    Rigidbody2D cuerpo;
    SpriteRenderer sprite;
    int vidaRestante;
    void Start()
    {
        knockBack = Vector2.zero;
        vidaRestante = vidaMax;
        movimiento = new Vector3();
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LeerMovimiento();
        LeerSalto();
        RotarPersonaje();
        EstaEnElSuelo();
        ActualizarAnimacion();
    }
    void ActualizarAnimacion()
    {
        if(!muerto){
            if(movimiento.x == 0)
            {
                anim.SetBool("Movimiento",false);
            }
            else
            {
                anim.SetBool("Movimiento",true);
            }
            if (!EstaEnElSuelo())
            {
                anim.SetBool("Saltando", true);
            }
            else
            {
                anim.SetBool("Saltando", false);
            }
            if (atacando && EstaEnElSuelo())
            {
                anim.SetTrigger("Ataque");
            }
            if(atacando && !EstaEnElSuelo())
            {
                anim.SetTrigger("AtaqueAire");
            }
        }
    }
    private void LeerSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !saltando && EstaEnElSuelo())
        {
            saltando = true;
        }
    }
    private void RotarPersonaje()
    {
        if(!muerto){
            if (movimiento.x < 0)
            {
                sprite.flipX = true;
            }
            else if (movimiento.x > 0)
            {
                sprite.flipX = false;
            }
        }
    }

    private void LeerMovimiento()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movimiento = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movimiento = Vector3.right;
        }
        else
        {
            movimiento = new Vector3();
        }
        if(!saltando){}
        if(Input.GetKeyDown(KeyCode.J)){
            
            atacando=true;
        }
        else
        {
            atacando=false;
        }
    }

    void FixedUpdate()
    {
        if (muerto)
        {
            cuerpo.velocity = Vector2.zero;
            return;
        }

        cuerpo.velocity = new Vector2(movimiento.x * velocidad, cuerpo.velocity.y);
        if(saltando)
        {
            cuerpo.AddForce(Vector2.up * fuerzaSalto);
            saltando = false;
        }
        
    }   
    public void RecibeDanno(int cuanto, Vector2 posEnemigo)
    {
         if (muerto)
        {
            return;
        }

    vidaRestante -= cuanto;

    Debug.Log("Ouch!! Vida restante: " + vidaRestante);

    if (vidaRestante <= 0)
    {
        muerto = true;
        anim.SetTrigger("Muerte");

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        return;
    }

    anim.SetTrigger("Dolor");

    Vector2 direccionAtaque = ((Vector2)transform.position) - posEnemigo;
    direccionAtaque.Normalize();

    knockBack = direccionAtaque * 550 + 200 * Vector2.up;
    }

    bool EstaEnElSuelo()
    {  
        RaycastHit2D rc = Physics2D.BoxCast(
            transform.position, tamannoCaja, 0, Vector2.down, distanciaCaja, capaSuelo);
        if (rc)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * distanciaCaja, tamannoCaja);
    }
}
