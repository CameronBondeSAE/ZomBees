using UnityEngine;

namespace Team_members.Lloyd.Queen.QueenFinal
{
    public class SharedMaterialChanger : MonoBehaviour
    {
        public Material sharedMaterial;
        private Material newMaterialInstance;

        private void Start()
        {
            newMaterialInstance = Instantiate(sharedMaterial);
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == sharedMaterial)
                    {
                        materials[i] = newMaterialInstance;
                    }
                }

                renderer.materials = materials;
            }
        }

        public void ChangeColorRed()
        {
            Color newColor = Color.red;
            newMaterialInstance.SetColor("_RedColor", newColor);
        }

        public void ChangeColorGreen()
        {
            Color newColor = new Color(0, .4f, .1f, 1);
            newMaterialInstance.SetColor("_RedColor", newColor);
        }

        public void ChangeColorOrange()
        {
            Color newColor = new Color(1, .6f, .2f, 1);
            newMaterialInstance.SetColor("_RedColor", newColor);
        }

        public void ChangeColorPurple()
        {
            Color newColor = new Color(.4f, 0, .8f, 1);
            newMaterialInstance.SetColor("_RedColor", newColor);
        }
    }
}