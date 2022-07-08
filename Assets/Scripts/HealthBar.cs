using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        healthBar = GetComponent<Image>();
    }

    public void SetHealthPercent(float precentage)
    {
        healthBar.fillAmount = precentage;
    }
}
