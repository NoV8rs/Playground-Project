using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RPG_Elements : MonoBehaviour
{
    [Header("Currency")]
    public int currentMoney = 0;
    private int moneyCap = 999999;
    public int currentDiamonds = 0;
    private int diamondCap = 999999;
    
    [Header("Currency Text")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI diamondText;
    
    private void Start()
    {
        UpdateCurrencyText();
    }
    
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        if (currentMoney > moneyCap)
        {
            currentMoney = moneyCap;
        }
        UpdateCurrencyText();
    }
    
    public void AddDiamonds(int amount)
    {
        currentDiamonds += amount;
        if (currentDiamonds > diamondCap)
        {
            currentDiamonds = diamondCap;
        }
        UpdateCurrencyText();
    }
    
    private void UpdateCurrencyText()
    {
        moneyText.text = ("Money :" + currentMoney.ToString());
        diamondText.text = ("Diamonds :" + currentDiamonds.ToString());
    }
    
}
