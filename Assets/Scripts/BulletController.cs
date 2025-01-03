using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float timer = 0;
    public bool BulletIsMoveRight = true;
    public SpriteRenderer BulletSpr;
    // Start is called before the first frame update
    void Start()
    {
        BulletSpr = this.gameObject.GetComponent<SpriteRenderer>();

        if (BulletIsMoveRight)
        {
            BulletSpr.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BulletIsMoveRight)
        {
            this.gameObject.transform.position += new Vector3(0.5f * Time.deltaTime * 60, 0, 0);
        }
        else
        {
            this.gameObject.transform.position -= new Vector3(0.5f * Time.deltaTime * 60, 0, 0);
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);   //時間到了消除子彈
        }
    }
}