using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Unity;
using TerraTech;

namespace ChangeTeamAnywhere
{
    class QPatch
    {
        public static void Main()
        {
            var harmony = HarmonyInstance.Create("mindless.ttmm.changeteamanywhere.mod");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }

    static class ThigsAndStuf
    {
        [HarmonyPatch(typeof(ManSpawn), "OnMouse")]
        static class MoreThigsAndStuf { static void Postfix(ManPointer.Event mouseEvent, bool touchDown)
            {
                if (mouseEvent == ManPointer.Event.LMB && touchDown)
                {
                    if (Singleton.Manager<ManInput>.inst.GetButton(32) && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.RaD)
                    {
                        Tank targetTank2 = Singleton.Manager<ManPointer>.inst.targetTank;
                        if (targetTank2 != null && targetTank2 != Singleton.playerTank && !targetTank2.IsNeutral())
                        {
                            if (targetTank2.Team != 0)
                            {
                                targetTank2.SetTeam(0);
                                targetTank2.AI.SetBehaviorType(AITreeType.AITypes.Idle);
                            }
                            else
                            {
                                targetTank2.SetTeam(1);
                                targetTank2.AI.SetOldBehaviour();
                            }
                        }
                    }
                }
            }
        }
        
    }
}
