using System.Collections;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    FishRaycast _fishRaycast;
    [SerializeField] float _speed = 0.013f;

    private void OnEnable()
    {
        _fishRaycast = GetComponent<FishRaycast>();
        if (_fishRaycast != null)
        {
            StartCoroutine(ChangeDirection());
        }
        else
            Debug.LogError("fishRaycast not found in FishMove", gameObject);
    }


    private void FixedUpdate()
    {
        transform.position += transform.right * _speed;

    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {

            var dirChange = _fishRaycast.RaycastDirectionCheckForRotation();
            if (dirChange != 0)
            {
                transform.Rotate(new(0, 0, dirChange));

            }

            var normalizedZRot = normalizeAngle(transform.localEulerAngles.z);

            if (Mathf.Abs( normalizedZRot) < 90)
            {
                if (transform.localScale.y > 0)
                    transform.localScale = new(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            }else if(transform.localScale.y < 0)
            {
                transform.localScale = new(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            }


            yield return new WaitForSeconds(0.2f);
        }

    }

    float normalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f)
            angle -= 360f;
        else if (angle < -180f)
            angle += 360f;
        return angle;
    }
}
