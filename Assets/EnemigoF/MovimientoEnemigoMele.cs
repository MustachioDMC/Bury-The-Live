using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigoM : MonoBehaviour
{
    public int vidaMax;
    public float tiempoPatrulla;
    public float velocidad;
    public Vector2 tamannoAtaque;
    public float distanciaAtaque;
    public LayerMask capaJugador;

    
    float temporizador;
    int direccion;
    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D cuerpo;
    bool atacando;
    bool muerto=false;
    GameObject aGolpear;
    int vidaRestante;

    // Start is called before the first frame update
    void Start()
    {
        vidaRestante = vidaMax;
        temporizador = tiempoPatrulla;
        direccion = 1;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ActualizarMovimiento();
        Girar();
        ActualizarTemporizador();
        if(!muerto){
        if (DetectarJugador())
            {
                anim.SetBool("Atacar", true);
                atacando = true;
            }
            else
            {
                anim.SetBool("Atacar", false);
                atacando = false;
            }
        }
    }
    public bool EstaMuerto
    {
        get { return muerto; }
    }

    private bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(
            transform.position, tamannoAtaque, 0, Vector2.right * (direccion == 0 ? 1 : direccion),
            distanciaAtaque, capaJugador
        );

        if (rayo) {
            aGolpear = rayo.rigidbody.gameObject;
            //jugador.GetComponent<MovimientoJugador>().RecibeDanno(0, transform.position);
            return true;
        }
        else
        {
            aGolpear = null;
        }
        return false;
    }

    private void Girar()
    {
        if (direccion >= 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void ActualizarTemporizador()
    {
        if(!muerto){
            if (!atacando) 
            {
                temporizador -= Time.deltaTime;
            }
            if (temporizador < 0)
            {
                direccion *= -1;
                temporizador = tiempoPatrulla;
            }
        }
    }

    void FixedUpdate()
    {
        if (muerto)
        {
            cuerpo.velocity = Vector2.zero;
            return;
        }

        if (atacando)
        {
            cuerpo.velocity = new Vector2(0, cuerpo.velocity.y);
        }
        else
        {
            cuerpo.velocity = new Vector2(direccion * velocidad, cuerpo.velocity.y);
        }
    }

    public void Punnalada()
    {
        if (aGolpear != null) aGolpear.GetComponent<MovimientoJugadorA>().RecibeDanno(1);
    }
    public void RecibeDanno(int cuanto)
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

    }

    void OnDrawGizmos()
    {
        if (DetectarJugador())
        {
            Gizmos.color = Color.red;
        } 
        Gizmos.DrawWireCube(transform.position + Vector3.right * (direccion == 0 ? 1 : direccion) * distanciaAtaque,
            tamannoAtaque
        );
        Gizmos.color = Color.white;
    }
    
}