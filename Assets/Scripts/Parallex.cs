using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    public Transform cam_;
    public float move_rate_;

    private float start_pos_;
    // Start is called before the first frame update
    void Start()
    {
        start_pos_ = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(start_pos_ + cam_.position.x * move_rate_, transform.position.y);
    }
}
