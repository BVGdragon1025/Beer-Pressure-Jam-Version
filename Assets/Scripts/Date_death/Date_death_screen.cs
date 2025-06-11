using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Date_death_screen : MonoBehaviour
{
    public TextMeshProUGUI largeText;

    void Start()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy    HH:mm");
        print(time);
        largeText.text = time;
    }
}
