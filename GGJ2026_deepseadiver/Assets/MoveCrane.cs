using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        //Vector3 craneMovementDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //craneMovementDirection = Vector3.ClampMagnitude(craneMovementDirection, 1);
        //transform.Translate(craneMovementDirection * speed * Time.deltaTime);
    }
}
