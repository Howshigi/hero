using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 60f;
    private Rigidbody rb;

    private Vector3 startPosition; // ตัวแปรเก็บตำแหน่งเริ่มต้น
    private Quaternion startRotation; // ตัวแปรเก็บการหมุนเริ่มต้น

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // เก็บตำแหน่งเริ่มต้นและการหมุนเริ่มต้นของรถ
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate() // เปลี่ยนจาก Update เป็น FixedUpdate
    {
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;

        // รีเซ็ตแรงที่ไม่ต้องการ
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // เคลื่อนที่ไปข้างหน้า
        Vector3 moveDirection = transform.forward * move * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
        
        // หมุนรถถัง
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);

        // รีเซ็ตตำแหน่งและการหมุนเมื่อกดปุ่ม R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPositionAndRotation();
        }
    }

    // ฟังก์ชั่นรีเซ็ตตำแหน่งและการหมุน
    void ResetPositionAndRotation()
    {
        rb.velocity = Vector3.zero; // รีเซ็ต velocity
        rb.angularVelocity = Vector3.zero; // รีเซ็ต angular velocity
        transform.position = startPosition; // รีเซ็ตตำแหน่ง
        transform.rotation = startRotation; // รีเซ็ตการหมุน
    }

}
