using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trainee : MonoBehaviour {
    public Text debugText;
    public GameObject storeArea;

    private float offset_x;
    private float offset_y;
    private Vector3 nowsize;
    private Vector3 smallscale;
    // Start is called before the first frame update
    void Start(){
        offset_x = 0;
        offset_y = 0;
        nowsize = GetComponent<SpriteRenderer>().bounds.size;
        debugText.text = "";
        smallscale = new Vector3(0.3f,0.3f,1);
    }

    // Update is called once per frame
    void Update(){
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    //debugText.text = "self: "+transform.position.ToString()+ Camera.main.ScreenToWorldPoint(touch.position);
                    offset_x = Camera.main.ScreenToWorldPoint(touch.position).x - transform.position.x;
                    offset_y = Camera.main.ScreenToWorldPoint(touch.position).y - transform.position.y;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    //debugText.text = "size: "+nowsize.ToString();
                    // Determine direction by comparing the current touch position with the initial one
                    if (offset_x < nowsize.x/2 && offset_x > -nowsize.x / 2 && offset_y < nowsize.y/2 && offset_y > -nowsize.y/2)//tapped
                    {
                        //debugText.text ="touch: "+ Camera.main.ScreenToWorldPoint(touch.position)+" "+transform.position.ToString();
                        transform.position=new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x - offset_x, Camera.main.ScreenToWorldPoint(touch.position).y - offset_y, 0);
                        
                    }
                    break;
            }
        }

        if (inArea(transform.position) && transform.localScale.x > smallscale.x)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1);
            nowsize = GetComponent<SpriteRenderer>().bounds.size;
        }
        if (!inArea(transform.position) && transform.localScale.x < 1)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, 1);
            nowsize = GetComponent<SpriteRenderer>().bounds.size;
        }

        if (inArea(transform.position) && Input.touchCount == 0)
        {
            if (!store.Instance().contains(gameObject.name))
                store.Instance().add(gameObject.name);
            transform.position = store.Instance().getWorldPos(gameObject.name);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = store.Instance().getOrder(gameObject.name);
        }
    }

    private bool inArea(Vector3 vec3)
    {
        Vector2 offset;
        offset.x = vec3.x - storeArea.transform.position.x;
        offset.y = vec3.y - storeArea.transform.position.y;
        Vector3 size = storeArea.GetComponent<SpriteRenderer>().bounds.size;
        if (offset.x < size.x / 2 && offset.x > -size.x / 2 && offset.y < size.y / 2 && offset.y > -size.y / 2)
            return true;
        else
            return false;
    }
}
