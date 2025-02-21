using System;

public partial class InteractUIPanel : Jyx2_UIBase
{
    public override UILayer Layer => UILayer.NormalUI;

    Action m_callback1;
    Action m_callback2;

    protected override void OnCreate()
    {
        InitTrans();

        BindListener(MainBg_Button1, () => OnBtnClick(0));
        BindListener(MainBg_Button2, () => OnBtnClick(1));
    }

    protected override void OnShowPanel(params object[] allParams)
    {
        base.OnShowPanel(allParams);

        if (allParams == null) return;

        int buttonCount = allParams.Length / 2;
        MainBg_Button2.gameObject.SetActive(buttonCount == 2);

        MainText_Text1.text = allParams[0] as string;
        m_callback1 = allParams[1] as Action;

        if (MainBg_Button2.gameObject.activeInHierarchy)
        {
            MainText_Text2.text = allParams[2] as string;
            m_callback2 = allParams[3] as Action;
        }
    }

    void OnBtnClick(int buttonIndex)
    {
        Action temp = buttonIndex == 0 ? m_callback1 : m_callback2;
        Jyx2_UIManager.Instance.HideUI("InteractUIPanel");
        temp?.Invoke();
    }

    protected override void OnHidePanel()
    {
        base.OnHidePanel();
        m_callback1 = null;
        m_callback2 = null;
    }
}
