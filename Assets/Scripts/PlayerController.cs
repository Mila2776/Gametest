using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;




public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //GetComponent
    private SpriteRenderer render;
    private Animation anim;
    private Rigidbody2D rig;

    public float speed_x_constraint;    //移動速度限制
    private float speed = 10;

    //方向控制
    private bool IsFaceRight = false;
    //跳躍控制
    private bool IsJumping = false;
    private float JumpingTime;
    //玩家數值
    //血條
    public Image Player_HP_bar;
    static public float hp = 10;//hp
    public float Maxhp = 0; //最大hp
    //分數
    public GameObject Point;
    public int point = 0;//分數
    public int Maxpoint = 7;
    
    void Start()
    {
         //GetComponent
        render = GetComponent<SpriteRenderer>();
        anim=GetComponent<Animation>();
        rig =GetComponent<Rigidbody2D>();

        Maxhp = 10;
        hp = Maxhp;

        //方向控制
        IsFaceRight = !render.flipX;
    }
   
    // Update is called once per frame
    void Update()
    {
        //方向控制

        //向左移動
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            rig.AddForce(new Vector2(-speed , 0), ForceMode2D.Force);

            if (IsFaceRight)
            {
                Flip();
            }
        }
        //向右移動
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D");
            rig.AddForce(new Vector2(speed , 0), ForceMode2D.Force);
            if (!IsFaceRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.Space)&!IsJumping)
        {
            Debug.Log("Space");
            IsJumping = true;
            JumpingTime = 0;
            rig.AddForce(new Vector2(0, 9), ForceMode2D.Impulse);

        }

        //血條顯示
        //float Playerhp = (float)hp / (float)Maxhp;
        Player_HP_bar.transform.localScale = new Vector3((float)hp / (float)Maxhp, Player_HP_bar.transform.localScale.y, Player_HP_bar.transform.localScale.z);
        //分數顯示
        Point = GameObject.Find("Point");
        Point.GetComponent<Text>().text = $"{point}/{Maxpoint}";
        //設定限速
        if (rig.velocity.x > speed_x_constraint)
        {
            rig.velocity = new Vector2(speed_x_constraint, rig.velocity.y);
        }
        if (rig.velocity.x < -speed_x_constraint)
        {
            rig.velocity = new Vector2(-speed_x_constraint, rig.velocity.y);
        }
    }
     private void Flip()
    {
        //方向控制
        IsFaceRight = !IsFaceRight;
        render.flipX = !render.flipX;
    }
    private void FixedUpdate()
    {
        //跳躍防呆
        if (IsJumping)
        {
            JumpingTime += Time.fixedDeltaTime;
            if (JumpingTime>.1f & rig.Cast(Vector2.down,new RaycastHit2D[10], .1f) > 0)
                
            {

                IsJumping = false;
            }
        }
       
       
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monster")   //碰到怪物
        {
            //print(coll.gameObject.name);   
            hp -= 1;     //扣血

            rig.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);

            if (hp < 0)
            {

                point = 0;
                Destroy(this.gameObject);
                SceneManager.LoadScene("Die");                //死亡

            }
        }
        if (coll.gameObject.tag == "Point")
        {
            point += 1;
        }
            
    }
}
