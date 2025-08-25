using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isEnemy;
    [SerializeField] int health = 50;
    [SerializeField] int scoreToAdd = 50;

    AudioPlayer audiolayer;
    LevelManager levelManager;
    ScoreKeeper scoreKeeper;
    ParticleSystem myParticleSystem;
    DamageDealer damageDealer;
    Shooter shooter;
    Animator animator;

    // The Awake method is called when the script instance is being loaded
    void Awake()
    {
        audiolayer = FindFirstObjectByType<AudioPlayer>();
        levelManager = FindFirstObjectByType<LevelManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        myParticleSystem = GetComponent<ParticleSystem>();
        damageDealer = GetComponent<DamageDealer>();
        shooter = GetComponent<Shooter>();
        animator = GetComponentInChildren<Animator>();
    }

    // on trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) // It checks if the colliding object has a DamageDealer component.
        {
            TakeDamage(damageDealer.GetDamage()); // If it does, it takes the damage from the DamageDealer
            damageDealer.Hit();                   // and calls the Hit() method on the DamageDealer to destroy it
        }
    }

    // recieve damage
    void TakeDamage(int damage)
    {
        health -= damage; // Reduces the health by the damage amount

        if (health <= 0) // If health is less than or equal to 0
        {
            Die();
        }

        audiolayer.PlayDamageClip();
    }

    // die
    void Die()
    {
        if (isEnemy)
        {
            scoreKeeper.ModifyScore(scoreToAdd);
        }
        else
        {
            myParticleSystem.Stop();
            levelManager.LoadGameOver();
        }
        animator.SetTrigger("die");
        Destroy(this);              // it destroys only the health
        Destroy(damageDealer);      // it destroys only the damage dealer
        Destroy(shooter);      // it destroys only the damage dealer
        Destroy(gameObject, 1f);    // it destroys the game object this script is attached to
    }

    // get the current health
    public int GetHealth()
    {
        return health;
    }
}
