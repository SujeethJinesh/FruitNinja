using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        fruitCollider.enabled = false;
        whole.SetActive(false);
        sliced.SetActive(true);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle); // rotate to where we sliced the fruit at

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
