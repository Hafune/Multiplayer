namespace Core.ExternalEntityLogics.ActionAttributes
{
    public interface IActionAttr
    {
        public void Setup<T>(int entity);
        
        public void Remove<T>(int entity);
    }
}