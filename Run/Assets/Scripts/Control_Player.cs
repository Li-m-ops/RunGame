using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_Player : MonoBehaviour
{
    //�������ƶ���
    Control_Scenes m_ControlScenes;
    //ǰ���ٶ�
    public float m_ForwardSpeeed = 7.0f;
    //�������
    private Animator m_Anim;
    //��������״̬
    private AnimatorStateInfo m_CurrentBaseState;

    //����״̬����
    static int m_jumpState = Animator.StringToHash("Base Layer.jump");
    static int m_slideState = Animator.StringToHash("Base Layer.slide");

    //��Ϸ����
    bool m_IsEnd = false;

    void Start()
    {
        //����������ϵ�Animator���
        m_Anim = GetComponent<Animator>();
        m_ControlScenes = GameObject.Find("Scripts").GetComponent<Control_Scenes>();
    }

    void Update()
    {
        //������ǰ��
        transform.position = new Vector3(transform.position.x + 1 * m_ForwardSpeeed * Time.deltaTime, -0.2f, transform.position.z);

        //������ڲ��ŵĶ���״̬
        m_CurrentBaseState = m_Anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Anim.SetBool("jump", true);//�л���״̬
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_Anim.SetBool("slide", true);//�л��»�״̬
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Change_PlayerZ(true);//�ƶ����������ƶ�
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Change_PlayerZ(false);//�ƶ����������ƶ�
        }
        if (m_CurrentBaseState.fullPathHash == m_jumpState)
        {
            m_Anim.SetBool("jump", false);//�ı䶯��״̬
        }
        else if (m_CurrentBaseState.fullPathHash == m_slideState)
        {
            m_Anim.SetBool("slide", false);//�ı䶯��״̬
        }
        if (m_IsEnd && Input.GetKeyDown(KeyCode.F1))
        {
            //���¿�ʼ��Ϸ
            SceneManager.LoadScene(0);
        }
    }

    public void Change_PlayerZ(bool IsAD)
    {
        if (IsAD)//�����¼��̵�A Ҳ���������ƶ�
        {
            //�����������
            if (transform.position.z == -5f)
                return;
            //�������м�
            else if (transform.position.z == -10f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
            }
            //���������ұ�
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            }
        }
        else//�����¼��̵�D Ҳ���������ƶ�
        {
            //���������ұ�
            if (transform.position.z == -15f)
                return;
            //�������м�
            else if (transform.position.z == -10f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -15f);
            }
            //�����������
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            }
        }
    }

    void OnGUI()
    {
        if (m_IsEnd)
        {
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "������~���F1���¿�ʼ��Ϸ", style);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //���������ײ���ϰ���
        if (other.gameObject.name == "Vehicle_DumpTruck" || other.gameObject.name == "Vehicle_MixerTruck")
        {
            m_IsEnd = true;
            m_ForwardSpeeed = 0;
            m_Anim.SetBool("idle", true);
        }
        if (other.gameObject.name == "MonitorPos0")
        {
            m_ControlScenes.Change_Road(0);
        }
        else if (other.gameObject.name == "MonitorPos1")
        {
            m_ControlScenes.Change_Road(1);
        }
        else if (other.gameObject.name == "MonitorPos2")
        {
            m_ControlScenes.Change_Road(2);
        }
    }

}