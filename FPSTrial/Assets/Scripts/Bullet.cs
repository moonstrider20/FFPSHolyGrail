using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;                //To hold rigidbody
    public Enemy target;                //To hold enemy
    public PlayerInput targetPlayer;    //To hold player

    //Damage
    public int explosionDamage;         //Damage done by bullet (when exploding)

    //Lifetime
    public float maxLifetime;           //How long bullet lives, don't want strays flying forever

    private void Update()
    {
        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        
        //If lifetime met
        if (maxLifetime <= 0)
        {
            Delay();        //Destroy bullet, delay so no bugs
        }
    }


    //Method to destroy bullet
    private void Delay()
    {
        Destroy(gameObject);
    }

    //On collsion do these things
    private void OnCollisionEnter(Collision collision)
    {
        //Explode if bullet hits an enemy 
        if (collision.collider.CompareTag("Enemy"))
        {
            target = collision.gameObject.GetComponent<Enemy>();

            target.TakeDamage(explosionDamage);
        }

        //Explode if bullet hits the player
        if (collision.collider.CompareTag("Player"))
        {
            targetPlayer = collision.gameObject.GetComponent<PlayerInput>();
            Debug.Log(targetPlayer);
            targetPlayer.TakeDamage(explosionDamage);
        }

        //Destroy bullet regardls of what was hit
        Delay();
    }
}