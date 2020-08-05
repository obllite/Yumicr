using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkull : MonoBehaviour
{
    [SerializeField]private float speed_ = 6f;
    [SerializeField]private float dir_;
    private Rigidbody2D rigidbody_;
    // Start is called before the first frame update
    void Start()
    {
        dir_ = GameObject.Find("FinalBoss/FirstPhase").transform.localScale.x;
        rigidbody_ = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody_.velocity = Vector2.left * speed_ * dir_;
        transform.localScale = rigidbody_.velocity.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
