using System.Collections.Generic;

namespace KronosTech.Gallery.Customization.Decoration
{
    public static class DecorationController
    {
        private static bool s_currentState = true;
        private static readonly List<DecorationModel> s_modelsList = new();

        public static void SetVisibility(bool state)
        {
            foreach (var model in s_modelsList)
            {
                model.gameObject.SetActive(state);
            }

            s_currentState = state;
        }

        public static void Add(DecorationModel model)
        {
            s_modelsList.Add(model);

            model.gameObject.SetActive(s_currentState);
        }
        public static void Remove(DecorationModel model)
        {
            s_modelsList.Remove(model);
        }
    }
}