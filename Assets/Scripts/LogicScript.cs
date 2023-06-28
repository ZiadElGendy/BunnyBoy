using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public int carrots = 0;
    public TMP_Text carrotText;
    public TMP_Text deathText;

    public void DisplayDeathText()
    {
          deathText.text = "You died!\nPress ENTER";
    }

    public void IncrementCarrots()
    {
        carrots++;
        carrotText.text = "Carrots:\n" + carrots.ToString() + "/12";

        if (carrots == 12)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
