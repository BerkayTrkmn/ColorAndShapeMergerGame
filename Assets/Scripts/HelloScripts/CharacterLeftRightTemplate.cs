using System.Collections;
using System.Collections.Generic;
using HelloScripts;
using UnityEngine;

public class CharacterLeftRightTemplate : MonoBehaviour
{

    public float leftRestriction = 5f;
    public float rightRestriction = 5f;

    public float playerLeftRightSpeed = 5f;
    public float playerPlatformHeight;

    public CharacterForward forward;

    private void Start()
    {
        TouchManager.Instance.onTouchBegan += OnTouchBegan;
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
        TouchManager.Instance.onTouchEnded += OnTouchEnded;

    }

    private void OnTouchBegan(TouchInput touch)
    {

    }
    private void OnTouchMoved(TouchInput touch)
    {
        PlayerMovement(touch);
        CharacterRestriction();

    }
    private void OnTouchEnded(TouchInput touch)
    {

    }

    public void PlayerMovement(TouchInput touch)
    {

        transform.position += new Vector3(touch.DeltaScreenPosition.x, 0, 0) * playerLeftRightSpeed * Time.deltaTime;
    }

    public void CharacterRestriction()
    {
        Vector3 characterPosition = transform.position;


        float _xMovementClamp = Mathf.Clamp(characterPosition.x, leftRestriction, rightRestriction);
        transform.position = new Vector3(_xMovementClamp, characterPosition.y, characterPosition.z);

    }


    public void CharacterForwardActiveState(bool activity)
    {
        forward.enabled = activity;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
