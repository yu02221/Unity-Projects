using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text scoreText;

    private Rigidbody rb;
    private int count;
    private int score;
    private int pickUpLength;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        score = 0;
        SetText();

        winText.text = "";

        GameObject[] PickUp1s = GameObject.FindGameObjectsWithTag("PickUp1");
        GameObject[] PickUp2s = GameObject.FindGameObjectsWithTag("PickUp2");
        GameObject[] PickUp3s = GameObject.FindGameObjectsWithTag("PickUp3");
        pickUpLength = PickUp1s.Length + PickUp2s.Length + PickUp3s.Length;
    }

    // fixed update 는 최대한 동일한 간격으로 호출되도록 유지
    // 일반적으로 물리적 연산 같은 경우(addforce 등) fixedUpdate로 사용
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "PickUp1":
                score += 10;
                break;
            case "PickUp2":
                score += 20;
                break;
            case "PickUp3":
                score += 30;
                break;
            default:
                break;
        }
        if (other.gameObject.tag.Contains("PickUp"))
        {
            count++;
            SetText();

            // other.gameObject.SetActive(false); // 비활성화
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && count < pickUpLength)
        {
            score -= 10;
            SetText();
        }
    }

    void SetText()
    {
        countText.text = "Count : " + count;
        scoreText.text = "Score : " + score;

        if (count >= pickUpLength)
        {
            countText.text = "";
            scoreText.text = "";
            winText.text = "You Win!\nScore : " + score;
        }
    }
}
