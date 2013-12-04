using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMC_Language_Modelling.src
{
    class MarkovChain
    {
        Dictionary<object, MarkovStateDescription> _states;
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkovChain"/> class.
        /// </summary>
        public MarkovChain()
        {
            //initialize State list
            _states = new Dictionary<object, MarkovStateDescription>();
        }

        public double getPropabillity(object currentState, object goalState)
        {
            double result = 0;
            if (_states.ContainsKey(currentState))
                result = _states[currentState].getPropabillity(goalState);
            return result;
        }

        public void increaseMarkovStatePropability(object currentState, object goalState)
        {
            if (_states.ContainsKey(currentState))
            {
                _states[currentState].increaseStatePropability(goalState);
            }
            else
            {
                MarkovStateDescription msd = new MarkovStateDescription(currentState);
                msd.increaseStatePropability(goalState);
                _states[currentState] = msd;
            }
        }

        /// <summary>
        /// Performs one Step from the given start State
        /// </summary>
        /// <param name="currentState">State where the Markov Chain should start from.</param>
        /// <returns>An object of the goal State</returns>
        public object perfomStep(object currentState)
        {
            object result = currentState;
            if (_states.ContainsKey(currentState))
                result = _states[currentState].getStateByPropabillity();
            return result;
        }
    }
}
