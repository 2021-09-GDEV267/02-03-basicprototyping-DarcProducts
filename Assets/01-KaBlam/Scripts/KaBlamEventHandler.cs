using UnityEngine;
using UnityEngine.Events;

public class KaBlamEventHandler : MonoBehaviour
{
    public UnityEvent OnBombDropped;
    public UnityEvent OnMissedBomb;
    public UnityEvent OnSingleBombExploded;
    public UnityEvent OnAllBombsExploded;
    public UnityEvent OnFinishedWave;
    public UnityEvent OnGameFinished;

    void OnEnable()
    {
        Bomb.BombMissed += MissedBomb;
        BombPicker.OnFinishedExplodingBombs += AllBombsExploded;
        Robber.OnFinishedBombWave += FinishedWave;
        Bomb.BombExploded += SingleBombExploded;
        Robber.OnDroppedBomb += BombDropped;
        BombPicker.OnGameFinished += GameFinished;
    }

    void OnDisable()
    {
        Bomb.BombMissed -= MissedBomb;
        BombPicker.OnFinishedExplodingBombs -= AllBombsExploded;
        Robber.OnFinishedBombWave -= FinishedWave;
        Bomb.BombExploded -= SingleBombExploded;
        Robber.OnDroppedBomb -= BombDropped;
        BombPicker.OnGameFinished -= GameFinished;
    }

    void BombDropped() => OnBombDropped?.Invoke();

    void MissedBomb() => OnMissedBomb?.Invoke();

    void AllBombsExploded() => OnAllBombsExploded?.Invoke();

    void FinishedWave() => OnFinishedWave?.Invoke();

    void SingleBombExploded() => OnSingleBombExploded?.Invoke();

    void GameFinished() => OnGameFinished?.Invoke();
}