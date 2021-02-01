using System;
using System.Collections;
using System.Collections.Generic;
using DEV.Scripts;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DEV.Scripts.Managers;
using DEV.Scripts.WorldSwitching;
using DEV.Scripts.Input;

public class CalculateStamina : MonoBehaviour
{
    public StateEnum state;
    public Slider staminaPlayer;
    private GhostStruct ghostStruct;
    private WorldSwitchManager worldSwitchManager;
    private bool reachNull;
    private MovementPlayer movementPlayer;
    private Settings.GhostAndWorldSettingsModel ghostAndWorldSettings;
    private SettingsEditor settings;
    


    [HideInInspector]
    public float spiritTimer=1f;

    public bool updateTimer;

    public float staminaMax = 100;
    public float staminaCurrent = 100;

    public void Start()
    {
        settings = SettingsEditor.Instance;
        ghostAndWorldSettings = settings.ghostAndWorldSettingsModel;
        staminaPlayer = SceneContext.Instance.sliderStamina;
        ghostStruct = SceneContext.Instance.ghostStruct;
        worldSwitchManager = SceneContext.Instance.worldSwitchManager;
        staminaCurrent = staminaMax;
        staminaPlayer.maxValue = staminaMax;
        staminaPlayer.value = staminaMax;
        //spiritTimer = 0.0f;

    }
    public enum StateEnum
    {
        IDLE,
        RUN,
        ATTACK,
        JUMP
    }

    //public enum StateEnumModifier
    //{
    //    IDLE,
    //    RUN = 13,
    //    ATTACK = 2,
    //}


    private void FixedUpdate()
    {
       // if (state != StateEnum.RUN&& state != StateEnum.JUMP&& state != StateEnum.ATTACK)
      //  {
       //     state = CalculateStamina.StateEnum.IDLE;
       // }
        if (worldSwitchManager.worldIsNormal == true&&staminaCurrent<staminaMax)
         {
            IncreaseStaminaBar();

            //staminaPlayer.value += staminaCurrent * Time.deltaTime;
            //staminaMax = 100;

         }
        if (worldSwitchManager.worldIsNormal == false)// && reachNull == false)
        {
            CalculateEffect();
            CheckStamina();
            HowLongSecSpiritWorld();
                //Debug.Log(spiritTimer);
            
            //HowLongSecSpiritWorld();
            //Debug.Log(spiritTimer);
        }
        //   else
        //  {
        //         if (worldSwitchManager.worldIsNormal == true&&staminaMax==0)
        //      {
        //          float timeSlice =- staminaPlayer.value / 10;
        //          staminaPlayer.value = timeSlice;
        //      }
        // }


        if (staminaCurrent <= 0 && worldSwitchManager.worldIsNormal == false)
        {
            staminaCurrent = 0;
        }
    }



    private void CalculateEffect()
    {
        if (state == StateEnum.RUN)
        {
            if (staminaMax != 0)
            {
                //float decresingStamina = 10;
                staminaCurrent -= ghostAndWorldSettings.decreasingRunStamina * Time.deltaTime;
                staminaPlayer.value = staminaCurrent;
            }
        }

        if (state == StateEnum.ATTACK)
        {
            if (staminaMax != 0)
            {
                //float decresingStamina = 20;
                staminaCurrent -= ghostAndWorldSettings.decreasingAttackStamina * Time.deltaTime;
                staminaPlayer.value = staminaCurrent;
                //Debug.Log("ATTACK");
            }
        }

        if (state == StateEnum.IDLE)
        {
            //none
        }

        if (state == StateEnum.JUMP)
        {
            if (staminaMax != 0)
            {
                //float decresingStamina = 10;
                staminaCurrent -= ghostAndWorldSettings.decreasingJumpStamina * Time.deltaTime;
                staminaPlayer.value = staminaCurrent;
                Debug.Log("JUMP");
            }
        }
    }
    //public void CheckAction(StateEnum inComingState, bool switched)
    //{
     //   if (switched == true){
      //      state = inComingState;
        //    switched = false;
         //   Debug.Log("It is now  " + inComingState);
        //    if (switched == false)
        //    {
        //        Debug.Log("it is now IDLE");
        //        state = StateEnum.IDLE;
       //     }
            

       // }
      //  else
      //  {
      //      Debug.Log(switched);
      //  }
        

    //}

    private void CheckStamina()
    {
        // switch (staminaMax)
        // {
        //     case var expression when staminaCurrent == 0://&& worldSwitchManager.worldIsNormal == false:
        //       
        //         //reachNull = true;
        //         ghostStruct.DoStopGhost();
        //         worldSwitchManager.EnterNormalWorld();
        //         break;
        //
        //     //When distance is smaller than AttackRange 
        //     case var expression when staminaCurrent == 0 && worldSwitchManager.worldIsNormal == true:
        //         //call increasestaminabar
        //         //IncreaseStaminaBar();
        //         break;
        // }

        if (staminaCurrent <= 0)
        {
            ghostStruct.DoStopGhost();
            worldSwitchManager.EnterNormalWorld();
        }
    }


    public void IncreaseStaminaBar()
    {
        if (staminaCurrent < staminaMax)
        {
            //float valueforincrease = 5f;
            // staminaCurrent += valueforincrease * Time.deltaTime;
            // staminaPlayer.value = staminaCurrent;

            float valueforincrease = 1f;
            staminaCurrent += valueforincrease / spiritTimer;
             staminaPlayer.value = staminaCurrent;

        }

        //worldSwitchManager.EnterNormalWorld();
        //if (worldSwitchManager.worldIsNormal == true&&staminaMax==0)
        //  {
        //    staminaMax += 500f * Time.deltaTime;
        //   Debug.Log(worldSwitchManager.worldIsNormal + "worldisnormal");
        //}
        //}
    }

    public void CheatInfiniteSpiritWorld()
    {
        staminaCurrent = 1000000;
        staminaPlayer.value = staminaCurrent;
    }

    public void HowLongSecSpiritWorld()
    {
        //houdt bij hoe lang je in de spiritworld zit
        spiritTimer=spiritTimer+ Time.deltaTime;

    }
}
