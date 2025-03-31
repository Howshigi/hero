using UnityEngine;

public class Bullet : MonoBehaviour
{

    public ParticleSystem explosionParticle;
    public float damage = 100f; 
    public float lifeTime = 5f; 
    public AudioSource audioSource;
    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Building"))   
        {
            Debug.Log("Hit");
            BuildingHealth building = other.GetComponent<BuildingHealth>();
            if (building != null)
            {
                building.TakeDamage(damage); 
            }
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject); 
            audioSource.Play();

        }
    }


}