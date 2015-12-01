using UnityEngine;
using System.Collections;

public enum MovementTypes
{
    WAIT,
    STRAIGHT,
    BEZIER
};

public enum FacingTypes
{
    LOOKAT,
    LOOKCHAIN,
    WAIT,
    FREELOOK
}

public enum EffectTypes
{
    SHAKE,
    SPLATTER,
    FADE,
    WAIT
}

public enum UpgradeTypes
{
    ACCELERATION,
    BRAKES,
    MAXSPEED,
    MAXBLACKOUT,
    BLACKOUTRESISTANCE,
    BLACKOUTRECOVERY
}

public enum AITypes
{
    BAD,
    GOOD,
    OKAY
}