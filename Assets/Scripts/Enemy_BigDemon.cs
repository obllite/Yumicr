using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_BigDemon : Enemy
{
    public Transform left_point_, right_point_;
    public float left_x_, right_x_;

    [SerializeField]private AudioSource alarm_audio_;
    protected override void Start()
    {
        base.Start();
        left_x_ = left_point_.position.x; Destroy(left_point_.gameObject);
        right_x_ = right_point_.position.x; Destroy(right_point_.gameObject);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        KeepWatch();
        Movement();
    }

    protected void KeepWatch()
    {
        //Ray2D chased_ray_ = new Ray2D(transform.position, transform.localScale.x * Vector2.right);
        //Vector2 beg = new Vector2(transform.position.x, transform.position.y - 2);
        //Vector2 dir = transform.localScale.x * Vector2.right;
        //Collider2D ray_coll_ = Physics2D.Raycast(beg, dir, right_x_ - left_x_).collider;
        //Debug.DrawRay(chased_ray_.origin,chased_ray_.direction, Color.blue);

        //if(ray_coll_ != null)
        //{
        //    if (ray_coll_.CompareTag("Player") && ray_coll_.transform.position.x > left_x_ && ray_coll_.transform.position.x < right_x_)
        //    {
        //        transform.GetChild(2).gameObject.SetActive(true);
        //        alarm_audio_.Play();
        //    }
        //}
    }
    private void ChaseTarget()
    {       
        transform.position = Vector2.MoveTowards(transform.position, chased_target_.position, speed_);
    }

    private void Movement()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (transform.localScale.x == 1)  // Face right
        {
            if (transform.position.x >= right_x_)
            {
                anim_.SetBool("idle", true);
                rigidbody_.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                rigidbody_.velocity = new Vector2(Math.Max(rigidbody_.velocity.x,speed_), rigidbody_.velocity.y);
            }
        }
        else if (transform.localScale.x == -1)  // Face left
        {
            if (transform.position.x <= left_x_)
            {
                anim_.SetBool("idle", true);
                rigidbody_.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                rigidbody_.velocity = new Vector2(-Math.Max(rigidbody_.velocity.x, speed_), rigidbody_.velocity.y);
            }
        }
    }
    private void PatrolTurnBack()
    {
        if (transform.localScale.x == 1)  // Face right
        {
            if (transform.position.x >= right_x_)
            {
                rigidbody_.bodyType = RigidbodyType2D.Dynamic;
                transform.localScale = new Vector3(-1, 1, 1);
                rigidbody_.velocity = new Vector2(-speed_, rigidbody_.velocity.y);
                anim_.SetBool("idle", false);
            }
        }
        else if (transform.localScale.x == -1)  // Face left
        {
            if (transform.position.x <= left_x_)
            {
                rigidbody_.bodyType = RigidbodyType2D.Dynamic;
                transform.localScale = new Vector3(1, 1, 1);
                rigidbody_.velocity = new Vector2(speed_, rigidbody_.velocity.y);
                anim_.SetBool("idle", false);
            }
        }
    }
}
