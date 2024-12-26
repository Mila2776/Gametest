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

    public float speed_x_constraint;    //���ʳt�׭���
    private float speed = 10;

    //��V����
    private bool IsFaceRight = false;
    //���D����
    private bool IsJumping = false;
    private float JumpingTime;
    //���a�ƭ�
    //���
    public Image Player_HP_bar;
    static public float hp = 10;//hp
    public float Maxhp = 0; //�̤jhp
    //����
    public GameObject Point;
    public int point = 0;//����
    public int Maxpoint = 7;
    
    void Start()
    {
         //GetComponent
        render = GetComponent<SpriteRenderer>();
        anim=GetComponent<Animation>();
        rig =GetComponent<Rigidbody2D>();

        Maxhp = 10;
        hp = Maxhp;

        //��V����
        IsFaceRight = !render.flipX;
    }
   
    // Update is called once per frame
    void Update()
    {
        //��V����

        //�V������
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A");
            rig.AddForce(new Vector2(-speed , 0), ForceMode2D.Force);

            if (IsFaceRight)
            {
                Flip();
            }
        }
        //�V�k����
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

        //������
        //float Playerhp = (float)hp / (float)Maxhp;
        Player_HP_bar.transform.localScale = new Vector3((float)hp / (float)Maxhp, Player_HP_bar.transform.localScale.y, Player_HP_bar.transform.localScale.z);
        //�������
        Point = GameObject.Find("Point");
        Point.GetComponent<Text>().text = $"{point}/{Maxpoint}";
        //�]�w���t
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
        //��V����
        IsFaceRight = !IsFaceRight;
        render.flipX = !render.flipX;
    }
    private void FixedUpdate()
    {
        //���D���b
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
        if (coll.gameObject.tag == "Monster")   //�I��Ǫ�
        {
            //print(coll.gameObject.name);   
            hp -= 1;     //����

            rig.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);

            if (hp < 0)
            {

                point = 0;
                Destroy(this.gameObject);
                SceneManager.LoadScene("Die");                //���`

            }
        }
        if (coll.gameObject.tag == "Point")
        {
            point += 1;
        }
            
    }
}
