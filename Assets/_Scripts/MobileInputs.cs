using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!hit) {
                return;
            }

            if (hit.collider.gameObject.layer == 7) { // TOWER
                hit.collider.gameObject.GetComponent<Tower>().DoDamanage();
            } else if (hit.collider.gameObject.layer == 8) { // Button
                hit.collider.gameObject.GetComponent<Shake>().ShakeOnClick();
                hit.collider.gameObject.GetComponent<BaseUpgradeButton>().Ugrade();
            }

        }

    }
}
