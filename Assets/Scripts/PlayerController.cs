using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform projectileSpawnPos;

    public GameObject projectilePrefab;
    public GameObject superProjectilePrefab;

    private float maxAngle = 90.0f;
    private float angle = 0.0f;

    [SerializeField] private float rotationSpeed = 45.0f;

    [SerializeField] private HealthBar powerupHealthBar;
    private bool havePowerup = false;
    private float powerupLifeTime = 7.5f;
    private float powerupLife = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        havePowerup = true;
        UpdatePowerup();
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot when the corresponding key is pressed
        // ABSTRACTION
        HandleShootInput();
    }

    private void FixedUpdate()
    {
        // Rotate according to player input
        // ABSTRACTION
        HandleMoveInput();

        // Update powerup life time
        UpdatePowerup();
    }

    private void HandleMoveInput()
    {
        float playerInput = Input.GetAxis("Horizontal");
        // Have to use negative input due to wrong rotation direction
        angle -= playerInput * rotationSpeed * Time.deltaTime;
        if (angle > maxAngle)
        {
            angle = maxAngle;
        }
        else if (angle < -maxAngle)
        {
            angle = -maxAngle;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void HandleShootInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = havePowerup ? superProjectilePrefab : projectilePrefab;
        Instantiate(projectile, projectileSpawnPos.position, projectileSpawnPos.rotation);
    }

    public void ActivatePowerup()
    {
        havePowerup = true;
        powerupLife = powerupLifeTime;
        powerupHealthBar.SetHealthPercent(1.0f);
        powerupHealthBar.transform.parent.gameObject.SetActive(true);
    }

    private void UpdatePowerup()
    {
        if (havePowerup)
        {
            powerupLife -= Time.deltaTime;
            if (powerupLife < 0)
            {
                havePowerup = false;
                powerupHealthBar.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                powerupHealthBar.SetHealthPercent(powerupLife / powerupLifeTime);
            }
        }
    }
}
