using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;     // �߰��� ���� ������
    public List<GameObject> currentRooms;   // ���� �߰��Ǿ� �ִ� �� ������Ʈ��

    float screenWidthInPoints;      // ȭ���� ����ũ��
    const string floor = "Floor";   // �ٴ� ������Ʈ�� �̸�

    public GameObject[] availableObjects;   // �߰��� ������Ʈ�� ������ �迭
    public List<GameObject> CurrentObjects; // ���� ���ӿ� �߰��Ǿ��ִ� ������Ʈ��(����, ������)
    public float objectsMinDistance = 5.0f; // ������Ʈ�� ���� �ּ� ����
    public float objectsMaxDistance = 10.0f;// ������Ʈ�� ���� �ִ� ����
    public float objectsMinY = -1.4f;       // ������Ʈ�� y�� �ּ� ��ġ
    public float objectsMaxY = 1.4f;        // ������Ʈ�� y�� �ִ� ��ġ
    public float trapY = -2.9f;
    public float objectsMinRotation = -45.0f;   // ������Ʈ�� �ּ� ȸ����
    public float objectsMaxRotation = 45.0f;    // ������Ʈ�� �ִ� ȸ����

    public MouseController mc;


    private void Start()
    {   // ȭ���� ����ũ�� ���
        float height = 2.0f * Camera.main.orthographicSize; 
        // ȭ���� ����ũ�� ��� (����ũ�� * ȭ�����) -> ����ũ�⸦ �ٷ� ������ �� ����
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateRoomIfRequired();
        GanerateObjectsIfRequired();
    }

    private void AddObject(float lastObjectX)
    {
        // ������ ������Ʈ�� ��ġ�� �������� ���� �� ����
        int randonIndex = Random.Range(0, availableObjects.Length);
        if (mc.isFever)
            randonIndex = Random.Range(0, 3);
        GameObject obj = Instantiate(availableObjects[randonIndex]);

        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
        if (randonIndex == availableObjects.Length - 1)
        {
            randomY = trapY;
            rotation = 1;
        }

        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        CurrentObjects.Add(obj);
    }

    private void GanerateObjectsIfRequired()
    {
        float playerX = transform.position.x;
        float removeObjectX = playerX - screenWidthInPoints;// ���� ���� ��ġ
        float addObjectX = playerX + screenWidthInPoints;   // ���� ���� ��ġ
        float farthestObjectX = 0;  // ���� �տ� �ִ� ������Ʈ ��ġ

        List<GameObject> objectToRemove = new List<GameObject>();

        foreach (var obj in CurrentObjects)
        {
            float objX = obj.transform.position.x;
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            // ���� ���� ��ġ���� �ڿ� ������ ����
            if (objX < removeObjectX 
                || mc.isFever && obj.CompareTag("Obstacle"))
                objectToRemove.Add(obj);
        }

        foreach (var obj in objectToRemove)
        {
            CurrentObjects.Remove(obj);
            Destroy(obj);
        }
        // ���� �տ� �ִ� ������Ʈ�� ���� ���� ��ġ���� �ڿ� ������ �� ������Ʈ ����
        if (farthestObjectX < addObjectX)
            AddObject(farthestObjectX);
    }

    private void AddRoom(float farthestRoomEndX)
    {
        // ��ü �� �������� �ϳ��� �������� ����
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        // ���õ� �� ������Ʈ�� ����
        GameObject room = Instantiate(availableRooms[randomRoomIndex]);
        // ���� ����ũ�⸦ ����
        float roomWidth = room.transform.Find(floor).localScale.x;
        // ���� �����ϴ� �� ���� ������ ������ ��ũ���� ���ݸ�ŭ ���� ��ġ�� ����
        float roomCenter = farthestRoomEndX + roomWidth / 2;
        // ���� ��ġ�� ���ο� �� ��ġ
        room.transform.position = new Vector3(roomCenter, 0, 0);
        // ���� �� ��Ͽ� ���ο� �� �߰�
        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        // ������ ���� ��� ����
        List<GameObject> roomsToRemove = new List<GameObject>();
        // �̹��� ���� �߰� �ؾ��ϴ��� ����
        bool addRooms = true;
        // �÷��̾�(���콺)�� x�� ��ġ
        float playerX = transform.position.x;
        // ���� �����ϴ� ���� x�� ��ġ
        float removeRoomX = playerX - screenWidthInPoints;
        // ���� �߰��ϴ� ���� x�� ��ġ
        float addRoomX = playerX + screenWidthInPoints;
        // ���� �����ʿ� �ִ� ���� x�� ��ġ
        float farthestRoomEndX = 0.0f;

        // ���� ���ӿ� �߰��Ǿ� �ִ� ���� �ϳ��� ����
        foreach (var room in currentRooms)
        {
            // transform.Find() : �ڽ� ������Ʈ �߿��� ã��
            // Floor ������Ʈ�� ����ũ�⸦ �̿��ؼ� ���� ����ũ�� ���ϱ�
            float roomWidth = room.transform.Find(floor).localScale.x;
            // ���� ���� �� x�� ��ġ
            float roomStartX = room.transform.position.x - roomWidth / 2;
            // ���� ������ �� x�� ��ġ
            float roomEndX = roomStartX + roomWidth;

            // ���� ���� �� ��ġ�� ���� �߰��ϴ� ���� ��ġ���� �����ʿ� ������
            if (roomStartX > addRoomX)
                addRooms = false;   // ���� �߰����� ����

            // ���� ������ �� ��ġ�� ���� �����ϴ� ���� ��ġ���� ���ʿ� ������
            if (roomEndX < removeRoomX)
                // ���� ������ ��Ͽ� �߰� (currentRooms�� ���� ���ε� ���߿� ����� ����)
                roomsToRemove.Add(room);    

            // �߰��� ��� �� ���� �����ʿ� �ִ� ���� ������ �� ��ġ�� ����
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        // ������ �� ����Ʈ�� ���� �ϳ��� ����
        foreach (var room in roomsToRemove)
        {
            // ���� ���ӿ� �߰��Ǿ� �ִ� �� ����Ʈ���� ����
            currentRooms.Remove(room);
            // ���� ������Ʈ�� ����
            Destroy(room);
        }
        // ���� �߰��ؾߵȴٸ� ���� ������ �� ��ġ�� �˷��ְ� ���� ����
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }
}
