using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
                finished = true;
                taskCompletionSource?.SetResult(true);
            });

            tween.OnKill(() =>
            {
                if (finished)
                {
                    return;
                }
                finished = true;
                taskCompletionSource?.SetResult(false);
            });

            return taskCompletionSource.Task;
        }

        public static UniTask<bool> AsAUniTask(this Tween tween)
        {
            var finished = false;
            var taskCompletionSource = new UniTaskCompletionSource<bool>();
            tween.OnComplete(() =>
            {
                if (finished)
                {
                    return;
                }
                finished = true;
                taskCompletionSource.TrySetResult(true);
            });
            tween.OnKill(() =>
            {
                if (finished)
                {
                    return;
                }
                finished = true;
                taskCompletionSource.TrySetResult(false);
            });

            return taskCompletionSource.Task;
        }
    }
}