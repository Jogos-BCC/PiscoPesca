using UnityEngine;

// creditos: https://www.youtube.com/watch?v=f473C43s8nE&
public class MoveCam : MonoBehaviour
{

    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
