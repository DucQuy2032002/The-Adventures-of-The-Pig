using UnityEngine;

public class MobileButtonManager : MonoBehaviour
{
    public void OnMobileRightButtonPressed()
    {
        PlayerControllers.Instance.OnMobileRightButtonPressed();
    }

    public void OnMobileLeftButtonPressed()
    {
        PlayerControllers.Instance.OnMobileLeftButtonPressed();
    }

    public void OnMobileRLButtonUpPressed()
    {
        PlayerControllers.Instance.OnMobileRLButtonUpPressed();
    }

    public void OnMobileShootButtonPressed()
    {
        PlayerControllers.Instance.OnMobileShootButtonPressed();
    }

    public void OnMobileJumpButtonPressed()
    {
       PlayerControllers.Instance.OnMobileJumpButtonPressed();
    }

    public void OnMobileDashButtonPressed()
    {
        PlayerControllers.Instance.OnMobileDashButtonPressed();
    }

    public void OnMobileSpeedRunButtonDownPressed()
    {
        PlayerControllers.Instance.OnMobileSpeedRunButtonDownPressed();
    }

    public void OnMobileSpeedRunButtonUpPressed()
    {
        PlayerControllers.Instance.OnMobileSpeedRunButtonUpPressed();
    }

    public void OnMobileBombButtonPressed()
    {
        PlayerControllers.Instance.OnMobileBombButtonPressed();
    }

    public void OnMobileShieldButtonPressed()
    {
        PlayerControllers.Instance.OnMobileShieldButtonPressed();
    }

    public void OnMobileMeteoriteButtonPressed()
    {
        PlayerControllers.Instance.OnMobileMeteoriteButtonPressed();
    }
}