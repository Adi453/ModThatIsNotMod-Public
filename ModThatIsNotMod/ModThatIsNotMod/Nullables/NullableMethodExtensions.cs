﻿using StressLevelZero.AI;
using StressLevelZero.Pool;
using StressLevelZero.VFX;
using StressLevelZero.Zones;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Audio;

namespace ModThatIsNotMod.Nullables
{
    public static class NullableMethodExtensions
    {
        public static void Attenuate(this AudioPlayer inst, float? volume, float? pitch, float? minDistance)
        {
            inst.Attenuate(
                new BoxedNullable<float>(volume),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip clip, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            inst.Play(clip, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip[] clips, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            inst.Play(clipsArr, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            AudioPlayer.PlayAtPoint(clip, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip[] clips, Vector3 position, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            AudioPlayer.PlayAtPoint(clipsArr, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void SetTrigger(this AITrigger inst, float? radius, float? fov, TriggerManager.TriggerTypes? type)
        {
            inst.SetTrigger(
                new BoxedNullable<float>(radius),
                new BoxedNullable<float>(fov),
                new BoxedNullable<TriggerManager.TriggerTypes>(type));
        }

        public static GameObject Spawn(this Pool inst, Vector3 position, Quaternion rotation, Vector3? scale, bool? autoEnable)
        {
            return inst.Spawn(position, rotation,
                new BoxedNullable<Vector3>(scale),
                new BoxedNullable<bool>(autoEnable));
        }

        public static void Despawn(this Poolee inst, bool? playVFX, Color? despawnColor)
        {
            inst.Despawn(
                new BoxedNullable<bool>(playVFX),
                new BoxedNullable<Color>(despawnColor));
        }

        public static void OnSpawn(this Poolee poolee, Vector3? scale)
        {
            poolee.OnSpawn(
                new BoxedNullable<Vector3>(scale));
        }

        public static GameObject PoolManager_Spawn(string name, Vector3 position, Quaternion rotation, bool? autoEnable)
        {
            return PoolManager.Spawn(name, position, rotation, new BoxedNullable<bool>(autoEnable));
        }

        public static GameObject PoolManager_Spawn(string name, Vector3 position, Quaternion rotation, Vector3 scale, bool? autoEnable)
        {
            return PoolManager.Spawn(name, position, rotation, scale, new BoxedNullable<bool>(autoEnable));
        }

        public static void SetRigidbody(this SpawnFragment inst, int idx, Vector3? velocity, Vector3? angularVelocity, float? mass, Vector3? worldCenter, float? explosiveForce)
        {
            inst.SetRigidbody(idx,
                new BoxedNullable<Vector3>(velocity),
                new BoxedNullable<Vector3>(angularVelocity),
                new BoxedNullable<float>(mass),
                new BoxedNullable<Vector3>(worldCenter),
                new BoxedNullable<float>(explosiveForce));
        }

        public static void SpawnEffect(this DespawnMeshVFX inst, Color? color)
        {
            inst.SpawnEffect(new BoxedNullable<Color>(color));
        }

        public static void SpawnEffectDisabledObj(this DespawnMeshVFX inst, Color? color)
        {
            inst.SpawnEffectDisabledObj(new BoxedNullable<Color>(color));
        }

        public static void Despawn(this ZoneTracker inst, bool? playVFX)
        {
            inst.Despawn(new BoxedNullable<bool>(playVFX));
        }
    }
}
