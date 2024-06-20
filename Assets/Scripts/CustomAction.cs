//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/CustomAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CustomAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CustomAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomAction"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""34f76bf6-9084-40ff-b7b3-b6b2ee069bf0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""211e28cd-0739-4d8e-88fe-5756e00aeed2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttackOff"",
                    ""type"": ""Button"",
                    ""id"": ""7aed9441-5590-41b7-be5e-99ed6c149c39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot1"",
                    ""type"": ""Button"",
                    ""id"": ""6333f598-5739-454a-a759-34213d0aab56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot2"",
                    ""type"": ""Button"",
                    ""id"": ""26b04222-8124-4619-9470-79ac81a01d44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot3"",
                    ""type"": ""Button"",
                    ""id"": ""ba234ec9-800e-4600-8c56-e85847fd485b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot4"",
                    ""type"": ""Button"",
                    ""id"": ""c4347818-a050-437a-873b-beeafbd4030f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot5"",
                    ""type"": ""Button"",
                    ""id"": ""c1da379b-a0fd-46db-986d-a3b1e6f3e66f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot6"",
                    ""type"": ""Button"",
                    ""id"": ""ff55d804-c5ff-4bec-85d0-befbfb122d78"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Talk"",
                    ""type"": ""Button"",
                    ""id"": ""07806609-dafd-4756-a766-a181cb7d0fce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SysMenu"",
                    ""type"": ""Button"",
                    ""id"": ""7cf9c5ee-693a-4702-b310-304f079eebf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CharInfo"",
                    ""type"": ""Button"",
                    ""id"": ""60910145-e171-49c3-a2db-8a26867c198e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""3907eac9-67f3-493d-805f-61378692c203"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill"",
                    ""type"": ""Button"",
                    ""id"": ""f571367b-2cd9-4c7a-9c9d-0063a84fe9e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Achievement"",
                    ""type"": ""Button"",
                    ""id"": ""118a5382-c972-4f5a-9834-7a1c4a41656d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Quest"",
                    ""type"": ""Button"",
                    ""id"": ""d66f10f4-684e-4bd7-bc73-c4f87291ecd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PartyInfo"",
                    ""type"": ""Button"",
                    ""id"": ""cf2faf3b-4cd0-4b38-a426-e6a7085e366f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Friends"",
                    ""type"": ""Button"",
                    ""id"": ""9e546994-40fd-4e2b-89ff-e16fcb6019d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Macro"",
                    ""type"": ""Button"",
                    ""id"": ""0ecc63cd-341e-4401-b7ca-10f97e292843"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""44470763-1efd-4726-ab63-97c55c531009"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53472018-5acb-4a31-9409-d45b8877aec5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9263ac1-3df0-42d2-b5b0-17e6f7aa400e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2841297d-2744-48f3-961b-b88f5a424d72"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd563f4c-2549-4ebf-af92-e39449668dea"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b941018e-8bd1-4f42-a40d-ec48062d0cd5"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""750ade63-ab85-47f0-980a-046ff6c43921"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbc35803-ffff-4dcd-905c-3e1b55e97af2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6befa81-c75a-4f69-b5f3-62361687b45c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d288f8e4-0134-43af-a8dc-a0614a434751"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SysMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""526abb1b-0939-4855-80cf-549269caf8a8"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CharInfo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2678c295-9fc8-4872-9589-1a9dad14645b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3471534e-9ee5-4191-aea4-ea475be7f1d3"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8579c984-3751-47e8-99cb-8102c661c685"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Achievement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ba0223d-9232-430d-92ea-13115b922c12"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f71df9a5-2b58-40f3-9daf-8dc117f51e9f"",
                    ""path"": ""<Keyboard>/f6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PartyInfo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94920eaa-5e26-4d44-9ae9-6fe0d73ae80c"",
                    ""path"": ""<Keyboard>/f7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Friends"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f47ac528-c431-46c0-9226-cb5a40b2de3c"",
                    ""path"": ""<Keyboard>/f8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Macro"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4402d2bd-bf2f-47e3-a88c-1b6d06a175a5"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f05a305-a0b3-4edd-ac78-0629a04b3e1c"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Equipment"",
            ""id"": ""dbb924ff-5715-432e-8b85-9edd21b265d6"",
            ""actions"": [
                {
                    ""name"": ""Preview"",
                    ""type"": ""Button"",
                    ""id"": ""b03eddc1-6679-486d-a9e5-51bd62bfaf06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Window"",
                    ""type"": ""Button"",
                    ""id"": ""0bc5f07a-7d15-413d-b4b3-5f1136864fe4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e802a886-8cdf-4e24-9ce6-3767c1bfba94"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Preview"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8ad6946-b2f0-4c34-b519-f2f35f1162b9"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Window"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Setting"",
            ""id"": ""a7efb66e-a908-4f8c-bf0c-7fd7016002ae"",
            ""actions"": [
                {
                    ""name"": ""Window"",
                    ""type"": ""Button"",
                    ""id"": ""87eecc6f-ae0d-4de5-8d40-e1c8bbb9e14c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc4d0e73-618b-4c0d-a724-b7bb70608e30"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Window"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
        m_Main_AttackOff = m_Main.FindAction("AttackOff", throwIfNotFound: true);
        m_Main_Slot1 = m_Main.FindAction("Slot1", throwIfNotFound: true);
        m_Main_Slot2 = m_Main.FindAction("Slot2", throwIfNotFound: true);
        m_Main_Slot3 = m_Main.FindAction("Slot3", throwIfNotFound: true);
        m_Main_Slot4 = m_Main.FindAction("Slot4", throwIfNotFound: true);
        m_Main_Slot5 = m_Main.FindAction("Slot5", throwIfNotFound: true);
        m_Main_Slot6 = m_Main.FindAction("Slot6", throwIfNotFound: true);
        m_Main_Talk = m_Main.FindAction("Talk", throwIfNotFound: true);
        m_Main_SysMenu = m_Main.FindAction("SysMenu", throwIfNotFound: true);
        m_Main_CharInfo = m_Main.FindAction("CharInfo", throwIfNotFound: true);
        m_Main_Inventory = m_Main.FindAction("Inventory", throwIfNotFound: true);
        m_Main_Skill = m_Main.FindAction("Skill", throwIfNotFound: true);
        m_Main_Achievement = m_Main.FindAction("Achievement", throwIfNotFound: true);
        m_Main_Quest = m_Main.FindAction("Quest", throwIfNotFound: true);
        m_Main_PartyInfo = m_Main.FindAction("PartyInfo", throwIfNotFound: true);
        m_Main_Friends = m_Main.FindAction("Friends", throwIfNotFound: true);
        m_Main_Macro = m_Main.FindAction("Macro", throwIfNotFound: true);
        // Equipment
        m_Equipment = asset.FindActionMap("Equipment", throwIfNotFound: true);
        m_Equipment_Preview = m_Equipment.FindAction("Preview", throwIfNotFound: true);
        m_Equipment_Window = m_Equipment.FindAction("Window", throwIfNotFound: true);
        // Setting
        m_Setting = asset.FindActionMap("Setting", throwIfNotFound: true);
        m_Setting_Window = m_Setting.FindAction("Window", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private List<IMainActions> m_MainActionsCallbackInterfaces = new List<IMainActions>();
    private readonly InputAction m_Main_Move;
    private readonly InputAction m_Main_AttackOff;
    private readonly InputAction m_Main_Slot1;
    private readonly InputAction m_Main_Slot2;
    private readonly InputAction m_Main_Slot3;
    private readonly InputAction m_Main_Slot4;
    private readonly InputAction m_Main_Slot5;
    private readonly InputAction m_Main_Slot6;
    private readonly InputAction m_Main_Talk;
    private readonly InputAction m_Main_SysMenu;
    private readonly InputAction m_Main_CharInfo;
    private readonly InputAction m_Main_Inventory;
    private readonly InputAction m_Main_Skill;
    private readonly InputAction m_Main_Achievement;
    private readonly InputAction m_Main_Quest;
    private readonly InputAction m_Main_PartyInfo;
    private readonly InputAction m_Main_Friends;
    private readonly InputAction m_Main_Macro;
    public struct MainActions
    {
        private @CustomAction m_Wrapper;
        public MainActions(@CustomAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Main_Move;
        public InputAction @AttackOff => m_Wrapper.m_Main_AttackOff;
        public InputAction @Slot1 => m_Wrapper.m_Main_Slot1;
        public InputAction @Slot2 => m_Wrapper.m_Main_Slot2;
        public InputAction @Slot3 => m_Wrapper.m_Main_Slot3;
        public InputAction @Slot4 => m_Wrapper.m_Main_Slot4;
        public InputAction @Slot5 => m_Wrapper.m_Main_Slot5;
        public InputAction @Slot6 => m_Wrapper.m_Main_Slot6;
        public InputAction @Talk => m_Wrapper.m_Main_Talk;
        public InputAction @SysMenu => m_Wrapper.m_Main_SysMenu;
        public InputAction @CharInfo => m_Wrapper.m_Main_CharInfo;
        public InputAction @Inventory => m_Wrapper.m_Main_Inventory;
        public InputAction @Skill => m_Wrapper.m_Main_Skill;
        public InputAction @Achievement => m_Wrapper.m_Main_Achievement;
        public InputAction @Quest => m_Wrapper.m_Main_Quest;
        public InputAction @PartyInfo => m_Wrapper.m_Main_PartyInfo;
        public InputAction @Friends => m_Wrapper.m_Main_Friends;
        public InputAction @Macro => m_Wrapper.m_Main_Macro;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void AddCallbacks(IMainActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @AttackOff.started += instance.OnAttackOff;
            @AttackOff.performed += instance.OnAttackOff;
            @AttackOff.canceled += instance.OnAttackOff;
            @Slot1.started += instance.OnSlot1;
            @Slot1.performed += instance.OnSlot1;
            @Slot1.canceled += instance.OnSlot1;
            @Slot2.started += instance.OnSlot2;
            @Slot2.performed += instance.OnSlot2;
            @Slot2.canceled += instance.OnSlot2;
            @Slot3.started += instance.OnSlot3;
            @Slot3.performed += instance.OnSlot3;
            @Slot3.canceled += instance.OnSlot3;
            @Slot4.started += instance.OnSlot4;
            @Slot4.performed += instance.OnSlot4;
            @Slot4.canceled += instance.OnSlot4;
            @Slot5.started += instance.OnSlot5;
            @Slot5.performed += instance.OnSlot5;
            @Slot5.canceled += instance.OnSlot5;
            @Slot6.started += instance.OnSlot6;
            @Slot6.performed += instance.OnSlot6;
            @Slot6.canceled += instance.OnSlot6;
            @Talk.started += instance.OnTalk;
            @Talk.performed += instance.OnTalk;
            @Talk.canceled += instance.OnTalk;
            @SysMenu.started += instance.OnSysMenu;
            @SysMenu.performed += instance.OnSysMenu;
            @SysMenu.canceled += instance.OnSysMenu;
            @CharInfo.started += instance.OnCharInfo;
            @CharInfo.performed += instance.OnCharInfo;
            @CharInfo.canceled += instance.OnCharInfo;
            @Inventory.started += instance.OnInventory;
            @Inventory.performed += instance.OnInventory;
            @Inventory.canceled += instance.OnInventory;
            @Skill.started += instance.OnSkill;
            @Skill.performed += instance.OnSkill;
            @Skill.canceled += instance.OnSkill;
            @Achievement.started += instance.OnAchievement;
            @Achievement.performed += instance.OnAchievement;
            @Achievement.canceled += instance.OnAchievement;
            @Quest.started += instance.OnQuest;
            @Quest.performed += instance.OnQuest;
            @Quest.canceled += instance.OnQuest;
            @PartyInfo.started += instance.OnPartyInfo;
            @PartyInfo.performed += instance.OnPartyInfo;
            @PartyInfo.canceled += instance.OnPartyInfo;
            @Friends.started += instance.OnFriends;
            @Friends.performed += instance.OnFriends;
            @Friends.canceled += instance.OnFriends;
            @Macro.started += instance.OnMacro;
            @Macro.performed += instance.OnMacro;
            @Macro.canceled += instance.OnMacro;
        }

        private void UnregisterCallbacks(IMainActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @AttackOff.started -= instance.OnAttackOff;
            @AttackOff.performed -= instance.OnAttackOff;
            @AttackOff.canceled -= instance.OnAttackOff;
            @Slot1.started -= instance.OnSlot1;
            @Slot1.performed -= instance.OnSlot1;
            @Slot1.canceled -= instance.OnSlot1;
            @Slot2.started -= instance.OnSlot2;
            @Slot2.performed -= instance.OnSlot2;
            @Slot2.canceled -= instance.OnSlot2;
            @Slot3.started -= instance.OnSlot3;
            @Slot3.performed -= instance.OnSlot3;
            @Slot3.canceled -= instance.OnSlot3;
            @Slot4.started -= instance.OnSlot4;
            @Slot4.performed -= instance.OnSlot4;
            @Slot4.canceled -= instance.OnSlot4;
            @Slot5.started -= instance.OnSlot5;
            @Slot5.performed -= instance.OnSlot5;
            @Slot5.canceled -= instance.OnSlot5;
            @Slot6.started -= instance.OnSlot6;
            @Slot6.performed -= instance.OnSlot6;
            @Slot6.canceled -= instance.OnSlot6;
            @Talk.started -= instance.OnTalk;
            @Talk.performed -= instance.OnTalk;
            @Talk.canceled -= instance.OnTalk;
            @SysMenu.started -= instance.OnSysMenu;
            @SysMenu.performed -= instance.OnSysMenu;
            @SysMenu.canceled -= instance.OnSysMenu;
            @CharInfo.started -= instance.OnCharInfo;
            @CharInfo.performed -= instance.OnCharInfo;
            @CharInfo.canceled -= instance.OnCharInfo;
            @Inventory.started -= instance.OnInventory;
            @Inventory.performed -= instance.OnInventory;
            @Inventory.canceled -= instance.OnInventory;
            @Skill.started -= instance.OnSkill;
            @Skill.performed -= instance.OnSkill;
            @Skill.canceled -= instance.OnSkill;
            @Achievement.started -= instance.OnAchievement;
            @Achievement.performed -= instance.OnAchievement;
            @Achievement.canceled -= instance.OnAchievement;
            @Quest.started -= instance.OnQuest;
            @Quest.performed -= instance.OnQuest;
            @Quest.canceled -= instance.OnQuest;
            @PartyInfo.started -= instance.OnPartyInfo;
            @PartyInfo.performed -= instance.OnPartyInfo;
            @PartyInfo.canceled -= instance.OnPartyInfo;
            @Friends.started -= instance.OnFriends;
            @Friends.performed -= instance.OnFriends;
            @Friends.canceled -= instance.OnFriends;
            @Macro.started -= instance.OnMacro;
            @Macro.performed -= instance.OnMacro;
            @Macro.canceled -= instance.OnMacro;
        }

        public void RemoveCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainActions instance)
        {
            foreach (var item in m_Wrapper.m_MainActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainActions @Main => new MainActions(this);

    // Equipment
    private readonly InputActionMap m_Equipment;
    private List<IEquipmentActions> m_EquipmentActionsCallbackInterfaces = new List<IEquipmentActions>();
    private readonly InputAction m_Equipment_Preview;
    private readonly InputAction m_Equipment_Window;
    public struct EquipmentActions
    {
        private @CustomAction m_Wrapper;
        public EquipmentActions(@CustomAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Preview => m_Wrapper.m_Equipment_Preview;
        public InputAction @Window => m_Wrapper.m_Equipment_Window;
        public InputActionMap Get() { return m_Wrapper.m_Equipment; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EquipmentActions set) { return set.Get(); }
        public void AddCallbacks(IEquipmentActions instance)
        {
            if (instance == null || m_Wrapper.m_EquipmentActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_EquipmentActionsCallbackInterfaces.Add(instance);
            @Preview.started += instance.OnPreview;
            @Preview.performed += instance.OnPreview;
            @Preview.canceled += instance.OnPreview;
            @Window.started += instance.OnWindow;
            @Window.performed += instance.OnWindow;
            @Window.canceled += instance.OnWindow;
        }

        private void UnregisterCallbacks(IEquipmentActions instance)
        {
            @Preview.started -= instance.OnPreview;
            @Preview.performed -= instance.OnPreview;
            @Preview.canceled -= instance.OnPreview;
            @Window.started -= instance.OnWindow;
            @Window.performed -= instance.OnWindow;
            @Window.canceled -= instance.OnWindow;
        }

        public void RemoveCallbacks(IEquipmentActions instance)
        {
            if (m_Wrapper.m_EquipmentActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IEquipmentActions instance)
        {
            foreach (var item in m_Wrapper.m_EquipmentActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_EquipmentActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public EquipmentActions @Equipment => new EquipmentActions(this);

    // Setting
    private readonly InputActionMap m_Setting;
    private List<ISettingActions> m_SettingActionsCallbackInterfaces = new List<ISettingActions>();
    private readonly InputAction m_Setting_Window;
    public struct SettingActions
    {
        private @CustomAction m_Wrapper;
        public SettingActions(@CustomAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Window => m_Wrapper.m_Setting_Window;
        public InputActionMap Get() { return m_Wrapper.m_Setting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SettingActions set) { return set.Get(); }
        public void AddCallbacks(ISettingActions instance)
        {
            if (instance == null || m_Wrapper.m_SettingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SettingActionsCallbackInterfaces.Add(instance);
            @Window.started += instance.OnWindow;
            @Window.performed += instance.OnWindow;
            @Window.canceled += instance.OnWindow;
        }

        private void UnregisterCallbacks(ISettingActions instance)
        {
            @Window.started -= instance.OnWindow;
            @Window.performed -= instance.OnWindow;
            @Window.canceled -= instance.OnWindow;
        }

        public void RemoveCallbacks(ISettingActions instance)
        {
            if (m_Wrapper.m_SettingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISettingActions instance)
        {
            foreach (var item in m_Wrapper.m_SettingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SettingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SettingActions @Setting => new SettingActions(this);
    public interface IMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttackOff(InputAction.CallbackContext context);
        void OnSlot1(InputAction.CallbackContext context);
        void OnSlot2(InputAction.CallbackContext context);
        void OnSlot3(InputAction.CallbackContext context);
        void OnSlot4(InputAction.CallbackContext context);
        void OnSlot5(InputAction.CallbackContext context);
        void OnSlot6(InputAction.CallbackContext context);
        void OnTalk(InputAction.CallbackContext context);
        void OnSysMenu(InputAction.CallbackContext context);
        void OnCharInfo(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnSkill(InputAction.CallbackContext context);
        void OnAchievement(InputAction.CallbackContext context);
        void OnQuest(InputAction.CallbackContext context);
        void OnPartyInfo(InputAction.CallbackContext context);
        void OnFriends(InputAction.CallbackContext context);
        void OnMacro(InputAction.CallbackContext context);
    }
    public interface IEquipmentActions
    {
        void OnPreview(InputAction.CallbackContext context);
        void OnWindow(InputAction.CallbackContext context);
    }
    public interface ISettingActions
    {
        void OnWindow(InputAction.CallbackContext context);
    }
}
