using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRooms;     // 추가될 방의 프리팹
    public List<GameObject> currentRooms;   // 현제 추가되어 있는 방 오브젝트들

    float screenWidthInPoints;      // 화면의 가로크기
    const string floor = "Floor";   // 바닥 오브젝트의 이름

    public GameObject[] availableObjects;   // 추가될 오브젝트의 프리팹 배열
    public List<GameObject> CurrentObjects; // 현재 게임에 추가되어있는 오브젝트들(코인, 레이저)
    public float objectsMinDistance = 5.0f; // 오브젝트들 간의 최소 간격
    public float objectsMaxDistance = 10.0f;// 오브젝트들 간의 최대 간격
    public float objectsMinY = -1.4f;       // 오브젝트의 y축 최소 위치
    public float objectsMaxY = 1.4f;        // 오브젝트의 y축 최대 위치
    public float trapY = -2.9f;
    public float objectsMinRotation = -45.0f;   // 오브젝트의 최소 회전값
    public float objectsMaxRotation = 45.0f;    // 오브젝트의 최대 회전값

    public MouseController mc;


    private void Start()
    {   // 화면의 세로크기 계산
        float height = 2.0f * Camera.main.orthographicSize; 
        // 화면의 가로크기 계산 (세로크기 * 화면비율) -> 가로크기를 바로 가져올 수 없음
        screenWidthInPoints = height * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        GenerateRoomIfRequired();
        GanerateObjectsIfRequired();
    }

    private void AddObject(float lastObjectX)
    {
        // 생성할 오브젝트와 위치를 랜덤으로 지정 및 생성
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
        float removeObjectX = playerX - screenWidthInPoints;// 제거 기준 위치
        float addObjectX = playerX + screenWidthInPoints;   // 생성 기준 위치
        float farthestObjectX = 0;  // 가장 앞에 있는 오브젝트 위치

        List<GameObject> objectToRemove = new List<GameObject>();

        foreach (var obj in CurrentObjects)
        {
            float objX = obj.transform.position.x;
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            // 제거 기준 위치보다 뒤에 있으면 제거
            if (objX < removeObjectX 
                || mc.isFever && obj.CompareTag("Obstacle"))
                objectToRemove.Add(obj);
        }

        foreach (var obj in objectToRemove)
        {
            CurrentObjects.Remove(obj);
            Destroy(obj);
        }
        // 가장 앞에 있는 오브젝트가 생성 기준 위치보다 뒤에 있으면 새 오브젝트 생성
        if (farthestObjectX < addObjectX)
            AddObject(farthestObjectX);
    }

    private void AddRoom(float farthestRoomEndX)
    {
        // 전체 방 프리팹중 하나를 랜덤으로 선택
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        // 선택된 방 오브젝트를 생성
        GameObject room = Instantiate(availableRooms[randomRoomIndex]);
        // 방의 가로크기를 구함
        float roomWidth = room.transform.Find(floor).localScale.x;
        // 현재 존재하는 방 가장 오른쪽 끝에서 방크기의 절반만큼 더한 위치를 구함
        float roomCenter = farthestRoomEndX + roomWidth / 2;
        // 구한 위치에 새로운 방 배치
        room.transform.position = new Vector3(roomCenter, 0, 0);
        // 현재 방 목록에 새로운 방 추가
        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        // 제거할 방의 목록 저장
        List<GameObject> roomsToRemove = new List<GameObject>();
        // 이번에 방을 추가 해야하는지 여부
        bool addRooms = true;
        // 플레이어(마우스)의 x축 위치
        float playerX = transform.position.x;
        // 방의 제거하는 기준 x축 위치
        float removeRoomX = playerX - screenWidthInPoints;
        // 방을 추가하는 기준 x축 위치
        float addRoomX = playerX + screenWidthInPoints;
        // 가장 오른쪽에 있는 방의 x축 위치
        float farthestRoomEndX = 0.0f;

        // 현재 게임에 추가되어 있는 방을 하나씩 접근
        foreach (var room in currentRooms)
        {
            // transform.Find() : 자식 오브젝트 중에서 찾기
            // Floor 오브젝트의 가로크기를 이용해서 방의 가로크기 구하기
            float roomWidth = room.transform.Find(floor).localScale.x;
            // 방의 왼쪽 끝 x축 위치
            float roomStartX = room.transform.position.x - roomWidth / 2;
            // 방의 오른쪽 끝 x축 위치
            float roomEndX = roomStartX + roomWidth;

            // 방의 왼쪽 끝 위치가 방을 추가하는 기준 위치보다 오른쪽에 있으면
            if (roomStartX > addRoomX)
                addRooms = false;   // 방을 추가하지 않음

            // 방의 오른쪽 끝 위치가 방을 제거하는 기준 위치보다 왼쪽에 있으면
            if (roomEndX < removeRoomX)
                // 방을 제거할 목록에 추가 (currentRooms에 접근 중인데 도중에 지우면 위험)
                roomsToRemove.Add(room);    

            // 추가된 방들 중 가장 오른쪽에 있는 방의 오른쪽 끝 위치를 저장
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        // 삭제할 방 리스트의 방을 하나씩 접근
        foreach (var room in roomsToRemove)
        {
            // 현제 게임에 추가되어 있는 방 리스트에서 제거
            currentRooms.Remove(room);
            // 실제 오브젝트도 제거
            Destroy(room);
        }
        // 방을 추가해야된다면 가장 오른쪽 끝 위치를 알려주고 방을 생성
        if (addRooms)
            AddRoom(farthestRoomEndX);
    }
}
