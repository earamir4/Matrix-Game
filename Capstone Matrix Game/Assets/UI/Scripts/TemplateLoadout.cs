using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateLoadout : MonoBehaviour {

    private GameObject[] MatrixSlots;
    public GameObject[] LevelLoadout;

    void Start()
    {
        MatrixSlots = GameObject.FindGameObjectsWithTag("TemplateSlot");
        for(int i = 0; i<LevelLoadout.Length; i++)
        {
            if (LevelLoadout[i] != null)
                Instantiate(LevelLoadout[i], MatrixSlots[i].transform);
        }
    }

}
