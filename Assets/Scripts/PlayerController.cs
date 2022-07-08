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

    [SerializeField] private float fireRate = 0.25f; // Time between shots
    [SerializeField] private float powerupFireRate = 0.15f;    // Same but for powerup
    private float currentFireRate;
    private float lastShotTime = 0.0f;

    [SerializeField] private HealthBar powerupHealthBar;
    private bool havePowerup = false;
    private float powerupLifeTime = 7.5f;
    private float powerupLife = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        DeactivatePowerup();
        lastShotTime = Time.realtimeSinceStartup - fireRate;
        currentFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameActive)
        {
            return;
        }

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
        if (!GameManager.Instance.IsGameActive)
        {
            return;
        }

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
        if (Input.GetKey(KeyCode.Space) && Time.realtimeSinceStartup - lastShotTime >= currentFireRate)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = havePowerup ? superProjectilePrefab : projectilePrefab;
        Instantiate(projectile, projectileSpawnPos.position, projectileSpawnPos.rotation);
        lastShotTime = Time.realtimeSinceStartup;
    }

    public void ActivatePowerup()
    {
        havePowerup = true;
        powerupLife = powerupLifeTime;
        powerupHealthBar.SetHealthPercent(1.0f);
        powerupHealthBar.transform.parent.gameObject.SetActive(true);
        currentFireRate = powerupFireRate;
    }

    private void DeactivatePowerup()
    {
        havePowerup = false;
        powerupHealthBar.transform.parent.gameObject.SetActive(false);
        currentFireRate = fireRate;
    }

    private void UpdatePowerup()
    {
        if (havePowerup)
        {
            powerupLife -= Time.deltaTime;
            if (powerupLife < 0)
            {
                DeactivatePowerup();
            }
            else
            {
                powerupHealthBar.SetHealthPercent(powerupLife / powerupLifeTime);
            }
        }
    }
}
