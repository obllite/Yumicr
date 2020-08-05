using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI component")]
    public Text text_label_;
    public Image profile_picture_;

    [Header("Text context")]
    public TextAsset text_file_;
    public int index_;
    public List<string> text_list_ = new List<string>();
    public float text_speed_;
    [SerializeField] bool is_text_finished_;
    [SerializeField] bool is_show_immediately_;

    [Header("Profile picture")]
    public Sprite player_profile_;
    public Sprite sensei_profile_;
    private void OnEnable()
    {
        // text_label_.text = text_list_[index_++];
        StartCoroutine("SetText");
    }
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(text_file_);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && index_ == text_list_.Count)
        {
            gameObject.SetActive(false);
            index_ = 0;
        }
        if(Input.GetKeyDown(KeyCode.R) && gameObject.activeSelf)
        {
            if(is_text_finished_ && !is_show_immediately_)
            {
                StartCoroutine(SetText());
            }
            else if(!is_text_finished_ && !is_show_immediately_)
            {
                is_show_immediately_ = !is_show_immediately_;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        text_list_.Clear();
        index_ = 0;

        var line_data = file.text.Split('\n');
        
        foreach(var line in line_data)
        {
            text_list_.Add(line);
        }
    }

    IEnumerator SetText()
    {
        is_text_finished_ = false;
        text_label_.text = "";

        switch(text_list_[index_])
        {
            case "A":
                profile_picture_.sprite = player_profile_;
                index_++;
                break;
            case "B":
                profile_picture_.sprite = sensei_profile_;
                index_++;
                break;
        }

        int i = 0;
        while(!is_show_immediately_ && i < text_list_[index_].Length - 1)
        {
            text_label_.text += text_list_[index_][i++];
            yield return new WaitForSeconds(text_speed_);
        }
        text_label_.text = text_list_[index_];

        is_show_immediately_ = false;
        is_text_finished_ = true;
        index_++;
    }
}
