using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Country
{
    Australia,
    Japan,
    America
};


public class Client : MonoBehaviour
{
    private int selection = 0;



    public List<GameObject> selction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (selection)
        {
            case 0:
                selction[0].SetActive(true);
                selction[1].SetActive(false);
                break;
            case 1:
                selction[1].SetActive(true);
                selction[0].SetActive(false);
                SceneManager.LoadScene("OutGame");
                break;
           
               
            
        }
    }

    

    public void OnClickJap()
    {
       // GameManager.Instance.Settings.contry = Country.Japan;
        selection = 1;
    }
    public void OnClickAus()
    {
       // GameManager.Instance.Settings.contry = Country.Australia;
        selection = 1;
    }
    public void OnClickAme()
    {
       // GameManager.Instance.Settings.contry = Country.America;
        selection = 1;
    }


}
