// GENERATED AUTOMATICALLY FROM 'Assets/DEV/Scripts/Player/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""b123167b-e749-4136-b747-23480d22ac0f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0e7d137d-93ef-4241-94f2-c04d2d1d7ef9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""View"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bba6bd09-e239-4826-9a55-4f832ad9850b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""06bac035-1cb4-4e1d-b8e3-ec88513c49e4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f2c6c08a-1f63-41d3-8d1f-5ff4bd5107cd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""45ee1acd-3d8f-46e0-a3a2-56ea67792c1d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f806fedd-be6f-4289-9920-5d793469bcc2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3354bbd6-24a8-4302-8423-d5b44e100844"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ff742c27-4287-492e-a27a-a20c83fe61a9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8b3eadfe-dbf6-4b66-93af-30221865cb63"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""280b797a-9e95-49fb-9c80-1f8055fbdda2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60ed3c92-1942-42e1-b339-da7f89340923"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68c78041-837f-4acc-a39f-0e71a2c6abeb"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Actions"",
            ""id"": ""79ece7c3-17a6-4c32-ad39-69329f8a785e"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""e459b1d0-0bd3-44ff-b2d0-558b4363ed2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f11ba701-3672-4f7b-9990-78b0a6f5de23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWorld"",
                    ""type"": ""Button"",
                    ""id"": ""ae446571-8d33-4b46-8a89-17da48d6cdf4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChargeBowPress"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3a37a237-5a5b-430a-99d1-c3b1d6876548"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChargeBowRelease"",
                    ""type"": ""PassThrough"",
                    ""id"": ""302a7a8a-8f01-4c50-9fed-cba9a07d26a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""5cae7c79-5cf4-4916-8b27-8853bd577ad0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockCursor"",
                    ""type"": ""Button"",
                    ""id"": ""ca9385a0-c1f3-4cfa-bbb4-230a9af2c34c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UnlockCursor"",
                    ""type"": ""Button"",
                    ""id"": ""c797cdb2-1103-4f24-a7ff-2cbfe846e34a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""238db787-dad7-4f98-bbd7-8b04d5fb2394"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AddCheckpoint"",
                    ""type"": ""Button"",
                    ""id"": ""ce45f168-8f30-47d9-85cf-aea7e8204ab0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GoBackOneCheckpoint"",
                    ""type"": ""Button"",
                    ""id"": ""a621f5fe-2949-49f4-808c-d72cd109161b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0a6ef882-afb8-423b-a21d-5c84ae7bbc5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReelGrapplePress"",
                    ""type"": ""Button"",
                    ""id"": ""cbd1f92a-c852-4b3b-a7ce-1a4b3bc237e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ReelGrappleRelease"",
                    ""type"": ""Button"",
                    ""id"": ""93c597f8-ad56-4bac-bcbc-6e343d60b3b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""SwapArrowType"",
                    ""type"": ""Button"",
                    ""id"": ""97b582e8-ae04-45b8-92fe-b0a986a6cc79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousArrow"",
                    ""type"": ""Button"",
                    ""id"": ""121c913f-4c1e-4610-839f-d239c73d1f7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextArrow"",
                    ""type"": ""Button"",
                    ""id"": ""8688f7fb-0175-4d8f-8697-e01ae1e03097"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchToAimCam"",
                    ""type"": ""Button"",
                    ""id"": ""7c754515-0042-4bfc-807e-e237e6cc8e72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2251f61-7c35-4461-aa7b-2468d80af14c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48a94146-0964-40bc-a545-41f1ceb62bf0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84cc96b2-66f9-4e50-a123-14bd3f2e1b5d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f379a9e-3bad-4b39-bdc1-d55e453a515b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d49a443-6007-4c95-afaf-df494eeb2baa"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWorld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8643d9c-280b-4edb-ad9f-18f4ef6770fb"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWorld"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""567b2ff4-9bd8-4e36-80b8-0beffa7bd9b9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargeBowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac915bf2-ae4f-4b65-920f-47983334039d"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargeBowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b80be5ac-a059-4823-b590-c9c49818c03c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargeBowRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2748a64-5565-419a-a42f-aff07db6402c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChargeBowRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c60d4b2c-b9f9-43ab-a107-ddd5ee0bfe68"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""127d829f-36ec-4e3f-8596-db65cae0b903"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cc34488-2297-4ff9-a5f4-8973dd6960f9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UnlockCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38449ce8-466b-46d5-8c54-2eb63ce1b603"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62fe6202-0d63-4f52-963e-0c3430575bf7"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16d8f954-3b68-4acb-86de-f0a501c8ef25"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""993d2220-998b-4602-8605-8a106dba3a4a"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoBackOneCheckpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc822669-f797-4fdc-a7a3-a51334277ec2"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GoBackOneCheckpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f845ad8b-259e-42f1-9ae3-48e169d13b6b"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddCheckpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f188d54e-85a7-4f7d-a68f-9e6e82f76d4a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddCheckpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a3902f1-97cb-4cf8-bee1-629aa57cc0b7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ac07037-5eba-4e7e-a6de-9355aba2d8e3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReelGrapplePress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""660e9cf6-e7ca-454d-b385-570ef31978d7"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReelGrappleRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b96702d-cc19-47e2-a87c-2bc181ffa5ce"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapArrowType"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e90ec2f1-7a07-4cf1-991d-66d0f015d6e9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb8d872-9c94-4561-8cc8-37411070dd75"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousArrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ad26799-98ec-43f5-a8f4-df99c51ff833"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToAimCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Cheats"",
            ""id"": ""52acfcb4-ba38-4409-847c-4923c320ed27"",
            ""actions"": [
                {
                    ""name"": ""CheatAddArrows"",
                    ""type"": ""Button"",
                    ""id"": ""c814e72a-df71-4cf1-b600-9720debeee0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleCheatPanel"",
                    ""type"": ""Button"",
                    ""id"": ""55f20885-729f-4398-b6f6-33f026a7550a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnReturn"",
                    ""type"": ""Button"",
                    ""id"": ""12cc2299-413c-42fd-90e7-02e35edd92e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""99b69c26-ed29-4f27-80d2-53fda7e3c7f5"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheatAddArrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f56adea-e817-4b2b-9605-53e4f75faca7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheatAddArrows"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d1c1562-822d-44a1-ad1f-8e2e4185afc7"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCheatPanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2eba8f28-9690-41df-a833-0a9cea9b0789"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnReturn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CinematicSkip"",
            ""id"": ""97cc094e-2124-480b-96d9-8c336789d858"",
            ""actions"": [
                {
                    ""name"": ""SkipCinematic"",
                    ""type"": ""Button"",
                    ""id"": ""110dc11a-1304-4d39-9ffd-f8a8e38a8b3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b3bc8c24-57c6-43c5-82e5-d13d73a9e45c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipCinematic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Movement = m_Movement.FindAction("Movement", throwIfNotFound: true);
        m_Movement_View = m_Movement.FindAction("View", throwIfNotFound: true);
        m_Movement_MousePosition = m_Movement.FindAction("MousePosition", throwIfNotFound: true);
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Jump = m_Actions.FindAction("Jump", throwIfNotFound: true);
        m_Actions_Interact = m_Actions.FindAction("Interact", throwIfNotFound: true);
        m_Actions_SwitchWorld = m_Actions.FindAction("SwitchWorld", throwIfNotFound: true);
        m_Actions_ChargeBowPress = m_Actions.FindAction("ChargeBowPress", throwIfNotFound: true);
        m_Actions_ChargeBowRelease = m_Actions.FindAction("ChargeBowRelease", throwIfNotFound: true);
        m_Actions_Attack = m_Actions.FindAction("Attack", throwIfNotFound: true);
        m_Actions_LockCursor = m_Actions.FindAction("LockCursor", throwIfNotFound: true);
        m_Actions_UnlockCursor = m_Actions.FindAction("UnlockCursor", throwIfNotFound: true);
        m_Actions_OpenInventory = m_Actions.FindAction("OpenInventory", throwIfNotFound: true);
        m_Actions_AddCheckpoint = m_Actions.FindAction("AddCheckpoint", throwIfNotFound: true);
        m_Actions_GoBackOneCheckpoint = m_Actions.FindAction("GoBackOneCheckpoint", throwIfNotFound: true);
        m_Actions_Pause = m_Actions.FindAction("Pause", throwIfNotFound: true);
        m_Actions_ReelGrapplePress = m_Actions.FindAction("ReelGrapplePress", throwIfNotFound: true);
        m_Actions_ReelGrappleRelease = m_Actions.FindAction("ReelGrappleRelease", throwIfNotFound: true);
        m_Actions_SwapArrowType = m_Actions.FindAction("SwapArrowType", throwIfNotFound: true);
        m_Actions_PreviousArrow = m_Actions.FindAction("PreviousArrow", throwIfNotFound: true);
        m_Actions_NextArrow = m_Actions.FindAction("NextArrow", throwIfNotFound: true);
        m_Actions_SwitchToAimCam = m_Actions.FindAction("SwitchToAimCam", throwIfNotFound: true);
        // Cheats
        m_Cheats = asset.FindActionMap("Cheats", throwIfNotFound: true);
        m_Cheats_CheatAddArrows = m_Cheats.FindAction("CheatAddArrows", throwIfNotFound: true);
        m_Cheats_ToggleCheatPanel = m_Cheats.FindAction("ToggleCheatPanel", throwIfNotFound: true);
        m_Cheats_OnReturn = m_Cheats.FindAction("OnReturn", throwIfNotFound: true);
        // CinematicSkip
        m_CinematicSkip = asset.FindActionMap("CinematicSkip", throwIfNotFound: true);
        m_CinematicSkip_SkipCinematic = m_CinematicSkip.FindAction("SkipCinematic", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Movement;
    private readonly InputAction m_Movement_View;
    private readonly InputAction m_Movement_MousePosition;
    public struct MovementActions
    {
        private @PlayerInputActions m_Wrapper;
        public MovementActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Movement_Movement;
        public InputAction @View => m_Wrapper.m_Movement_View;
        public InputAction @MousePosition => m_Wrapper.m_Movement_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMovement;
                @View.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnView;
                @View.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnView;
                @View.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnView;
                @MousePosition.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @View.started += instance.OnView;
                @View.performed += instance.OnView;
                @View.canceled += instance.OnView;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Jump;
    private readonly InputAction m_Actions_Interact;
    private readonly InputAction m_Actions_SwitchWorld;
    private readonly InputAction m_Actions_ChargeBowPress;
    private readonly InputAction m_Actions_ChargeBowRelease;
    private readonly InputAction m_Actions_Attack;
    private readonly InputAction m_Actions_LockCursor;
    private readonly InputAction m_Actions_UnlockCursor;
    private readonly InputAction m_Actions_OpenInventory;
    private readonly InputAction m_Actions_AddCheckpoint;
    private readonly InputAction m_Actions_GoBackOneCheckpoint;
    private readonly InputAction m_Actions_Pause;
    private readonly InputAction m_Actions_ReelGrapplePress;
    private readonly InputAction m_Actions_ReelGrappleRelease;
    private readonly InputAction m_Actions_SwapArrowType;
    private readonly InputAction m_Actions_PreviousArrow;
    private readonly InputAction m_Actions_NextArrow;
    private readonly InputAction m_Actions_SwitchToAimCam;
    public struct ActionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public ActionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Actions_Jump;
        public InputAction @Interact => m_Wrapper.m_Actions_Interact;
        public InputAction @SwitchWorld => m_Wrapper.m_Actions_SwitchWorld;
        public InputAction @ChargeBowPress => m_Wrapper.m_Actions_ChargeBowPress;
        public InputAction @ChargeBowRelease => m_Wrapper.m_Actions_ChargeBowRelease;
        public InputAction @Attack => m_Wrapper.m_Actions_Attack;
        public InputAction @LockCursor => m_Wrapper.m_Actions_LockCursor;
        public InputAction @UnlockCursor => m_Wrapper.m_Actions_UnlockCursor;
        public InputAction @OpenInventory => m_Wrapper.m_Actions_OpenInventory;
        public InputAction @AddCheckpoint => m_Wrapper.m_Actions_AddCheckpoint;
        public InputAction @GoBackOneCheckpoint => m_Wrapper.m_Actions_GoBackOneCheckpoint;
        public InputAction @Pause => m_Wrapper.m_Actions_Pause;
        public InputAction @ReelGrapplePress => m_Wrapper.m_Actions_ReelGrapplePress;
        public InputAction @ReelGrappleRelease => m_Wrapper.m_Actions_ReelGrappleRelease;
        public InputAction @SwapArrowType => m_Wrapper.m_Actions_SwapArrowType;
        public InputAction @PreviousArrow => m_Wrapper.m_Actions_PreviousArrow;
        public InputAction @NextArrow => m_Wrapper.m_Actions_NextArrow;
        public InputAction @SwitchToAimCam => m_Wrapper.m_Actions_SwitchToAimCam;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnInteract;
                @SwitchWorld.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchWorld;
                @SwitchWorld.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchWorld;
                @SwitchWorld.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchWorld;
                @ChargeBowPress.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowPress;
                @ChargeBowPress.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowPress;
                @ChargeBowPress.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowPress;
                @ChargeBowRelease.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowRelease;
                @ChargeBowRelease.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowRelease;
                @ChargeBowRelease.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnChargeBowRelease;
                @Attack.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAttack;
                @LockCursor.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnLockCursor;
                @LockCursor.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnLockCursor;
                @LockCursor.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnLockCursor;
                @UnlockCursor.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUnlockCursor;
                @UnlockCursor.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUnlockCursor;
                @UnlockCursor.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUnlockCursor;
                @OpenInventory.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnOpenInventory;
                @AddCheckpoint.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAddCheckpoint;
                @AddCheckpoint.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAddCheckpoint;
                @AddCheckpoint.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnAddCheckpoint;
                @GoBackOneCheckpoint.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnGoBackOneCheckpoint;
                @GoBackOneCheckpoint.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnGoBackOneCheckpoint;
                @GoBackOneCheckpoint.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnGoBackOneCheckpoint;
                @Pause.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @ReelGrapplePress.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrapplePress;
                @ReelGrapplePress.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrapplePress;
                @ReelGrapplePress.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrapplePress;
                @ReelGrappleRelease.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrappleRelease;
                @ReelGrappleRelease.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrappleRelease;
                @ReelGrappleRelease.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReelGrappleRelease;
                @SwapArrowType.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwapArrowType;
                @SwapArrowType.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwapArrowType;
                @SwapArrowType.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwapArrowType;
                @PreviousArrow.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPreviousArrow;
                @PreviousArrow.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPreviousArrow;
                @PreviousArrow.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPreviousArrow;
                @NextArrow.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnNextArrow;
                @NextArrow.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnNextArrow;
                @NextArrow.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnNextArrow;
                @SwitchToAimCam.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchToAimCam;
                @SwitchToAimCam.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchToAimCam;
                @SwitchToAimCam.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSwitchToAimCam;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @SwitchWorld.started += instance.OnSwitchWorld;
                @SwitchWorld.performed += instance.OnSwitchWorld;
                @SwitchWorld.canceled += instance.OnSwitchWorld;
                @ChargeBowPress.started += instance.OnChargeBowPress;
                @ChargeBowPress.performed += instance.OnChargeBowPress;
                @ChargeBowPress.canceled += instance.OnChargeBowPress;
                @ChargeBowRelease.started += instance.OnChargeBowRelease;
                @ChargeBowRelease.performed += instance.OnChargeBowRelease;
                @ChargeBowRelease.canceled += instance.OnChargeBowRelease;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @LockCursor.started += instance.OnLockCursor;
                @LockCursor.performed += instance.OnLockCursor;
                @LockCursor.canceled += instance.OnLockCursor;
                @UnlockCursor.started += instance.OnUnlockCursor;
                @UnlockCursor.performed += instance.OnUnlockCursor;
                @UnlockCursor.canceled += instance.OnUnlockCursor;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @AddCheckpoint.started += instance.OnAddCheckpoint;
                @AddCheckpoint.performed += instance.OnAddCheckpoint;
                @AddCheckpoint.canceled += instance.OnAddCheckpoint;
                @GoBackOneCheckpoint.started += instance.OnGoBackOneCheckpoint;
                @GoBackOneCheckpoint.performed += instance.OnGoBackOneCheckpoint;
                @GoBackOneCheckpoint.canceled += instance.OnGoBackOneCheckpoint;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @ReelGrapplePress.started += instance.OnReelGrapplePress;
                @ReelGrapplePress.performed += instance.OnReelGrapplePress;
                @ReelGrapplePress.canceled += instance.OnReelGrapplePress;
                @ReelGrappleRelease.started += instance.OnReelGrappleRelease;
                @ReelGrappleRelease.performed += instance.OnReelGrappleRelease;
                @ReelGrappleRelease.canceled += instance.OnReelGrappleRelease;
                @SwapArrowType.started += instance.OnSwapArrowType;
                @SwapArrowType.performed += instance.OnSwapArrowType;
                @SwapArrowType.canceled += instance.OnSwapArrowType;
                @PreviousArrow.started += instance.OnPreviousArrow;
                @PreviousArrow.performed += instance.OnPreviousArrow;
                @PreviousArrow.canceled += instance.OnPreviousArrow;
                @NextArrow.started += instance.OnNextArrow;
                @NextArrow.performed += instance.OnNextArrow;
                @NextArrow.canceled += instance.OnNextArrow;
                @SwitchToAimCam.started += instance.OnSwitchToAimCam;
                @SwitchToAimCam.performed += instance.OnSwitchToAimCam;
                @SwitchToAimCam.canceled += instance.OnSwitchToAimCam;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);

    // Cheats
    private readonly InputActionMap m_Cheats;
    private ICheatsActions m_CheatsActionsCallbackInterface;
    private readonly InputAction m_Cheats_CheatAddArrows;
    private readonly InputAction m_Cheats_ToggleCheatPanel;
    private readonly InputAction m_Cheats_OnReturn;
    public struct CheatsActions
    {
        private @PlayerInputActions m_Wrapper;
        public CheatsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @CheatAddArrows => m_Wrapper.m_Cheats_CheatAddArrows;
        public InputAction @ToggleCheatPanel => m_Wrapper.m_Cheats_ToggleCheatPanel;
        public InputAction @OnReturn => m_Wrapper.m_Cheats_OnReturn;
        public InputActionMap Get() { return m_Wrapper.m_Cheats; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatsActions set) { return set.Get(); }
        public void SetCallbacks(ICheatsActions instance)
        {
            if (m_Wrapper.m_CheatsActionsCallbackInterface != null)
            {
                @CheatAddArrows.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnCheatAddArrows;
                @CheatAddArrows.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnCheatAddArrows;
                @CheatAddArrows.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnCheatAddArrows;
                @ToggleCheatPanel.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleCheatPanel;
                @ToggleCheatPanel.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleCheatPanel;
                @ToggleCheatPanel.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnToggleCheatPanel;
                @OnReturn.started -= m_Wrapper.m_CheatsActionsCallbackInterface.OnOnReturn;
                @OnReturn.performed -= m_Wrapper.m_CheatsActionsCallbackInterface.OnOnReturn;
                @OnReturn.canceled -= m_Wrapper.m_CheatsActionsCallbackInterface.OnOnReturn;
            }
            m_Wrapper.m_CheatsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CheatAddArrows.started += instance.OnCheatAddArrows;
                @CheatAddArrows.performed += instance.OnCheatAddArrows;
                @CheatAddArrows.canceled += instance.OnCheatAddArrows;
                @ToggleCheatPanel.started += instance.OnToggleCheatPanel;
                @ToggleCheatPanel.performed += instance.OnToggleCheatPanel;
                @ToggleCheatPanel.canceled += instance.OnToggleCheatPanel;
                @OnReturn.started += instance.OnOnReturn;
                @OnReturn.performed += instance.OnOnReturn;
                @OnReturn.canceled += instance.OnOnReturn;
            }
        }
    }
    public CheatsActions @Cheats => new CheatsActions(this);

    // CinematicSkip
    private readonly InputActionMap m_CinematicSkip;
    private ICinematicSkipActions m_CinematicSkipActionsCallbackInterface;
    private readonly InputAction m_CinematicSkip_SkipCinematic;
    public struct CinematicSkipActions
    {
        private @PlayerInputActions m_Wrapper;
        public CinematicSkipActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @SkipCinematic => m_Wrapper.m_CinematicSkip_SkipCinematic;
        public InputActionMap Get() { return m_Wrapper.m_CinematicSkip; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CinematicSkipActions set) { return set.Get(); }
        public void SetCallbacks(ICinematicSkipActions instance)
        {
            if (m_Wrapper.m_CinematicSkipActionsCallbackInterface != null)
            {
                @SkipCinematic.started -= m_Wrapper.m_CinematicSkipActionsCallbackInterface.OnSkipCinematic;
                @SkipCinematic.performed -= m_Wrapper.m_CinematicSkipActionsCallbackInterface.OnSkipCinematic;
                @SkipCinematic.canceled -= m_Wrapper.m_CinematicSkipActionsCallbackInterface.OnSkipCinematic;
            }
            m_Wrapper.m_CinematicSkipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SkipCinematic.started += instance.OnSkipCinematic;
                @SkipCinematic.performed += instance.OnSkipCinematic;
                @SkipCinematic.canceled += instance.OnSkipCinematic;
            }
        }
    }
    public CinematicSkipActions @CinematicSkip => new CinematicSkipActions(this);
    public interface IMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnView(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
    public interface IActionsActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSwitchWorld(InputAction.CallbackContext context);
        void OnChargeBowPress(InputAction.CallbackContext context);
        void OnChargeBowRelease(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLockCursor(InputAction.CallbackContext context);
        void OnUnlockCursor(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnAddCheckpoint(InputAction.CallbackContext context);
        void OnGoBackOneCheckpoint(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnReelGrapplePress(InputAction.CallbackContext context);
        void OnReelGrappleRelease(InputAction.CallbackContext context);
        void OnSwapArrowType(InputAction.CallbackContext context);
        void OnPreviousArrow(InputAction.CallbackContext context);
        void OnNextArrow(InputAction.CallbackContext context);
        void OnSwitchToAimCam(InputAction.CallbackContext context);
    }
    public interface ICheatsActions
    {
        void OnCheatAddArrows(InputAction.CallbackContext context);
        void OnToggleCheatPanel(InputAction.CallbackContext context);
        void OnOnReturn(InputAction.CallbackContext context);
    }
    public interface ICinematicSkipActions
    {
        void OnSkipCinematic(InputAction.CallbackContext context);
    }
}
