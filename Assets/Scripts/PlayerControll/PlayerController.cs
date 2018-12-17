using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    const float skinWidth = .015f;//the skin width

    public int horizontalRayCount = 2;
    public int verticalRayCount = 1;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    public LayerMask collisionMask;
    public CollisionIngo collisions;
    BoxCollider collider;//the skin of player's collider 
    RaycastOrigings raycastOrigings;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        CalculateRaySpacing();
    }
    void VerticalCollision(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigings.BehindBottomLeft : raycastOrigings.BehindTopLeft ;
            Ray ray = new Ray(rayOrigin, Vector3.up * directionY);
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, Vector3.up * directionY, rayLength, collisionMask))
            {
                //velocity.y = (hit.distance - skinWidth) * directionY;
                collisions.above = directionY ==  1;
                collisions.below = directionY == -1;
                Debug.DrawRay(rayOrigin + Vector3.right * verticalRaySpacing * i, Vector3.up * rayLength * directionY, Color.red);
            }

        }

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigings.InFrontBottomLeft : raycastOrigings.InFrontTopLeft;
            if (Physics.Raycast(rayOrigin, Vector3.up * directionY, rayLength, collisionMask))
            {
                collisions.above = directionY ==  1;
                collisions.below = directionY == -1;
                Debug.DrawRay(rayOrigin + Vector3.right * verticalRaySpacing * i, Vector3.up * rayLength * directionY, Color.red);
            }
           
        }

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigings.BehindBottomLeft : raycastOrigings.BehindTopLeft;
            if (Physics.Raycast(rayOrigin, Vector3.up * directionY, rayLength, collisionMask))
            {
                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
                Debug.DrawRay(rayOrigin + Vector3.forward * verticalRaySpacing * i, Vector3.up * rayLength * directionY, Color.red);
            }

        }

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigings.BehindBottomRight : raycastOrigings.BehindTopRight;
            if (Physics.Raycast(rayOrigin, Vector3.up * directionY, rayLength, collisionMask))
            {
                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
                Debug.DrawRay(rayOrigin + Vector3.forward * verticalRaySpacing * i, Vector3.up * rayLength * directionY, Color.red);
            }
        }
    }

    public void Move(ref Vector3 velocity)
    {
        UpdateRaycastOrigins();

        if(velocity.y != 0)
            VerticalCollision(ref velocity);

        //transform.Translate(velocity);
    }

    //計算每個雷射的間隔
    void CalculateRaySpacing()
    {
        
        Bounds bounds = collider.bounds;//邊界參數
        bounds.Expand(skinWidth * -2);//正的代表放大，負的代表縮小

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        //邊界的間隔 = 邊界的y大小 / (水平雷射數量 - 1)   ex: | | | 中間只有兩個間隔
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    //即時更新邊界的每個點
    void UpdateRaycastOrigins()
    {
        
        Bounds bounds = collider.bounds;//邊界參數
        bounds.Expand(skinWidth * -2);//正的代表放大，負的代表縮小

        //set the bound edge

        //behind bottom edge
        raycastOrigings.BehindBottomLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        raycastOrigings.BehindBottomRight = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);

        //behind top edge
        raycastOrigings.BehindTopLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        raycastOrigings.BehindTopRight = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);

        //Infront bottom edge
        raycastOrigings.InFrontBottomLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        raycastOrigings.InFrontBottomRight = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);

        //behind top edge
        raycastOrigings.InFrontTopLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        raycastOrigings.InFrontTopRight = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
    }

    //邊界的頂點數據結構
    public struct RaycastOrigings
    {
        public Vector3 InFrontTopRight, InFrontTopLeft,BehindTopRight,BehindTopLeft;
        public Vector3 InFrontBottomRight, InFrontBottomLeft,BehindBottomRight, BehindBottomLeft;
    }

    public struct CollisionIngo
    {
        public bool below, above;
        public void reset()
        {
            below = above = false;
        }
    }
}
