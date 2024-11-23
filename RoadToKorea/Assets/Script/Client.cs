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

public enum Chracter
{
    woman,
    man
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
                selction[2].SetActive(false);
                selction[1].SetActive(false);
                break;
            case 1:
                selction[1].SetActive(true);
                selction[0].SetActive(false);
                selction[2].SetActive(false);
                break;
            case 2:
                selction[0].SetActive(false);
                selction[1].SetActive(false);
                selction[2].SetActive(true);
                break;
            case 3:
                SceneManager.LoadScene("OutGame");
                break;
               
            
        }
    }

    public void OnClickHardMode()
    {
        //GameManager.Instance.PlayerData.hard = true;
        selection = 1;
    }
    public void OnClickEasyMode()
    {
        //GameManager.Instance.PlayerData.hard = false;
        selection = 1;

    }

    public void OnClickJap()
    {
       // GameManager.Instance.Settings.contry = Country.Japan;
        selection = 2;
    }
    public void OnClickAus()
    {
       // GameManager.Instance.Settings.contry = Country.Australia;
        selection = 2;
    }
    public void OnClickAme()
    {
       // GameManager.Instance.Settings.contry = Country.America;
        selection = 2;
    }

    public void OnClickman()
    {
       // GameManager.Instance.Settings.chrac = Chracter.man;
        selection = 3;
    }

    public void OnClickwom()
    {
       // GameManager.Instance.Settings.chrac = Chracter.woman;
        selection = 3;
    }
}
