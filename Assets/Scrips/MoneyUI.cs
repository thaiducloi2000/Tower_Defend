using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : MonoBehaviour
{
    public Text money;
    
    void Update()
    {
        money.text = "$" + PlayerStats.Money.ToString();
    }
}
