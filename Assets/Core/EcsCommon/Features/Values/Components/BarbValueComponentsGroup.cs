using System;

namespace Core.Components
{
    [Serializable]
    public struct BarbFrenzyStackValueComponent : IValue
    {
        public float value { get; set; }
    }
    
    [Serializable]
    public struct BarbDamageFrenzyValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageWeaponThrowValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageWhirlwindValueComponent : IValue
    {
        public float value { get; set; }
    }

    // [Serializable]
    // public struct BarbDamageEarthquakeValueComponent : IValue
    // {
    //     public float value { get; set; }
    // }

    // [Serializable]
    // public struct BarbDamageCallOfTheAncientsValueComponent : IValue
    // {
    //     public float value { get; set; }
    // }

    // [Serializable]
    // public struct BarbDamageAncientSpearValueComponent : IValue
    // {
    //     public float value { get; set; }
    // }

    [Serializable]
    public struct BarbDamageHammerOfTheAncientsValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageRevengeValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageCleaveValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageRendValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageOverPowerValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageSeismicSlamValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct BarbDamageBashValueComponent : IValue
    {
        public float value { get; set; }
    }

    // [Serializable]
    // public struct BarbDamageFuriousChargeValueComponent : IValue
    // {
    //     public float value { get; set; }
    // }
}