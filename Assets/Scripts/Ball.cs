using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private int score;

    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject lvl, next, menu, restart, gameover;
    [SerializeField] private int gelenlvl;
    public AudioClip patlama;
    AudioSource aSource;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        aSource =GetComponent<AudioSource>();
        Time.timeScale = 1f;
        var random = Random.value > .5f ? 1 : -1;
        var force = new Vector2(random, -1);
        _rigidbody2D.AddForce(force.normalized * speed);
        score= 0;
        next.SetActive(false);
        menu.SetActive(false);
        lvl.SetActive(false);
        gameover.SetActive(false);
        restart.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (score >= 24)
        {
            Time.timeScale = 0f;
            next.SetActive(true);
            menu.SetActive(true);
            lvl.SetActive(true);


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Breakable"))
        {
            aSource.PlayOneShot(patlama);
            collision.gameObject.SetActive(false);
            score++;
        }
        if (collision.transform.CompareTag("dead"))
        {
            Time.timeScale= 0f;
            restart.SetActive(true);
            menu.SetActive(true);
            gameover.SetActive(true);

        }
    }

    public void nextgame()
    {
        SceneManager.LoadScene(gelenlvl + 1);
    }
    public void menuu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resetgame()
    {
        SceneManager.LoadScene(gelenlvl);
    }


}