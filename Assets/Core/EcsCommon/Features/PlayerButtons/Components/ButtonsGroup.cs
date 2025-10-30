namespace Core.Components
{
    public struct ButtonMoveTag : IButtonComponent
    {
    }
    
    public struct ButtonUseHealing : IButtonComponent
    {
    }
    
    public struct ButtonTeleport : IButtonComponent
    {
    }
    
    public struct ButtonForwardFTag : IButtonComponent
    {
    }
    
    public struct ButtonJumpForwardTag : IButtonComponent
    {
    }
    
    public struct ButtonJumpTag : IButtonComponent
    {
    }
    
    //PC
    public struct MouseLeftTag : IButtonComponent
    {
    }
    
    public struct MouseRightTag : IButtonComponent
    {
    }
    
    public struct Button1Tag : IButtonComponent
    {
    }
    
    public struct Button2Tag : IButtonComponent
    {
    }
    
    public struct Button3Tag : IButtonComponent
    {
    }
    
    public struct Button4Tag : IButtonComponent
    {
    }
    
    //---------------------------------------------------------------------

    public struct EventButtonPerformed<T> where T : struct, IButtonComponent
    {
    }

    public struct EventButtonCanceled<T> where T : struct, IButtonComponent
    {
    }

    public interface IButtonComponent
    {
    }
}