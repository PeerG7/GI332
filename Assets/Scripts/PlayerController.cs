using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public GameObject focalpoint;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    private bool hasPowerUp = false;

    private Coroutine smashRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        float v = moveAction.ReadValue<Vector2>().y;
        rb.AddForce(v * speed * focalpoint.transform.forward);
        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (smashAction.triggered && hasPowerUp)
        {
            smashRoutine = StartCoroutine(SmashRoutine());
        }
    }

    /*IEnumerator UpdateRoutine()
    {
        while (true)
        {
            float v = moveAction.ReadValue<Vector2>().y;
            rb.AddForce(v * speed * focalpoint.transform.forward);
            if (breakAction.IsPressed())
            {
                rb.linearVelocity = Vector3.zero;
            }
            yield return null;
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCooldown(60f));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            var d = other.transform.position - transform.position;
            var dir = d.normalized;
            var enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(dir * 10f, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        hasPowerUp = false;
        if (smashRoutine != null)
        {
            StopCoroutine(smashRoutine);
        }
    }

    IEnumerator SmashRoutine()
    {
        float chargingTime = 0f;

        while (smashAction.IsPressed())
        {
            chargingTime += Time.deltaTime;
            if (chargingTime > 2.0f)
            {
                break;
            }
            yield return null;
        }

        if (chargingTime < 2.0f)
        {
            yield break;
        }

        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemyrb = enemies[i].GetComponent<Rigidbody>();
            enemyrb.AddForce(Vector3.up * 15f, ForceMode.Impulse);

        }
    }
}
