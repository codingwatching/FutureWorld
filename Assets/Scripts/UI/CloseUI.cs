using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    private Crafting crafting;

    private void Start()
    {
        crafting = GameObject.Find("Player").GetComponent<Crafting>();
    }

    public void CloseUIElement()
    {
        crafting.craftingActive = false;
        SoundManager.instance.PlaySound(2);
    }
}
