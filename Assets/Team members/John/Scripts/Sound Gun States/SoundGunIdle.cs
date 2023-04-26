using System;

namespace Johns
{
    public class SoundGunIdle : StateBase
    {
        private void OnEnable()
        {
            GetComponent<StateManager>().ChangeState(GetComponent<SoundGunIdle>());
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }
    }
}
