using UnityEngine;

public class HarmfulSeed : MonoBehaviour
{
    public GameObject Juice;
    private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0,2) == 0)
            dir = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 120f * Time.deltaTime * dir);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Instantiate(Juice, new Vector3(transform.position.x, transform.position.y, 0.3f), Quaternion.identity);
            other.transform.GetComponent<KiwiPlayer>().Seeded();
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            Destroy(gameObject);
        }
    }
}
