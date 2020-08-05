using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp_;

    protected Animator anim_;
    protected Rigidbody2D rigidbody_;
    protected AudioSource death_audio_;
    [SerializeField] protected float speed_;
    [SerializeField] protected float max_hp_;
    [SerializeField] public Transform chased_target_;
    [SerializeField] protected Transform player_;

    private SpriteRenderer sprite_renderer_;
    private const float hurt_length_ = 0.1f;
    private float hurt_count_;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp_ = max_hp_;
        chased_target_ = null;
        anim_ = GetComponent<Animator>();
        rigidbody_ = GetComponent<Rigidbody2D>();
        death_audio_ = GetComponent<AudioSource>();
        player_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sprite_renderer_ = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if(hurt_count_ <= 0)
        {
            sprite_renderer_.material.SetFloat("_FlashAmount", 0);
        }
        else
        {
            hurt_count_ -= Time.deltaTime;
        }
    }

    protected virtual void FixedUpdate()
    {

    }

    public void Death()
    {
        Destroy(gameObject);
    }
  
    public virtual void TakenDamage(float damage_amout)
    {
        hp_ -= damage_amout;
        HurtShader();

        if (hp_ <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            rigidbody_.bodyType = RigidbodyType2D.Static;
            death_audio_.Play();
            anim_.SetTrigger("death");
        } 
    }

    private void HurtShader()
    {
        sprite_renderer_.material.SetFloat("_FlashAmount", 1f);
        hurt_count_ = hurt_length_;
    }
}
