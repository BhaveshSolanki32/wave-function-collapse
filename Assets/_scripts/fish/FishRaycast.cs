using UnityEngine;

public class FishRaycast : MonoBehaviour //raycast and tells fish where to move
{
    const int _steerAngle=45;

    public float RaycastDirectionCheckForRotation()
    {
#if UNITY_EDITOR
        Debug.DrawRay(transform.position, getDirection(0), Color.red);
        Debug.DrawRay(transform.position, getDirection(_steerAngle), Color.red);
        Debug.DrawRay(transform.position, getDirection(-_steerAngle), Color.red);
#endif
        var dist = 1.6f;
        var hitForward = Physics2D.Raycast(transform.position, getDirection(0), dist);

        if (hitForward.collider != null)
        {
            var hitDown = Physics2D.Raycast(transform.position, getDirection(-_steerAngle));
            var hitUp = Physics2D.Raycast(transform.position, getDirection(_steerAngle));

            if(hitUp.collider!=null)
            {
                if (hitDown.collider != null)
                {
                    if (getDistance(transform.position, hitDown.collider.transform.position) > getDistance(transform.position, hitUp.collider.transform.position))
                    {
                        return -1*_steerAngle;
                    }
                    else
                    {
                        return _steerAngle;
                    }
                }
                else
                {
                  return  _steerAngle;
                }
            }
            else if(hitDown.collider != null)
            {
                return -1*_steerAngle;
            }
            else
            {
                throw new System.Exception();
            }  
            

        }
        else
        {
            return 0;
        }

    }

    float getDistance(Vector3 firstPoint, Vector3 secondPoint)
    {
        return (secondPoint - firstPoint).magnitude;
    }

    Vector3 getDirection(float angle)
    {
        return transform.rotation * Quaternion.Euler(0, 0, angle) * Vector2.right;
    }

}
