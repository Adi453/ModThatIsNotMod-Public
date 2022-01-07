﻿using MelonLoader;
using ModThatIsNotMod.Internals;
using ModThatIsNotMod.MonoBehaviours;
using Newtonsoft.Json;
using StressLevelZero.Combat;
using StressLevelZero.Data;
using StressLevelZero.Interaction;
using StressLevelZero.Props;
using StressLevelZero.SFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ModThatIsNotMod.RandomShit
{
    public static class AdManager
    {
        internal static List<string> ads { get; private set; } = new List<string>()
        {
            "BONETOME.COM FOR COOL NEW MODS",
            "GET THE CHUNGUS GUN IT DOES CHUNGUS STUFF \n\nhttps://bonetome.com/boneworks/code/87/",
            "MAKE THE GAME HARDER AND POWERED BY PAINKILLERS \n\nhttps://bonetome.com/boneworks/code/147/",
            "FEELING SNACKY? DOWNLOAD THE FOOD FRAMEWORK TODAY! \n\nhttps://bonetome.com/boneworks/code/185/",
            "i've never used this but apparently it's good? \n\nhttps://bonetome.com/boneworks/code/323/",
            "WHAT DOES IT DO? IT MODS. \n\nhttps://bonetome.com/boneworks/code/498/",
            "No memes here, this mod is actually so fun. \n\nhttps://bonetome.com/boneworks/code/661/",
            "DOWNLOAD NOW TO MAKE CHROMIUM VERY ANGRY!!!!1! \n\nhttps://bonetome.com/boneworks/simple/505/",
            "BEST MOD EVER v2 \n\nhttps://bonetome.com/boneworks/simple/719/",
            "GET YOUR GUNS AT BONETOME.COM TODAY",
            "THIS INTERRUPTION TO YOUR GAME IS SPONSORED BY TOMME150",
            "HAHAHA LASER GUN GO BRRRRR \n\nhttps://bonetome.com/boneworks/weapon/823/",
            "I don't get paid enough for this, and neither does TabloidA. \n\nSUPPORT THE BONETOME ON PATREON TODAY! \n\nhttps://www.patreon.com/thebonetome",
            "kinda pog ngl \n\nhttps://bonetome.com/boneworks/weapon/891/ https://bonetome.com/boneworks/weapon/894/",
            "BEST. PISTOL. EVER. \n\nhttps://bonetome.com/boneworks/weapon/798/",
            "THE THERMAL KATANA \nBRIGHTER THAN MY FUTURE. \n\nhttps://bonetome.com/boneworks/weapon/858/",
            "uwu uwu owo uwu uwu \n\nhttps://bonetome.com/boneworks/weapon/771/",
            "OMG GUYS IT'S A SHOTGUN!! LEAK!!1!!!!! \n\nhttps://bonetome.com/boneworks/weapon/413/",
            "THE BEST GUN YOU WILL EVER USE \n\nhttps://bonetome.com/boneworks/weapon/99/",
            "Just a friendly little fellow to keep you company while you play. \n\nhttps://bonetome.com/boneworks/item/114/",
            "guys this thing is really big lol \n\nhttps://bonetome.com/boneworks/weapon/361/",
            "Chap is Gay MINIGUN also tabloid no one likes you \n\nhttps://bonetome.com/boneworks/weapon/107/",
            "my motivation is gone and my life is spiraling into the abyss, but at least i have these honey badgers! download today! \n\nhttps://bonetome.com/boneworks/weapon/515/",
            "the constant stream of void energy being pumped into my brain is killing me, i can feel the names of my family growing fainter in my mind, but at least i have these honey badgers! download today! \n\nhttps://bonetome.com/boneworks/weapon/515/",
            "Ever wanted to have a thermal scope on a dope ass gun with laser beams? No? Well here it is anyways: \n\nhttps://bonetome.com/boneworks/weapon/918/",
            "Have bad aim? Download the SMART PISTOL! \n\nhttps://bonetome.com/boneworks/weapon/447/",
            "Half Life: Alyx pistol, for when you're too poor to afford the actual game. \n\nhttps://bonetome.com/boneworks/weapon/348/",
            "AMONG US PET POGGERS https://bonetome.com/boneworks/npc/464/",
            "Happy friendly NPCs for a good time :) \n\nhttps://bonetome.com/boneworks/npc/222/",
            "BONETOME IS GOOD \n\n\n\nBOTTOM TEXT",
            "there's a big purple thing and it looks kinda cool i guess \n\nhttps://bonetome.com/boneworks/map/520/",
            "void bad, you need to escape it. \n\nhttps://bonetome.com/boneworks/map/893/",
            "parkour but with melons? \nidk i never played it. \n\nhttps://bonetome.com/boneworks/map/899/",
        };

        private static GameObject baseAd;

        private static float chanceForDogAd = 1 / 4f;
        private static float chanceForSpecialDog = 1 / 10f;
        private static byte[] dogBytes;


        public static GameObject CreateNewAd() => CreateNewAd(ads[Random.Range(0, ads.Count)]);
        public static GameObject CreateNewAd(string adText)
        {
            GameObject newAd = GameObject.Instantiate(baseAd);
            TextMeshPro tmpro = newAd.GetComponentInChildren<TextMeshPro>();
            tmpro.text = adText;
            newAd.SetActive(true);
            tmpro.alignment = TextAlignmentOptions.Center; // For some reason this has to be set here as well ¯\_(ツ)_/¯

            newAd.AddComponent<Ad>();
            newAd.AddComponent<CustomItem>();

            return newAd;
        }

        public static void CreateDogAd() => GetDogBytes((byte[] imageBytes) => { dogBytes = imageBytes; });

        public static async void GetDogBytes(Action<byte[]> callback)
        {
            if (Random.value < chanceForSpecialDog)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string[] specialDogs = assembly.GetManifestResourceNames().Where(x => x.Contains("Dog") && (x.EndsWith(".png") || x.EndsWith(".jpg"))).ToArray();
                using (Stream stream = assembly.GetManifestResourceStream(specialDogs[Random.Range(0, specialDogs.Length)]))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        callback.Invoke(memoryStream.ToArray());
                    }
                }
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    string result = await client.DownloadStringTaskAsync("https://dog.ceo/api/breeds/image/random");
                    DogApiJson resultObj = JsonConvert.DeserializeObject<DogApiJson>(result);
                    string imageUrl = resultObj.message;
                    byte[] imageBytes = await client.DownloadDataTaskAsync(imageUrl);
                    callback.Invoke(imageBytes);
                }
            }
        }

        internal static void StartCoroutines()
        {
            MelonCoroutines.Start(CoSpawnAds());
            MelonCoroutines.Start(CoTrySpawnDogAds());
        }

        internal static IEnumerator CoTrySpawnDogAds()
        {
            // Wait until a dog pic has been downloaded
            while (dogBytes == null || dogBytes.Length == 0)
                yield return new WaitForSeconds(0.1f);

            // Load the byte array into a texture
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, dogBytes);

            // Instantiate the ad gameobject and disable the text
            GameObject newAd = GameObject.Instantiate(baseAd);
            newAd.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);

            // Assign the dog pic to the material
            MeshRenderer renderer = newAd.GetComponentInChildren<MeshRenderer>();
            Material mat = renderer.material;
            mat.mainTexture = texture;
            mat.color = Color.white;

            // Scale the mesh and grip so the pic isn't stretched
            Vector3 curScale = renderer.transform.localScale;
            Vector3 newScale = new Vector3((float)texture.width / texture.height * curScale.y, curScale.y, curScale.z);
            renderer.transform.localScale = newScale;
            renderer.transform.Rotate(renderer.transform.up, 180);

            foreach (BoxCollider col in newAd.GetComponentsInChildren<BoxCollider>())
                col.size = newScale;

            newAd.SetActive(true);

            // Place the pic in front of the player
            newAd.transform.position = Player.GetPlayerHead().transform.position + Player.GetPlayerHead().transform.forward * 2;
            newAd.transform.rotation = Quaternion.LookRotation(newAd.transform.position - Player.GetPlayerHead().transform.position);

            dogBytes = null;

            MelonCoroutines.Start(CoTrySpawnDogAds());
        }

        internal static IEnumerator CoSpawnAds()
        {
            if (!Preferences.autoSpawnAds.value)
                yield break;

            yield return new WaitForSeconds(Random.Range(Preferences.timeBetweenAds.value.x, Preferences.timeBetweenAds.value.y));

            while (baseAd == null)
                yield return new WaitForSeconds(5f);

            if (Random.value < chanceForDogAd)
            {
                CreateDogAd();
            }
            else
            {
                GameObject newAd = CreateNewAd();
                newAd.transform.position = Player.GetPlayerHead().transform.position + Player.GetPlayerHead().transform.forward * 2;
                newAd.transform.rotation = Quaternion.LookRotation(newAd.transform.position - Player.GetPlayerHead().transform.position);
            }

            MelonCoroutines.Start(CoSpawnAds());
        }

        /// <summary>
        /// So basically when I was making this I wasn't using any asset bundles yet and I wanted to keep it that way for whatever reason, so instead of just making an asset bundle with the prefab in it like a normal person, I somehow convinced myself that it was a perfectly sane idea to instead manually create the gameobject with a grip component and text and all that fun stuff through code, and that's how this absolute monstrosity of a function came into existence :)
        /// </summary>
        internal static void CreateBaseAd()
        {
            #region Resources
            AudioClip[] clips = Resources.FindObjectsOfTypeAll<AudioClip>();
            List<AudioClip> sounds = new List<AudioClip>();
            foreach (var clip in clips)
                if (clip.name.Contains("ImpactSoft_SwordBroad"))
                    sounds.Add(clip);

            HandPose sandwichGrip = null;
            HandPose edgeGrip = null;
            HandPose cornerGrip = null;
            HandPose faceGrip = null;
            HandPose[] poses = Resources.FindObjectsOfTypeAll<HandPose>();
            foreach (var p in poses)
            {
                if (p.name == "BoxSandwichGrip")
                    sandwichGrip = p;
                else if (p.name == "BoxEdgeGrip")
                    edgeGrip = p;
                else if (p.name == "BoxCornerGrip")
                    cornerGrip = p;
                else if (p.name == "BoxFaceGrip")
                    faceGrip = p;
            }
            #endregion

            #region Base object
            baseAd = new GameObject($"Ad Base");
            baseAd.SetActive(false);

            Rigidbody rb = baseAd.AddComponent<Rigidbody>();
            rb.mass = 8;
            rb.drag = 0.15f;
            rb.angularDrag = 0.15f;

            ImpactProperties impactProperties = baseAd.AddComponent<ImpactProperties>();
            impactProperties.material = ImpactPropertiesVariables.Material.PureMetal;
            impactProperties.modelType = ImpactPropertiesVariables.ModelType.Model;
            impactProperties.MainColor = Color.white;
            impactProperties.SecondaryColor = Color.white;
            impactProperties.PenetrationResistance = 0.9f;
            impactProperties.megaPascalModifier = 1;
            impactProperties.FireResistance = 100;

            ImpactSFX sfx = baseAd.AddComponent<ImpactSFX>();
            sfx.impactSoft = sounds.ToArray();
            sfx.impactHard = sounds.ToArray();
            sfx.outputMixer = Audio.sfxMixer;
            sfx.pitchMod = 1;
            sfx.bluntDamageMult = 1;
            sfx.minVelocity = 0.4f;
            sfx.velocityClipSplit = 4;
            sfx.jointBreakVolume = 1;

            InteractableHost host = baseAd.AddComponent<InteractableHost>();
            host.hasRigidbody = true;
            #endregion

            #region Mesh object
            GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mesh.name = "Mesh";
            mesh.transform.parent = baseAd.transform;
            mesh.transform.localPosition = Vector3.zero;
            mesh.transform.localScale = new Vector3(2f, 1f, 0.02f);
            mesh.GetComponent<MeshRenderer>().material.color = new Color(0.1509434f, 0.1509434f, 0.1509434f);
            #endregion

            #region Grip object
            GameObject grip = new GameObject("Grip");
            grip.transform.parent = baseAd.transform;
            grip.layer = LayerMask.NameToLayer("Interactable");

            BoxCollider col = grip.AddComponent<BoxCollider>();
            col.size = mesh.transform.localScale;
            mesh.GetComponent<BoxCollider>().enabled = false;

            Interactable interactable = grip.AddComponent<Interactable>();
            interactable.defaulGripScore = float.PositiveInfinity;

            BoxGrip boxGrip = grip.AddComponent<BoxGrip>();
            boxGrip.virtualControllerMode = Grip.VirtualControllerModes.GENERIC;
            boxGrip.type = Grip.Type.SECONDARY;
            boxGrip.isThrowable = true;
            boxGrip.handleAmplifyCurve = new AnimationCurve(new Keyframe[] { new Keyframe(-180, 0), new Keyframe(180, 0) });
            boxGrip.gripOptions = InteractionOptions.MultipleHands;
            boxGrip.priority = 1;
            boxGrip.bodyDominance = 1;
            boxGrip.minBreakForce = float.PositiveInfinity;
            boxGrip.maxBreakForce = float.PositiveInfinity;
            boxGrip.defaultGripDistance = float.PositiveInfinity;
            boxGrip.gripDistanceSqr = float.PositiveInfinity;
            boxGrip.rotationLimit = 180;
            boxGrip.rotationPriorityBuffer = 20;

            boxGrip.sandwitchSize = 0.12f;
            boxGrip.edgePadding = 0.1f;
            boxGrip.sandwichHandPose = sandwichGrip;
            boxGrip.canBeSandwichedGrabbed = true;
            boxGrip.sandwhichMinBreakForce = float.PositiveInfinity;
            boxGrip.sandwhichMaxBreakForce = float.PositiveInfinity;

            boxGrip.edgeHandPose = edgeGrip;
            boxGrip.edgeHandPoseRadius = 0.05f;
            boxGrip.canBeEdgeGrabbed = true;
            boxGrip.edgeMinBreakForce = 1000;
            boxGrip.edgeMaxBreakForce = 2000;

            boxGrip.cornerHandPose = cornerGrip;
            boxGrip.cornerHandPoseRadius = 0.05f;
            boxGrip.canBeCornerGrabbed = true;
            boxGrip.cornerMinBreakForce = 800;
            boxGrip.cornerMaxBreakForce = 1600;

            boxGrip.faceHandPose = faceGrip;
            boxGrip.faceHandPoseRadius = 1;
            boxGrip.canBeFaceGrabbed = true;
            boxGrip.faceMinBreakForce = 400;
            boxGrip.faceMaxBreakForce = 600;

            boxGrip.enabledCorners = (BoxGrip.Corners)(-1);
            boxGrip.enabledEdges = (BoxGrip.Edges)(-1);
            boxGrip.enabledFaces = (BoxGrip.Faces)(-1);

            boxGrip._boxCollider = col;
            #endregion

            #region Destructable object
            ObjectDestructable destructable = baseAd.AddComponent<ObjectDestructable>();
            destructable.damageFromImpact = true;
            destructable.blasterType = StressLevelZero.Pool.PoolSpawner.BlasterType.Sparks;
            destructable.blasterScale = 3;
            destructable.maxHealth = 30;
            destructable.reqHitCount = 1;
            destructable.perBloodied = 0.1f;
            destructable.explosiveForceOnDestruct = 1;
            destructable.damageFromAttack = true;
            destructable.attackMod = 2;
            destructable.modTypeDamage = 2;
            destructable.modImpact = 1;
            destructable.thrImpact = 3;
            destructable.feetDamageMult = 0.1f;
            destructable._impactSfx = sfx;
            #endregion

            #region Text object
            GameObject text = new GameObject("Text");
            text.transform.parent = baseAd.transform;

            TextMeshPro tmpro = text.AddComponent<TextMeshPro>();
            tmpro.enableAutoSizing = true;
            tmpro.fontSizeMin = 0.5f;
            tmpro.fontSizeMax = 4;
            tmpro.alignment = TextAlignmentOptions.Center;
            tmpro.enableWordWrapping = true;

            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2, 1);
            rectTransform.localPosition = new Vector3(0, 0, -0.015f);
            #endregion
        }
    }

    internal struct DogApiJson
    {
#pragma warning disable CS0649
        public string message;
        public string status;
#pragma warning restore CS0649
    }
}
