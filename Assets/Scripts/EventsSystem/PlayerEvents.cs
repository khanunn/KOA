using System;
public class PlayerEvents
{
    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level)
    {
        if (onPlayerLevelChange != null)
        {
            onPlayerLevelChange(level);
        }
    }
    /* public event Action<int> onExperienceChange;
    public void ExperienceChange(int experience)
    {
        if (onExperienceChange != null)
        {
            onExperienceChange(experience);
        }
    } */

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience)
    {
        if (onExperienceGained != null)
        {
            onExperienceGained(experience);
        }
    }

    public event Action<Interactable> onPlayerInteractableChange;
    public void PlayerInteractableChange(Interactable interactable)
    {
        if (onPlayerInteractableChange != null)
        {
            onPlayerInteractableChange(interactable);
        }
    }

    public event Action<int> onPlayerDamaged;
    public void PlayerDamaged(int damaged)
    {
        if (onPlayerDamaged != null)
        {
            onPlayerDamaged(damaged);
        }
    }
}
