using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCharacterController : MonoBehaviour {
    [SerializeField]
    float radius = 0.5f;
    private bool contact;
    private void Update()
    {
        contact = false;
        foreach (Collider col in Physics.OverlapSphere(transform.position,radius))
        {
            //与自身表面碰撞器最接近的点
            Vector3 contactPoint = Vector3.zero;
            if (col is BoxCollider)
            {
                contactPoint = ClosestPointOn((BoxCollider)col,transform.position);
            }
            else if (col is SphereCollider)
            {
                contactPoint = ClosestPointOn((SphereCollider)col,transform.position);
            }
            DebugDraw.DrawMarker(contactPoint,2f,Color.red,0f,false);
            Vector3 v = transform.position - contactPoint;
            transform.position += Vector3.ClampMagnitude(v,Mathf.Clamp(radius-v.magnitude,0,radius));
            contact = true;
        }
    }
    //最近点
    Vector3 ClosestPointOn(SphereCollider collider, Vector3 to)
    {
        Vector3 p;
        p = to - collider.transform.position;
        p.Normalize();
        p *= collider.radius * collider.transform.localScale.x;
        p += collider.transform.position;
        return p;
    }
    Vector3 ClosestPointOn(BoxCollider collider, Vector3 to)
    {
        if (collider.transform.rotation == Quaternion.identity)
        {
            return collider.ClosestPointOnBounds(to);
        }
        return ClosestPointOnOBB(collider,to);
    }
    Vector3 ClosestPointOnOBB(BoxCollider collider, Vector3 to)
    {
        var ct = collider.transform;
        //将世界坐标转换为本地坐标
        var local = ct.InverseTransformPoint(to);
        local -= collider.center;
        //确定自身相对与碰撞对象的位置
        var localNorm = new Vector3(Mathf.Clamp(local.x, -collider.size.x * 0.5f, collider.size.x * 0.5f),
            Mathf.Clamp(local.y,-collider.size.y*0.5f,collider.size.y*0.5f),
            Mathf.Clamp(local.z,-collider.size.z*0.5f,collider.size.z*0.5f)
            );
     //   print(to + ":" + local+":"+localNorm+":"+collider.size);
        localNorm += collider.center;
                 //本地坐标转世界坐标
        return ct.TransformPoint(localNorm);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = contact ? Color.cyan : Color.yellow;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
