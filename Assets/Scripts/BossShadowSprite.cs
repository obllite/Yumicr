using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossShadowSprite : MonoBehaviour
{
    [SerializeField] private Transform boss_;
    [SerializeField] private SpriteRenderer shadow_sprite_;
    [SerializeField] private SpriteRenderer boss_sprite_;
    [SerializeField] private Color color_;

    [Header("Arguments for time control")]
    public float active_time_;
    public float active_start_;

    [Header("Argument for opacity control")]
    private float alpha_;
    public float alpha_set_;
    public float alpha_multiplier_;

    private void OnEnable()
    {
        boss_ = GameObject.Find("FinalBoss/FirstPhase").transform;
        shadow_sprite_ = GetComponent<SpriteRenderer>();
        boss_sprite_ = boss_.GetComponent<SpriteRenderer>();

        alpha_ = alpha_set_;

        shadow_sprite_.sprite = boss_sprite_.sprite;

        transform.position = boss_.position;
        transform.localScale = boss_.localScale;
        transform.rotation = boss_.rotation;

        active_start_ = Time.time;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        alpha_ *= alpha_multiplier_;
        color_ = new Color(1, 1, 1, alpha_);
        shadow_sprite_.color = color_;

        if(Time.time >= active_start_ + active_time_)
        {
            // Return to object pool
            ObjectPool.instance_.PutIntoPool(this.gameObject);
        }
    }


}
