namespace Core.Components
{
    //Нельзя хранить данные в ISlotTag т.к. их добавление и удаление контролируется фреймворком 
    public struct ThroughProjectileSlotTag : ISlotTag
    {
    }
    
    //Используем для этого отдельный компонент
    public struct ThroughProjectileComponent
    {
        public int hitsCount;
        public int maxHitsCount;
    }
}