#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditorInternal;
using UdonSharp;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_Master : MonoBehaviour
    {
        [Serializable]
        public struct ComponentSet
        {
            public string title;
            public T23_ModuleBase module;
        }

        public int groupID = -1;

        public List<string> broadcastTitles = new List<string>();
        public ComponentSet broadcastSet;

        public List<string> triggerTitles = new List<string>();
        public List<ComponentSet> triggerSet = new List<ComponentSet>();

        public List<string> actionTitles = new List<string>();
        public List<ComponentSet> actionSet = new List<ComponentSet>();

        public string interactText = "Use";
        public float proximity = 2;
        public Component[] components = new Component[0];
        public bool hasObjectSync = true;
        public bool reliable = false;
        public bool randomize = false;

        public int turn = 0;
        public int maxturn = 0;

        public bool shouldMoveComponents;

        public void SetupGroup()
        {
            if (groupID == -1)
            {
                T23_Master[] masters = GetComponents<T23_Master>();
                for (int i = 0; i < masters.Length; i++)
                {
                    if (masters[i] == this)
                    {
                        groupID = i;
                        break;
                    }
                }
            }

            RebuildeModules();
        }

        private void RebuildeModules()
        {
            var modules = GetComponents<T23_ModuleBase>();
            if (broadcastSet.module == null)
            {
                foreach (var module in modules)
                {
                    if (module.groupID != groupID) { continue; }
                    if (broadcastSet.title == module.title)
                    {
                        broadcastSet.module = module;
                    }
                }
            }
            for (int i = 0; i < triggerSet.Count; i++)
            {
                if (triggerSet[i].module == null)
                {
                    foreach (var module in modules)
                    {
                        if (triggerSet[i].title == module.title)
                        {
                            var newTriggerSet = new ComponentSet();
                            newTriggerSet.title = module.title;
                            newTriggerSet.module = module;
                            triggerSet[i] = newTriggerSet;
                        }
                    }
                }
            }
            for (int i = 0; i < actionSet.Count; i++)
            {
                if (actionSet[i].module == null)
                {
                    foreach (var module in modules)
                    {
                        if (actionSet[i].title == module.title)
                        {
                            var newActionSet = new ComponentSet();
                            newActionSet.title = module.title;
                            newActionSet.module = module;
                            actionSet[i] = newActionSet;
                        }
                    }
                }
            }
        }

        public void CheckComponents()
        {
            turn++;
            if (maxturn > 0)
            {
                if (turn > maxturn) { turn = 0; }
                if (turn != groupID) { return; }
            }

            bool modified = false;
            bool changed = false;

            Component[] newComponents = GetSurfaceComponents();
            if (newComponents.Length != components.Length)
            {
                modified = true;
                changed = true;
            }
            maxturn = 0;
            for (int i = 0; i < newComponents.Length; i++)
            {
                if (newComponents.GetType() == typeof(T23_Master)) { maxturn++; }
                if (i < components.Length && newComponents[i] != components[i])
                {
                    modified = true;
                    changed = true;
                    break;
                }
            }
            components = newComponents;

            var objSync = GetComponent<VRC.SDK3.Components.VRCObjectSync>();
            if (objSync)
            {
                if (!hasObjectSync)
                {
                    hasObjectSync = true;
                    reliable = false;
                    modified = true;
                }
            }
            else
            {
                if (hasObjectSync)
                {
                    hasObjectSync = false;
                    reliable = true;
                    modified = true;
                }
            }

            if (modified)
            {
                OrderComponents();
                shouldMoveComponents = changed;
            }
        }

        public void SetBroadcast(Type module)
        {
            if (broadcastSet.module && module.ToString().Contains(broadcastSet.module.title)) { return; }

            if (broadcastTitles.Count > 0)
            {
                broadcastTitles.Clear();
                ChangeBroadcast();
            }

            ComponentSet set = AddUdonComponent(module, broadcastTitles);
            broadcastSet = set;
            broadcastTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void JoinBroadcast(T23_ModuleBase baseComponent)
        {
            if (broadcastTitles.Count > 0)
            {
                broadcastTitles.Clear();
                ChangeBroadcast();
            }

            ComponentSet set = JoinUdonComponent(baseComponent, broadcastTitles);
            broadcastSet = set;
            broadcastTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void ChangeBroadcast()
        {
            if (!broadcastTitles.Contains(broadcastSet.title) || broadcastSet.module == null)
            {
                RemoveBroadcast();
            }
        }

        public void RemoveBroadcast()
        {
            if (broadcastSet.module)
            {
                DestroyImmediate(broadcastSet.module);
            }
            broadcastSet = new ComponentSet();
        }

        public void AddTrigger(Type module)
        {
            ComponentSet set = AddUdonComponent(module, triggerTitles);
            triggerSet.Add(set);
            triggerTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void JoinTrigger(T23_ModuleBase baseComponent)
        {
            ComponentSet set = JoinUdonComponent(baseComponent, triggerTitles);
            triggerSet.Add(set);
            triggerTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void ChangeTrigger()
        {
            List<ComponentSet> deleteSet = new List<ComponentSet>();
            foreach (ComponentSet set in triggerSet)
            {
                if (!triggerTitles.Contains(set.title) || set.module == null)
                {
                    deleteSet.Add(set);
                }
            }
            foreach (ComponentSet set in deleteSet)
            {
                if (set.module)
                {
                    DestroyImmediate(set.module);
                }
                triggerSet.Remove(set);
            }
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void AddAction(Type module)
        {
            ComponentSet set = AddUdonComponent(module, actionTitles);
            actionSet.Add(set);
            actionTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void JoinAction(T23_ModuleBase baseComponent)
        {
            ComponentSet set = JoinUdonComponent(baseComponent, actionTitles);
            actionSet.Add(set);
            actionTitles.Add(set.title);
            OrderComponents();
            shouldMoveComponents = true;
        }

        public void ChangeAction()
        {
            List<ComponentSet> deleteSet = new List<ComponentSet>();
            foreach (ComponentSet set in actionSet)
            {
                if (!actionTitles.Contains(set.title) || set.module == null)
                {
                    deleteSet.Add(set);
                }
            }
            foreach (ComponentSet set in deleteSet)
            {
                if (set.module)
                {
                    DestroyImmediate(set.module);
                }
                actionSet.Remove(set);
            }
            OrderComponents();
            shouldMoveComponents = true;
        }

        private ComponentSet AddUdonComponent(Type module, List<string> titleArray)
        {
            string title = ConfirmTitle(module.Name.Replace("T23_", ""), titleArray);

            Type t23Type = module;
            Component newComponent = gameObject.AddComponent(t23Type);

            FieldInfo groupField = t23Type.GetField("groupID");
            if (groupField != null)
            {
                groupField.SetValue(newComponent, groupID);
            }

            FieldInfo titleField = t23Type.GetField("title");
            if (titleField != null)
            {
                titleField.SetValue(newComponent, title);
            }

            ComponentSet res = new ComponentSet();
            res.title = title;
            res.module = newComponent as T23_ModuleBase;

            return res;
        }

        private ComponentSet JoinUdonComponent(T23_ModuleBase baseComponent, List<string> titleArray)
        {
            string title = ConfirmTitle(baseComponent.GetType().Name.Replace("T23_", ""), titleArray);

            baseComponent.title = title;

            ComponentSet res = new ComponentSet();
            res.title = title;
            res.module = baseComponent as T23_ModuleBase;

            return res;
        }

        private string ConfirmTitle(string className, List<string> titleArray)
        {
            string title = "";
            int cnt = 1;
            while (true)
            {
                title = className.Replace("T23_", "");
                if (cnt > 1) { title += " (" + cnt + ")"; }
                if (titleArray == null)
                {
                    break;
                }
                else
                {
                    if (!titleArray.Contains(title))
                    {
                        break;
                    }
                }
                cnt++;
            }
            return title;
        }

        private ComponentSet GetComponentSet(List<ComponentSet> list, string title)
        {
            foreach (ComponentSet set in list)
            {
                if (set.title == title)
                {
                    return set;
                }
            }
            return new ComponentSet();
        }

        public void OrderComponents()
        {
            List<Component> orderList = new List<Component>();
            if (broadcastSet.module)
            {
                orderList.Add(broadcastSet.module);
            }
            else
            {
                broadcastTitles.Remove(broadcastSet.title);
                broadcastSet = new ComponentSet();
            }

            List<ComponentSet> deleteSet = new List<ComponentSet>();
            foreach (string title in triggerTitles)
            {
                ComponentSet set = GetComponentSet(triggerSet, title);
                if (set.module)
                {
                    orderList.Add(set.module);
                }
                else
                {
                    deleteSet.Add(set);
                }
            }
            foreach (ComponentSet set in deleteSet)
            {
                triggerTitles.Remove(set.title);
                triggerSet.Remove(set);
            }

            deleteSet = new List<ComponentSet>();
            foreach (string title in actionTitles)
            {
                ComponentSet set = GetComponentSet(actionSet, title);
                if (set.module)
                {
                    orderList.Add(set.module);
                }
                else
                {
                    deleteSet.Add(set);
                }
            }
            foreach (ComponentSet set in deleteSet)
            {
                actionTitles.Remove(set.title);
                actionSet.Remove(set);
            }
        }

        public void UnifyUdonParameters()
        {
            var udons = GetComponents<UdonBehaviour>();
            foreach (var udon in udons)
            {
                udon.interactText = interactText;
                udon.proximity = proximity;
                udon.SyncMethod = reliable ? VRC.SDKBase.Networking.SyncType.Manual : VRC.SDKBase.Networking.SyncType.Continuous;
            }
        }

        public bool MoveComponents()
        {
            int correctidx = GetComponentIndex(components, this);
            bool success = true;
            if (broadcastSet.module)
            {
                if (!AlignComponent(broadcastSet.module, ref correctidx))
                {
                    success = false;
                }
            }
            foreach (string title in triggerTitles)
            {
                ComponentSet set = GetComponentSet(triggerSet, title);
                if (set.module)
                {
                    if (!AlignComponent(set.module, ref correctidx))
                    {
                        success = false;
                    }
                }
            }
            foreach (string title in actionTitles)
            {
                ComponentSet set = GetComponentSet(actionSet, title);
                if (set.module)
                {
                    if (!AlignComponent(set.module, ref correctidx))
                    {
                        success = false;
                    }
                }
            }
            return success;
        }

        private Component[] GetSurfaceComponents()
        {
            return GetComponents<Component>().Where(o => o.GetType() != typeof(UdonBehaviour)).ToArray();
            //List<string> usharpClass = new List<string>();
            //UdonBehaviour[] udonComponents = GetComponents<UdonBehaviour>();
            //foreach (var udon in udonComponents)
            //{
            //    UdonSharpProgramAsset program = udon.programSource as UdonSharpProgramAsset;
            //    if (program != null)
            //    {
            //        usharpClass.Add(program.GetClass().Name);
            //    }
            //}
            //return GetComponents<Component>().Where(o => !usharpClass.Contains(o.GetType().ToString())).ToArray();
        }

        private bool AlignComponent(Component target, ref int correctidx)
        {
            var goal = components[correctidx];
            correctidx++;

            //string log = $"target:{target.GetType().Name} goal:{goal.GetType().Name} correctidx:{correctidx}\n";
            //for (int i = 0; i < components.Length; i++)
            //{
            //    log += $"[{i}]{components[i].GetType().Name} ";
            //}
            //log += "\n";

            int index = GetComponentIndex(components, target);
            while (index != correctidx)
            {
                if (index < correctidx)
                {
                    ComponentUtility.MoveComponentDown(target);
                    index++;
                }
                else
                {
                    ComponentUtility.MoveComponentUp(target);
                    index--;
                }
                if (index == correctidx - 1)
                {
                    correctidx--;
                }
            }
            components = GetSurfaceComponents();
            index = GetComponentIndex(components, target);
            bool success = index == correctidx;

            //for (int i = 0; i < components.Length; i++)
            //{
            //    log += $"[{i}]{components[i].GetType().Name} ";
            //}
            //log += $"\nsuccess:{success}";
            //Debug.Log(log);

            return success;
        }

        private int GetComponentIndex(Component[] array, Component target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target) { return i; }
            }
            return -1;
        }

        public static void JoinMaster(T23_ModuleBase body, int gid, int category)
        {
            T23_Master master = GetMaster(body, gid, category, false);
            if (!master)
            {
                master = body.gameObject.AddComponent<T23_Master>();
                master.groupID = gid;
            }

            switch (category)
            {
                case 0:
                    master.JoinBroadcast(body);
                    break;
                case 1:
                    master.JoinTrigger(body);
                    break;
                case 2:
                    master.JoinAction(body);
                    break;
            }
        }

        public static T23_Master GetMaster(T23_ModuleBase body, int gid, int category, bool fixTitle, string title = "")
        {
            T23_Master[] masters = body.transform.GetComponents<T23_Master>();
            for (int i = 0; i < masters.Length; i++)
            {
                if (masters[i].groupID == gid)
                {
                    if (!fixTitle)
                    {
                        return masters[i];
                    }
                    else
                    {
                        switch (category)
                        {
                            case 0:
                                if (title == masters[i].broadcastSet.title)
                                {
                                    return masters[i];
                                }
                                break;
                            case 1:
                                if (masters[i].triggerTitles.Contains(title))
                                {
                                    return masters[i];
                                }
                                break;
                            case 2:
                                if (masters[i].actionTitles.Contains(title))
                                {
                                    return masters[i];
                                }
                                break;
                        }
                    }
                }
            }
            return null;
        }
    }
}
#endif
