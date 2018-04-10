using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemplateMenuNavagator : MonoBehaviour {

    public GameObject[] templatePages;
    public Text pageTitle;
    private int pageNumber;

    public void ChangePage(int navigate)
    {
        templatePages[pageNumber].SetActive(false);
        pageNumber += navigate;
        if (pageNumber >= templatePages.Length)
            pageNumber = 0;
        if (pageNumber < 0)
            pageNumber = templatePages.Length - 1;
        templatePages[pageNumber].SetActive(true);

        pageTitle.text = ("Page " + (pageNumber + 1));
    }
}
