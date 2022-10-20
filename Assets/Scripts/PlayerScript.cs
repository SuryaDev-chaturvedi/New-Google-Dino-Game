using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Text currentScore;
    public Text highScore;
    int Score = 0;

    public Animator anim;
    public float jumpPower = 100f;
    Rigidbody2D myRigidbody;
    bool isGround = false;
    bool isGameOver = false;
    public float xPos = -11f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            Score++;
            currentScore.text = Score.ToString();
        }

        if(Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Score);
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();

        }

        if (isGameOver) return;

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            myRigidbody.AddForce(Vector3.up * myRigidbody.gravityScale * myRigidbody.mass * jumpPower * Time.deltaTime);
            anim.SetBool("Jump", true);
        }
        if (Input.GetKey(KeyCode.DownArrow) && isGround)
        {
            myRigidbody.AddForce(Vector3.down * myRigidbody.gravityScale * myRigidbody.mass * jumpPower * Time.deltaTime);
            anim.SetBool("MoveDown", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("MoveDown", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGround = true;
            anim.SetBool("Jump", false);
        }
        if (other.collider.tag == "Challenges")
        {
            GameOver();
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGround = true;
            anim.SetBool("Jump", false);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGround = false;
            anim.SetBool("Jump", true);
        }
    }

    public void GameOver()
    {
        myRigidbody.gravityScale = 0f;
        isGameOver = true;
        FindObjectOfType<ChallengeScroller>().GameOver();
        FindObjectOfType<Scroll>().xVel = 0f;
        FindObjectOfType<ScrollClouds>().xVel = 0f;
        anim.SetBool("GameOver", true);

        FindObjectOfType<Score>().GameOver();
    }
}
