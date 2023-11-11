using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI heathText;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "Coins: 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Returning to the beginning of the current level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateCoinText(int coins)
    {
        coinText.text = "Coins: " + coins.ToString();
    }

    public void UpdateHealthText(int currentHealth, int maxHealth)
    {
        heathText.text = currentHealth + "/" + maxHealth.ToString();
        float newCurrentHealth = (float)currentHealth / maxHealth;
        healthSlider.value = newCurrentHealth;
    }
}