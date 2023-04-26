using System;

namespace Johns
{
    public class SoundGunSwitchedOn : StateBase
    {
        private void OnEnable()
        {
            GetComponent<StateManager>().ChangeState(GetComponent<SoundGunSwitchedOn>());
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }
    }
}
