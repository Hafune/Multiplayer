using Core.ExternalEntityLogics;

namespace Core.Components
{
    public interface IEntityActionComponent
    {
        public AbstractEntityAction logic { get; set; }
    }
}