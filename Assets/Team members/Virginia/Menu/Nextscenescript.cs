using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Virginia
{
    public class Nextscenescript : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (InputSystem.GetDevice<Keyboard>().anyKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Main"); // goes to the next scene when any button is pushed
            }
        }
    }
}
