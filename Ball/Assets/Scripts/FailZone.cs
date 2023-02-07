using UnityEngine; 

public class FailZone : MonoBehaviour
{
    // 부딛혔을 때 호출(isTrigger가 체크 되어 있을 때)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {

            // GameObject gmObj = GameObject.Find("GameManager");
            // RestartGame 이라는 메소드를 호출. RestartGame이 아직 구현이 안돼 있더라도 오류x(실행시 오류)
            // gmObj.SendMessage("RestartGame");   //실제로 많이 쓰진 않음

            // GameManager의 컴포넌트 중 GameManager 스크립트를 가져옴
            GameManager gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
            // 호출하려는 메소드가 public 이여야 함.
            gm.RestartGame();  // 이 방법을 많이 씀
        }
    }
}
