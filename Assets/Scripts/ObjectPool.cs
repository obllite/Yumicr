using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance_;
    [Header("Boss Shadow")]
    public GameObject boss_shadow_prefab_;
    public int boss_shadow_count_;
    private Queue<GameObject> available_shadow_ = new Queue<GameObject>();

    private void Awake()
    {
        instance_ = this;

        // Initiate object pool
        FillPool();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillPool()
    {
        for(int i = 0; i < boss_shadow_count_; i++)
        {
            var new_shadow = Instantiate(boss_shadow_prefab_);
            new_shadow.transform.SetParent(transform);

            PutIntoPool(new_shadow);
        }
    }

    public void PutIntoPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        available_shadow_.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if(available_shadow_.Count == 0)
        {
            FillPool();
        }
        var out_shadow = available_shadow_.Dequeue();
        out_shadow.SetActive(true);
        return out_shadow;
    }
}
