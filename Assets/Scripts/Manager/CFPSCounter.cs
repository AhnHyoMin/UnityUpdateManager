using UnityEngine;
using System.Collections;

public class CFPSCounter : MonoBehaviour
{
    public Rect m_rtStartRect;
    public bool m_bUpdateColor = true;
    public bool m_bAllowDrag = true;
    public float m_fFrequency = 0.5f;
    public int m_bDecimal = 1;

    private float m_fAccum = 0.0f;
    private int m_nFrames = 0;
    private Color m_pColor = Color.white;
    private string m_strFPS = "";
    private GUIStyle m_pStyle;

    public bool m_UseUpdateEx = true;

    void Start()
    {
        m_rtStartRect = new Rect(Screen.width / 2 - 37f, 10f, 75f, 50f);
    }
    void Update()
    {
        if (m_UseUpdateEx)
            return;

        m_fAccum += Time.timeScale / Time.deltaTime;
        ++m_nFrames;
    }

    public void UpdateEx()
    {
        if (m_UseUpdateEx == false)
            return;

        m_fAccum += Time.timeScale / Time.deltaTime;
        ++m_nFrames;
    }

    // 이 컴포넌트가 있는 객체가 활성화 / 비활성화될 때 갱신할 수 있게 하기 위해, Start() 대신 OnEnable() / OnDisable()에서 코루틴을 조정한다.
    void OnEnable()
    {
        StartCoroutine(FPS());
    }

    void OnDisable()
    {
        StopCoroutine(FPS());
    }

    IEnumerator FPS()
    {
        while (true)
        {
            float fps = 0.0f;
            if (m_nFrames != 0.0f)
                fps = m_fAccum / m_nFrames;
            m_strFPS = fps.ToString("f" + Mathf.Clamp(m_bDecimal, 0, 10));

            //Update the color
            m_pColor = (fps >= 30) ? Color.green : ((fps > 10) ? Color.yellow : Color.red);

            m_fAccum = 0.0F;
            m_nFrames = 0;

            yield return new WaitForSeconds(m_fFrequency);
        }
    }

    void OnGUI()
    {
        if (m_pStyle == null)
        {
            m_pStyle = new GUIStyle(GUI.skin.label);
            m_pStyle.normal.textColor = Color.white;
            m_pStyle.alignment = TextAnchor.MiddleCenter;
        }

        GUI.color = m_bUpdateColor ? m_pColor : Color.white;
        m_rtStartRect = GUI.Window(0, m_rtStartRect, DoMyWindow, "");
    }

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(0, 0, m_rtStartRect.width, m_rtStartRect.height), m_strFPS + " FPS", m_pStyle);
        if (m_bAllowDrag) GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
}