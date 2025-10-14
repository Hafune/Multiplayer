using System.ComponentModel;

namespace Core.Components
{
    [Description(
        "Эвент для транспортной сущности с указание цели транспортировки, должен быть удалён вместе с транспортной сущьностью буквально в следующем же фильтре")]
    public struct EventCopy
    {
        public int entity;
    }
}