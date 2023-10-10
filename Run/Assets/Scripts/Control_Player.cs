using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_Player : MonoBehaviour
{
    //场景控制对象
    Control_Scenes m_ControlScenes;
    //前进速度
    public float m_ForwardSpeeed = 7.0f;
    //动画组件
    private Animator m_Anim;
    //动画现在状态
    private AnimatorStateInfo m_CurrentBaseState;

    //动画状态参照
    static int m_jumpState = Animator.StringToHash("Base Layer.jump");
    static int m_slideState = Animator.StringToHash("Base Layer.slide");

    //游戏结束
    bool m_IsEnd = false;

    void Start()
    {
        //获得主角身上的Animator组件
        m_Anim = GetComponent<Animator>();
        m_ControlScenes = GameObject.Find("Scripts").GetComponent<Control_Scenes>();
    }

    void Update()
    {
        //主角向前跑
        transform.position = new Vector3(transform.position.x + 1 * m_ForwardSpeeed * Time.deltaTime, -0.2f, transform.position.z);

        //获得现在播放的动画状态
        m_CurrentBaseState = m_Anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Anim.SetBool("jump", true);//切换跳状态
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_Anim.SetBool("slide", true);//切换下滑状态
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Change_PlayerZ(true);//移动主角向左移动
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Change_PlayerZ(false);//移动主角向右移动
        }
        if (m_CurrentBaseState.fullPathHash == m_jumpState)
        {
            m_Anim.SetBool("jump", false);//改变动画状态
        }
        else if (m_CurrentBaseState.fullPathHash == m_slideState)
        {
            m_Anim.SetBool("slide", false);//改变动画状态
        }
        if (m_IsEnd && Input.GetKeyDown(KeyCode.F1))
        {
            //重新开始游戏
            SceneManager.LoadScene(0);
        }
    }

    public void Change_PlayerZ(bool IsAD)
    {
        if (IsAD)//当按下键盘的A 也就是向左移动
        {
            //主角在最左边
            if (transform.position.z == -5f)
                return;
            //主角在中间
            else if (transform.position.z == -10f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
            }
            //主角在最右边
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            }
        }
        else//当按下键盘的D 也就是向右移动
        {
            //主角在最右边
            if (transform.position.z == -15f)
                return;
            //主角在中间
            else if (transform.position.z == -10f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -15f);
            }
            //主角在最左边
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
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "你输了~点击F1重新开始游戏", style);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //主角如果碰撞到障碍物
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