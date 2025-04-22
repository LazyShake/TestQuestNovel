using UnityEngine;
using Naninovel;

[CommandAlias("waitMiniGame")]
public class WaitMiniGameCommand : Command
{
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        // Ждём, пока мини-игра не завершится
        while (!MiniGameWaiter.MiniGameFinished)
        {
            await UniTask.Yield(); // отпускаем кадр, чтобы не замораживать движок
        }

        // Сбросим флаг, чтобы в будущем можно было снова вызывать
        MiniGameWaiter.MiniGameFinished = false;
    }
}
