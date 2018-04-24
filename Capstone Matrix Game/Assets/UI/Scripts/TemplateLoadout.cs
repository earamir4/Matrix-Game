using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateLoadout : MonoBehaviour {

    private GameObject TemplateList;
    public GameObject[] LevelLoadout;
    public GameObject TemplateSlot;

    void Start()
    {
        TemplateList = GameObject.Find("TemplateList");
        for(int i = 0; i<LevelLoadout.Length; i++)
        {
            if (LevelLoadout[i] != null)
            {
                GameObject newSlot = Instantiate(TemplateSlot, TemplateList.transform);
                Instantiate(LevelLoadout[i], newSlot.transform);
            }
        }
    }

}
