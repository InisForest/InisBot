#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InisBot
{
    internal class Pipeline<TState>
    {

        private readonly List<Func<TState, Func<Task>, Task>> _steps;

        public Pipeline() =>
            this._steps = new List<Func<TState, Func<Task>, Task>>();

        public void Add(Func<TState, Func<Task>, Task> step) =>
            this._steps.Add(step);

        public Task Run(TState obj) =>
            new PipelineRun<TState>(this._steps, obj).RunAsync();

        private class PipelineRun<TState>
        {

            private readonly Func<TState, Func<Task>, Task>[] _steps;
            private readonly TState _state;
            private int _currentStep = -1;

            public PipelineRun(IEnumerable<Func<TState, Func<Task>, Task>> steps, TState state)
            {
                this._steps = steps.ToArray();
                this._state = state;
            }

            public Task RunAsync()
            {
                this._currentStep = 0;
                return this._steps[0](this._state, NextAsync);
            }

            private Task NextAsync()
            {
                ++this._currentStep;
                if (this._currentStep >= this._steps.Length)
                    return Task.CompletedTask;
                return this._steps[this._currentStep](_state, NextAsync);
            }

        }

    }

}
