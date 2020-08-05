using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed_;
    public float jump_force_;
    public float attack_damage_ = 50;
    public LayerMask ground_layer_;
    public Text score_text_;
    public Transform ceiling_check_;
    public Transform buttom_check_;
    public Transform dead_line_;

    private int score_ = 0;
    private float dead_line_y_;
    private Rigidbody2D rigidbody_;
    private Animator anim_;
    private int jump_count_ = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        anim_ = GetComponent<Animator>();
        dead_line_y_ = GameObject.Find("DeadLine").transform.position.y;    Destroy(dead_line_.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        Jump();
        SwitchAnim();
    }

    private void FixedUpdate()
    {
        Run();

        if (rigidbody_.position.y < dead_line_y_)
        {
            Invoke("Restart", 1f);
        }
    }
    void Run()
    {
        if (!anim_.GetBool("hurt"))
        {
            float horizontal_move = Input.GetAxis("Horizontal");
            float face_direction = Input.GetAxisRaw("Horizontal");

            rigidbody_.velocity = new Vector2(horizontal_move * speed_, rigidbody_.velocity.y);
            anim_.SetFloat("running", Mathf.Abs(horizontal_move));
            if (face_direction != 0)
            {
                transform.localScale = new Vector3(face_direction, 1, 1);
            }
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jump_count_ > 0)
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, jump_force_);
            anim_.SetBool("jumping", true);
            anim_.SetBool("falling", false);
            jump_count_--;
            SoundManager.instance_.JumpAudio();
        }
    }
    
    void SwitchAnim()
    {
        if (rigidbody_.velocity.y < 0.1f && !Physics2D.OverlapBox(buttom_check_.position,new Vector2(2 ,0.2f), ground_layer_))
        {
            anim_.SetBool("falling", true);
        }

        if (anim_.GetBool("jumping"))
        {
            if (rigidbody_.velocity.y < 0)
            {
                anim_.SetBool("jumping", false);
                anim_.SetBool("falling", true);
            }
        }
        else if (anim_.GetBool("hurt"))
        {
            if(Mathf.Abs(rigidbody_.velocity.x) < 3f && Mathf.Abs(rigidbody_.velocity.y) < 3f)
            {
                anim_.SetBool("hurt", false);
            }
        }
        else if (Physics2D.OverlapCircle(buttom_check_.position, 0.1f, ground_layer_))
        {
            anim_.SetBool("falling", false);
            jump_count_ = 2;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Collect collections
        if (other.CompareTag("Collection"))
        {
            Destroy(other.gameObject);
            score_++;
            score_text_.text = score_.ToString();
            SoundManager.instance_.CollectAudio();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakenDamage(attack_damage_);
            other.GetComponent<Enemy>().chased_target_ = transform;
            other.gameObject.transform.position += new Vector3(transform.localScale.x, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // Was hurt by enemy
        if (collisionInfo.gameObject.CompareTag("Enemy"))
        {
            TakenDamage(collisionInfo.transform, 50); // temperate code
        }
    }

    public void TakenDamage(Transform enemy, float damage)
    {
        if (transform.position.x < enemy.position.x)
        {
            rigidbody_.velocity = new Vector2(-5, 5);
            anim_.SetBool("hurt", true);
            SoundManager.instance_.HurtAudio();
        }
        else if (transform.position.x > enemy.transform.position.x)
        {
            rigidbody_.velocity = new Vector2(5, 5);
            anim_.SetBool("hurt", true);
            SoundManager.instance_.HurtAudio();
        }
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
