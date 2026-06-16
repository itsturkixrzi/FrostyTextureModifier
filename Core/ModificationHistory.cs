using System.Collections.Generic;

namespace FrostyTextureModifier
{
    /// <summary>
    /// Manages modification history for undo/redo functionality
    /// </summary>
    public class ModificationHistory
    {
        private List<string> historyStack = new List<string>();
        private int currentIndex = -1;

        public void AddSnapshot(string description)
        {
            // Remove any redo history
            if (currentIndex < historyStack.Count - 1)
            {
                historyStack.RemoveRange(currentIndex + 1, historyStack.Count - currentIndex - 1);
            }

            historyStack.Add(description);
            currentIndex++;
        }

        public void Undo()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
        }

        public void Redo()
        {
            if (currentIndex < historyStack.Count - 1)
            {
                currentIndex++;
            }
        }

        public void Clear()
        {
            historyStack.Clear();
            currentIndex = -1;
        }

        public string GetCurrentState()
        {
            if (currentIndex >= 0 && currentIndex < historyStack.Count)
            {
                return historyStack[currentIndex];
            }
            return "Original";
        }

        public bool CanUndo => currentIndex > 0;
        public bool CanRedo => currentIndex < historyStack.Count - 1;
    }
}
