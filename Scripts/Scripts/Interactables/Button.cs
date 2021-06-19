using Units;

namespace Interactables
{
    public class Button : Stationary
    {
        public bool IsActivated;
        public override void Interact(UnitBase unit)
        {
            IsActivated = !IsActivated;
        }
    }
}
