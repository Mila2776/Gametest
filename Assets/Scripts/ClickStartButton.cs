using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //GetComponent Button
        
    }
    public void OnClick()
    {
        //������C������
        SceneManager.LoadScene("Game_1");

    }
    public void OnClick2()
    {
        //������C������
        SceneManager.LoadScene("Start");

    }
}
