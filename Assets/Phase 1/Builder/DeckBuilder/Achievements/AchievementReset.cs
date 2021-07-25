using UnityEngine;

namespace Phase_1.Builder.DeckBuilder.Achievements
{
    /// <summary>
    /// Only intended to be used in the editor.
    /// Allows the developer to easily reset all unlockables to their default state 
    /// </summary>
    [ExecuteInEditMode]
    public class AchievementReset : MonoBehaviour
    {
        public Unlockable[] unlockedByDefault;
        public Unlockable[] lockedByDefault;

        public void Reset()
        {
            foreach (var unlockable in lockedByDefault)
            {
                unlockable.isUnlocked = false;
            }

            foreach (var unlockable in unlockedByDefault)
            {
                unlockable.isUnlocked = true;
            }
        }
    }
}