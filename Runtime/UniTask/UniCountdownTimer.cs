using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Strangeman.Task.Timer
{
    public class UniCountdownTimer
    {
        public Action<int> CountdownProgress;
        public Action CountdownEnded;

        bool _inprogress;
        float _duration = 1f;
        CancellationToken _token;

        public bool InProgress => _inprogress;

        public UniCountdownTimer With(CancellationToken token)
        {
            _token = token;
            return this;
        }

        public UniCountdownTimer With(float duration)
        {
            _duration = duration;
            return this;
        }

        public void StartCountdown()
        {
            if (_inprogress) return;
            _inprogress = true;
            Countdown().Forget();
        }

        private async UniTaskVoid Countdown()
        {
            for (int i = 0; i <= 100; i++)
            {
                if (_token != null) _token.ThrowIfCancellationRequested();
                await UniTask.Delay(MillisecondOnePercentDelay(), cancellationToken: _token);
                CountdownProgress?.Invoke(i);
            }
            _inprogress = false;
            CountdownEnded?.Invoke();
        }

        int MillisecondOnePercentDelay() => (int)_duration * 10;
    }
}
