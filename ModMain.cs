using UnityEngine;
using MelonLoader;

namespace PatchQuestTrainer
{
    public class ModMain : MelonMod
    {
        public static ModMain Instance;
        public static bool showWindow = true;
        public static bool gameLoaded = false;

        public static int windowWidth = 300;
        public static int windowHeight = 200;

        public Rect windowRect = new Rect(20, 20, windowWidth, windowHeight);

        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
            Instance = this;
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (gameLoaded && showWindow)
            {
                windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)DrawGuiWindow, "Patch Quest Trainer (Insert)");
            }
        }

        void DrawGuiWindow(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, windowWidth, 20));


            GUILayoutOption[] sliderOptions = { GUILayout.Width(100) };

            Settings.invulnerable = GUILayout.Toggle(Settings.invulnerable, "Invulnerable (i)", null);

            GUILayout.Label($"Blaster Damage Scaling [{Settings.blasterDamageScaling}]", null);
            Settings.blasterDamageScaling = GUILayout.HorizontalSlider(Settings.blasterDamageScaling, 1f, 100f, sliderOptions);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == 1)
            {
                gameLoaded = true;
                LoggerInstance.Msg("Mod Initialized");
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (!gameLoaded)
            {
                return;
            }

            if (showWindow)
            {
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Settings.invulnerable = !Settings.invulnerable;
            }
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                showWindow = !showWindow;
            }

            if (Settings.invulnerable)
            {
                PatchQuest.Player.P1.GrantImmunity(1f);
                PatchQuest.Player.P2.GrantImmunity(1f);
            }

        }
    }
}
