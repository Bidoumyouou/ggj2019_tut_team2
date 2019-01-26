using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        var direction = Input.mousePosition - screenPos;

        //Debug.Log(direction);
        var angle = GetAim(transform.position, direction);
        //Debug.Log(angle);

        var a = transform.localEulerAngles;
        a.z = angle-90;
        transform.localEulerAngles = a;
        //var angle = GetAim(transform.position, direction);
        //var a = transform.localEulerAngles;
        //a.z = angle - transform.localEulerAngles.z;
        //transform.localEulerAngles = a;
    }

    public float GetAim(Vector2 from, Vector2 to)
        {
            float dx = to.x - from.x;
            float dy = to.y - from.y;
            float rad = Mathf.Atan2(dy, dx);
            return rad * Mathf.Rad2Deg;
        }
    
        //    var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //    var direction = Input.mousePosition - screenPos;

        //    Debug.Log(screenPos);

        //    var angle = GetAim(transform.position, screenPos);
        //    var a = new Vector3(0.0f, 0.0f, angle);
        //    Debug.Log(angle);

        //    transform.localEulerAngles = a;
        //}

        //public float GetAim(Vector3 from, Vector3 to)
        //{
        //    float dx = to.x - from.x;
        //    float dy = to.y - from.y;
        //    float rad = Mathf.Atan2(dx, dy);
        //    return rad * 180 / Mathf.PI;
    }

