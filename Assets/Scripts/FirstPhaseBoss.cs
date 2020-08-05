using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FirstPhaseBoss : Enemy
{
    [Header("Fireball")]
    public GameObject fireball_prefab_;
    [SerializeField]private Vector2[] fireball_pos_ = new Vector2[5];
    [SerializeField]private float fireball_spell_time_ = 0.2f;
    [SerializeField]bool fireball_spell_ = false;
    [SerializeField]short fireball_index_;

    [Header("FireSkull")]
    public GameObject fireskull_prefab_;
    [SerializeField] private bool fireskull_spell_ = false;

    [Header("BreathAttack")]
    [SerializeField]private bool breath_attack_ = false;
    [SerializeField]private float breath_shadow_deltatime = 0.2f;

    [Header("SummonGhost")]
    public GameObject ghost_prefab_;
    [SerializeField]private bool summon_ghost_ = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        speed_ = 8;
        // Fireball init
        fireball_index_ = 0;
        foreach(Transform t in GameObject.Find("FinalBoss/FirstPhase/FireBallSpell").GetComponentInChildren<Transform>())
        {
            fireball_pos_[fireball_index_++] = t.position;
        }
        fireball_index_ = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        transform.localScale = rigidbody_.velocity.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        
        if (fireball_spell_)
        {
            FireBallAttack();
        }

        if (fireskull_spell_)
        {
            FireSkullAttack();
        }

        if(breath_attack_)
        {
            BreathAttack();
        }

        if(summon_ghost_)
        {
            SummonGhost();
        }
    }

    private void SummonGhost()
    {
        GameObject.Instantiate(ghost_prefab_, fireball_pos_[1], new Quaternion(0, 0, 0, 0));
        GameObject.Instantiate(ghost_prefab_, fireball_pos_[3], new Quaternion(0, 0, 0, 0));
        summon_ghost_ = false;
    }
    private void BreathAttack()
    {
        Vector2 target_pos = (Vector2)player_.transform.position + Vector2.up * 4;
        target_pos += transform.localScale.x == 1 ? Vector2.right * 3 : Vector2.left * 3;

        rigidbody_.velocity = (float)(target_pos - (Vector2)transform.position).sqrMagnitude > 0.2f ?
          (target_pos - (Vector2)transform.position).normalized * speed_ * 2f : Vector2.zero;

        if (rigidbody_.velocity == Vector2.zero)
        {
            transform.localScale = transform.position.x > player_.transform.position.x ? Vector3.left : Vector3.right;
            anim_.SetBool("breath", true);
            breath_attack_ = false;
        }

        //ObjectPool.instance_.GetFromPool();
    }
    private void FireSkullAttack()
    {
        if (transform.position.y - player_.position.y > 0.1)
        {
            rigidbody_.velocity = Vector2.down  * speed_;
        }
        else if(transform.position.y - player_.position.y < -0.1)
        {
            rigidbody_.velocity = Vector2.up * speed_;
        }
        else
        {
            GameObject.Instantiate(fireskull_prefab_, transform.position + Vector3.left * 5, new Quaternion(0, 0, 0, 0));
            fireskull_spell_ = false;
            rigidbody_.velocity = new Vector2(0, 0);
        }
    }
    private void FireBallAttack()
    {

        if (fireball_spell_time_ < 0)
        {
            GameObject.Instantiate(fireball_prefab_, fireball_pos_[fireball_index_], new Quaternion(0, 0, 0, 0));
            fireball_index_++;
            fireball_spell_time_ = 0.1f;
        }
        else
        {
            fireball_spell_time_ -= Time.deltaTime;
        }

        if (fireball_index_ == fireball_pos_.Length)
        {
            fireball_index_ = 0;
            fireball_spell_ = false;
        }
    }

    private void StartAttak()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        switch (Random.Range(0,2))
        {
            case 0:
                anim_.SetBool("spell", true);
                break;
            case 1:
                breath_attack_ = true;
                break;
            default:
                break;
        }
    }

    private void Spell()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        switch (Random.Range(0, 3))
        {
            case 0:
                fireball_spell_ = true;
                break;
            case 1:
                fireskull_spell_ = true;
                break;
            case 2:
                summon_ghost_ = true;
                break;
            case 3:
                breath_attack_ = true;
                break;
            default:
                break;
        }
    }

    private void AttackFinish()
    {
        anim_.SetBool("breath", false);
        anim_.SetBool("spell", false);
        anim_.SetBool("idle", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakenDamage(transform, 1);
        }
    }
}
