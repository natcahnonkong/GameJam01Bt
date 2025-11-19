using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // เรียกตอนกดปุ่ม Start
    public void StartGame()
    {
        // เปลี่ยนไป Scene ที่ต้องการ เช่น "GameScene"
        SceneManager.LoadScene("Test");
    }

    // เรียกตอนกดปุ่ม Quit
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // จะเห็นแค่ใน Editor
        Application.Quit();     // ออกเกมตอน Build จริง
    }
}
