using System;

namespace Core
{
    public interface IUiEntityBuilder
    {
        int UiRawEntity { get; }
        IUiEntityBuilder ValueUpdated<T1>(Action<int, T1> refreshFunction) where T1 : struct;
        IUiEntityBuilder OnEvent<T1>(Action<int, T1> refreshFunction) where T1 : struct;
    }
}