using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 60f;
    public float jumpForce = 10f;  // แรงกระโดด
    private Rigidbody rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // ตัวแปรใหม่เพื่อจำกัดการกระโดด
    private int jumpCount = 0;  // จำนวนการกระโดดที่ใช้ไป
    private float cooldownTime = 5f;  // เวลาคูลดาวน์หลังจากกระโดดครบ 2 ครั้ง
    private float timeSinceLastJump = 0f;  // เวลาที่ผ่านมานับจากการกระโดดครั้งสุดท้าย

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // ป้องกันไม่ให้เกิดการหมุนเกินไป
        rb.freezeRotation = true;

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // อัพเดตเวลา
        timeSinceLastJump += Time.deltaTime;

        // รับค่าการเคลื่อนไหว
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        // คำนวณการเคลื่อนไหว
        Vector3 moveDirection = transform.forward * move * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // คำนวณการหมุน
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);

        // ตรวจสอบการกดปุ่ม Space เพื่อกระโดด
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // ตรวจสอบการกด R เพื่อรีเซ็ตตำแหน่ง
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPositionAndRotation();
        }
    }

    // ฟังก์ชันกระโดด
    void Jump()
    {
        // หากเวลาคูลดาวน์หมดหรือยังสามารถกระโดดได้
        if (timeSinceLastJump >= cooldownTime)
        {
            jumpCount = 0;  // รีเซ็ตจำนวนการกระโดดเมื่อเวลาคูลดาวน์หมด
        }

        // ให้กระโดดได้สูงสุด 2 ครั้ง
        if (jumpCount < 2)
        {
            // เพิ่มแรงกระโดดในแกน Y
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // เพิ่มจำนวนการกระโดด
            jumpCount++;

            // รีเซ็ตเวลาหลังจากกระโดด
            timeSinceLastJump = 0f;
        }
    }

    // ฟังก์ชันรีเซ็ตตำแหน่งและการหมุน
    void ResetPositionAndRotation()
    {
        rb.velocity = Vector3.zero; // รีเซ็ตความเร็วเมื่อรีเซ็ตตำแหน่ง
        rb.angularVelocity = Vector3.zero; // รีเซ็ตการหมุน
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
