using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelwin;
    [SerializeField] private GameObject _panellose;

    public bool IsOver { get; private set; }
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public TrailController trailController;
    public BoxCollider2D TapCollider;

    private bool _isGameEnded = false;

    void Start()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (_isGameEnded)
        {
            return;
        }
        Birds.RemoveAt(0);

        if(Birds.Count > 0)
            SlingShooter.InitiateBird(Birds[0]);
    }

    public void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }
        if(Enemies.Count == 0)
        {
            _isGameEnded = true;
            _panelwin.gameObject.SetActive (true);
        }
        if(Birds.Count == 0){
            _isGameEnded = true;
            _panellose.gameObject.SetActive (true);
        };
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.R))

        {

            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

        }
    }
}
