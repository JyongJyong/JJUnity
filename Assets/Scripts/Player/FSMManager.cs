using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState  // enum = 열거형
{
    IDLE = 0, // 하나를 지정해주면 다음부터는 그 다음 숫자로 자동적으로 지정해줌 (예) 0, 1, 2, 3, 4 …
    RUN,
    CHASE,
    ATTACK
}

public class FSMManager : MonoBehaviour {

    public PlayerState currentState;
    public PlayerState startStaate;
    public Transform marker;

    // 딕셔너리
    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(PlayerState.IDLE, GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());

        // states[startStaate].enabled = true; // 활성화 , enabled = false => 비활성화;
    }

    public void SetState(PlayerState newState)
    {
        foreach(PlayerFSMState fsm in states.Values)  // Values들을 다 돌려서 비활성화, 기획변경에 유연하게 대처할 수 있음
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
    }

    // Use this for initialization
    void Start () {
        SetState(startStaate);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) // 버튼을 한 번 누를 때 감지, ButtonDown 누를 때 / ButtonUp 땔 때 / Button 누르고 있을 때
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // 탐지를 할 수 있는 광선을 만들었음(탐지는 아직 X) -> 메인카메라로 설정함(Camer.main)
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000)) // 탐지기능O
            {
                marker.position = hit.point;

                SetState(PlayerState.RUN);
            } 


            // Debug.Log("Click" + Input.mousePosition); // "Click"과 마우스 포지션 Console창 출력, 왼쪽 아래(0,0);
        }
	}
}
