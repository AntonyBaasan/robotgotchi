using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_location_update : MonoBehaviour
{
    private IEnumerator character_move_cool_down;

    private void Awake()
    {
        menu_actions.PlayerLocationUpdate?.Invoke(this.gameObject.transform.position);
    }

    private void Start()
    {
        character_move_cool_down = WaitAndPrint(.1f);
        StartCoroutine(character_move_cool_down);
        
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
            menu_actions.PlayerCanMove?.Invoke();
        }
    }


    // Start is called before the first frame update

}
