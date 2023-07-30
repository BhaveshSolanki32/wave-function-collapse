using System.Collections;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public float Speed = 0.013f;
    FishRaycast fishRaycast;

    private void OnEnable()
    {
        fishRaycast = GetComponent<FishRaycast>();
        if (fishRaycast != null)
        {
            StartCoroutine(ChangeDirection());
        }
        else
            Debug.LogError("fishRaycast not found in FishMove", gameObject);
    }


    private void FixedUpdate()
    {
        transform.position += transform.right * Speed;

    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {

            float _dirChange = fishRaycast.RaycastDirectionCheckForRotation();
            if (_dirChange != 0)
            {
                transform.Rotate(new(0, 0, _dirChange));

            }

            float _normalizedZRot = normalizeAngle(transform.localEulerAngles.z);

            if (Mathf.Abs( _normalizedZRot) < 90)
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

    float normalizeAngle(float _angle)
    {
        _angle %= 360f;
        if (_angle > 180f)
            _angle -= 360f;
        else if (_angle < -180f)
            _angle += 360f;
        return _angle;
    }
}
