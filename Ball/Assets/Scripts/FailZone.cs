using UnityEngine; 

public class FailZone : MonoBehaviour
{
    // �ε����� �� ȣ��(isTrigger�� üũ �Ǿ� ���� ��)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {

            // GameObject gmObj = GameObject.Find("GameManager");
            // RestartGame �̶�� �޼ҵ带 ȣ��. RestartGame�� ���� ������ �ȵ� �ִ��� ����x(����� ����)
            // gmObj.SendMessage("RestartGame");   //������ ���� ���� ����

            // GameManager�� ������Ʈ �� GameManager ��ũ��Ʈ�� ������
            GameManager gm = GameObject.Find("GameManager")
                .GetComponent<GameManager>();
            // ȣ���Ϸ��� �޼ҵ尡 public �̿��� ��.
            gm.RestartGame();  // �� ����� ���� ��
        }
    }
}
