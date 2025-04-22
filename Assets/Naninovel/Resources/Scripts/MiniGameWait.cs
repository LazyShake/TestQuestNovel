using UnityEngine;
using Naninovel;

[CommandAlias("waitMiniGame")]
public class WaitMiniGameCommand : Command
{
    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        
        while (!MiniGameWaiter.MiniGameFinished)
        {
            await UniTask.Yield(); 
        }

        
        MiniGameWaiter.MiniGameFinished = false;
    }
}
