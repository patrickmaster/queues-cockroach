using System.Linq;

namespace Queue.Algorithm.Cockroach
{
    public abstract class Cockroach<TState> : ICockroach<TState>
    {
        private const int CockroachesCount = 100;
        private const int DispersionPercentProbability = 5;
        private const int RuthlessPercentProbability = 5;
        private const int SingleCockroachDissipationPercentProbability = 5;
        private const int CannibalismPercentThreshold = 25;

        private StateValue[] _cockroaches;
        private StateValue _leader;
        private bool _initialized;

        public CockroachResult<TState> GetNext()
        {
            if (!_initialized)
                Initialize();

            FollowTheLeader();
            if (GetRandomPercentValue() <= DispersionPercentProbability)
                Disperse();
            if (GetRandomPercentValue() <= RuthlessPercentProbability)
                BeRuthless();
            SetLeader();

            return new CockroachResult<TState>(_leader.State, _leader.Value);
        }

        protected abstract void Follow(TState cockroach, TState leader);

        protected abstract double GetValue(TState state);

        protected abstract TState GetRandomState();

        private void Initialize()
        {
            _cockroaches = new StateValue[CockroachesCount];

            for (int i = 0; i < CockroachesCount; i++)
            {
                _cockroaches[i] = new StateValue();
                SetRandomStateValue(_cockroaches[i]);
            }

            SetLeader();

            _initialized = true;
        }

        private void SetLeader()
        {
            var candidate = _cockroaches.Aggregate((max, x) => max == null || max.Value < x.Value ? x : max);
            if (_leader != candidate)
                _leader = candidate;
        }

        private void FollowTheLeader()
        {
            foreach (var cockroach in _cockroaches)
                if (cockroach != _leader)
                    Follow(cockroach.State, _leader.State);
        }

        private void Disperse()
        {
            foreach (var cockroach in _cockroaches)
                if (GetRandomPercentValue() < SingleCockroachDissipationPercentProbability)
                    SetRandomStateValue(cockroach);
        }

        private void BeRuthless()
        {
            var minValue = _cockroaches.Min(x => x.Value);
            var maxValue = _leader.Value;
            var weakestThresholdValue = minValue + CannibalismPercentThreshold * (maxValue - minValue) / 100;

            foreach (var cockroach in _cockroaches.Where(c => c.Value < weakestThresholdValue))
                SetRandomStateValue(cockroach);
        }

        private void SetRandomStateValue(StateValue stateValue)
        {
            var cockroach = GetRandomState();
            stateValue.State = cockroach;
            stateValue.Value = GetValue(cockroach);
        }

        private int GetRandomPercentValue()
        {
            return Randomizer.GetRandomPercent();
        }

        private class StateValue
        {
            public TState State;
            public double Value;
        }
    }
}
