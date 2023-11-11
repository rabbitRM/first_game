using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public int CollectedCoins = 0;
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    private Camera mainCamera;
    Rigidbody rb;
    GameManager gameManager;

    public int maxHealth = 0;
    int currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentHealth = maxHealth;

        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager object in the scene
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        Vector3 moveDirection = cameraForward.normalized * horizontalInput + cameraRight.normalized * verticalInput;

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            Vector3 damageDirection = other.transform.position - transform.position;
            damageDirection.Normalize();
            rb.AddForce(-damageDirection * 2, ForceMode.Impulse);

            currentHealth--;
            if (currentHealth <= 0)
            {
                gameManager.Restart();
            }

            gameManager.UpdateHealthText(currentHealth, maxHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CollectedCoins++;
            gameManager.UpdateCoinText(CollectedCoins);
            Destroy(other.gameObject);
        }
    }
}