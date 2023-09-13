using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public const float TIME_STEP = 0.05f;
    public const string PLAYER_INCOME_KEY = "INCOMEUPGRADE";
    public const string PLAYER_SPEED_KEY = "SPEEDUPGRADE";
    public const string PLAYER_INVENTORY_KEY = "INVENTORYUPGRADE";
    public const string NPC_COUNT_KEY = "NPCCOUNTUPGRADE";
    public const string NPC_SPEED_KEY = "NPCSPEEDUPGRADE";

    public const string NPC_INVENTORY_KEY = "NPCINVENTORYUPGRADE";
    public const string START_TUTORIAL_COMPLETE = "STARTTUTORIAL";
    public const string FIRST_CHARGER_TUTORIAL_COMPLETE = "FIRSTTUTORIAL";
    public const string SECOND_BROKEN_TUTORIAL_COMPLETE = "SECONDTUTORIAL";
    public const string THIRD_TIRE_TUTORIAL_COMPLETE = "THIRDTUTORIAL";
    public const string FOURTH_CUSTOMER_TUTORIAL_COMPLETE = "FOURTHTUTORIAL";
    public const string FIFTH_VIP_TUTORIAL_COMPLETE = "FIFTHTUTORIAL";

    public const string STACKER_UPGRADE_KEY = "STACKERUPGRADE";
    public const float HAT_SCALE = 1.257031f;
    public const float HAT_WORN_SCALE = 0.9465261f;

    public const float LOW_GAS_CAR_REPAIR_DURATION = 3f;
    public const float LOW_GAS_CAR_ATTACH_HANDLE_DURATION = 0.5f;
    public const float HANDLE_MAX_DISTANCE_FROM_STATION = 10f;

    public const float TIRE_DROP_TWEEN_DURATION = 1f;
    public const float TOOLBOX_DROP_TWEEN_DURATION = .35f;
    public const float TOOLBOX_DROP_REPAIR_ANIMATION_DURATION = 2f;
    public static readonly Vector3 CAMERA_LOOK_OFFSET = new Vector3(0f, -45f, 0f);

    //TAGS
    public const string HELPER_NPC_TAG = "HelperNPC";
    public const string PLAYER_TAG = "Player";
    public const float PLAYER_BASE_SPEED = 5f;

    //GameState
    public const string FIRST_TIME_GAME_STARTED = "FirstTimeGameStarted";


}
