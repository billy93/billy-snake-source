using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2 _direction = Vector2.right;
    public Transform body;

    private List<Transform> _bodies;

    // Start is called before the first frame update
    void Start()
    {
        _bodies = new List<Transform>();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            _direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            _direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate(){

        for(int x = _bodies.Count - 1; x > 0; x--){
            _bodies[x].position = _bodies[x-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow(){
        Transform body = Instantiate(this.body);
        body.position = _bodies[_bodies.Count - 1].position;
        _bodies.Add(body);
    }

    private void ResetGame(){
        for(int x = 1; x < _bodies.Count; x++){
            Destroy(_bodies[x].gameObject);
        }
        _bodies.Clear();
        _bodies.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Food"){
            Grow();
        }
        else if(other.tag == "Obstacle"){
            ResetGame();
        }
    }
}
