using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Eagle : Enemy
{
    public Transform top_point_;
    public Transform buttom_point_;

    private bool is_rise_;
    private Vector2 topright_beacon_;
    private Vector2 buttomleft_beacon_;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        topright_beacon_ = top_point_.position;
        buttomleft_beacon_ = buttom_point_.position;
        Destroy(top_point_.gameObject);
        Destroy(buttom_point_.gameObject);
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Movement();
    }

    void Movement()
    {
        KeepWatch();
        if (chased_target_ == null)
        {
            Patrol();
        }
        else if (chased_target_ == GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>())
        {
            ChaseTarget();
        }
        
    }

    void KeepWatch()
    {
        chased_target_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (chased_target_.position.x > topright_beacon_.x || chased_target_.position.x < buttomleft_beacon_.x) // Is the player in chased range ?
        {
            chased_target_ = null;
        }
    }

    void ChaseTarget()
    {
        rigidbody_.velocity = new Vector2(0, 0);
        transform.localScale = transform.parent.gameObject.GetComponent<AIPath>().desiredVelocity.x >= 0.01f ? new Vector3(-1,1,1) : new Vector3(1,1,1);
        gameObject.transform.parent.gameObject.GetComponent<AIDestinationSetter>().target = chased_target_;
    }
    void Patrol()
    {
        if (is_rise_)
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, speed_);
            if(transform.position.y > topright_beacon_.y)
            {
                is_rise_ = false;
            }
        }
        else 
        {
            rigidbody_.velocity = new Vector2(rigidbody_.velocity.x, -speed_);
            if(transform.position.y < buttomleft_beacon_.y)
            {
                is_rise_ = true;
            }
        }
    }
}
