using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BounceCount = 3;
    public bool canBePickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BounceCount == 3)
        {
            Destroy(gameObject); // Hủy đối tượng viên đạn sau khi nảy 3 lần
        }
    }

    // Phương thức này được gọi khi đạn va chạm với một collider khác
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Kiểm tra nếu đạn va chạm với một collider có tag khác "Player" (ví dụ: Ground)
        if (other.gameObject.CompareTag("Ground"))
        {
            BounceCount++;
        }

        if (other.gameObject.CompareTag("Player"))
        {   
            canBePickedUp = true;
        }
    }
}
