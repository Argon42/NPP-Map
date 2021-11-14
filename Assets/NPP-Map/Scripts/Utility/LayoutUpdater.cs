using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NPPMap.Utility
{
    public class LayoutUpdater : MonoBehaviour
    {
        public void UpdateLayouts()
        {
            var layouts = GetAllChildren(transform)
                .SelectMany(child => child.GetComponents<LayoutGroup>())
                .ToList();

            var fitters = GetAllChildren(transform)
                .SelectMany(child => child.GetComponents<ContentSizeFitter>())
                .ToList();

            foreach (LayoutGroup layoutGroup in layouts)
            {
                layoutGroup.CalculateLayoutInputHorizontal();
                layoutGroup.CalculateLayoutInputVertical();
            }

            foreach (ContentSizeFitter fitter in fitters)
            {
                fitter.SetLayoutHorizontal();
                fitter.SetLayoutVertical();
            }
        }

        private IEnumerable<Transform> GetAllChildren(Transform self)
        {
            foreach (Transform child in self)
            {
                yield return child;

                foreach (Transform subChild in GetAllChildren(child))
                    yield return subChild;
            }
        }
    }
}