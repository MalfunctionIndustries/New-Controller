using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : RaycastController {
	
	public LayerMask passengerMask;
	public Vector3 move;
	
	public override void Start()
	{
		base.Start();
	}
	
	void Update () {
		UpdateRaycastOrigins();
		
		Vector3 velocity = move * Time.deltaTime;
		
		MovePassengers(velocity);
		transform.Translate(velocity);
	}
	
	void MovePassengers(Vector3 velocity)
	{
		HashSet<Transform> movedPassengers = new HashSet<Transform> ();
		
		float directionX = Mathf.Sign(velocity.x);
		float directionY = Mathf.Sign(velocity.y);
		
		// Vertically moving platform
		if(velocity.y != 0)
		{
			float rayLength = Mathf.Abs(velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i++)
			{
				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;    //if moving down, originate ray from bottom left, otherwise, from top left
				rayOrigin += Vector2.right * (verticalRaySpacing * i);                                          // cast rays from point we WILL be, once we've moved on x axis
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);
				
				
				if(hit) // we have a passenger
				{
					if (!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						
						float pushX = (directionY == 1) ? velocity.x : 0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;
						
						hit.transform.Translate(new Vector3(pushX, pushY));
					}
				}
			}
		}
		
		// Horizontally moving platform
		if (velocity.x != 0)
		{
			float rayLength = Mathf.Abs(velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i++)
			{
				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;    //if moving down, originate ray from bottom left, otherwise, from top left
				rayOrigin += Vector2.right * (verticalRaySpacing * i);                                          // cast rays from point we WILL be, once we've moved on x axis
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);
				
				if (hit) // we have a passenger
				{
					if (!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						float pushY = 0;
						
						hit.transform.Translate(new Vector3(pushX, pushY));
					}
				}
			}
		}
		
		
		// If a passenger is on top of a horizontally or downward moving platform
		if(directionY == -1 || velocity.y == 0 && velocity.x != 0)
		{
			float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i++)
			{
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
				
				if (hit) // we have a passenger
				{
					if (!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						
						float pushX = velocity.x;
						float pushY = velocity.y;
						
						hit.transform.Translate(new Vector3(pushX, pushY));
					}
				}
			}
		}
		
	}
}
