using System.Collections.Generic;
using UnityEngine;

public class Control_Scenes : MonoBehaviour
{
    //�Ƿ񵽴��һ��·
    bool m_ISFirst;
    //���е�·�ζ���
    public GameObject[] m_RoadArray;
    //�ϰ����������
    public GameObject[] m_ObstacleArray;
    //�ϰ����λ��
    public List<Transform> m_ObstaclePosArray = new List<Transform>();

    void Start()
    {
        m_ISFirst = true;
        //��Ϸ��ʼ�Զ�����3���ϰ���
        for (int i = 0; i < 3; i++)
        {
            Spawn_Obstacle(i);
        }
    }

    //���ݴ��ݵĲ����������л�����·��
    public void Change_Road(int index)
    {
        //�����һ��·�β��л�
        if (m_ISFirst && index == 0)
        {
            m_ISFirst = false;
            return;
        }
        else
        {
            int lastIndex = index - 1;
            if (lastIndex < 0)
                lastIndex = 2;
            //��ǰ�����·�ε���һ��·�ν����л���Ҳ����˵�ﵽ�ڶ���·�Σ�����һ��·�ν����л�
            m_RoadArray[lastIndex].transform.position = m_RoadArray[lastIndex].transform.position + new Vector3(135, 0, 0);
            Spawn_Obstacle(lastIndex);
        }
    }

    public void Spawn_Obstacle(int index)
    {
        //����ԭ���Ķ���
        GameObject[] obsPast = GameObject.FindGameObjectsWithTag("Obstacle" + index);
        for (int i = 0; i < obsPast.Length; i++)
        {
            Destroy(obsPast[i]);
        }
        //�����ϰ���
        foreach (Transform item in m_ObstaclePosArray[index])
        {
            GameObject prefab = m_ObstacleArray[Random.Range(0, m_ObstacleArray.Length)];
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            GameObject obj = Instantiate(prefab, item.position, Quaternion.Euler(eulerAngle));
            obj.transform.parent = GameObject.Find("Instantiate").transform;//��ʵ����������ŵ�������֮��
            obj.tag = "Obstacle" + index;
            obj.SetActive(true);
        }
    }
}
