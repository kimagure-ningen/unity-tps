using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    private float maxHealth = 100f;
    public float currentHealth;

    [Header("Hunger")]
    private float maxHunger = 100f;
    private float hungerDepletionRate = 1f;
    private float hungerDeficiencyDamage = 2f;
    public float currentHunger;

    [Header("Oxygen")]
    private float maxOxygen = 100f;
    private float oxygenDepletionRate = 5f;
    private float oxygenDeficiencyDamage = 10f;
    public float currentOxygen;

    [Header("Shooting Mode")]
    public CinemachineVirtualCamera vCam2;
    private int defaultPriority;
    public bool isShootingMode = false;
    public GameObject crosshair;

    private void Start() {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentOxygen = maxOxygen;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        defaultPriority = vCam2.Priority;
    }

    private void Update() {
        currentHunger -= hungerDepletionRate * Time.deltaTime;
        currentOxygen -= oxygenDepletionRate * Time.deltaTime;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnPlayerDeath();
        }

        if (currentHunger <= 0) {
            currentHunger = 0;
            Damage(hungerDeficiencyDamage * Time.deltaTime);
        }

        if (currentOxygen <= 0) {
            currentOxygen = 0;
            Damage(oxygenDeficiencyDamage * Time.deltaTime);
        }

        SwitchCamera();
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player Died");
        // Time.timeScale = 0;
    }

    private void SwitchCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (vCam2.Priority == 20)
            {
                vCam2.Priority = defaultPriority;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(false);
                isShootingMode = false;
            }
            else
            {
                vCam2.Priority = 20;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                crosshair.SetActive(true);
                isShootingMode = true;
            }
        }
    }

    public void ReplenishHunger(float hungerAmount) {
        currentHunger += hungerAmount;

        if (currentHunger > maxHunger){
            currentHunger = maxHunger;
        }
    }

    public void ReplenishOxygen(float oxygenAmount) {
        currentOxygen += oxygenAmount;

        if (currentOxygen > maxOxygen) {
            currentOxygen = maxOxygen;
        }
    }
}
