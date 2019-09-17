using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public class CUpdateManager : MonoBehaviour {

    public static CUpdateManager Get
    {
        get
        {
            if (m_pSingleton == null)
            {
                //PROJECT MANGER OBJECT
                GameObject _manager = GameObject.Find("CUpdateManager");

                if (_manager == null)
                {
                    GameObject _managerPrefab = Resources.Load("CUpdateManager", typeof(GameObject)) as GameObject;
                    _manager = Instantiate(_managerPrefab) as GameObject;
                }
            }
            return m_pSingleton;
        }
    }

    [SerializeField]
    private CFPSCounter m_CFPSCounter = null;

    [SerializeField]
    private int m_Count = 1000;


    private static CUpdateManager m_pSingleton = null;


    private HashSet<IUpdater> m_UpdateExList = new HashSet<IUpdater>();

    private List<GameObject> m_UpdateList = new List<GameObject>();



    // Update is called once per frame
    void Update()
    {
        IEnumerator<IUpdater> _Enumerator = m_UpdateExList.GetEnumerator();

        while (_Enumerator.MoveNext())
        {
            _Enumerator.Current.UpdateEx();

        }

        //foreach (var item in m_UpdateExList)
        //{
        //    item.UpdateEx();
        //}
        m_CFPSCounter.UpdateEx();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "UseUpdateEX"))
            UseUpdateEX();

        if (GUI.Button(new Rect(10, 70, 100, 30), "UseUpdate"))
            UseUpdate();

        if (GUI.Button(new Rect(120, 10, 220, 40), "1000"))
            SetCount(1000);

        if (GUI.Button(new Rect(120, 70, 220, 30), "5000"))
            SetCount(5000);

        if (GUI.Button(new Rect(120, 130, 220, 30), "10000"))
            SetCount(10000);
    }

    /// <summary>
    /// 업데이트 매니저에 추가
    /// </summary>
    /// <param name="_AddTarget"></param>
    public void AddUpdate(CUpdateEx _AddTarget)
    {
        

        m_UpdateExList.Add(_AddTarget);


    }

    /// <summary>
    ///  업데이트 매니저에서 제거
    /// </summary>
    /// <param name="_AddTarget"></param>
    public void RemoveUpdate(CUpdateEx _AddTarget)
    {
        m_UpdateExList.Remove(_AddTarget);
    }

    public void UseUpdate()
    {
        m_CFPSCounter.m_UseUpdateEx = false;

        for (int i = 0; i < m_UpdateList.Count; i++)
        {
            Destroy(m_UpdateList[i].gameObject);
        }
        m_UpdateList.Clear();
        foreach (var item in m_UpdateExList)
        {
            Destroy((item as MonoBehaviour).gameObject);
        }
        m_UpdateExList.Clear();

    

        GameObject _GameObject = Resources.Load("CUpdate") as GameObject;
        for (int i = 0; i < m_Count; i++)
        {
            _GameObject = Instantiate(_GameObject) as GameObject;
            _GameObject.name = "CUpdate";
            _GameObject.transform.position = Random.insideUnitSphere * 20;
            m_UpdateList.Add(_GameObject);
        }
    }

    public void SetCount(int _Count)
    {
        m_Count = _Count;
    }
    public void UseUpdateEX()
    {
        m_CFPSCounter.m_UseUpdateEx = true;

        foreach (var item in m_UpdateExList)
        {
            Destroy((item as MonoBehaviour).gameObject);
        }
        m_UpdateExList.Clear();

        for (int i = 0; i < m_UpdateList.Count; i++)
        {
            Destroy(m_UpdateList[i].gameObject);
        }
        m_UpdateList.Clear();
        GameObject _GameObject = Resources.Load("CUpdateEx") as GameObject;
        for (int i = 0; i < m_Count; i++)
        {
            _GameObject = Instantiate(_GameObject) as GameObject;
            _GameObject.name = "CUpdateEx";
            _GameObject.transform.position = Random.insideUnitSphere * 20;
            CUpdateEx _CEntity = _GameObject.GetComponent<CUpdateEx>();

            AddUpdate(_CEntity);
        }
    }
}
