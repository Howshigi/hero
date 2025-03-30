using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float moveSpeed = 10f;  // ความเร็วในการเคลื่อนที่ไปข้างหน้า/ถอยหลัง
    public float turnSpeed = 80f;  // ความเร็วในการหมุน
    public float jumpForce = 5f;  // แรงกระโดด
    private Rigidbody rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    // ตัวแปรใหม่เพื่อจำกัดการกระโดด
    private int jumpCount = 0;  // จำนวนการกระโดดที่ใช้ไป
    private float cooldownTime = 5f;  // เวลาคูลดาวน์หลังจากกระโดดครบ 2 ครั้ง
    private float timeSinceLastJump = 0f;  // เวลาที่ผ่านมานับจากการกระโดดครั้งสุดท้าย

    // แรงโน้มถ่วงที่เราใช้เอง
    public float customGravity = -9.81f;  // แรงโน้มถ่วง (m/s^2)
    private Vector3 gravityForce;  // ตัวแปรเก็บแรงโน้มถ่วง


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ป้องกันไม่ให้เกิดการหมุนเกินไป (ไม่ให้ Rigidbody หมุนเอง)
        rb.freezeRotation = true;

        // เก็บตำแหน่งและการหมุนเริ่มต้น
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // อัพเดตเวลา
        timeSinceLastJump += Time.deltaTime;

        // รับค่าการเคลื่อนไหวจาก input
        float move = Input.GetAxis("Vertical") * moveSpeed; // การเคลื่อนที่ไปข้างหน้าและถอยหลัง
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime; // การหมุน

        // คำนวณการเคลื่อนไหวโดยใช้ฟิสิกส์
        Vector3 moveDirection = transform.forward * move;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z); // เพิ่มความเร็วในแนว X และ Z

        // การหมุนรถถัง (เหมือนหมุนวงล้อ)
        float turnAmount = turn * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f)); // หมุนตามค่าที่ได้จาก input

        // คำนวณแรงโน้มถ่วงเอง
        gravityForce = new Vector3(0, customGravity, 0);  // แรงโน้มถ่วงในแนว Y
        rb.AddForce(gravityForce, ForceMode.Acceleration);  // เพิ่มแรงโน้มถ่วงให้กับ Rigidbody

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
        if (jumpCount < 2 && IsGrounded())  // เช็กว่ารถถังอยู่บนพื้นหรือไม่
        {
            // เพิ่มแรงกระโดดในแกน Y
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // เพิ่มจำนวนการกระโดด
            jumpCount++;

            // รีเซ็ตเวลาหลังจากกระโดด
            timeSinceLastJump = 0f;
        }
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
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
    