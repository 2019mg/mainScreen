using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basketball : MonoBehaviour
{
    public GameObject storeArea;
    public GameObject cxk;
    public Text debugText;
    public Text cxkText;
    public AudioSource cxkSound;

    private Vector2 touchOffset;
    private Vector3 nowsize;
    private Vector3 smallscale;
   // private Vector3 bigsize;
    private Vector2 cxkoffset;
    private bool played;
    //private bool stored;
    void Start()
    {
        smallscale = new Vector3(0.7f, 0.7f, 1);
        nowsize = GetComponent<SpriteRenderer>().bounds.size;
        debugText.text = "";
        cxkText.text = "";
        played = false;
        //stored = false;
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                touchOffset.x= Camera.main.ScreenToWorldPoint(touch.position).x - transform.position.x;
                touchOffset.y = Camera.main.ScreenToWorldPoint(touch.position).y - transform.position.y;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if(touchOffset.x<nowsize.x/2&& touchOffset.x > -nowsize.x / 2 && touchOffset.y < nowsize.y / 2 && touchOffset.y > -nowsize.y / 2)
                {//touched the object, need to move it
                    transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x - touchOffset.x, Camera.main.ScreenToWorldPoint(touch.position).y - touchOffset.y, 0);
                }
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

        debugText.text = transform.position.ToString() + '\n' + cxk.transform.position.ToString(); 
        cxkoffset.x = transform.position.x - cxk.transform.position.x;
        cxkoffset.y = transform.position.y - cxk.transform.position.y;
        if (cxkoffset.x < 0)
            cxkoffset.x = -cxkoffset.x;
        if (cxkoffset.y < 0)
            cxkoffset.y = -cxkoffset.y;
        if (cxkoffset.x < 0.25 && cxkoffset.y < 1.45 && cxkoffset.y > 1.05 && (!(transform.localScale.x < 1)) && (!(cxk.transform.localScale.x < 1)))
        {
            cxkText.text = "基尼太美！";
            if (!played)
            {
                cxkSound.Play();
                played = true;
            }
        }
        else
        {
            cxkText.text = "";
            cxkSound.Stop();
            played = false;
        }


        if (inArea(transform.position) && Input.touchCount == 0)
        {
            if (!store.Instance().contains(gameObject.name))
                store.Instance().add(gameObject.name);
            transform.position = store.Instance().getWorldPos(gameObject.name);
            cxkText.text = transform.position.ToString();
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
