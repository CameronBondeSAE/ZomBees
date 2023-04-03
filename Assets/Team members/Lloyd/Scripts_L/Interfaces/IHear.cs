using UnityEngine;

public interface IHear
{
    public void SoundHeard(GameObject source, SoundEmitter.SoundType soundType, float volume, float fear, float beeness, Team team);
}