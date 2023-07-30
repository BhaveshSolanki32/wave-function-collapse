using UnityEngine;

public class FishRaycast : MonoBehaviour //raycast and tells fish where to move
{

    public float RaycastDirectionCheckForRotation()
    {
        Debug.DrawRay(transform.position, getDirection(0), Color.red);
        Debug.DrawRay(transform.position, getDirection(45), Color.red);
        Debug.DrawRay(transform.position, getDirection(-45), Color.red);

        RaycastHit2D _hitForward = Physics2D.Raycast(transform.position, getDirection(0), 1.6f);

        if (_hitForward.collider != null)
        {
            RaycastHit2D _hitDown = Physics2D.Raycast(transform.position, getDirection(-45));
            RaycastHit2D _hitUp = Physics2D.Raycast(transform.position, getDirection(45));

            if(_hitUp.collider!=null)
            {
                if (_hitDown.collider != null)
                {
                    if (getDistance(transform.position, _hitDown.collider.transform.position) > getDistance(transform.position, _hitUp.collider.transform.position))
                    {
                        return -45;
                    }
                    else
                    {
                        return 45;
                    }
                }
                else
                {
                  return  45;
                }
            }
            else if(_hitDown.collider != null)
            {
                return -45;
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

    float getDistance(Vector3 _firstPoint, Vector3 _secondPoint)
    {
        return (_secondPoint - _firstPoint).magnitude;
    }

    Vector3 getDirection(float _angle)
    {
        return transform.rotation * Quaternion.Euler(0, 0, _angle) * Vector2.right;
    }

}
