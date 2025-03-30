using UnityEngine;

public class Bullet : MonoBehaviour
{

    public ParticleSystem explosionParticle;
    public float damage = 100f; // ดาเมจของกระสุน
    public float lifeTime = 5f; // กระสุนจะหายไปหลังจาก 5 วินาที
    public AudioSource audioSource;
    void Start()
    {
        Destroy(gameObject, lifeTime); // ทำลายกระสุนถ้าไม่ได้โดนอะไร
    }

    void OnTriggerEnter(Collider other)
    {
        // เช็กว่ากระสุนชนกับตึกหรือไม่
        if (other.CompareTag("Building"))   
        {
            Debug.Log("Hit");
            BuildingHealth building = other.GetComponent<BuildingHealth>();
            if (building != null)
            {
                building.TakeDamage(damage); // ลดเลือดตึก
            }
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject); // ทำลายกระสุนเมื่อชนตึก
            audioSource.Play();

        }
    }


}