using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject button_;
    public GameObject talk_UI_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(button_.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talk_UI_.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button_.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        button_.SetActive(false);
    }
}
