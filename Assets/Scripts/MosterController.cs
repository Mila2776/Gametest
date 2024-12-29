using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MosterController : MonoBehaviour
{
    public int monsterHp = 0;              //怪物hp
    public int monsterMaxHp = 0;    //怪物最大hp
  
    // Start is called before the first frame update
    void Start()
    {
        monsterMaxHp = 10;
        monsterHp = monsterMaxHp;

    }

    // Update is called once per frame
    void Update()
    {
        if (monsterHp <= 0)
        {
            Destroy(this.gameObject);
        }
        
        


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            
            Destroy(other.gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            monsterHp -= 1;
        }
    }


}

