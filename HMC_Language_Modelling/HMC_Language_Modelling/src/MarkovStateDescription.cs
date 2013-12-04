using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMC_Language_Modelling.src
{
    class MarkovStateDescription
    {
        private object _state;
        //count how often an object has been added to the dictionary
        private Dictionary<object, int> _count;
        //count how many objects have been added to the dictionary in total
        private int _totalCount;
        private Random _rndGenerator;

        public MarkovStateDescription(object ownState)
        {
            //create Generator here to only seed one for this instance
            //frequent seeding in a short time causes the random values to repeat.
            _rndGenerator = new Random();
            this._state = ownState;
            this._count = new Dictionary<object, int>();
            this._totalCount = 0;
        }

        /// <summary>
        /// Increases the state propability for the given goal State.
        /// </summary>
        /// <param name="goalState">State of the goal.</param>
        internal void increaseStatePropability(object goalState)
        {
            if (_count.ContainsKey(goalState))
            {
                _count[goalState]++;
            }
            else
            {
                _count[goalState] = 1;
            }
            _totalCount++;
        }

        /// <summary>
        /// Gets the propabillity to reach the specified goalState
        /// </summary>
        /// <param name="goalState">State of the goal.</param>
        /// <returns>A propabillity p \in [0,1]</returns>
        internal double getPropabillity(object goalState)
        {
            double result = 0;
            if (_count.ContainsKey(goalState))
                result = ((double)_count[goalState]) / _totalCount;
            return result;
        }

        /// <summary>
        /// Gets the state by propabillity.
        /// </summary>
        /// <returns>A goal state from all possibel states by propabillity</returns>
        internal object getStateByPropabillity()
        {
            int rndInt = _rndGenerator.Next(_totalCount);
            object result = _state;
            foreach (KeyValuePair<object,int> p in _count)
            {
                rndInt -= p.Value;
                if (rndInt <= 0)
                {
                    result = p.Key;
                    break;
                }
            }
            return result;
        }
    }
}
