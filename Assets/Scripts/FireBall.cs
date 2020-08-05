using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector2 chased_dir_;
    private Rigidbody2D rigidbody_;
    private Animator anim_;
    private float speed_ = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        anim_ = GetComponent<Animator>();
        chased_dir_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        chased_dir_ = (new Vector2(chased_dir_.x - transform.position.x, chased_dir_.y - transform.position.y)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        rigidbody_.velocity = chased_dir_ * speed_;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody_.bodyType = RigidbodyType2D.Static;
        anim_.SetBool("Boom", true);
    }

    private void Boom()
    {
        Destroy(gameObject);
    }
}
