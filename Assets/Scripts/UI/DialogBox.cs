using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [Header("Needs a Box Collider 2D with trigger on the object")]
    public GameObject dialogBox;
    public Text dialogText;
    public bool dialogBoxActive = false;
    [TextArea]
    public string dialog;
    bool playerInRange;
    

    float animationSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.transform.localScale = new Vector3(1, 0, 1);
        dialogText.text = dialog;
        dialogBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogBoxActive)
            {
                dialogBoxActive = false;
            }
            else
            {
                dialogBoxActive = true;
            }
        }

        BoxAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBoxActive = false;
        }
    }

    void BoxAnimation()
    {
        if (dialogBoxActive)
        {
            if (dialogBox.transform.localScale.y < 1f)
            {
                dialogBox.transform.localScale += new Vector3(0, Time.deltaTime * animationSpeed, 0);
            }
            else
            {
                dialogBox.transform.localScale = new Vector3(1, 1, 1);

            }
        }
        else
        {
            if (dialogBox.transform.localScale.y > 0f)
            {
                dialogBox.transform.localScale -= new Vector3(0, Time.deltaTime * animationSpeed, 0);
            }
            else
            {
                dialogBox.transform.localScale = new Vector3(1, 0, 1);
            }
        }
    }
}
