using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector2 playerChange;
    private CameraMovement cameraMove;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    void Start()
    {
        cameraMove = Camera.main.GetComponent<CameraMovement>();
    }

    void Update(){

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            cameraMove.minPosition += cameraChange;
            cameraMove.maxPosition += cameraChange;
            other.transform.position += (Vector3) playerChange;
            if(needText){
               StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo(){
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
