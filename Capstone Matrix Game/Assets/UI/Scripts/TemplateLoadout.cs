using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateLoadout : MonoBehaviour {

    public GameObject[] TemplateSlots;
    public GameObject[] LevelLoadout;

	void Start () {

		for(int i = 0; i < LevelLoadout.Length; i++)
        {
            if (LevelLoadout[i] != null && TemplateSlots[i] != null)
                Instantiate(LevelLoadout[i], TemplateSlots[i].transform);
        }
	}

}
