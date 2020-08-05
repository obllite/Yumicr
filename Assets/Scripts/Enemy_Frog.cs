using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    public Transform left_point_;
    public Transform right_point_;
    public float left_x_, right_x_;
    public float jump_force_ = 8f;
    public LayerMask ground_;

    private Collider2D coll_;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll_ = GetComponent<Collider2D>();

        left_x_ = left_point_.position.x; Destroy(left_point_.gameObject);
        right_x_ = right_point_.position.x; Destroy(right_point_.gameObject);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SwitchAnim();
    }

    void Movement()
    {
        if(transform.localScale.x == 1)
        {
            if (transform.position.x < left_x_)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (coll_.IsTouchingLayers(ground_))
            {
                anim_.SetBool("jumping", true);
                rigidbody_.velocity = new Vector2(-speed_ * transform.localScale.x, jump_force_);
            }
        }
        else
        {
            if (transform.position.x > right_x_)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (coll_.IsTouchingLayers(ground_))
            {
                anim_.SetBool("jumping", true);
                rigidbody_.velocity = new Vector2(-speed_ * transform.localScale.x, jump_force_);
            }
        }
    }

    void SwitchAnim()
    {
        if(anim_.GetBool("jumping"))
        {
            if(rigidbody_.velocity.y < 0.1f)
            {
                anim_.SetBool("jumping", false);
                anim_.SetBool("falling", true);
            }
        }
        if(coll_.IsTouchingLayers(ground_) && anim_.GetBool("falling"))
        {
            anim_.SetBool("falling", false);
        }
    }
}
