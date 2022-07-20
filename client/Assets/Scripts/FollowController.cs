using UnityEngine;

public class FollowController : MonoBehaviour
{
    public float Speed = 5;
    public Vector3 Offset = new Vector3(0,15,0);
    public Transform Target;

    private void FixedUpdate()
    {
        if (!Target) return;
        
        var position = Target.position + Offset;
        transform.position = Vector3.Lerp(transform.position, position, Speed * Time.deltaTime);
    }
}