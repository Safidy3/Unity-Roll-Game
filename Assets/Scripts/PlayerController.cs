using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private Rigidbody rb;
	private int count;
	private float movementX;
	private float movementY;
	public float speed = 5;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	void SetCountText() 
	{
		countText.text =  "Count: " + count.ToString();
	}

	void Start()
	{
		count = 0;
		rb = GetComponent <Rigidbody>(); 
		winTextObject.SetActive(false);
		SetCountText();
	}

	void OnMove (InputValue movementValue)
	{
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x; 
		movementY = movementVector.y; 
	}

	private void FixedUpdate() 
	{
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			count++;
			SetCountText();
			if (count >= 6)
			{
				winTextObject.SetActive(true);
				Destroy(GameObject.FindGameObjectWithTag("Enemy"));
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Destroy the current object
			Destroy(gameObject);
			// Update the winText to display "You Lose!"
			winTextObject.gameObject.SetActive(true);
			winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
		}
	}
}
