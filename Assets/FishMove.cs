using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public float Speed=0.02f;
    FishRaycast fishRaycast;

    private void OnEnable()
    {
        fishRaycast = GetComponent<FishRaycast>();
    }


    private void FixedUpdate()
    {
        transform.position += transform.right * Speed;
        if (fishRaycast != null)
        {
            float _dirChange = fishRaycast.RaycastDirectionCheckForRotation();
            if (_dirChange != 0)
            {
                StopAllCoroutines();
                StartCoroutine(ChangeDirection(_dirChange));
            }
        }
        else
            Debug.LogError("fishRaycast not found in FishMove",gameObject);
        
    }

    IEnumerator ChangeDirection(float _angle)
    {

        transform.Rotate(new(0, 0, _angle));
        yield return new WaitForSeconds(0);

    }
}
