using UnityEngine;

public class Control_Camera : MonoBehaviour
{
	//�������
	public float m_DistanceAway = 5f;
	//����߶�
	public float m_DistanceHeight = 4f;
	//ƽ��ֵ
	public float smooth = 2f;
	//Ŀ���
	private Vector3 m_TargetPosition;
	//���յ�
	Transform m_Follow;

	void Start()
	{
		m_Follow = GameObject.Find("Player").transform;
	}

	void LateUpdate()
	{
		m_TargetPosition = m_Follow.position + Vector3.up * m_DistanceHeight - m_Follow.forward * m_DistanceAway;
		transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
	}
}

