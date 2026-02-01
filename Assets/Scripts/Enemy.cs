using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = player.transform.position - transform.position;
        Vector3 dir = d.normalized;
        rb.AddForce(dir * speed);
    }
}
