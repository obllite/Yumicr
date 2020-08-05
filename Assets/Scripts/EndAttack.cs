using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttack : MonoBehaviour
{
    public void FinishAttack()
    {
        gameObject.SetActive(false);
    }
}
