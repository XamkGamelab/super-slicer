using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }

    int PointValue { get; set; }

    void Damage();
}
