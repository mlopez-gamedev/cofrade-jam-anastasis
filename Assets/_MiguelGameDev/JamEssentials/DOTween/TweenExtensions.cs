using System.Threading.Tasks;
//using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MiguelGameDev.Generic.Extensions
{
    public static class TweenExtensions
    {
        public static Task<bool> AsATask(this Tween tween)
        {
            var finished = false;
            var taskCompletionSource = new TaskCompletionSource<bool>();
            tween.OnComplete(() =>
            {
                if (finished)
                {
                    return;
                }
                taskCompletionSource?.SetResult(true);
                finished = true;
            });

            tween.OnKill(() =>
            {
                if (finished)
                {
                    return;
                }
                taskCompletionSource?.SetResult(false);
                finished = true;
            });

            return Task.Run(() => taskCompletionSource.Task);
        }

        //public static UniTask<bool> AsAUniTask(this Tween tween)
        //{
        //    var taskCompletionSource = new UniTaskCompletionSource<bool>();
        //    tween.OnComplete(() =>
        //    {
        //        taskCompletionSource.TrySetResult(true);
        //    });
        //    tween.OnKill(() =>
        //    {
        //        taskCompletionSource.TrySetResult(false);
        //    });

        //    return UniTask.RunOnThreadPool(() => taskCompletionSource.Task);
        //}
    }
}