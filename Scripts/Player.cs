using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] LayerMask milk;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool isFinalLevel;

    private bool dead;
    private bool win;
    private bool paused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !win && !dead)
        {
            if (paused)
            {
                paused = false;
                pauseMenu.SetActive(false);
                GetComponent<PlayerMovement>().enabled = true;
                GetComponent<Rigidbody>().useGravity = true;

                // locks the cursor
                LockCursor(true);
            } else
            {
                paused = true;
                pauseMenu.SetActive(true);
                DisableMovement();

                // unlocks the cursor
                LockCursor(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Milk" && !dead)
        {
            timer.active = false;

            // player won the level, very cool
            Win();

            DisableMovement();
            
            // unlocks cursor
            LockCursor(false);
        } else if(other.transform.name == "Lava" && !dead && !win)
        {
            timer.active = false;

            // player ded, very bad
            dead = true;
            paused = true;

            pauseMenu.SetActive(true);
            winMenu.SetActive(false);

            DisableMovement();

            // unlocks cursor
            LockCursor(false);
        }
    }

    private void DisableMovement()
    {
        // disables movement on the player
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void LockCursor(bool value)
    {
        if (value) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

        Cursor.visible = !value;
    }

    private void Win()
    {
        win = true;
        paused = true;
        dead = false;

        if (isFinalLevel)
        {
            pauseMenu.SetActive(true);
            winMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
            winMenu.SetActive(true);
        }
    }
}
