using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISetSelected : MonoBehaviour
{
    bool selected = false;

    private void Update()
    {
        if (GameManager.State == GameState.GameOver && !selected)
        {
            print("Select");
            selected = true;
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
