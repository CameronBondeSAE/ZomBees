using System;

namespace Johns
{
    public class SoundGunSwitchedOff : StateBase
    {
        private void OnEnable()
        {
            GetComponent<StateManager>().ChangeState(GetComponent<SoundGunSwitchedOff>());
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }
    }
}

