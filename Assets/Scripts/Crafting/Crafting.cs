using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameObject craftingPanel;

    public bool craftingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCraftingPanel();
    }

    void SetCraftingPanel()
    {
        if (craftingActive)
        {
            craftingPanel.SetActive(true);
        }
        else
        {
            craftingPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftingActive)
            {
                SoundManager.instance.PlaySound(2);
                craftingActive = false;
            }
            else
            {
                SoundManager.instance.PlaySound(2);
                craftingActive = true;
            }
        }
    }
}
