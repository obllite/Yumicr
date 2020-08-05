using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private Vector2 attack_pos_;
    private bool is_attak_ = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        transform.localScale = rigidbody_.velocity.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    private void AfterAppear()
    {
        anim_.SetTrigger("appeared");
    }

    private void StartAttak()
    {
        attack_pos_ = player_.position;
        is_attak_ = true;
        anim_.SetBool("attack", true);
    }

    private void EndAttack()
    {
        if (((Vector2)transform.position - attack_pos_).sqrMagnitude < 0.2f)
        {
            is_attak_ = false;
            anim_.SetBool("attack", false);
            rigidbody_.velocity = Vector2.zero;
        }
        else
        {
            rigidbody_.velocity = (attack_pos_ - (Vector2)transform.position).normalized * speed_;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim_.SetBool("attack", false);
    }
}
